using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Coordinator.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("15a0d1f1-91c9-41a2-badd-15f2b3bedc42"));

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("65b8f0e0-1855-48a8-9d51-696198a730dc"));

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("c699994c-902a-4c13-a53e-523502350d6c"));

            migrationBuilder.RenameColumn(
                name: "ReadyType",
                table: "NodeStates",
                newName: "IsReady");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "NodeStates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Nodes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("8b0ca06a-232a-4bdc-90fc-05c2a8da2001"), "Payment.API" },
                    { new Guid("d6fd4394-b244-45fb-99af-300212565c9a"), "Order.API" },
                    { new Guid("f56ddf6e-0d63-4b05-aa77-44665f21b3f9"), "Stock.API" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("8b0ca06a-232a-4bdc-90fc-05c2a8da2001"));

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("d6fd4394-b244-45fb-99af-300212565c9a"));

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("f56ddf6e-0d63-4b05-aa77-44665f21b3f9"));

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "NodeStates");

            migrationBuilder.RenameColumn(
                name: "IsReady",
                table: "NodeStates",
                newName: "ReadyType");

            migrationBuilder.InsertData(
                table: "Nodes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("15a0d1f1-91c9-41a2-badd-15f2b3bedc42"), "Order.API" },
                    { new Guid("65b8f0e0-1855-48a8-9d51-696198a730dc"), "Payment.API" },
                    { new Guid("c699994c-902a-4c13-a53e-523502350d6c"), "Stock.API" }
                });
        }
    }
}
