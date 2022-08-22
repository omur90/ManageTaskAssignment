using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageTaskAssignment.Assignment.Api.Migrations
{
    public partial class Initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "workOrder");

            migrationBuilder.CreateTable(
                name: "tblWorkOrders",
                schema: "workOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00be01a1-5cd2-49fc-8bb1-c2f967ae8c85")),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWorkOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblWorkOrderDetails",
                schema: "workOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("8452ac27-3902-4d6d-80a7-1bf7dd9bfaaa")),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DetailsOfTask = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWorkOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWorkOrderDetails_tblWorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalSchema: "workOrder",
                        principalTable: "tblWorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblWorkOrderDetails_WorkOrderId",
                schema: "workOrder",
                table: "tblWorkOrderDetails",
                column: "WorkOrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblWorkOrderDetails",
                schema: "workOrder");

            migrationBuilder.DropTable(
                name: "tblWorkOrders",
                schema: "workOrder");
        }
    }
}
