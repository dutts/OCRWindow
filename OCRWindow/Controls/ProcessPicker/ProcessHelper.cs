using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Security.Principal;

namespace OCRWindow.Controls.ProcessPicker
{
    public static class ProcessHelper
    {
        public static IEnumerable<ProcessData> GetCurrentUsersProcesses()
        {
            var currentUser = WindowsIdentity.GetCurrent();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * from Win32_Process");
            if (searcher.Get().Count == 0) yield break;
            foreach (ManagementObject obj in searcher.Get())
            {
                var o = new String[1];
                try
                {
                    obj.InvokeMethod("GetOwnerSid", o);
                }
                catch
                {
                    // TODO
                }

                var sid = o[0];

                if (sid != currentUser.User.Value) continue;

                var id = (uint)obj["ProcessId"];
                var name = (string)obj["Name"];

                var title = Process.GetProcessById((int)id).MainWindowTitle;

                yield return new ProcessData() { Id = id, MainWindowTitle = title, Name = name };
            }
        }

    }
}
