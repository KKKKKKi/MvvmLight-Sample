namespace MvvmLightSample.ViewModel
{
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;

    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            Messenger.Default.Register<NotificationMessage>(this, msg =>
            {
                if (msg.Sender is SettingViewModel)
                {
                    _dialogService.ShowMessage(msg.Notification, msg.Notification);
                }
            });
        }

        private IDialogService _dialogService => ServiceLocator.Current.GetInstance<IDialogService>();
        private INavigationService _navigationService => ServiceLocator.Current.GetInstance<INavigationService>();

        private RelayCommand _loadedCommand;

        public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(() =>
        {
            _navigationService.NavigateTo(nameof(View.SettingView), null);
        }));

        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
            base.Cleanup();
        }
    }
}