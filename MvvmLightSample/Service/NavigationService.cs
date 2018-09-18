namespace MvvmLightSample.Service
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class NavigationService : ViewModelBase, INavigationService
    {
        private readonly string _NavigationFrameKey = "NavigationFrame";
        private readonly string _GoBackParameter = "<GO=_=BK>";
        private readonly Dictionary<string, Uri> _pagesByKey;
        private readonly List<string> _historic;
        private string _currentPageKey;
        private Frame _navigationFrame;
        public object Parameter { get; private set; }

        public NavigationService()
        {
            _pagesByKey = new Dictionary<string, Uri>();
            _historic = new List<string>();
        }

        public string CurrentPageKey
        {
            get => _currentPageKey;
            private set => Set(ref _currentPageKey, value);
        }

        public virtual void GoBack()
        {
            if (_historic.Count <= 1)
                return;
            _historic.Remove(_historic.Last());
            NavigateTo(_historic.Last(), _GoBackParameter);
        }

        public virtual void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public virtual void NavigateTo(string pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                if (!_pagesByKey.ContainsKey(pageKey))
                    throw new ArgumentException($"No such page: {pageKey}", nameof(pageKey));

                if (_navigationFrame == null && GetDescendantFromName(Application.Current.MainWindow, _NavigationFrameKey) is Frame frame)
                    _navigationFrame = frame;

                if (_navigationFrame != null)
                {
                    // frame.Source = _pagesByKey[pageKey];
                    _navigationFrame.Navigate(_pagesByKey[pageKey], parameter);
                }
                Parameter = parameter;
                if (parameter == null || !_GoBackParameter.Equals(parameter.ToString()))
                {
                    _historic.Add(pageKey);
                }
                CurrentPageKey = pageKey;
            }
        }

        public void Configure(string key, Uri pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(key))
                {
                    _pagesByKey[key] = pageType;
                }
                else
                {
                    _pagesByKey.Add(key, pageType);
                }
            }
        }

        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (int i = 0; i < count; i++)
            {
                if (VisualTreeHelper.GetChild(parent, i) is FrameworkElement frameworkElement)
                {
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    frameworkElement = GetDescendantFromName(frameworkElement, name);

                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }
            return null;
        }

    }
}
