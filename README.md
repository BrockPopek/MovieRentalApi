# MovieRentalApi
An Api for Movie Rentals

This Api allows for creating users in the Api either in a Manager or Customer role.  You then can create customers that can rent out movies.  Only users with a Manager role can add, edit, and delete the movie catalog.

## Database Setup
To initialize your database, please update connection string in the appsettings.json files in the MovieRentalApi and MovieRental.DataAccess.  Then run the following command in the Package Manager Console in Visual Studio:

`Update-Database -Project MovieRental.DataAccess -Context ApplicationUserDbContext`

## Running the Api

To startup the solution, just press F5, or debug.  This will open a Swagger page at http://localhost:44389/swagger.  The Swagger page will then allow you to view the available Api calls.  Then go to the Register call and add a user.  When the user is added, copy the returned token string.  Then press the Authorize button at the top and enter the following in the text box:

`Bearer CopiedTokenString`

Then log in, and you should be able to get access to the Api calls that your newly created user's role was set to.  The Swagger page documents the Api calls that can be used for the service.  To start renting out movies, a customer for the user has to be created along with movies.

## Architecture
The architecture of the Api follows Unit of Work and Repository patterns.  The soluton is done in .NET Core using Asp.Net Core and Entity Framework Core.  It uses JWT Bearer tokens to do the authentication.

Each project does the following:

### MovieRentalApi
The main Api for the solutions.  This is the project that opens with a Swagger page to document and run the Api calls.

### MovieRental.Contract
The project that handles all of the interfaces for Data Access and Services.  This project also handles the contract models that are used for transport.

### MovieRental.Service
The service or Unit Of Work level.  This handles all of the Business logic.  The Api controllers calls to this to get the needed data.

### MovieRental.DataAccess
The Data Access or Repository level.  This handles of all of the data access to a SQL Server database.  It uses Entity Framework Core for the Object Relationship Mapping (ORM).  

The Data Access and Service layers are abstracted away to allow for different implementations if need be.



