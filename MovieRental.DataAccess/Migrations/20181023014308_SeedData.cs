using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieRental.DataAccess.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1646a245-9af3-484c-98b8-cf766394a824", "0417f073-17de-47c8-a4c6-862e43054b71", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8543b860-d901-49e3-bd9f-e2642891f8e4", "5f8ecac8-f9e6-42fe-b5dc-6792938cd289", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1646a245-9af3-484c-98b8-cf766394a824", "0417f073-17de-47c8-a4c6-862e43054b71" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "8543b860-d901-49e3-bd9f-e2642891f8e4", "5f8ecac8-f9e6-42fe-b5dc-6792938cd289" });
        }
    }
}
