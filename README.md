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