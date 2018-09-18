namespace MvvmLightSample.View
{
    using ViewModel;

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private MainViewModel Vm => DataContext as MainViewModel;
    }
}
