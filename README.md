# Currency Exchange API
This is a simple Web API to process requests regarding currency values (exchange rate value for a specific date, maximum and minimal exchange rate value from <i>n</i> last quotations, major difference between ask and bid exchange values from <i>n</i> last quotations).
The application queries data from [NBP Web API](http://api.nbp.pl/) to return relevant information.
<br><br>
To start the application:
- Make sure you have [.NET](https://dotnet.microsoft.com/en-us/download/dotnet) (version 7 or higher) and [Git](https://git-scm.com/downloads) installed
- Open the terminal
- Clone this repository <code>git clone https://github.com/pati-g/currency-exchange-api.git</code>
- Navigate to the folder CurrencyExchangeAPI

- Run this command: <code>dotnet run</code>
- Open the web browser and enter this URL to go to Swagger UI: http://localhost:5051/swagger/index.html

OR

If you're using [Docker](https://docs.docker.com/get-docker/) and want to run the application in a container:
- Run the command <code>docker pull patig/currencyexchangeapi:latest</code> to pull the latest Docker image from Docker Hub
- Run the application: <code>docker run -dp 8080:80 currency-exchange-api</code>
- Open your web browser and enter this URL: http://localhost:8080/swagger/ to open Swagger UI
