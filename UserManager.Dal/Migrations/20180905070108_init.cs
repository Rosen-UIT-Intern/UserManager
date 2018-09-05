using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManager.Dal.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    ProfileImage = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    WorkPhone = table.Column<string>(nullable: false),
                    PrivatePhone = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("c00af6d2-5c26-44cc-8414-dbb420d0f942"), "Rosen" },
                    { new Guid("a7bd1b7b-1110-4c6c-9fd6-f47a9cc7fbda"), "UIT" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("fa83781c-c13e-4b2a-a13b-cc557cfba720"), "Technical Lead" },
                    { new Guid("77817bb6-2a22-4635-8dda-b820356ed8f9"), "HR Lead" },
                    { new Guid("d1eb257f-9a58-4751-8a6d-a1f0ed91b3ba"), "Engineer" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name", "OrganizationId" },
                values: new object[,]
                {
                    { new Guid("3777ec35-2393-4053-95ad-cc587d87a3e3"), "Technical", new Guid("c00af6d2-5c26-44cc-8414-dbb420d0f942") },
                    { new Guid("ab2ace08-2daf-4422-9242-293025aab9f6"), "HR", new Guid("c00af6d2-5c26-44cc-8414-dbb420d0f942") },
                    { new Guid("abba6119-b935-4870-9c06-be6b8872fb32"), "SoftwareEngineer", new Guid("a7bd1b7b-1110-4c6c-9fd6-f47a9cc7fbda") },
                    { new Guid("f90317a4-a87c-4800-8d24-8e7c5e84073e"), "ComputerEngineer", new Guid("a7bd1b7b-1110-4c6c-9fd6-f47a9cc7fbda") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Mobile", "OrganizationId", "PrivatePhone", "ProfileImage", "WorkPhone" },
                values: new object[] { "12345", "[{\"address\":\"main email\",\"isMain\":true},{\"address\":\"not mail email\",\"isMain\":false}]", "Minh", "Nguyen Le", "[{\"number\":\"333444\",\"isMain\":true},{\"number\":\"555666\",\"isMain\":false}]", new Guid("c00af6d2-5c26-44cc-8414-dbb420d0f942"), "[{\"number\":\"91011\",\"isMain\":true},{\"number\":\"121314\",\"isMain\":false}]", "image", "[{\"number\":\"1234\",\"isMain\":true},{\"number\":\"5678\",\"isMain\":false}]" });

            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "UserId", "GroupId", "IsMain" },
                values: new object[] { "12345", new Guid("3777ec35-2393-4053-95ad-cc587d87a3e3"), true });

            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "UserId", "GroupId", "IsMain" },
                values: new object[] { "12345", new Guid("ab2ace08-2daf-4422-9242-293025aab9f6"), false });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId", "IsMain" },
                values: new object[] { "12345", new Guid("d1eb257f-9a58-4751-8a6d-a1f0ed91b3ba"), true });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_OrganizationId",
                table: "Groups",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupId",
                table: "UserGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
