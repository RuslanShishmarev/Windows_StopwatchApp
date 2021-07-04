using StopwatchApp.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace StopwatchApp.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {

        private ObservableCollection<StopwatchCounter> _allRecords = new ObservableCollection<StopwatchCounter>();
        public ObservableCollection<StopwatchCounter> AllRecords
        {
            get
            {
                return _allRecords;
            }
            set
            {
                _allRecords = value;
                OnPropertyChanged(nameof(AllRecords));
            }
        }

        private StopwatchCounter _counter = new StopwatchCounter();
        public StopwatchCounter Counter
        {
            get
            {
                return _counter;
            }
            set
            {
                _counter = value;
                OnPropertyChanged(nameof(Counter));
            }
        }

        private StopwatchCounter _selectedRecorded;

        public StopwatchCounter SelectedRecorded
        {
            get
            {
                return _selectedRecorded;
            }
            set
            {
                _selectedRecorded = value;
                OnPropertyChanged(nameof(SelectedRecorded));
            }
        }

        private bool _isStopwatcherWork = false;
        public bool IsStopwatcherWork
        { 
            get
            {
                return _isStopwatcherWork;
            }
            set
            {
                _isStopwatcherWork = value;
                OnPropertyChanged(nameof(IsStopwatcherWork));
            }
        }

        private bool _canBeReset = false;
        public bool CanBeReset
        {
            get
            {
                return _canBeReset;
            }
            set
            {
                _canBeReset = value;
                OnPropertyChanged(nameof(CanBeReset));
            }
        }


        private RelayCommand startStopwatcher;
        public RelayCommand StartStopwatcher
        {
            get
            {
                return startStopwatcher ?? new RelayCommand(obj =>
                {
                    IsStopwatcherWork = !IsStopwatcherWork;
                    CountWatcher();
                });
            }
        }

        private RelayCommand resetWatcher;
        public RelayCommand ResetWatcher
        {
            get
            {
                return resetWatcher ?? new RelayCommand(obj =>
                {
                    IsStopwatcherWork = CanBeReset = false;
                    Counter = new StopwatchCounter();
                });
            }
        }

        private RelayCommand recordCounter;
        public RelayCommand RecordCounter
        {
            get
            {
                return recordCounter ?? new RelayCommand(obj =>
                {
                    GetCounter();
                });
            }
        }

        private RelayCommand removeRecordedCounter;
        public RelayCommand RemoveRecordedCounter
        {
            get
            {
                return removeRecordedCounter ?? new RelayCommand(obj =>
                {
                    var selected = AllRecords.FirstOrDefault(w => w.AllSeconds == SelectedRecorded.AllSeconds);
                    AllRecords.Remove(selected);
                });
            }
        }
        private void GetCounter()
        {
            AllRecords.Add(Counter);
        }

        private async void CountWatcher()
        {
            while (IsStopwatcherWork)
            {
                Counter++;
                await Task.Delay(1000);
                CanBeReset = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
