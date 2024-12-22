# Mortgage Pricing Service

Mortgage Pricing Service is a robust ASP.NET Core web application that provides functionality for calculating, publishing, and processing mortgage pricing updates. The project uses **Azure Service Bus** for messaging with publisher and subscriber patterns to handle mortgage pricing updates efficiently.

## Features
- **Mortgage Pricing Calculation**: Calculates mortgage pricing based on user inputs.
- **Azure Service Bus Integration**: Publishes and subscribes to mortgage pricing updates using Azure Service Bus.
- **Swagger Integration**: API documentation available at `/swagger`.
- **Scalable Architecture**: Designed for scalability and maintainability with a clean separation of concerns.

## Technology Stack
- **Backend**: ASP.NET Core 7.0
- **Messaging**: Azure Service Bus (Topics & Subscriptions)
- **Build Tool**: .NET CLI
- **Deployment**: Azure App Service
- **Development Tools**: Visual Studio Code, Docker (optional)

## Prerequisites
1. **.NET SDK**: Install [.NET 7.0 SDK](https://dotnet.microsoft.com/download).
2. **Azure Service Bus**: Set up an Azure Service Bus instance.
3. **Azure App Service**: Create an App Service for deployment (optional).

## Getting Started
### Clone the Repository
```bash
git clone https://github.com/your-repository/mortgage-pricing-service.git
cd mortgage-pricing-service
```
### Set Up Azure Service Bus
1. Create an Azure Service Bus namespace.
2. Create a **Topic**: `mortgage-pricing-updates`.
3. Add Subscriptions:
   - `Notifications`
   - `Audit`
4. Retrieve the connection string for your Azure Service Bus and add it to your environment variables:
   ```bash
   export ServiceBusConnectionString="YourAzureServiceBusConnectionString"
   ```

### Run the Application
1. Restore dependencies:
   ```bash
   dotnet restore
   ```
2. Build the application:
   ```bash
   dotnet build
   ```
3. Run the application:
   ```bash
   dotnet run
   ```

Access the application at:
- **Swagger**: [http://localhost:5000/swagger](http://localhost:5000/swagger)

## Deployment
### Deploy to Azure App Service
1. Publish the application:
   ```bash
   dotnet publish -c Release -o ./publish
   ```
2. Deploy using Azure CLI:
   ```bash
   az webapp deployment source config-zip        --resource-group <YourResourceGroupName>        --name <YourAppServiceName>        --src ./publish.zip
   ```
