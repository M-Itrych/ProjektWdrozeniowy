using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Newtonsoft.Json;

namespace ProjektWdrozeniowy
{
    public partial class MainWindow : Window
    {
        private readonly ZipHandler _zipHandler;
        public List<CountryInfo>? Countries { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _zipHandler = new ZipHandler();

            PrepareTempDirectory();
            StartDownloadsAsync();
        }

        private void PrepareTempDirectory()
        {
            string tempDir = "./temp";
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
            Directory.CreateDirectory(tempDir);
        }

        private async void StartDownloadsAsync()
        {
            try
            {
                var download1 = Download("https://www.kaggle.com/api/v1/datasets/download/arpitsinghaiml/most-dangerous-countries-for-women-2024", "danger.zip", "./temp");
                var download2 = Download("https://www.kaggle.com/api/v1/datasets/download/ppb00x/country-gdp", "gpd.zip", "./temp");

                await Task.WhenAll(download1, download2);
                CSVHandler.ToJson();

                LoadCountries();
                this.DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while downloading or processing files: {ex.Message}");
            }
        }

        private async Task Download(string sUrl, string sDPath, string sEPath)
        {
            try
            {
                await _zipHandler.DownloadExtractZip(sUrl, sDPath, sEPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading file from {sUrl}: {ex.Message}");
            }
        }

        private void LoadCountries()
        {
            try
            {
                string dataPath = "./temp/data.json";
                string jsonData = File.ReadAllText(dataPath);

                Countries = JsonConvert.DeserializeObject<List<CountryInfo>>(jsonData);

                if (Countries != null)
                {
                    foreach (var country in Countries)
                    {
                        country.IsSelected = false;
                    }

                    Debug.WriteLine($"{Countries.Count} countries");
                    Dispatcher.Invoke(() =>
                    {
                        countrySelect.ItemsSource = Countries;
                    });
                }
                else
                {
                    MessageBox.Show("Failed to load country data.");
                }
            }
            catch (IOException ioEx)
            {
                Debug.WriteLine($"File error: {ioEx.Message}");
                MessageBox.Show($"Error reading data: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error: {ex.Message}");
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        private void DisableSelection(object sender, SelectionChangedEventArgs e)
        {
            countrySelect.SelectedItem = null;
            countrySelect.IsDropDownOpen = true;
        }

        private void ExitApp(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void OpenGithub(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void SelectType(object sender, RoutedEventArgs e)
        {
            MenuItem clickedItem = sender as MenuItem;
            if (clickedItem != null)
            {
                clickedItem.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00ffff"));
                string headerValue = clickedItem.Header?.ToString();

                foreach (MenuItem item in FindSibblings<MenuItem>(this))
                {
                    if (item != clickedItem)
                    {
                        item.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#666666"));
                    }
                }

                if (headerValue != null)
                {
                    MessageBox.Show(headerValue);
                }
            }
        }

        private static IEnumerable<T> FindSibblings<T>(DependencyObject depObj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T)
                {
                    yield return (T)child;
                }

                foreach (T childOfChild in FindSibblings<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }
    }
}
