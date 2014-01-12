using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace XMouse
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (is64BitOperatingSystem)
                {
                    RegistryKey reg = Registry.ClassesRoot;
                    reg = reg.OpenSubKey(@"Installer\Products\60A9912A4C987814AAA4A36967BF97D9");
                    if((int)reg.GetValue("Version") != 33554445)
                        throw new SlimDxNotFoundException("Es wurde keine Version von SlimDX(x64) gefunden!");
                }
                else
                {
                    RegistryKey reg = Registry.ClassesRoot;
                    reg = reg.OpenSubKey(@"Installer\Products\34E0DBE70CA68AC49909005E0096DA92");
                    if((int)reg.GetValue("Version") != 33554445)
                        throw new SlimDxNotFoundException("Es wurde keine Version von SlimDX(x86) gefunden!");
                }
            }
            catch (SlimDxNotFoundException e)
            {
                MessageBox.Show(e.Message);
                Application.Exit();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static bool is64BitProcess = (IntPtr.Size == 8);
        static bool is64BitOperatingSystem = is64BitProcess || InternalCheckIsWow64();

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(
        [In] IntPtr hProcess,
        [Out] out bool wow64Process
        );

        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
