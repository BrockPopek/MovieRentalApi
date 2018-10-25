# MovieRentalApi
An Api for Movie Rentals

This Api allows for creating users in the Api either in a Manager or Customer role.  You then can create customers that can rent out movies.  Only users with a Manager role can add, edit, and delete the movie catalog.

To startup the solution, just press F5 or go and debug.  This will open a Swagger page at http://localhost:44389/swagger.  The Swagger page will then allow you to view the available Api calls.

The architecture of the Api follows Unit of Work and Repository patterns.  The soluton is done in .NET Core using Asp.Net Core and Entity Framework Core.  It uses JWT Bearer tokens to do the authentication.

Each project does the following:

MovieRentalApi - The main Api for the solutions.  This is the project that opens with a Swagger page to document and run the Api calls.

MovieRental.Contract - The project that handles all of the interfaces for Data Access and Services.  This project also handles the contract models that are used for transport.

MovieRental.Service - The service or Unit Of Work level.  This handles all of the Business logic.  The Api controllers calls to this to get the needed data.

MovieRental.DataAccess - The Data Access or Repository level.  This handles of all of the data access to a SQL Server database.  It uses Entity Framework Core.  

The Data Access and Service layers are abstracted away to allow for different implementations if need be.

## Database Setup
To initialize your database, please update connection string in the appsettings.json files in the MovieRentalApi and MovieRental.DataAccess.  Then run the following command:

Update-Database -Project MovieRental.DataAccess -Context ApplicationUserDbContext

