using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms; 

namespace KeyHookLib
{
    public class ExternalKeyHook:Object 
    {

        #region Data Members From WinAPI

        //Hook Type Enum
        private enum HookType
        {
        WH_JOURNALRECORD = 0,
        WH_JOURNALPLAYBACK = 1,
        WH_KEYBOARD = 2,
        WH_GETMESSAGE = 3,
        WH_CALLWNDPROC = 4,
        WH_CBT = 5,
        WH_SYSMSGFILTER = 6,
        WH_MOUSE = 7,
        WH_HARDWARE = 8,
        WH_DEBUG = 9,
        WH_SHELL = 10,
        WH_FOREGROUNDIDLE = 11,
        WH_CALLWNDPROCRET = 12,
        WH_KEYBOARD_LL = 13,
        WH_MOUSE_LL = 14
        }

        //Windows Message Enum
        private enum WndMsg
        {
        //KeyBoard
        WM_KEYDOWN = 256,
        WM_KEYUP = 257,
        WM_SYSKEYDOWN = 260,
        WM_SYSKEYUP = 261,

        //Mouse General Action
        WM_MOUSEACTIVATE = 0x21,
        WM_MOUSEFIRST = 0x200,
        WM_MOUSEHOVER = 0x2A1,
        WM_MOUSELAST = 0x20D,
        WM_MOUSELEAVE = 0x2A3,
        WM_MOUSEMOVE = 0x200,
        WM_MOUSEWHEEL = 0x20A,
        WM_MOUSEHWHEEL = 0x20E,

        //Mouse Left Button
        WM_LBUTTONDBLCLK = 0x203,
        WM_LBUTTONDOWN = 0x201,
        WM_LBUTTONUP = 0x202,

        //Mouse Middle Button
        WM_MBUTTONDBLCLK = 0x209,
        WM_MBUTTONDOWN = 0x207,
        WM_MBUTTONUP = 0x208,

        //Mouse Right Button
        WM_RBUTTONDBLCLK = 0x206,
        WM_RBUTTONDOWN = 0x204,
        WM_RBUTTONUP = 0x205,

        //NC Message
        WM_NCACTIVATE = 0x86,
        WM_NCCALCSIZE = 0x83,
        WM_NCCREATE = 0x81,
        WM_NCDESTROY = 0x82,
        WM_NCHITTEST = 0x84,
        WM_NCLBUTTONDBLCLK = 0xA3,
        WM_NCLBUTTONDOWN = 0xA1,
        WM_NCLBUTTONUP = 0xA2,
        WM_NCMBUTTONDBLCLK = 0xA9,
        WM_NCMBUTTONDOWN = 0xA7,
        WM_NCMBUTTONUP = 0xA8,
        WM_NCMOUSEMOVE = 0xA0,
        WM_NCPAINT = 0x85,
        WM_NCRBUTTONDBLCLK = 0xA6,
        WM_NCRBUTTONDOWN = 0xA4,
        WM_NCRBUTTONUP = 0xA5
        }

        //KeyboardHookStrucct
        [StructLayout(LayoutKind.Sequential)]
        private class KeyboardHookStruct
        {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public int dwExtraInfo;
        }

        #endregion

        #region KeyEventHandler and WinAPI Function

        //Define Keyboard Events
        public event KeyEventHandler KeyDown;

        //Windows API Functions
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        #endregion

        #region Data Members

        ///-----------------------Variable------------------------------'

        //Handle of Hook
        static int hKeyboardHook = 0;

        //System Key Tag
        private Boolean Key_Control_Down = false;
        private Boolean Key_Shift_Down = false;
        private Boolean Key_Alt_Down = false;

        //Set KeyCallNextHook Tag
        private Boolean bIsBlock = true;

        ///----------------------Delegate&Class-------------------------'

        //Claim Delegates
        public delegate int HookProc(int nCode,Int32 wParam,IntPtr lParam);
        private HookProc KeyboardHookProcedure;
        #endregion

        #region Properties

        /// <summary>
        /// Change the Block property. If true, Keyboard will be blocked except hot key.
        /// </summary>
        public bool IsBlockKeyboard
        {
            set
            {
                bIsBlock = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// bIsHold determines whether the keyboard will be block for other App. True means block.
        /// </summary>
        public ExternalKeyHook(Boolean bIsHold)
        {
            bIsBlock = bIsHold;
        }

        /// <summary>
        /// Start Hook
        /// </summary>
        public void Start()
        {
            //Initialize Keyboard Hook
            if (hKeyboardHook == 0)
            {
                Key_Control_Down = false;
                Key_Shift_Down = false;
                Key_Alt_Down = false;
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                hKeyboardHook = SetWindowsHookEx((int)HookType.WH_KEYBOARD_LL, KeyboardHookProcedure, Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                if (hKeyboardHook == 0)
                {
                    StopMe();
                    throw new Exception("SetWindowsHookEx ist failed.");
                }
            }

        }

        /// <summary>
        /// Stop Hook
        /// </summary>
        public void StopMe()
        {
                Boolean  retKeyboard;
                if (hKeyboardHook != 0)
                {
                    retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                    hKeyboardHook = 0;

                    Key_Control_Down = false;
                    Key_Shift_Down = false;
                    Key_Alt_Down = false;

                    if (!retKeyboard)
                    {
                        throw new Exception("UnhookWindowsHookEx failed.");
                    }
                }
        }

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {

                //Define Keyboard Hook Structure
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)(Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct)));

                #region KeyDown Handle
                //Define Key Down Handle
                if (wParam == (int)WndMsg.WM_KEYDOWN || wParam == (int)WndMsg.WM_SYSKEYDOWN)
                {
                    //Get Key Data
                    Keys keyData = (Keys)(MyKeyboardHookStruct.vkCode);

                    //Sys Key Judgement
                    switch(keyData)
                    {
                        case Keys.LControlKey:
                        case Keys.RControlKey:
                            {
                                Key_Control_Down = true;
                                break;
                            }
                        case Keys.LShiftKey:
                        case Keys.RShiftKey:
                            {
                                Key_Shift_Down = true;
                                break;
                            }
                        case Keys.LMenu:
                        case Keys.RMenu:
                            {
                                Key_Alt_Down = true;
                                break;
                            }
                    }

                    //Set SysKey Info to sender
                    if (Key_Control_Down)
                        keyData = keyData | Keys.Control;
                    if (Key_Shift_Down)
                        keyData = keyData | Keys.Shift;
                    if (Key_Alt_Down)
                        keyData = keyData | Keys.Alt;

                    //Raise Key Down Event
                    KeyEventArgs e= new KeyEventArgs(keyData);
                    KeyDown(this,e);
                }
                #endregion

                //Key Up Handle
                #region KeyUp Handle
                if (wParam == (int)WndMsg.WM_KEYUP || wParam == (int)WndMsg.WM_SYSKEYUP)
                {
                    //Get Key Data
                    Keys keyData = (Keys)(MyKeyboardHookStruct.vkCode);

                    //Sys Key Judgement
                    switch (keyData)
                    {
                        case Keys.LControlKey:
                        case Keys.RControlKey:
                            Key_Control_Down = false;
                            break;
                        case Keys.LShiftKey:
                        case Keys.RShiftKey:
                            Key_Shift_Down = false;
                            break;
                        case Keys.LMenu:
                        case Keys.RMenu:
                            Key_Alt_Down = false;
                            break;
                    }
                    //Set SysKey Info to sender
                    if (Key_Control_Down)
                        keyData = keyData | Keys.Control;
                    if (Key_Shift_Down)
                        keyData = keyData | Keys.Shift;
                    if (Key_Alt_Down)
                        keyData = keyData | Keys.Alt;

                    //Raise Key Down Event
                    KeyEventArgs e= new KeyEventArgs(keyData);
                    //KeyUp(this,e);
                }
                #endregion

                //Determine whether pass the key message to other APP
                if (bIsBlock == false)
                    return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
                else
                    return 1;
            }
            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }

        #endregion



    }
}
