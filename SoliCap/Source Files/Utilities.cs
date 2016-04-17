using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Data;


namespace SoliCap
{
    public sealed class APIUtls
    {
        #region Enums

        /// <summary>
        /// Types of Virtual Alloc usage, contains Commited, Reversed, Wasted and Available
        /// </summary>
        internal enum VirtualAllocExTypes
        {
            WRITE_WATCH_FLAG_RESET = 0x00000001, // Win98 only
            MEM_COMMIT = 0x00001000,
            MEM_RESERVE = 0x00002000,
            MEM_COMMIT_OR_RESERVE = 0x00003000,
            MEM_DECOMMIT = 0x00004000,
            MEM_RELEASE = 0x00008000,
            MEM_FREE = 0x00010000,
            MEM_PUBLIC = 0x00020000,
            MEM_MAPPED = 0x00040000,
            MEM_RESET = 0x00080000, // Win2K only
            MEM_TOP_DOWN = 0x00100000,
            MEM_WRITE_WATCH = 0x00200000, // Win98 only
            MEM_PHYSICAL = 0x00400000, // Win2K only
            //MEM_4MB_PAGES    = 0x80000000, // ??
            SEC_IMAGE = 0x01000000,
            MEM_IMAGE = SEC_IMAGE
        }

        /// <summary>
        /// Page Access Protection Flags
        /// </summary>
        internal enum AccessProtectionFlags
        {
            PAGE_NOACCESS = 0x001,
            PAGE_READONLY = 0x002,
            PAGE_READWRITE = 0x004,
            PAGE_WRITECOPY = 0x008,
            PAGE_EXECUTE = 0x010,
            PAGE_EXECUTE_READ = 0x020,
            PAGE_EXECUTE_READWRITE = 0x040,
            PAGE_EXECUTE_WRITECOPY = 0x080,
            PAGE_GUARD = 0x100,
            PAGE_NOCACHE = 0x200,
            PAGE_WRITECOMBINE = 0x400
        }

        /// <summary>
        /// 
        /// </summary>
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }
        #endregion

        #region Structs

        /// <summary>
        /// Struct Processor Info Union, used in struct System info.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct PROCESSOR_INFO_UNION
        {
            [FieldOffset(0)]
            internal uint dwOemId;
            [FieldOffset(0)]
            internal ushort wProcessorArchitecture;
            [FieldOffset(2)]
            internal ushort wReserved;
        }

        /// <summary>
        /// System Info Variable that getted from "GetSystemInfo" API Method.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            internal PROCESSOR_INFO_UNION p;
            public uint dwPageSize;
            public UIntPtr lpMinimumApplicationAddress;
            public UIntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public uint wProcessorLevel;
            public uint wProcessorRevision;
        };

        /// <summary>
        /// Memory Basic Infomation Struct. Used in Virtual Query API Method.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public UIntPtr BaseAddress;
            public UIntPtr AllocationBase;
            public uint AllocationProtect;
            public UIntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        /// <summary>
        /// contains information about the current state of both physical and virtual memory, including extended memory
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MEMORYSTATUSEX
        {
            /// <summary>
            /// Size of the structure, in bytes. You must set this member before calling GlobalMemoryStatusEx. 
            /// </summary>
            public uint dwLength;

            /// <summary>
            /// Number between 0 and 100 that specifies the approximate percentage of physical memory that is in use (0 indicates no memory use and 100 indicates full memory use). 
            /// </summary>
            public uint dwMemoryLoad;

            /// <summary>
            /// Total size of physical memory, in bytes.
            /// </summary>
            public ulong ullTotalPhys;

            /// <summary>
            /// Size of physical memory available, in bytes. 
            /// </summary>
            public ulong ullAvailPhys;

            /// <summary>
            /// Size of the committed memory limit, in bytes. This is physical memory plus the size of the page file, minus a small overhead. 
            /// </summary>
            public ulong ullTotalPageFile;



            /// <summary>
            /// Size of available memory to commit, in bytes. The limit is ullTotalPageFile. 
            /// </summary>
            public ulong ullAvailPageFile;

            /// <summary>
            /// Total size of the user mode portion of the virtual address space of the calling process, in bytes. 
            /// </summary>
            public ulong ullTotalVirtual;

            /// <summary>
            /// Size of unreserved and uncommitted memory in the user mode portion of the virtual address space of the calling process, in bytes. 
            /// </summary>
            public ulong ullAvailVirtual;

            /// <summary>
            /// Size of unreserved and uncommitted memory in the extended portion of the virtual address space of the calling process, in bytes. 
            /// </summary>
            public ulong ullAvailExtendedVirtual;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:MEMORYSTATUSEX"/> class.
            /// </summary>
            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            }
        }



        #endregion

        #region Methods

        [DllImport("kernel32.dll")]
        internal static extern uint VirtualQueryEx(IntPtr hProcess, UIntPtr lpAddress,
           ref MEMORY_BASIC_INFORMATION lpBuffer, UIntPtr dwLength);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
           uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        internal static extern void GetSystemInfo(
            [MarshalAs(UnmanagedType.Struct)] ref SYSTEM_INFO lpSystemInfo
        );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
           uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetSystemTimes(
                    out System.Runtime.InteropServices.ComTypes.FILETIME lpIdleTime,
                    out System.Runtime.InteropServices.ComTypes.FILETIME lpKernelTime,
                    out System.Runtime.InteropServices.ComTypes.FILETIME lpUserTime
                    );

        public static void GetCPUTime(ref double sys, ref double idle)
        {

            System.Runtime.InteropServices.ComTypes.FILETIME idleTime, kernelTime, userTime;
            GetSystemTimes(out idleTime, out kernelTime, out userTime);
            ulong idleTimeLong = ((ulong)idleTime.dwHighDateTime << 32) + (uint)idleTime.dwLowDateTime;
            ulong kernelTimeLong = ((ulong)kernelTime.dwHighDateTime << 32) + (uint)kernelTime.dwLowDateTime;
            ulong userTimeLong = ((ulong)userTime.dwHighDateTime << 32) + (uint)userTime.dwLowDateTime;
            idle = idleTimeLong*1.0 / TimeSpan.TicksPerMillisecond;
            sys = (kernelTimeLong + userTimeLong)*1.0 / TimeSpan.TicksPerMillisecond;
        }


        #endregion

    }

    /// <summary>
    /// Message that show the column setting in the table
    /// </summary>
    public struct ColumnSettingMsg
    {
        public bool bIsCPUUsage;
        public bool bIsVQCurrent;
        public bool bIsVQPeak;
        public bool bIsWSCurrent;
        public bool bIsWSPeak;
        public bool bIsPFCurrent;
        public bool bIsPFPeak;
        public bool bIsVQCommitted;
        public bool bIsVQReversed;
        public bool bIsVQWasted;
        public bool bIsVQFree;
        public bool bIsStartTime;
        public bool bIsExitTime;
        public bool bIsFileName;
        /// <summary>
        /// Set ColumnSetting Message Value
        /// </summary>
        /// <param name="isVQ">Veirfy CPU Usage column is exist.</param>
        /// <param name="isVQ">Verify Whether VQ column is exist.</param>
        /// <param name="isVQPeak">Verify Whether VQ Peak column is exist.</param>
        /// <param name="isWS">Verify Whether WS column is exist.</param>
        /// <param name="isWSPeak">Verify Whether WS Peak column is exist.</param>
        /// <param name="isPF">Verify Whether PF column is exist.</param>
        /// <param name="isPFPeak">Verify Whether PF Peak column is exist.</param>
        /// <param name="isVQCommit">Verify Whether VQ commit column is exist.</param>
        /// <param name="isVQReverse">Verify Whether VQ reverse column is exist.</param>
        /// <param name="isVQWaste">Verify Whether VQ waste column is exist.</param>
        /// <param name="isVQFree">Verify Whether VQ free column is exist.</param>
        /// <param name="isStartTime">Verify Whether Start Time column is exist.</param>
        /// <param name="isEndTime">Verify Whether End Time column is exist.</param>
        /// <param name="isFileName">Verify Whether File Name column is exist.</param>
        public void setValue(bool isUsage, bool isVQ, bool isVQPeak, bool isWS, bool isWSPeak,
                                bool isPF, bool isPFPeak,
                                bool isVQCommit, bool isVQReverse, bool isVQWaste, bool isVQFree,
                                bool isStartTime, bool isExitTime, bool isFileName)
        {
            bIsCPUUsage = isUsage;
            bIsVQCurrent = isVQ;
            bIsVQPeak = isVQPeak;
            bIsWSCurrent = isWS;
            bIsWSPeak = isWSPeak;
            bIsPFCurrent = isPF;
            bIsPFPeak = isPFPeak;
            bIsVQCommitted = isVQCommit;
            bIsVQReversed = isVQReverse;
            bIsVQWasted = isVQWaste;
            bIsVQFree = isVQFree;
            bIsStartTime = isStartTime;
            bIsExitTime = isExitTime;
            bIsFileName = isFileName;
        }
    }

    /// <summary>
    /// The Memory Unit Type
    /// </summary>
    public enum MemUnitType
    {
        Smart,
        GB,
        MB,
        KB,
        B,
    }

    /// <summary>
    /// Probe Status Enum
    /// </summary>
    public enum ProbeStatus
    {
        Newborn,
        Catching,
        Dead,
    }

    public struct TimerSettingMsg
    {
        public Keys SysKey;
        public Keys NormalKey;
        public bool bIsLockBaseWhenStart;
        public bool bIsStopRefreshWhenStop;

        /// <summary>
        /// Set Timer Setting Message
        /// </summary>
        /// <param name="sys">The system hot key, such as Ctrl, Alt, Shift, Delete</param>
        /// <param name="normal">The normal hot key.</param>
        /// <param name="islockbase">If true, the base will be locked when timer is start</param>
        /// <param name="isStopRefresh">If true, the refresh will stop when timer is stop</param>
        public void SetValue(Keys sys, Keys normal, bool islockbase, bool isStopRefresh)
        {
            SysKey = sys;
            NormalKey = normal;
            bIsLockBaseWhenStart = islockbase;
            bIsStopRefreshWhenStop = isStopRefresh;
        }
    }

    /// <summary>
    /// Data Table that store the memory check result.
    /// </summary>
    public class StringDataTable : DataTable
    {
        #region Data Members

        #endregion

        #region Properties

        #endregion

        #region Methods

            public StringDataTable()
            {
                List<string> Items = new List<string>();
                
                //Initialize Items.
                Items.Add(ResSoli.ColumnProcessName);
                Items.Add(ResSoli.ColumnCPU_Usage);
                Items.Add(ResSoli.ColumnVQ);
                Items.Add(ResSoli.ColumnPeakVQ);
                Items.Add(ResSoli.ColumnWS);
                Items.Add(ResSoli.ColumnPeakWS);
                Items.Add(ResSoli.ColumnPF);
                Items.Add(ResSoli.ColumnPeakPF);
                Items.Add(ResSoli.ColumnCommitVQ);
                Items.Add(ResSoli.ColumnReverseVQ);
                Items.Add(ResSoli.ColumnWastedVQ);
                Items.Add(ResSoli.ColumnFreeVQ);
                Items.Add(ResSoli.ColumnStartTime);
                Items.Add(ResSoli.ColumnExitTime);
                Items.Add(ResSoli.ColumnFile);
                
                foreach(string item in Items)
                {
                    DataColumn col = new DataColumn();
                    col.DataType = System.Type.GetType("System.String");
                    col.ColumnName = item;
                    col.AutoIncrement = false;
                    col.Caption = item;
                    col.Unique = false;
                    this.Columns.Add(col);
                }
            }

        #endregion
    }
}

