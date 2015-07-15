using System;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using OCRWindow.Controls;
using OCRWindow.Controls.ProcessPicker;

namespace OCRWindow
{
    public class MainWindowViewModel : BindableBase
    {
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

        private ProcessData _selectedProcess;
        public ProcessData SelectedProcess
        {
            get { return _selectedProcess; }
            set { SetProperty(ref _selectedProcess, value); }
        }

        private string _outputText;
        public string OutputText
        {
            get { return _outputText; }
            set { SetProperty(ref _outputText, value); }
        }
    }

}
