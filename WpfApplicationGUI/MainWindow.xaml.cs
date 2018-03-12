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
        ServiceJCDecauxReference.Station[] stations;

        // Cache to avoid multiple requests on the JCDecaux API for the same information
        Dictionary<string, ContractInformations> cacheForStations;

        public MainWindow()
        {
            InitializeComponent();
            modifyVisibility(Visibility.Hidden);
            cacheForStations = new Dictionary<string, ContractInformations>();
        }

        // Hide or show the search bar for the station
        // When the user has entered a valid contract, the stations are displayed and the search bar appeared
        // It disappeared when the contract search found nothing
        public void modifyVisibility(Visibility visibility)
        {
            Station.Visibility = visibility;
            StationName.Visibility = visibility;
            SearchStation.Visibility = visibility;
        }

        // Method used when the user search a contract in the search bar
        // It calls the method for the request if necessary and display the informations found
        private void Search_City(object sender, RoutedEventArgs e)
        {
            // If the information searched is not in the cache OR if the information is in the cache but is outdated
            // The system makes a new request and refresh the cache
            if (!cacheForStations.ContainsKey(Name.Text.ToLower()) || !cacheForStations[Name.Text.ToLower()].isInformationsTimeValid())
            {
                cacheForStations.Remove(Name.Text.ToLower());
                getInformationsFromJCDecaux();
            } else
            {
                stations = cacheForStations[Name.Text.ToLower()].GetStations();
            }
            StationView.ItemsSource = stations;
        }

        // Method to create and use the object designed to make the request to the JCDecaux API
        // It also stores the new informations in th cache with the timestamp
        private void getInformationsFromJCDecaux()
        {
            ServiceJCDecauxReference.Service1Client service = new ServiceJCDecauxReference.Service1Client();
            stations = service.GetStationsOfContractNamed(Name.Text);
            cacheForStations.Add(Name.Text.ToLower(), new ContractInformations(stations, DateTime.Now));
            if (stations.Length != 0)
            {
                modifyVisibility(Visibility.Visible);
            }
            else
            {
                modifyVisibility(Visibility.Hidden);
            }
        }

        // Method used to search a specific station with the station search bar
        // The bar appeared when it is necessary 
        // This method allows to find every station in the actual contract containing the name typed by the user 
        private void Search_Station(object sender, RoutedEventArgs e)
        {
            if (stations != null)
            {
                List<ServiceJCDecauxReference.Station> foundStations = new List<ServiceJCDecauxReference.Station>();
                String search = StationName.Text;
                foreach (ServiceJCDecauxReference.Station station in stations)
                {
                    if (station.name.ToLower().Contains(search))
                    {
                        foundStations.Add(station);
                    }
                }
                StationView.ItemsSource = foundStations;
            }
        }
    }
}
