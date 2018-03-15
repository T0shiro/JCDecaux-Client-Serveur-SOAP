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
        // Collection of the actual stations displayed in the UI
        ServiceJCDecauxReference.Station[] stations;

        // Distant service of the server to make the requests to the JCDecaux API 
        ServiceJCDecauxReference.Service1Client service;

        public MainWindow()
        {
            InitializeComponent();
            service = new ServiceJCDecauxReference.Service1Client();
            modifyVisibility(Visibility.Hidden);
            setUpContracts();
        }

        // Method to get all contract's names from JCDecaux API, sort them in alphabetic order, and display them on the scroll box
        private async void setUpContracts()
        {
            List<string> tmp = (await service.GetContractsAsync()).ToList();
            tmp.Sort();
            Name.ItemsSource = tmp;
        }

        // Hide or show the search bar for the station
        // When the user has choosed a contract, the stations are displayed and the search bar appeared
        public void modifyVisibility(Visibility visibility)
        {
            Station.Visibility = visibility;
            StationName.Visibility = visibility;
            SearchStation.Visibility = visibility;
        }

        // Method used when the user selects a contract in the scroll menu and clicks on the Ok button
        private async void Search_City(object sender, RoutedEventArgs e)
        {
            Validate.IsEnabled = false;
            stations = await service.GetStationsOfContractNamedAsync(Name.Text);
            modifyVisibility(Visibility.Visible);
            StationView.ItemsSource = stations;
            Validate.IsEnabled = true;
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
