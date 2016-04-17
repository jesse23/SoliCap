using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;
using System.Threading;

namespace SoliCap
{
    public class MemProbe
    {
        #region Data members

        /// <summary>
        /// Process
        /// </summary>
        private Process op;

        /// <summary>
        /// The target name that probe need to catch.
        /// </summary>
        private string tName;

        /// <summary>
        /// The Process ID that will remember when memprobe is created.
        /// </summary>
        private int PID;

        //VQ Details Value
        private long iVQPeak = 0;  
        private long iCommit;
        private long iReversed;
        private long iWaste;
        private long iFree;
        private long iVQ;

        /// <summary>
        /// List that contains all id of exist probes.
        /// </summary>
        private static List<int> iProbeList;

        /// <summary>
        /// Unit Type of probes
        /// </summary>
        public static MemUnitType eUnitType;

        /// <summary>
        /// The tag that show whether the base is locked.
        /// </summary>
        public static bool isLockBase = false;

        /// <summary>
        /// System info that used to check VQ.
        /// </summary>
        private static APIUtls.SYSTEM_INFO system_info;

        /// <summary>
        /// Saved Base. 0:VQ, 1:WS, 2:PF, 3:Commit, 4:Reversed, 5:Waste, 6:Free 
        /// </summary>
        private long[] oBase = new long[]{0,0,0,0,0,0,0};

        /// <summary>
        /// The current status of Probe
        /// </summary>
        private ProbeStatus eProbeStatus;

        //Memory Infomation variable
        private APIUtls.MEMORY_BASIC_INFORMATION mbi;

        //CPU Usage Data
        DateTime oTimeBase;
        double timeOldIdle;
        double timeOldSys;
        double timeOldPre;

        #endregion

        #region Properties

        public static List<int> ProbeIDs
        {
            get
            {
                return iProbeList;
            }
        }

        public static APIUtls.SYSTEM_INFO sys_info
        {
            get
            {
                return system_info;
            }
        }

        public string TargetName
        {
            get
            {
                return tName;
            }
        }

        public ProbeStatus ProbeStatus
        {
            get
            {
                return eProbeStatus;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get System Info For Memory Test.
        /// </summary>
        public static void Initialize()
        {
            APIUtls.GetSystemInfo(ref system_info);
            eUnitType = MemUnitType.MB;
            iProbeList = new List<int>();
        }

        /// <summary>
        /// Finalize static variable in Memprobe.
        /// </summary>
        public static void Finish()
        {
            iProbeList.Clear();
            iProbeList = null;

        }

        /// <summary>
        /// Create MemProbe by ID, if not, return null.
        /// </summary>
        /// <param name="id">The Process ID you want to get</param>
        public MemProbe(int pid)
        {
            try
            {
                op = Process.GetProcessById(pid);
                tName = op.ProcessName;
                PID = pid;
                iProbeList.Add(pid);
                eProbeStatus = ProbeStatus.Catching;
                oTimeBase = DateTime.Now;
            }
            catch
            {
                MessageBox.Show("No process with this ID exist.", "SoliCap");
                Dispose();
            }
        }

        /// <summary>
        /// Create MemProbe by Name, return a Newborn probe.
        /// </summary>
        /// <param name="name">the target process name.</param>
        public MemProbe(string name)
        {
            tName = name;
            eProbeStatus = ProbeStatus.Newborn;
        }

        /// <summary>
        /// Create MemProbe by Detailed process.
        /// </summary>
        /// <param name="p"></param>
        public MemProbe(Process p)
        {
            try
            {
                op = p;
                tName = op.ProcessName;
                PID = op.Id;
                iProbeList.Add(PID);
                eProbeStatus = ProbeStatus.Catching;
                oTimeBase = DateTime.Now;
            }
            catch
            {
                MessageBox.Show("No process with this ID exist.", "SoliCap");
                Dispose();
            }
        }

        /// <summary>
        /// Try to catch process for new born probe
        /// </summary>
        /// <param name="oRow">Related Data row</param>
        /// <param name="msg">column setting in the row</param>
        public void CatchNew(ref DataRow oRow, ColumnSettingMsg msg)
        {
            try
            {
                foreach (Process eop in Process.GetProcessesByName(tName))
                {
                    if (!iProbeList.Contains(eop.Id))
                    {
                        //Catched. Change Newborn to Catch.
                        op = eop;
                        tName = op.ProcessName;
                        PID = op.Id;
                        iProbeList.Add(PID);
                        eProbeStatus = ProbeStatus.Catching;
                        oTimeBase = DateTime.Now;
                        oRow[ResSoli.ColumnProcessName] = null;
                        break;
                    }
                }
                if (op == null)
                {
                    throw new Exception();
                }
            }
            catch
            {
                //Catch fail. If row is empty, add name.
                if (oRow.IsNull(ResSoli.ColumnProcessName))
                {
                    oRow[ResSoli.ColumnProcessName] = tName + " (Null)";
                }
                return;
            }
            Update(ref oRow, msg);
        }

        /// <summary>
        /// Get the newest status from probe.
        /// </summary>
        /// <returns>If update succes, return true; return false if updat fail</returns>
        public void Update(ref DataRow oRow,ColumnSettingMsg msg)
        {
            double timeNewIdle = 0;
            double timeNewSys = 0;
            double timeNewPre = 0;

            #region Refresh Process and set CPU Usage. If Exit, Set it as Dead and Return
            //Refresh process
            op.Refresh();

            //Get CPU Usage Base
            #region Set CPU Usage
            //Set the value which is not judged by VQ catch and base lock.
            if (msg.bIsCPUUsage == true)
            {
                op.Refresh();
                APIUtls.GetCPUTime(ref timeNewSys, ref timeNewIdle);
                DateTime oTimeCurrent = DateTime.Now;
                double total = timeNewSys - timeOldSys;
                if (PID == 0)
                {
                    double idle = timeNewIdle - timeOldIdle;
                    if (total != 0)
                        oRow[ResSoli.ColumnCPU_Usage] = Decimal.Round((Decimal)(idle * 100 / total), 1);
                    else
                        oRow[ResSoli.ColumnCPU_Usage] = Decimal.Round((Decimal)idle / system_info.dwNumberOfProcessors, 1);
                    timeOldIdle = timeNewIdle;
                }
                else
                {

                    try
                    {
                        timeNewPre = op.TotalProcessorTime.TotalMilliseconds;
                        if (total != 0)
                            oRow[ResSoli.ColumnCPU_Usage] = Decimal.Round((Decimal)((timeNewPre - timeOldPre) * 100.0 /  total), 1);
                        else
                            oRow[ResSoli.ColumnCPU_Usage] = Decimal.Round((Decimal)((timeNewPre - timeOldPre) * 100.0 / (oTimeCurrent - oTimeBase).TotalMilliseconds / system_info.dwNumberOfProcessors), 1);
                    }
                    catch
                    {
                        oRow[ResSoli.ColumnCPU_Usage] = "N/A";    
                    }
                    
                }    
                timeOldSys = timeNewSys;
                timeOldPre = timeNewPre;
            }
            #endregion

            try
            {
                //Process exit. Set probe to dead.
                if (op.HasExited == true)
                {
                    SetDeadStatus(ref oRow);
                    return;
                }
                if (msg.bIsCPUUsage == true)
                {
                    timeOldPre = op.TotalProcessorTime.TotalMilliseconds;
                }
            }
            catch
            {
            }
            #endregion
           
            #region Get Virtual Query Details by API
            //Get VQ Details by API Method.
            iVQ = 0;
            iCommit = 0;
            iReversed = 0;
            iWaste = 0;
            iFree = 0;

            try
            {
                //The Memory Address Pointer
                UIntPtr pAddress = (UIntPtr)system_info.lpMinimumApplicationAddress;

                while (true)
                {
                    //If out of address arrangement, exit while;
                    if (APIUtls.VirtualQueryEx(op.Handle, pAddress, ref mbi,
                    (UIntPtr)System.Runtime.InteropServices.Marshal.SizeOf(mbi))
                    != System.Runtime.InteropServices.Marshal.SizeOf(mbi))
                    {
                        break;
                    }

                    switch (mbi.State)
                    {
                        case (uint)APIUtls.VirtualAllocExTypes.MEM_COMMIT:
                            {
                                iCommit += (long)mbi.RegionSize;
                                break;
                            }
                        case (uint)APIUtls.VirtualAllocExTypes.MEM_RESERVE:
                            {
                                iReversed += (long)mbi.RegionSize;
                                break;
                            }
                        case (uint)APIUtls.VirtualAllocExTypes.MEM_FREE:
                            {
                                iFree += (long)mbi.RegionSize;
                                if ((ulong)mbi.RegionSize < system_info.dwAllocationGranularity)
                                {
                                    iWaste += (long)mbi.RegionSize;
                                }
                                else
                                {
                                    iWaste += ((long)mbi.RegionSize % system_info.dwAllocationGranularity);
                                }
                                break;
                            }
                    }

                    //Move the pointer to next memory region.
                    pAddress = (UIntPtr)(mbi.BaseAddress.ToUInt64() + mbi.RegionSize.ToUInt64());

                }

                //Reduce the waste part from free
                iFree -= iWaste;

                //Set VQ Peak;
                iVQ = iCommit + iReversed + iWaste;
                if (iVQ > iVQPeak)
                    iVQPeak = iVQ;
            }
            catch
            {
                iVQ = 0;
            }
            #endregion

            #region Set Row Data

            #region If New Row, Set Name,ID and Start Time
            //If new row, set name and start time.
            if (oRow.IsNull(ResSoli.ColumnProcessName))
            {
                oRow[ResSoli.ColumnProcessName] = tName + " (ID:" + PID.ToString() + ")";
                try
                {
                    oRow[ResSoli.ColumnFile] = op.MainModule.FileName;
                }
                catch
                {
                    oRow[ResSoli.ColumnFile] = "N/A";
                }
                try
                {
                    oRow[ResSoli.ColumnStartTime] = op.StartTime.ToLongTimeString();
                }
                catch
                {
                    oRow[ResSoli.ColumnStartTime] = "N/A";
                }
                finally
                {
                    oRow[ResSoli.ColumnExitTime] = "--";
                }
            }
            #endregion

            #region Set Peak Value
            if (msg.bIsWSPeak == true)
                oRow[ResSoli.ColumnPeakWS] = ConvertToStrData(op.PeakWorkingSet64);

            if (msg.bIsPFPeak == true)
                oRow[ResSoli.ColumnPeakPF] = ConvertToStrData(op.PeakPagedMemorySize64);

            if (msg.bIsVQPeak == true)
            {
                if (iVQPeak == 0)
                    oRow[ResSoli.ColumnPeakVQ] = "N/A";
                else
                    oRow[ResSoli.ColumnPeakVQ] = ConvertToStrData((long)iVQPeak);
            }
            #endregion

            #region Set Dynamic Value based on Lock Base Setting
            //when base locked.
            if (isLockBase == true)
            {
                if (msg.bIsWSCurrent == true)
                    oRow[ResSoli.ColumnWS] = ConvertToStrData(oBase[1]) + " + " + ConvertToStrData(op.WorkingSet64 - oBase[1]);


                if (msg.bIsPFCurrent == true)
                    oRow[ResSoli.ColumnPF] = ConvertToStrData(oBase[2]) + " + " + ConvertToStrData(op.PagedMemorySize64 - oBase[2]);

                if (iVQ == 0)
                {
                    if (msg.bIsVQCurrent == true)
                        oRow[ResSoli.ColumnVQ] = "N/A";
                    if (msg.bIsVQCommitted == true)
                        oRow[ResSoli.ColumnCommitVQ] = "N/A";
                    if (msg.bIsVQReversed == true)
                        oRow[ResSoli.ColumnReverseVQ] = "N/A";
                    if (msg.bIsVQWasted == true)
                        oRow[ResSoli.ColumnWastedVQ] = "N/A";
                    if (msg.bIsVQFree == true)
                        oRow[ResSoli.ColumnFreeVQ] = "N/A";
                }
                else
                {
                    if (msg.bIsVQCurrent == true)
                        oRow[ResSoli.ColumnVQ] = ConvertToStrData(oBase[0]) + " + " + ConvertToStrData(iVQ - oBase[0]);
                    if (msg.bIsVQCommitted == true)
                        oRow[ResSoli.ColumnCommitVQ] = ConvertToStrData(oBase[3]) + " + " + ConvertToStrData(iCommit - oBase[3]);
                    if (msg.bIsVQReversed == true)
                        oRow[ResSoli.ColumnReverseVQ] = ConvertToStrData(oBase[4]) + " + " + ConvertToStrData(iReversed - oBase[4]);
                    if (msg.bIsVQWasted == true)
                        oRow[ResSoli.ColumnWastedVQ] = ConvertToStrData(oBase[5]) + " + " + ConvertToStrData(iWaste - oBase[5]);
                    if (msg.bIsVQFree == true)
                        oRow[ResSoli.ColumnFreeVQ] = ConvertToStrData(oBase[6]) + " + " + ConvertToStrData(iFree - oBase[6]);
                }
            }
            else
            {
                if (msg.bIsWSCurrent == true)
                    oRow[ResSoli.ColumnWS] = ConvertToStrData(op.WorkingSet64);
                if (msg.bIsPFCurrent == true)
                oRow[ResSoli.ColumnPF] = ConvertToStrData(op.PagedMemorySize64);

                if (iVQ == 0)
                {
                    if (msg.bIsVQCurrent == true)
                        oRow[ResSoli.ColumnVQ] = "N/A";
                    if (msg.bIsVQCommitted == true)
                        oRow[ResSoli.ColumnCommitVQ] = "N/A";
                    if (msg.bIsVQReversed == true)
                        oRow[ResSoli.ColumnReverseVQ] = "N/A";
                    if (msg.bIsVQWasted == true)
                        oRow[ResSoli.ColumnWastedVQ] = "N/A";
                    if (msg.bIsVQFree == true)
                        oRow[ResSoli.ColumnFreeVQ] = "N/A";
                }
                else
                {
                    if (msg.bIsVQCurrent == true)
                        oRow[ResSoli.ColumnVQ] = ConvertToStrData(iVQ);
                    if (msg.bIsVQCommitted == true)
                        oRow[ResSoli.ColumnCommitVQ] = ConvertToStrData(iCommit);
                    if (msg.bIsVQReversed == true)
                        oRow[ResSoli.ColumnReverseVQ] = ConvertToStrData(iReversed);
                    if (msg.bIsVQWasted == true)
                        oRow[ResSoli.ColumnWastedVQ] = ConvertToStrData(iWaste);
                    if (msg.bIsVQFree == true)
                        oRow[ResSoli.ColumnFreeVQ] = ConvertToStrData(iFree);
                }
            }

            #endregion



            #endregion
        }

        /// <summary>
        /// Reborn the dead process
        /// </summary>
        /// <param name="row">the row that related with the MemProbe</param>
        public void Reborn(ref DataRow row, ColumnSettingMsg msg)
        {
                row[ResSoli.ColumnProcessName] = tName + " (Null)";
                eProbeStatus = ProbeStatus.Newborn;
                CatchNew(ref row, msg);
        }

        /// <summary>
        /// Destructor
        /// </summary>
        public void Dispose()
        {
            tName = null;
            if (eProbeStatus == ProbeStatus.Catching)
            {
                iProbeList.Remove(PID);
            }
            try
            {
                op.Dispose();
            }
            catch
            {
            }

        }

        /// <summary>
        /// Kill the process
        /// </summary>
        public void Kill()
        {
            
            if (eProbeStatus == ProbeStatus.Catching)
            {
                try
                {
                    op.Kill();
                }
                catch
                {
                    MessageBox.Show("Kill Failed.", "SoliCap");
                    throw new Exception();
                }
                op.Dispose();
                tName = null;
                iProbeList.Remove(PID);
            }
        }

        /// <summary>
        /// Set Base Value
        /// </summary>
        public void SetBase()
        {
            if (op != null)
                oBase = new long[] { iVQ, op.WorkingSet64, op.PagedMemorySize64, iCommit, iReversed, iWaste, iFree };
            else
                oBase = new long[] { 0, 0, 0, 0, 0, 0, 0 };
        }

        /// <summary>
        /// Convent memory value to string based on current unit set.
        /// </summary>
        /// <param name="lValue">Memory value</param>
        /// <returns></returns>
        private string ConvertToStrData(long lValue)
        {
            switch (eUnitType)
            {
                case MemUnitType.B:
                    return (Convert.ToString(lValue) + "byte");
                case MemUnitType.KB:
                    return (Convert.ToString(Math.Round((decimal)lValue / 1024, 1)) + "Kb");
                case MemUnitType.MB:
                    return (Convert.ToString(Math.Round((decimal)lValue / (long)Math.Pow(1024, 2), 1)) + "Mb");
                case MemUnitType.GB:
                    return (Convert.ToString(Math.Round((decimal)lValue / (long)Math.Pow(1024, 3), 2)) + "Gb");
                case MemUnitType.Smart:
                    {
                        if (lValue < 1024 * 2)
                            return (Convert.ToString(lValue) + "b");
                        else if ((lValue >= 1024 * 2) && (lValue < 2097152)) //1024*2 = 2KB, 1024*1024*2 = 2MB
                            return (Convert.ToString(Math.Round((decimal)lValue / 1024, 1)) + "Kb");
                        else if ((lValue >= 2097152) && (lValue < 1342177280))   //1024*1024*2 = 2MB,  1024*1024*1.25 = 1.25GB
                            return (Convert.ToString(Math.Round((decimal)lValue / (long)Math.Pow(1024, 2), 1)) + "Mb");
                        else if (lValue > 1342177280)             //1024*1024*1.25 = 1.25GB
                            return (Convert.ToString(Math.Round((decimal)lValue / (long)Math.Pow(1024, 3), 2)) + "Gb");
                        return "";
                    }
                default:
                    return "";
            }
        }

        private void SetDeadStatus(ref DataRow oRow)
        {
            iProbeList.Remove(PID);
            oRow[ResSoli.ColumnProcessName] = tName + " (Exit)";
            try
            {
                oRow[ResSoli.ColumnExitTime] = op.ExitTime.ToLongTimeString();
            }
            catch
            {
                oRow[ResSoli.ColumnExitTime] = "N/A";
            }

            op.Dispose();
            op = null;

            PID = -1;
            eProbeStatus = ProbeStatus.Dead;
            //Exit
        }

        #endregion

    }
}