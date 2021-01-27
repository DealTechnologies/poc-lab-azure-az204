# poc-lab-azure-az204
> Prova de conceito ambiente azure
- event grid topics
- azure storage
- function timmer trigger
- function http trigger
- function event grid trigger
- function event queue
- console storage queue pulling
- console event grid publisher

# Introduction 
A simple exemple using delivery order with azure events

# Getting Started
> Unzip and deploy lab with ARM template: ***../ExportedTemplate-rg-lab204.rar*** 

> Create free mongo db cluster on cloud or official image on docker hub

- [On Free Cloud Official](https://www.mongodb.com/cloud/atlas)
- [On Free Cloud Third Party](https://mlab.com/)
- [On Docker](https://hub.docker.com/_/mongo)

# Set Environment Variables
> Set variables into *Solution/Function/Properties/**local.settings.json***

- "TRANSACTIONTOPICENDPOINT": **"Set your topic event endpoint"**
- "TRANSACTIONTOPICKEY": **"XXX-XXXX-XXXX"**
- "STORAGECONNECTIONSTRING": **"Set your storage connection"**
- "STORAGEQUEUE": **"Set your storage queue name"**
- "STORAGECONNECTIONSTRING": **"XXX-XXXX-XXXX"**
- "OCPAPIMKEY": **"XXX-XXXX-XXXX"**
- "MongoDBAtlasConnectionString": **"Set your mongodb string connection"**

# Build 
run command: **func start host**

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)

## Project Diagram, Structure and Comments
[![BWO](blob:https://whimsical.com/9351fd75-05f5-4e5d-8f1c-0eb7525ed8e1)](https://whimsical.com/azure-lab-204x-9ePDg7aYs7cGxKfrBx1pba)
For more detail access [Whimsical Diagram Page](https://whimsical.com/azure-lab-204x-9ePDg7aYs7cGxKfrBx1pba) to see the comments in each node.

# More

- [Azure Functions](https://azure.microsoft.com/en-us/services/functions/#documentation)
- [Dotnet](https://dotnet.microsoft.com/download)
- [Mongo DB](https://www.mongodb.com/)
- [EF.Core](https://docs.microsoft.com/pt-br/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
- [Docker](https://www.docker.com/)
