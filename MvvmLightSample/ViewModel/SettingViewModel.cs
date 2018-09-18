namespace MvvmLightSample.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;
    using Helper;

    public class SettingViewModel : ViewModelBase
    {
        private IDialogService _dialogService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IDialogService>();
        private INavigationService _navigationService = CommonServiceLocator.ServiceLocator.Current.GetInstance<INavigationService>();

        private RelayCommand<MouseEventArgs> _mouseMoveCommand;
        private RelayCommand _resetCommand;

        public RelayCommand<MouseEventArgs> MouseMoveCommand => _mouseMoveCommand ?? 
            (_mouseMoveCommand = new RelayCommand<MouseEventArgs>(
                e => 
                {
                    Point p = e.GetPosition(e.OriginalSource as UIElement);
                    Status = $"{p.X}x{p.Y}";
                }));

        public RelayCommand ResetCommand => _resetCommand ??
            (_resetCommand = new RelayCommand(
                () => { Status = "Reset"; }));

        private string _status = "Hello ...";

        public string Status
        {
            get => _status;
            private set => Set(ref _status, value);
        }

        public List<string> Langs { get; } = new List<string>()
        {
            "简体中文", "English",
        };

        private int _languageIndex = 0;

        public int LanguageIndex
        {
            get => _languageIndex;
            set
            {
                if (Set(ref _languageIndex, value))
                {
                    SwitchLang(value);
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, Application.Current.Resources["LangChanged"].ToString()));
                }
            }
        }

        private void SwitchLang(int index)
        {
            switch (index)
            {
                case 0:
                    LanguageHelper.LoadXamlStringsResource("pack://application:,,,/Resources/Strings/zh-Hans-CN.xaml");
                    // LanguageHelper.LoadXmlStringsResource("pack://application:,,,/Resources/Strings/zh-Hans-CN.xml");
                    break;
                case 1:
                    LanguageHelper.LoadXamlStringsResource("pack://application:,,,/Resources/Strings/en-US.xaml");
                    // LanguageHelper.LoadXmlStringsResource("pack://application:,,,/Resources/Strings/en-US.xml");
                    break;
                default:
                    break;
            }
        }
    }
}
