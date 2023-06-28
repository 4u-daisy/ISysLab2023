using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISysLab2023.Backend.WebApi.Migrations
{
    public partial class __init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Title = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: false),
                    SubdivisionCode = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Title = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: false),
                    ProjectCode = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BasePerson",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: true),
                    Surname = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: true),
                    Patronymic = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: true),
                    BirthDay = table.Column<DateOnly>(type: "date", nullable: true),
                    Email = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: true),
                    Phone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    Discriminator = table.Column<string>(type: "longtext", nullable: false),
                    JobTitle = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: true),
                    EmployeeCode = table.Column<int>(type: "int", nullable: true),
                    IdHeadManager = table.Column<string>(type: "longtext", nullable: true),
                    HeadManagerId = table.Column<string>(type: "varchar(255)", nullable: true),
                    IdDepartment = table.Column<string>(type: "varchar(255)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasePerson_BasePerson_HeadManagerId",
                        column: x => x.HeadManagerId,
                        principalTable: "BasePerson",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BasePerson_Departments_IdDepartment",
                        column: x => x.IdDepartment,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmployeeProjects",
                columns: table => new
                {
                    Key = table.Column<string>(type: "varchar(255)", nullable: false),
                    IdEmployee = table.Column<string>(type: "varchar(255)", nullable: false),
                    IdProject = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProjects", x => x.Key);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_BasePerson_IdEmployee",
                        column: x => x.IdEmployee,
                        principalTable: "BasePerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Projects_IdProject",
                        column: x => x.IdProject,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BasePerson_EmployeeCode",
                table: "BasePerson",
                column: "EmployeeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasePerson_HeadManagerId",
                table: "BasePerson",
                column: "HeadManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePerson_IdDepartment",
                table: "BasePerson",
                column: "IdDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_IdEmployee",
                table: "EmployeeProjects",
                column: "IdEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_IdProject",
                table: "EmployeeProjects",
                column: "IdProject");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeProjects");

            migrationBuilder.DropTable(
                name: "BasePerson");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
