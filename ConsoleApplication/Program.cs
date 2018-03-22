using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        // Distant service of the server to make the requests to the JCDecaux API 
        private static ServiceJCDecauxReference.Service1Client service = new ServiceJCDecauxReference.Service1Client();

        static void Main(string[] args)
        {

            string command = "";
            Console.WriteLine("JCDecaux Console Client Application");
            while (!command.Equals("exit"))
            {
                Console.Write("\nJCDecaux > ");
                command = Console.ReadLine();
                switch(command)
                {
                    case "stations":
                        Console.Write("Contract name ? ");
                        string contract = Console.ReadLine();
                        GetStationsOf(contract);
                        break;
                    case "contracts":
                        GetContracts();
                        break;
                    default:
                        List<string> commands = GetCommands();
                        foreach(string commandInfo in commands)
                        {
                            Console.WriteLine($"    - {commandInfo}");
                        }
                        break;
                }
            }
        }

        private static List<string> GetCommands()
        {
            List<string> commands = new List<string>();
            commands.Add("contracts : List all the contracts of JCDecaux");
            commands.Add("stations : Ask for the name of the contract and list all the stations for this contract");
            commands.Add("exit : Exit the JCDecaux Console client");
            commands.Add("any other command : Give the help with all the commands");
            return commands;
        }

        private static void GetContracts()
        {
            List<string> commands = service.GetContracts().ToList();
            commands.Sort();
            foreach (string command in commands)
            {
                Console.WriteLine($"    - {command}");
            }
        }

        private static void GetStationsOf(string contractName)
        {
            ServiceJCDecauxReference.Station[] stations = service.GetStationsOfContractNamed(contractName.ToLower());
            if (stations.Length != 0)
            {
                foreach (ServiceJCDecauxReference.Station station in stations)
                {
                    Console.WriteLine($@"{station.name} :
   - {station.available_bikes} available Velibs
   - {station.available_bike_stands} available bike stands");
                }
            } else
            {
                Console.WriteLine($"No station found for the contract named {contractName}.");
            }
            
        }
    }
}
