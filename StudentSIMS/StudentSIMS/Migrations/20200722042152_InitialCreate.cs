using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentSIMS.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    studentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(maxLength: 100, nullable: false),
                    middleName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: false),
                    emailAddress = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<int>(nullable: false),
                    timeCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.studentId);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    addressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentID = table.Column<int>(nullable: false),
                    streetNumber = table.Column<string>(maxLength: 100, nullable: false),
                    street = table.Column<string>(maxLength: 100, nullable: false),
                    suburb = table.Column<string>(maxLength: 100, nullable: false),
                    city = table.Column<string>(maxLength: 100, nullable: false),
                    postcode = table.Column<int>(nullable: false),
                    country = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.addressID);
                    table.ForeignKey(
                        name: "FK_Address_Student_studentID",
                        column: x => x.studentID,
                        principalTable: "Student",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_studentID",
                table: "Address",
                column: "studentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
