using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MVVMSample007.Models;
using System;
using System.Threading.Tasks;

namespace MVVMSample007.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private bool _isBusy;
        /// <summary>
        /// Processing flag
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        private string _progressText;
        public string ProgressText
        {
            get => _progressText;
            set => SetProperty(ref _progressText, value);
        }

        private HeavyWorkModel _model;

        public IAsyncRelayCommand ExecuteCommand { get; }
        public IRelayCommand CancelCommand { get; }

        /// <summary>
        /// constructor
        /// </summary>
        public MainWindowViewModel()
        {
            IsBusy = false;

            ExecuteCommand = new AsyncRelayCommand(OnExecuteAsync, CanExecute);

            CancelCommand = new RelayCommand(
                () =>
                {
                    _model?.Cancel();
                    UpdateCommandStatus();
                },
                () => IsBusy);
        }

        private async Task OnExecuteAsync()
        {
            IsBusy = true;
            UpdateCommandStatus();

            _model = new HeavyWorkModel();

            var p = new Progress<ProgressInfo>();
            p.ProgressChanged += (sender, e) =>
            {
                ProgressValue = e.ProgressValue;
                ProgressText = e.ProgressText;
            };

            await _model.HeavyWorkAsync(p);

            IsBusy = false;
            UpdateCommandStatus();
        }

        private bool CanExecute()
        {
            return !IsBusy;
        }

        private void UpdateCommandStatus()
        {
            ExecuteCommand.NotifyCanExecuteChanged();
            CancelCommand.NotifyCanExecuteChanged();
        }
    }
}
