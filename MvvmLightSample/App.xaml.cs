namespace MvvmLightSample
{
    using GalaSoft.MvvmLight.Threading;

    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            DispatcherHelper.Initialize();
        }
    }
}
