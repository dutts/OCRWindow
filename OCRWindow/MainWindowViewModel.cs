using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using OCRWindow.Controls;
using OCRWindow.Controls.AreaSelectionOverlay;
using OCRWindow.Controls.ProcessPicker;
using OCRWindow.PInvoke;

namespace OCRWindow
{
    public class MainWindowViewModel : BindableBase
    {
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        private DelegateCommand _showProcessPickerWindow;
        public DelegateCommand ShowProcessPickerWindow
        {
            get
            {
                return _showProcessPickerWindow ?? (_showProcessPickerWindow = new DelegateCommand(() =>
                {
                    var ppvm = new ProcessPickerViewModel();
                    var processPickerWindow = new CloseAwareWindow{ Content = new ProcessPicker(),  DataContext = ppvm };

                    WeakEventManager<ProcessPickerViewModel, EventArgs<ProcessData>>.AddHandler(ppvm, "ProcessSelected", (sender, args) =>
                    {
                        SelectedProcess = args.Payload;
                    });
                    processPickerWindow.Show();
                }));
            }
        }

        private DelegateCommand _showAreaSelectionWindow;
        public DelegateCommand ShowAreaSelectionWindow
        {
            get
            {
                return _showAreaSelectionWindow ?? (_showAreaSelectionWindow = new DelegateCommand(() =>
                {
                    RECT rc;
                    GetWindowRect(SelectedProcess.Handle, out rc);

                    var bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
                    var gfxBmp = Graphics.FromImage(bmp);
                    var hdcBitmap = gfxBmp.GetHdc();
                    PrintWindow(SelectedProcess.Handle, hdcBitmap, 0);
                    gfxBmp.ReleaseHdc(hdcBitmap);
                    gfxBmp.Dispose();

                    var areaSelectionViewModel = new AreaSelectionViewModel{ WindowPosLeft = rc.X, WindowPosTop = rc.Y, WindowHeight = rc.Height, WindowWidth = rc.Width };
                    var areaSelectionOverlay = new AreaSelectionOverlay() { DataContext = areaSelectionViewModel };
                    areaSelectionOverlay.ShowDialog();
                }, () => (SelectedProcess != null)));
            }
        }

        private ProcessData _selectedProcess;
        public ProcessData SelectedProcess
        {
            get { return _selectedProcess; }
            set
            {
                SetProperty(ref _selectedProcess, value);
                ShowAreaSelectionWindow.RaiseCanExecuteChanged();
            }
        }

        private string _outputText;
        public string OutputText
        {
            get { return _outputText; }
            set { SetProperty(ref _outputText, value); }
        }
    }

}
