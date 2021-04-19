using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JurisTempus.Data.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address_Address1 = table.Column<string>(nullable: true),
                    Address_Address2 = table.Column<string>(nullable: true),
                    Address_Address3 = table.Column<string>(nullable: true),
                    Address_CityTown = table.Column<string>(nullable: true),
                    Address_StateProvince = table.Column<string>(nullable: true),
                    Address_PostalCode = table.Column<string>(nullable: true),
                    Address_Country = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    GovernmentId = table.Column<string>(nullable: true),
                    BillingRate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    PublicComments = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeBills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkDate = table.Column<DateTime>(nullable: false),
                    TimeSegments = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    WorkDescription = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    CaseId = table.Column<int>(nullable: true),
                    InvoiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeBills_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeBills_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeBills_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Contact", "Name", "Phone", "Address_Address1", "Address_Address2", "Address_Address3", "Address_CityTown", "Address_Country", "Address_PostalCode", "Address_StateProvince" },
                values: new object[] { 1, "Frank Sloan", "Sloan Taxis", "555-555-1212", "123 Main Street", null, null, "Atlanta", null, "12345", "GA" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BillingRate", "FirstName", "GovernmentId", "LastName", "Role" },
                values: new object[] { 1, 45m, "Shawn", "1234567890", "Wildermuth", "Paralegal" });

            migrationBuilder.InsertData(
                table: "Cases",
                columns: new[] { "Id", "ClientId", "FileNumber", "Status" },
                values: new object[] { 1, 1, "ATL12394872", 1 });

            migrationBuilder.InsertData(
                table: "TimeBills",
                columns: new[] { "Id", "CaseId", "EmployeeId", "InvoiceId", "Rate", "TimeSegments", "WorkDate", "WorkDescription" },
                values: new object[] { 1, 1, 1, null, 175.00m, 5, new DateTime(2021, 4, 19, 0, 0, 0, 0, DateTimeKind.Local), "Entered data for the client" });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ClientId",
                table: "Cases",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientId",
                table: "Invoices",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeBills_CaseId",
                table: "TimeBills",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeBills_EmployeeId",
                table: "TimeBills",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeBills_InvoiceId",
                table: "TimeBills",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeBills");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
