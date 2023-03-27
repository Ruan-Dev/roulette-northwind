# SQL and C# Assessment + Solution
In this repository you will find my solution to the below assessment. 
Follow the instructions on this readme to follow along. 

### Contents
[Prerequisite](#prerequisite)<br/>
[SQL Question](#sql-question)<br/>
[SQL Solution](#sql-solution)<br/>
[C# Question](#csharp-question)<br/>
[C# Solution](#csharp-solution)<br/>


<a name=prerequisite></a>
### Prerequisite
- [Visual Studio](https://visualstudio.microsoft.com/downloads/)
- [SQL Server](https://www.microsoft.com/en-us/evalcenter/evaluate-sql-server-2019)
- [Microsoft Northwind Sample Database]()

---

<a name=sql-question></a>
## SQL Question

Write a SQL Stored Procedure (pr_GetOrderSummary) to return a summary of orders from the data in the Northwind database. You may NOT use Dymanic SQL to solve the problem.

#### The results should be able to be filtered by specifying parameters:
- Date of the Order (@StartDate and @EndDate)
- Nullable Parameter to filter for a specific Employee (@EmployeeID)
- Nullable Parameter to filter for a specific Customer (@CustomerID)

#### The columns to be returned are:
- EmployeeFullName (TitleOfCourtesy + FirstName + LastName)
- Shipper CompanyName
- Customer CompanyName
- NumberOfOders
- Date
- TotalFreightCost
- NumberOfDifferentProducts
- TotalOrderValue

#### The results should be grouped by:
- Order Day (i.e. grouped by day)
- Employee 
- Customer
- Shipper

#### Some helpful tests:
- exec pr_GetOrderSummary @StartDate='1 Jan 1996', @EndDate='31 Aug 1996', @EmployeeID=NULL , @CustomerID=NULL
- exec pr_GetOrderSummary @StartDate='1 Jan 1996', @EndDate='31 Aug 1996', @EmployeeID=5 , @CustomerID=NULL
- exec pr_GetOrderSummary @StartDate='1 Jan 1996', @EndDate='31 Aug 1996', @EmployeeID=NULL , @CustomerID='VINET'
- exec pr_GetOrderSummary @StartDate='1 Jan 1996', @EndDate='31 Aug 1996', @EmployeeID=5 , @CustomerID='VINET'

---

<a name=sql-solution></a>
## SQL Solution

#### Getting Started
In order to run the solution, you will require the Northwind sample database for SQL Server. 
Before you can use the Northwind database, you have to run the <span style="background-color: #e6e6e6">'instnwnd.sql'</span> script file, found in the SQL directory, to recreate the database on an instance of SQL Server by using [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) or a similar tool. 

#### Executing the script
The script to create the stored procedure is called <span style="background-color: #e6e6e6">'pr_GetOrderSummary.sql'</span> and can be found in the SQL directory. 
1. Open Microsoft [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16).
2. Connect to the Northwind Database.
3. Open a new query window by clicking on "New Query" in the toolbar.
4. Copy and paste the entire script for the stored procedure into the query window.
5. Click on "Execute" in the toolbar to execute the script and create the stored procedure.
6. Use the above mentioned tests to execute the procedure, or create your own using the same format. 

---

<a name=csharp-question></a>
## C# Question
1. Create a C# REST API based around the game of roulette. Simple functions will need to be created, for example:
	- PlaceBet
	- Spin
	- Payout
	- ShowPreviousSpins
2. Save the PlaceBet data to a SQLite DB.

![image info](https://i.ibb.co/2Yvj1Th/roulette.jpg)
#### Important Notes:
1. The requests should not be thread blocking
2. The response to the requests needs to be a JSON body
3. Implementation should follow the SOLID principals.
4. Implementations should be covered by Unit Tests
5. Implement global exception handling.
6. Your implementation should cover at least one Design Pattern.
7. You are not necessarily expected to complete the entire assessment, rather to demonstrate your approach to coding an API/stored procedure, the structure of code, etc

---

<a name=csharp-solution></a>
## C# Solution
This is a .NET 6 Web API for a simple roulette game. The game allows users to place bets on a number between 0 and 36, or on other betting options such as red or black, odd or even, and groups of numbers.

#### Dependencies
This project has the following dependencies:
- .NET 6
- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools
- Swashbuckle.AspNetCore
- Newtonsoft.Json

#### API Endpoints
The API has the following endpoints:
- POST /bet : Place a bet
- POST /spin : Spin the roulette wheel
- GET /payout/{spinNumber} : Calculate the payout for a given spin
- GET /spins : Retrieve a list of previous spins


#### Getting Started
To get started with the project, follow the steps below:

- Install the latest version of Visual Studio on your computer.
- Install .NET 6 SDK
- Clone the solution from the C# directory in this repository.
- Open the solution file (.sln) in Visual Studio.
- Open the Package Manager Console by going to Tools > NuGet Package Manager > Package Manager Console.
- Run the following command to create the database schema: Update-Database
- Build the solution by going to Build > Build Solution.
- Start the application by running it in debug mode by pressing F5 or by clicking on the Start button in the toolbar.

#### Please Note: 
Make sure to update the connection string in the 'appsettings.json' file to point to the correct database location on your computer.

