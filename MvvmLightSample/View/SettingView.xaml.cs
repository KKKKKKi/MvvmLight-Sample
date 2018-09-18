namespace MvvmLightSample.View
{
    using ViewModel;
    using System.Windows.Navigation;

    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingView
    {
        public SettingView()
        {
            InitializeComponent();
        }

        private SettingViewModel Vm => DataContext as SettingViewModel;
    }
}
