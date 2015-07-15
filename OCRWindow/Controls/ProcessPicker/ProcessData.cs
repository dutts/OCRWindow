using System;
using System.Diagnostics;
using Microsoft.Practices.Prism.Mvvm;

namespace OCRWindow.Controls.ProcessPicker
{
    public class ProcessData : BindableBase
    {
        private uint _id;
        public uint Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _mainWindowTitle;
        public string MainWindowTitle
        {
            get { return _mainWindowTitle; }
            set { SetProperty(ref _mainWindowTitle, value); }
        }

        private Process _process;
        public Process Process
        {
            get { return _process ?? (_process = Process.GetProcessById((int) Id)); }
        }

        private IntPtr? _handle;
        public IntPtr Handle
        {
            get
            {
                if (_handle == null) _handle = Process.MainWindowHandle;
                return (IntPtr) _handle;
            }
        }


    }

}
