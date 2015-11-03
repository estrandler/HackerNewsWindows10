using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using HackerNewsWindows10.Models;
using HackerNewsWindows10.Services;
using System.Diagnostics;
using System.Windows.Input;


namespace HackerNewsWindows10
{
    public sealed partial class MainPage : Page
    {
        private readonly NewsService _ns = new NewsService();
        private ObservableCollection<Story> _all;
        private string _currentCriteria = null;

        public MainPage()
        {
            InitializeComponent();
            _all = new ObservableCollection<Story>();
            var resp = _ns.GetStoryIDs(null);
            resp.Wait();


            foreach (var story in resp.Result.Take(10).Select(id => _ns.GetStory(id)))
            {
                story.Wait();
                _all.Add(story.Result);
            }

            list1.ItemsSource = _all;
        }

        private void ViewMoreStories_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var breakPoint = _all.Count + 10;
            var resp = _ns.GetStoryIDs(_currentCriteria);
            resp.Wait();


            foreach (
                var story in
                    resp.Result.Select(id => _ns.GetStory(id))
                        .Where(story => !_all.Select(x => x.id).Contains(story.Result.id)))
            {
                _all.Add(story.Result);
                if (_all.Count == breakPoint) break;
            }


            list1.ItemsSource = _all;
        }

        private void menuButtons_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var b = (Button) sender;
            _currentCriteria = b.Name == "Home" ? null : b.Name;
            var resp = _ns.GetStoryIDs(_currentCriteria);
            resp.Wait();

            _all.Clear();

            foreach (var story in resp.Result.Take(10).Select(id => _ns.GetStory(id)))
            {
                story.Wait();
                _all.Add(story.Result);
            }

            list1.ItemsSource = _all;
        }

        private async void url_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var b = (Button) sender;
            var story = (Story) b.DataContext;
            var url = story.url;
            await Windows.System.Launcher.LaunchUriAsync(new System.Uri(url));
        }

        private async void author_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var b = (Button) sender;
            var story = (Story) b.DataContext;
            var url = string.Format("https://news.ycombinator.com/user?id={0}", story.by);
            await Windows.System.Launcher.LaunchUriAsync(new System.Uri(url));
        }
    }
}