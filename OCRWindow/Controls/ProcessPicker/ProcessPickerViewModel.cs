using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using OCRWindow.Interfaces;

namespace OCRWindow.Controls.ProcessPicker
{
    public class EventArgs<T> : EventArgs
    {
        public T Payload { get; set; }
    }

    public class ProcessPickerViewModel : BindableBase, IDisposable, IRequestCloseViewModel
    {
        public event EventHandler RequestClose;

        public event EventHandler<EventArgs<ProcessData>> ProcessSelected;

        private BackgroundWorker _processScanner;

        private ObservableCollection<ProcessData> _processList;
        public ObservableCollection<ProcessData> ProcessList
        {
            get { return _processList; }
            set { SetProperty(ref _processList, value); }
        }

        private ICollectionView _filteredProcessList;
        public ICollectionView FilteredProcessList
        {
            get { return _filteredProcessList; }
            set { SetProperty(ref _filteredProcessList, value); }
        }

        public bool HasTitle(object p)
        {
            ProcessData pd = p as ProcessData;
            return (pd.MainWindowTitle.Length > 0);
        }

        private ProcessData _selectedProcess;
        public ProcessData SelectedProcess
        {
            get { return _selectedProcess; }
            set
            {
                SetProperty(ref _selectedProcess, value);
                SelectProcess.RaiseCanExecuteChanged();
            }
        }

        private DelegateCommand _selectProcess;
        public DelegateCommand SelectProcess
        {
            get
            {
                return _selectProcess ?? (_selectProcess = new DelegateCommand(() =>
                {
                    if (ProcessSelected != null) ProcessSelected(this, new EventArgs<ProcessData>() { Payload = SelectedProcess });
                    if (RequestClose != null) RequestClose(this, null);

                }, () => SelectedProcess != null));
            }
        }

        private Task ProcessScannerTask;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public ProcessPickerViewModel()
        {
            _processList = new ObservableCollection<ProcessData>();

            FilteredProcessList = CollectionViewSource.GetDefaultView(ProcessList);
            if (FilteredProcessList != null)
            {
                FilteredProcessList.Filter += HasTitle;
            }

            ProcessScannerTask = Task.Factory.StartNew(() => ScanProcesses(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
        }

        private void ScanProcesses(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var deletionCandidates = ProcessList.ToList<ProcessData>();

                foreach (var proc in ProcessHelper.GetCurrentUsersProcesses())
                {
                    var foundProc = ProcessList.FirstOrDefault(p => p.Id == proc.Id);
                    if (foundProc == null)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            ProcessList.Add(proc);
                        }));
                    }
                    else
                    {
                        foundProc.MainWindowTitle = proc.MainWindowTitle;
                        deletionCandidates.Remove(foundProc);
                    }
                }

                foreach (var procToDelete in deletionCandidates)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        ProcessList.Remove(procToDelete);
                    }));
                }
                Thread.Sleep(500);
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
    }

}
