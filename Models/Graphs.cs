using Newtonsoft.Json;
using ProjektWdrozeniowy;
using ScottPlot.WPF;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Globalization;
using ScottPlot;


namespace ProjektWdrozeniowy
{
    public class Graphs
    {
        private MainWindow _mainWindow;
        public WpfPlot WpfPlot1;

        public Graphs(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            WpfPlot1 = new WpfPlot();
        }

        public List<CountryInfo>? Countries { get; set; }

        public void CreateImgGraph()
        {
            try
            {
                int index = 1;
                List<Tick> ticks = new List<Tick>();

                GetCountries();
                //var countryNames = new List<string>();
                //var gdp = new List<double>();

                foreach (var country in Countries)
                {
                   
                    ticks.Add(new Tick(index, country.Country));

                    // Parse IMF_GDP with InvariantCulture
                    if (!string.IsNullOrWhiteSpace(country.IMF_GDP) &&
                        double.TryParse(country.IMF_GDP, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedGdp))
                    {
                        WpfPlot1.Plot.Add.Bar(index, parsedGdp);

                    }
                    else
                    {
                        Debug.WriteLine($"Invalid IMF_GDP for {country.Country}: {country.IMF_GDP}");
                    }
                    index++;
                }
                Tick[] tickArray = ticks.ToArray();
                WpfPlot1.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(tickArray);
                WpfPlot1.Plot.HideGrid();
                WpfPlot1.Plot.Axes.Margins(bottom: 0);
                WpfPlot1.Refresh();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                MessageBox.Show($"An error occurred: {ex.Message}");
            }


        }

        public void GetCountries()
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

                    Debug.WriteLine($"{Countries.Count} countries loaded.");
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
    }
}
