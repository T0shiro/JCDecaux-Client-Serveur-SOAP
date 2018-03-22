# Velib Gateway Web Service

An intermediary Web service (IWS) between the Velib WS and some WS client

## Project structure

This project have two different parts :
- An **Intermediary Web service** exposing a WS-SOAP API to access to the Velib Web service from [**JCDecaux**](https://developer.jcdecaux.com/#/login?page=getstarted)  
You can find that part in the `WcfApplicationVelib` directory.
- A **client with a UI** connected to the IWS which request and display information about JCDecaux Velibs.  
You can find that part in the `WpfApplicationGUI` directory.
- A **console client** connected to the IWS which request and display information about JCDecaux Velibs.  
You can find that part in the `ConsoleApplication` directory.

## Resource files

This system has 2 resource files :
- **api_key.txt** : contains the key to use the JCDecaux API
- **cache_refresh_time.txt** : contains the duration of validity for the data in the cache, in seconds

*Note :* More information about the cache in *Details on the cache extension* below

⚠ **To use my solution, you have to create this 2 files in the `WcfApplicationVelib` directory.** ⚠  
To do that, you need your own JCDecaux API key.

### Get an API key for JCDecaux
In order to use the **JCDecaux API**, you need to have an API key delivered after subscription on [**their website**](https://developer.jcdecaux.com/#/signup).

## Available commands for the client

For the console client, here are the available commands :
- **contracts** : List all the contracts of JCDecaux
- **stations** : Ask for the name of the contract and list all the stations for this contract
- **exit** : Exit the JCDecaux Console client
- **any other command** : Give the help with all the commands 

## Extensions

### Development

- [X] **Graphical User Interface for the client**
- [X] **Asynchronous accesses to the WS**
- [X] **Cache in the IWS**

### Monitoring

- [ ] Not Done

### Deployment

- [ ] Not Done

## Details on the UI extension

When the user launch the system, first he has to choose his contract, so his city in the scroll menu and click on **OK** button.
The button is blocked during the request made to JCDecaux API.
When the request is finished, the stations are displayed and a new field appears at the bottom of the UI.
A search bar appears to simplify the research of a specific station.

## Details on the cache extension

The cache stores each request done by the user.
The limit time for the validity of the data is stored in the resource file `WcfApplicationVelib/cache_refresh_time.txt`.
Let's say, for the rest of this part, that you have set in your resource file the limit time to 5 minutes (300 seconds).
Each time the user asks for a specific contract, in the server side, the system checks in the cache if the request has already be done before.
Two cases here :
- The user has never asked for this contract, so the cache doesn't have informations about it.
The system makes the request to the JCDecaux API.
- The user has already searched for this contract. Two possibilities also here :
	- The previous research has been done **less** than 5 minutes before the actual one.
	The cache gives the data without making a new request.
	- The previous research has been done **more** than 5 minutes ago.
	The cache deletes the outdated data and makes a new request to JCDecaux API.

To do that, the system has a class `ContractInformations` that contains :
- the list of stations of the contract
- a `timestamp` parameter to store when the request has been done
- a method ``isInformationsTimeValid()`` that checks the validity of the information in the cache thanks to the timestamp
 