using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplicationGUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceReference1.Station[] stations;

        public MainWindow()
        {
            InitializeComponent();
            modifyVisibility(Visibility.Hidden);
        }

        public void modifyVisibility(Visibility visibility)
        {
            Station.Visibility = visibility;
            StationName.Visibility = visibility;
            SearchStation.Visibility = visibility;
        }

        private void Search_City(object sender, RoutedEventArgs e)
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            stations = service.GetVelibOfCity(Name.Text);
            if (stations.Length == 0)
            {

                stations[0] = errorStationSearch();
            }
            StationView.ItemsSource = stations;
            modifyVisibility(Visibility.Visible);
        }

        private void Search_Station(object sender, RoutedEventArgs e)
        {
            if (stations != null)
            {
                List<ServiceReference1.Station> foundStations = new List<ServiceReference1.Station>();
                String search = StationName.Text;
                foreach (ServiceReference1.Station station in stations)
                {
                    if (station.name.ToLower().Contains(search))
                    {
                        foundStations.Add(station);
                    }
                }
                if (foundStations.Count == 0)
                {
                    
                    foundStations.Add(errorStationSearch());
                }
                StationView.ItemsSource = foundStations;
            }
        }

        private ServiceReference1.Station errorStationSearch()
        {
            ServiceReference1.Station emptyStation = stations.Last();
            emptyStation.name = "No station found for your search";
            emptyStation.status = "";
            emptyStation.available_bike_stands = 0;
            emptyStation.available_bikes = 0;
            return emptyStation;
        }
    }
}
