# Velib Gateway Web Service

An intermediary Web service (IWS) between the Velib WS and some WS client

## Project structure

This project have two different parts :
- An **Intermediary Web service** exposing a WS-SOAP API to access to the Velib Web service from [**JCDecaux**](https://developer.jcdecaux.com/#/login?page=getstarted)
You can find that part in the `WcfApplicationVelib` directory.

- A **client with a UI** connected to the IWS which request and display the informations about JCDecaux Velibs.
You can find that part in the `WpfApplicationGUI` directory.

## Extensions

### Development

- [X] **Graphical User Interface for the client**
- [X] **Asynchronous accesses to the WS**
- [X] **Cache in the IWS**

### Monitoring

- [ ] Not Done

### Deployment

- [ ] Not Done

## Details on the cache extension

The cache stores each request done by the user.
The limit time for the validity of the data is 5 minutes.
Each time the user asks for a specific contract, the system checks in the cache if the request has already be done before.
Two cases here : 
- The user has never asked for this contract, so the cache doesn't have informations about it.
The system makes the request to the JCDecaux API.
- The user has already searched for this station. Two possibilities also here :
	- The previous research has been done **less** than 5 minutes before the actual one.
	The cache gives the data without making a new request.
	- The previous research has been done **more** than 5 minutes ago.
	The cache deletes the outdated data and makes a new request to JCDecaux API.

To do that, the system have a class `ContractInformations` that contains :
- the list of stations of the contract
- a `timestamp` parameter to store when the request has been done
- a method ``isInformationsTimeValid()`` that checks the difference between the actual time and the timestamp

 