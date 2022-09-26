using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoProject.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppClaimTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppClaimTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppClaimTypes_Hist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppClaimTypes_Hist", x => new { x.Id, x.Ver });
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppRoleId = table.Column<int>(type: "int", nullable: false),
                    AppRoleVer = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims_Hist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppRoleId = table.Column<int>(type: "int", nullable: false),
                    AppRoleVer = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims_Hist", x => new { x.Id, x.Ver });
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles_Hist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles_Hist", x => new { x.Id, x.Ver });
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppUserVer = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Issuer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims_Hist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppUserVer = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Issuer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims_Hist", x => new { x.Id, x.Ver });
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppUserVer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins_Hist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppUserVer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins_Hist", x => new { x.Id, x.Ver });
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppUserVer = table.Column<int>(type: "int", nullable: false),
                    AppRoleId = table.Column<int>(type: "int", nullable: false),
                    AppRoleVer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles_Hist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppUserVer = table.Column<int>(type: "int", nullable: false),
                    AppRoleId = table.Column<int>(type: "int", nullable: false),
                    AppRoleVer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles_Hist", x => new { x.Id, x.Ver });
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers_Hist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers_Hist", x => new { x.Id, x.Ver });
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppUserVer = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens_Hist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppUserVer = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens_Hist", x => new { x.Id, x.Ver });
                });

            migrationBuilder.CreateTable(
                name: "ListSettingsForUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppUserVer = table.Column<int>(type: "int", nullable: false),
                    ListProgName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageNumber = table.Column<int>(type: "int", nullable: false),
                    PageSize = table.Column<int>(type: "int", nullable: false),
                    SortBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuickSearch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListSettingsForUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListSettingsForUsers_Hist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ver = table.Column<int>(type: "int", nullable: false),
                    who = table.Column<int>(type: "int", nullable: true),
                    whoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    what = table.Column<string>(type: "CHAR(1)", nullable: false),
                    when = table.Column<DateTime>(type: "datetime2", nullable: false),
                    when_str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppUserVer = table.Column<int>(type: "int", nullable: false),
                    ListProgName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageNumber = table.Column<int>(type: "int", nullable: false),
                    PageSize = table.Column<int>(type: "int", nullable: false),
                    SortBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuickSearch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListSettingsForUsers_Hist", x => new { x.Id, x.Ver });
                });

            migrationBuilder.InsertData(
                table: "AppClaimTypes",
                columns: new[] { "Id", "Description", "when", "when_str", "whoName", "who", "what", "Name", "Ver" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Users", 1 },
                    { 2, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Roles", 1 },
                    { 3, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Companies", 1 }
                });

            migrationBuilder.InsertData(
                table: "AppClaimTypes_Hist",
                columns: new[] { "Id", "Ver", "Description", "when", "when_str", "whoName", "who", "what", "Name" },
                values: new object[,]
                {
                    { 1, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Users" },
                    { 2, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Roles" },
                    { 3, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Companies" }
                });

            migrationBuilder.InsertData(
                table: "AppRoleClaims",
                columns: new[] { "Id", "AppRoleId", "AppRoleVer", "ClaimType", "ClaimValue", "when", "when_str", "whoName", "who", "what", "Ver" },
                values: new object[,]
                {
                    { 1, 1, 1, "Users", "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", 1 },
                    { 2, 1, 1, "Roles", "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", 1 },
                    { 3, 1, 1, "Companies", "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", 1 }
                });

            migrationBuilder.InsertData(
                table: "AppRoleClaims_Hist",
                columns: new[] { "Id", "Ver", "AppRoleId", "AppRoleVer", "ClaimType", "ClaimValue", "when", "when_str", "whoName", "who", "what" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, "Users", "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i" },
                    { 2, 1, 1, 1, "Roles", "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i" },
                    { 3, 1, 1, 1, "Companies", "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i" }
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions", "Ver" },
                values: new object[,]
                {
                    { 1, "Full sys admin", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Administrator", "ADMINISTRATOR", "Users, Roles, Companies", 1 },
                    { 2, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 002", "ROLE 002", "", 1 },
                    { 3, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 003", "ROLE 003", "", 1 },
                    { 4, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 004", "ROLE 004", "", 1 },
                    { 5, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 005", "ROLE 005", "", 1 },
                    { 6, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 006", "ROLE 006", "", 1 },
                    { 7, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 007", "ROLE 007", "", 1 },
                    { 8, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 008", "ROLE 008", "", 1 },
                    { 9, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 009", "ROLE 009", "", 1 },
                    { 10, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 010", "ROLE 010", "", 1 },
                    { 11, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 011", "ROLE 011", "", 1 },
                    { 12, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 012", "ROLE 012", "", 1 },
                    { 13, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 013", "ROLE 013", "", 1 },
                    { 14, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 014", "ROLE 014", "", 1 },
                    { 15, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 015", "ROLE 015", "", 1 },
                    { 16, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 016", "ROLE 016", "", 1 },
                    { 17, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 017", "ROLE 017", "", 1 },
                    { 18, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 018", "ROLE 018", "", 1 },
                    { 19, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 019", "ROLE 019", "", 1 },
                    { 20, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 020", "ROLE 020", "", 1 },
                    { 21, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 021", "ROLE 021", "", 1 },
                    { 22, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 022", "ROLE 022", "", 1 },
                    { 23, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 023", "ROLE 023", "", 1 },
                    { 24, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 024", "ROLE 024", "", 1 },
                    { 25, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 025", "ROLE 025", "", 1 },
                    { 26, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 026", "ROLE 026", "", 1 },
                    { 27, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 027", "ROLE 027", "", 1 },
                    { 28, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 028", "ROLE 028", "", 1 },
                    { 29, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 029", "ROLE 029", "", 1 },
                    { 30, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 030", "ROLE 030", "", 1 }
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions", "Ver" },
                values: new object[,]
                {
                    { 31, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 031", "ROLE 031", "", 1 },
                    { 32, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 032", "ROLE 032", "", 1 },
                    { 33, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 033", "ROLE 033", "", 1 },
                    { 34, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 034", "ROLE 034", "", 1 },
                    { 35, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 035", "ROLE 035", "", 1 },
                    { 36, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 036", "ROLE 036", "", 1 },
                    { 37, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 037", "ROLE 037", "", 1 },
                    { 38, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 038", "ROLE 038", "", 1 },
                    { 39, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 039", "ROLE 039", "", 1 },
                    { 40, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 040", "ROLE 040", "", 1 },
                    { 41, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 041", "ROLE 041", "", 1 },
                    { 42, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 042", "ROLE 042", "", 1 },
                    { 43, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 043", "ROLE 043", "", 1 },
                    { 44, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 044", "ROLE 044", "", 1 },
                    { 45, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 045", "ROLE 045", "", 1 },
                    { 46, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 046", "ROLE 046", "", 1 },
                    { 47, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 047", "ROLE 047", "", 1 },
                    { 48, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 048", "ROLE 048", "", 1 },
                    { 49, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 049", "ROLE 049", "", 1 },
                    { 50, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 050", "ROLE 050", "", 1 },
                    { 51, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 051", "ROLE 051", "", 1 },
                    { 52, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 052", "ROLE 052", "", 1 },
                    { 53, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 053", "ROLE 053", "", 1 },
                    { 54, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 054", "ROLE 054", "", 1 },
                    { 55, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 055", "ROLE 055", "", 1 },
                    { 56, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 056", "ROLE 056", "", 1 },
                    { 57, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 057", "ROLE 057", "", 1 },
                    { 58, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 058", "ROLE 058", "", 1 },
                    { 59, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 059", "ROLE 059", "", 1 },
                    { 60, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 060", "ROLE 060", "", 1 },
                    { 61, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 061", "ROLE 061", "", 1 },
                    { 62, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 062", "ROLE 062", "", 1 },
                    { 63, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 063", "ROLE 063", "", 1 },
                    { 64, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 064", "ROLE 064", "", 1 },
                    { 65, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 065", "ROLE 065", "", 1 },
                    { 66, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 066", "ROLE 066", "", 1 },
                    { 67, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 067", "ROLE 067", "", 1 },
                    { 68, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 068", "ROLE 068", "", 1 },
                    { 69, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 069", "ROLE 069", "", 1 },
                    { 70, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 070", "ROLE 070", "", 1 },
                    { 71, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 071", "ROLE 071", "", 1 },
                    { 72, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 072", "ROLE 072", "", 1 }
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions", "Ver" },
                values: new object[,]
                {
                    { 73, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 073", "ROLE 073", "", 1 },
                    { 74, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 074", "ROLE 074", "", 1 },
                    { 75, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 075", "ROLE 075", "", 1 },
                    { 76, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 076", "ROLE 076", "", 1 },
                    { 77, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 077", "ROLE 077", "", 1 },
                    { 78, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 078", "ROLE 078", "", 1 },
                    { 79, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 079", "ROLE 079", "", 1 },
                    { 80, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 080", "ROLE 080", "", 1 },
                    { 81, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 081", "ROLE 081", "", 1 },
                    { 82, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 082", "ROLE 082", "", 1 },
                    { 83, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 083", "ROLE 083", "", 1 },
                    { 84, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 084", "ROLE 084", "", 1 },
                    { 85, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 085", "ROLE 085", "", 1 },
                    { 86, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 086", "ROLE 086", "", 1 },
                    { 87, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 087", "ROLE 087", "", 1 },
                    { 88, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 088", "ROLE 088", "", 1 },
                    { 89, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 089", "ROLE 089", "", 1 },
                    { 90, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 090", "ROLE 090", "", 1 },
                    { 91, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 091", "ROLE 091", "", 1 },
                    { 92, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 092", "ROLE 092", "", 1 },
                    { 93, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 093", "ROLE 093", "", 1 },
                    { 94, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 094", "ROLE 094", "", 1 },
                    { 95, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 095", "ROLE 095", "", 1 },
                    { 96, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 096", "ROLE 096", "", 1 },
                    { 97, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 097", "ROLE 097", "", 1 },
                    { 98, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 098", "ROLE 098", "", 1 },
                    { 99, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 099", "ROLE 099", "", 1 },
                    { 100, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 100", "ROLE 100", "", 1 },
                    { 101, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 101", "ROLE 101", "", 1 },
                    { 102, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 102", "ROLE 102", "", 1 },
                    { 103, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 103", "ROLE 103", "", 1 },
                    { 104, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 104", "ROLE 104", "", 1 },
                    { 105, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 105", "ROLE 105", "", 1 },
                    { 106, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 106", "ROLE 106", "", 1 },
                    { 107, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 107", "ROLE 107", "", 1 },
                    { 108, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 108", "ROLE 108", "", 1 },
                    { 109, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 109", "ROLE 109", "", 1 },
                    { 110, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 110", "ROLE 110", "", 1 },
                    { 111, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 111", "ROLE 111", "", 1 },
                    { 112, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 112", "ROLE 112", "", 1 },
                    { 113, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 113", "ROLE 113", "", 1 },
                    { 114, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 114", "ROLE 114", "", 1 }
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions", "Ver" },
                values: new object[,]
                {
                    { 115, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 115", "ROLE 115", "", 1 },
                    { 116, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 116", "ROLE 116", "", 1 },
                    { 117, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 117", "ROLE 117", "", 1 },
                    { 118, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 118", "ROLE 118", "", 1 },
                    { 119, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 119", "ROLE 119", "", 1 },
                    { 120, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 120", "ROLE 120", "", 1 },
                    { 121, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 121", "ROLE 121", "", 1 },
                    { 122, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 122", "ROLE 122", "", 1 },
                    { 123, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 123", "ROLE 123", "", 1 },
                    { 124, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 124", "ROLE 124", "", 1 },
                    { 125, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 125", "ROLE 125", "", 1 },
                    { 126, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 126", "ROLE 126", "", 1 },
                    { 127, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 127", "ROLE 127", "", 1 },
                    { 128, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 128", "ROLE 128", "", 1 },
                    { 129, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 129", "ROLE 129", "", 1 },
                    { 130, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 130", "ROLE 130", "", 1 },
                    { 131, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 131", "ROLE 131", "", 1 },
                    { 132, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 132", "ROLE 132", "", 1 },
                    { 133, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 133", "ROLE 133", "", 1 },
                    { 134, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 134", "ROLE 134", "", 1 },
                    { 135, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 135", "ROLE 135", "", 1 },
                    { 136, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 136", "ROLE 136", "", 1 },
                    { 137, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 137", "ROLE 137", "", 1 },
                    { 138, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 138", "ROLE 138", "", 1 },
                    { 139, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 139", "ROLE 139", "", 1 },
                    { 140, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 140", "ROLE 140", "", 1 },
                    { 141, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 141", "ROLE 141", "", 1 },
                    { 142, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 142", "ROLE 142", "", 1 },
                    { 143, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 143", "ROLE 143", "", 1 },
                    { 144, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 144", "ROLE 144", "", 1 },
                    { 145, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 145", "ROLE 145", "", 1 },
                    { 146, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 146", "ROLE 146", "", 1 },
                    { 147, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 147", "ROLE 147", "", 1 },
                    { 148, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 148", "ROLE 148", "", 1 },
                    { 149, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 149", "ROLE 149", "", 1 },
                    { 150, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 150", "ROLE 150", "", 1 },
                    { 151, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 151", "ROLE 151", "", 1 },
                    { 152, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 152", "ROLE 152", "", 1 },
                    { 153, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 153", "ROLE 153", "", 1 },
                    { 154, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 154", "ROLE 154", "", 1 },
                    { 155, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 155", "ROLE 155", "", 1 },
                    { 156, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 156", "ROLE 156", "", 1 }
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions", "Ver" },
                values: new object[,]
                {
                    { 157, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 157", "ROLE 157", "", 1 },
                    { 158, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 158", "ROLE 158", "", 1 },
                    { 159, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 159", "ROLE 159", "", 1 },
                    { 160, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 160", "ROLE 160", "", 1 },
                    { 161, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 161", "ROLE 161", "", 1 },
                    { 162, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 162", "ROLE 162", "", 1 },
                    { 163, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 163", "ROLE 163", "", 1 },
                    { 164, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 164", "ROLE 164", "", 1 },
                    { 165, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 165", "ROLE 165", "", 1 },
                    { 166, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 166", "ROLE 166", "", 1 },
                    { 167, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 167", "ROLE 167", "", 1 },
                    { 168, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 168", "ROLE 168", "", 1 },
                    { 169, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 169", "ROLE 169", "", 1 },
                    { 170, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 170", "ROLE 170", "", 1 },
                    { 171, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 171", "ROLE 171", "", 1 },
                    { 172, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 172", "ROLE 172", "", 1 },
                    { 173, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 173", "ROLE 173", "", 1 },
                    { 174, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 174", "ROLE 174", "", 1 },
                    { 175, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 175", "ROLE 175", "", 1 },
                    { 176, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 176", "ROLE 176", "", 1 },
                    { 177, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 177", "ROLE 177", "", 1 },
                    { 178, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 178", "ROLE 178", "", 1 },
                    { 179, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 179", "ROLE 179", "", 1 },
                    { 180, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 180", "ROLE 180", "", 1 },
                    { 181, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 181", "ROLE 181", "", 1 },
                    { 182, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 182", "ROLE 182", "", 1 },
                    { 183, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 183", "ROLE 183", "", 1 },
                    { 184, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 184", "ROLE 184", "", 1 },
                    { 185, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 185", "ROLE 185", "", 1 },
                    { 186, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 186", "ROLE 186", "", 1 },
                    { 187, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 187", "ROLE 187", "", 1 },
                    { 188, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 188", "ROLE 188", "", 1 },
                    { 189, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 189", "ROLE 189", "", 1 },
                    { 190, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 190", "ROLE 190", "", 1 },
                    { 191, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 191", "ROLE 191", "", 1 },
                    { 192, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 192", "ROLE 192", "", 1 },
                    { 193, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 193", "ROLE 193", "", 1 },
                    { 194, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 194", "ROLE 194", "", 1 },
                    { 195, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 195", "ROLE 195", "", 1 },
                    { 196, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 196", "ROLE 196", "", 1 },
                    { 197, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 197", "ROLE 197", "", 1 },
                    { 198, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 198", "ROLE 198", "", 1 }
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions", "Ver" },
                values: new object[,]
                {
                    { 199, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 199", "ROLE 199", "", 1 },
                    { 200, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 200", "ROLE 200", "", 1 },
                    { 201, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 201", "ROLE 201", "", 1 },
                    { 202, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 202", "ROLE 202", "", 1 },
                    { 203, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 203", "ROLE 203", "", 1 },
                    { 204, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 204", "ROLE 204", "", 1 },
                    { 205, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 205", "ROLE 205", "", 1 },
                    { 206, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 206", "ROLE 206", "", 1 },
                    { 207, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 207", "ROLE 207", "", 1 },
                    { 208, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 208", "ROLE 208", "", 1 },
                    { 209, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 209", "ROLE 209", "", 1 },
                    { 210, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 210", "ROLE 210", "", 1 },
                    { 211, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 211", "ROLE 211", "", 1 },
                    { 212, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 212", "ROLE 212", "", 1 },
                    { 213, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 213", "ROLE 213", "", 1 },
                    { 214, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 214", "ROLE 214", "", 1 },
                    { 215, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 215", "ROLE 215", "", 1 },
                    { 216, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 216", "ROLE 216", "", 1 },
                    { 217, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 217", "ROLE 217", "", 1 },
                    { 218, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 218", "ROLE 218", "", 1 },
                    { 219, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 219", "ROLE 219", "", 1 },
                    { 220, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 220", "ROLE 220", "", 1 },
                    { 221, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 221", "ROLE 221", "", 1 },
                    { 222, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 222", "ROLE 222", "", 1 },
                    { 223, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 223", "ROLE 223", "", 1 },
                    { 224, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 224", "ROLE 224", "", 1 },
                    { 225, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 225", "ROLE 225", "", 1 },
                    { 226, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 226", "ROLE 226", "", 1 },
                    { 227, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 227", "ROLE 227", "", 1 },
                    { 228, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 228", "ROLE 228", "", 1 },
                    { 229, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 229", "ROLE 229", "", 1 },
                    { 230, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 230", "ROLE 230", "", 1 },
                    { 231, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 231", "ROLE 231", "", 1 },
                    { 232, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 232", "ROLE 232", "", 1 },
                    { 233, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 233", "ROLE 233", "", 1 },
                    { 234, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 234", "ROLE 234", "", 1 },
                    { 235, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 235", "ROLE 235", "", 1 },
                    { 236, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 236", "ROLE 236", "", 1 },
                    { 237, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 237", "ROLE 237", "", 1 },
                    { 238, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 238", "ROLE 238", "", 1 },
                    { 239, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 239", "ROLE 239", "", 1 },
                    { 240, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 240", "ROLE 240", "", 1 }
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions", "Ver" },
                values: new object[,]
                {
                    { 241, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 241", "ROLE 241", "", 1 },
                    { 242, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 242", "ROLE 242", "", 1 },
                    { 243, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 243", "ROLE 243", "", 1 },
                    { 244, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 244", "ROLE 244", "", 1 },
                    { 245, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 245", "ROLE 245", "", 1 },
                    { 246, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 246", "ROLE 246", "", 1 },
                    { 247, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 247", "ROLE 247", "", 1 },
                    { 248, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 248", "ROLE 248", "", 1 },
                    { 249, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 249", "ROLE 249", "", 1 },
                    { 250, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 250", "ROLE 250", "", 1 },
                    { 251, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 251", "ROLE 251", "", 1 },
                    { 252, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 252", "ROLE 252", "", 1 },
                    { 253, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 253", "ROLE 253", "", 1 },
                    { 254, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 254", "ROLE 254", "", 1 },
                    { 255, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 255", "ROLE 255", "", 1 },
                    { 256, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 256", "ROLE 256", "", 1 },
                    { 257, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 257", "ROLE 257", "", 1 },
                    { 258, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 258", "ROLE 258", "", 1 },
                    { 259, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 259", "ROLE 259", "", 1 },
                    { 260, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 260", "ROLE 260", "", 1 },
                    { 261, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 261", "ROLE 261", "", 1 },
                    { 262, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 262", "ROLE 262", "", 1 },
                    { 263, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 263", "ROLE 263", "", 1 },
                    { 264, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 264", "ROLE 264", "", 1 },
                    { 265, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 265", "ROLE 265", "", 1 },
                    { 266, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 266", "ROLE 266", "", 1 },
                    { 267, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 267", "ROLE 267", "", 1 },
                    { 268, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 268", "ROLE 268", "", 1 },
                    { 269, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 269", "ROLE 269", "", 1 },
                    { 270, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 270", "ROLE 270", "", 1 },
                    { 271, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 271", "ROLE 271", "", 1 },
                    { 272, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 272", "ROLE 272", "", 1 },
                    { 273, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 273", "ROLE 273", "", 1 }
                });

            migrationBuilder.InsertData(
                table: "AppRoles_Hist",
                columns: new[] { "Id", "Ver", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions" },
                values: new object[,]
                {
                    { 1, 1, "Full sys admin", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Administrator", "ADMINISTRATOR", "Users, Roles, Companies" },
                    { 2, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 002", "ROLE 002", "" },
                    { 3, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 003", "ROLE 003", "" },
                    { 4, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 004", "ROLE 004", "" },
                    { 5, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 005", "ROLE 005", "" },
                    { 6, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 006", "ROLE 006", "" },
                    { 7, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 007", "ROLE 007", "" },
                    { 8, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 008", "ROLE 008", "" },
                    { 9, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 009", "ROLE 009", "" }
                });

            migrationBuilder.InsertData(
                table: "AppRoles_Hist",
                columns: new[] { "Id", "Ver", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions" },
                values: new object[,]
                {
                    { 10, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 010", "ROLE 010", "" },
                    { 11, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 011", "ROLE 011", "" },
                    { 12, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 012", "ROLE 012", "" },
                    { 13, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 013", "ROLE 013", "" },
                    { 14, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 014", "ROLE 014", "" },
                    { 15, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 015", "ROLE 015", "" },
                    { 16, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 016", "ROLE 016", "" },
                    { 17, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 017", "ROLE 017", "" },
                    { 18, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 018", "ROLE 018", "" },
                    { 19, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 019", "ROLE 019", "" },
                    { 20, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 020", "ROLE 020", "" },
                    { 21, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 021", "ROLE 021", "" },
                    { 22, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 022", "ROLE 022", "" },
                    { 23, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 023", "ROLE 023", "" },
                    { 24, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 024", "ROLE 024", "" },
                    { 25, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 025", "ROLE 025", "" },
                    { 26, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 026", "ROLE 026", "" },
                    { 27, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 027", "ROLE 027", "" },
                    { 28, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 028", "ROLE 028", "" },
                    { 29, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 029", "ROLE 029", "" },
                    { 30, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 030", "ROLE 030", "" },
                    { 31, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 031", "ROLE 031", "" },
                    { 32, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 032", "ROLE 032", "" },
                    { 33, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 033", "ROLE 033", "" },
                    { 34, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 034", "ROLE 034", "" },
                    { 35, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 035", "ROLE 035", "" },
                    { 36, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 036", "ROLE 036", "" },
                    { 37, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 037", "ROLE 037", "" },
                    { 38, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 038", "ROLE 038", "" },
                    { 39, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 039", "ROLE 039", "" },
                    { 40, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 040", "ROLE 040", "" },
                    { 41, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 041", "ROLE 041", "" },
                    { 42, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 042", "ROLE 042", "" },
                    { 43, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 043", "ROLE 043", "" },
                    { 44, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 044", "ROLE 044", "" },
                    { 45, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 045", "ROLE 045", "" },
                    { 46, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 046", "ROLE 046", "" },
                    { 47, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 047", "ROLE 047", "" },
                    { 48, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 048", "ROLE 048", "" },
                    { 49, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 049", "ROLE 049", "" },
                    { 50, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 050", "ROLE 050", "" },
                    { 51, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 051", "ROLE 051", "" }
                });

            migrationBuilder.InsertData(
                table: "AppRoles_Hist",
                columns: new[] { "Id", "Ver", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions" },
                values: new object[,]
                {
                    { 52, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 052", "ROLE 052", "" },
                    { 53, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 053", "ROLE 053", "" },
                    { 54, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 054", "ROLE 054", "" },
                    { 55, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 055", "ROLE 055", "" },
                    { 56, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 056", "ROLE 056", "" },
                    { 57, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 057", "ROLE 057", "" },
                    { 58, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 058", "ROLE 058", "" },
                    { 59, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 059", "ROLE 059", "" },
                    { 60, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 060", "ROLE 060", "" },
                    { 61, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 061", "ROLE 061", "" },
                    { 62, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 062", "ROLE 062", "" },
                    { 63, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 063", "ROLE 063", "" },
                    { 64, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 064", "ROLE 064", "" },
                    { 65, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 065", "ROLE 065", "" },
                    { 66, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 066", "ROLE 066", "" },
                    { 67, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 067", "ROLE 067", "" },
                    { 68, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 068", "ROLE 068", "" },
                    { 69, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 069", "ROLE 069", "" },
                    { 70, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 070", "ROLE 070", "" },
                    { 71, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 071", "ROLE 071", "" },
                    { 72, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 072", "ROLE 072", "" },
                    { 73, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 073", "ROLE 073", "" },
                    { 74, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 074", "ROLE 074", "" },
                    { 75, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 075", "ROLE 075", "" },
                    { 76, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 076", "ROLE 076", "" },
                    { 77, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 077", "ROLE 077", "" },
                    { 78, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 078", "ROLE 078", "" },
                    { 79, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 079", "ROLE 079", "" },
                    { 80, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 080", "ROLE 080", "" },
                    { 81, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 081", "ROLE 081", "" },
                    { 82, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 082", "ROLE 082", "" },
                    { 83, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 083", "ROLE 083", "" },
                    { 84, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 084", "ROLE 084", "" },
                    { 85, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 085", "ROLE 085", "" },
                    { 86, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 086", "ROLE 086", "" },
                    { 87, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 087", "ROLE 087", "" },
                    { 88, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 088", "ROLE 088", "" },
                    { 89, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 089", "ROLE 089", "" },
                    { 90, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 090", "ROLE 090", "" },
                    { 91, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 091", "ROLE 091", "" },
                    { 92, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 092", "ROLE 092", "" },
                    { 93, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 093", "ROLE 093", "" }
                });

            migrationBuilder.InsertData(
                table: "AppRoles_Hist",
                columns: new[] { "Id", "Ver", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions" },
                values: new object[,]
                {
                    { 94, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 094", "ROLE 094", "" },
                    { 95, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 095", "ROLE 095", "" },
                    { 96, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 096", "ROLE 096", "" },
                    { 97, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 097", "ROLE 097", "" },
                    { 98, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 098", "ROLE 098", "" },
                    { 99, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 099", "ROLE 099", "" },
                    { 100, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 100", "ROLE 100", "" },
                    { 101, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 101", "ROLE 101", "" },
                    { 102, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 102", "ROLE 102", "" },
                    { 103, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 103", "ROLE 103", "" },
                    { 104, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 104", "ROLE 104", "" },
                    { 105, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 105", "ROLE 105", "" },
                    { 106, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 106", "ROLE 106", "" },
                    { 107, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 107", "ROLE 107", "" },
                    { 108, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 108", "ROLE 108", "" },
                    { 109, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 109", "ROLE 109", "" },
                    { 110, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 110", "ROLE 110", "" },
                    { 111, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 111", "ROLE 111", "" },
                    { 112, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 112", "ROLE 112", "" },
                    { 113, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 113", "ROLE 113", "" },
                    { 114, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 114", "ROLE 114", "" },
                    { 115, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 115", "ROLE 115", "" },
                    { 116, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 116", "ROLE 116", "" },
                    { 117, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 117", "ROLE 117", "" },
                    { 118, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 118", "ROLE 118", "" },
                    { 119, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 119", "ROLE 119", "" },
                    { 120, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 120", "ROLE 120", "" },
                    { 121, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 121", "ROLE 121", "" },
                    { 122, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 122", "ROLE 122", "" },
                    { 123, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 123", "ROLE 123", "" },
                    { 124, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 124", "ROLE 124", "" },
                    { 125, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 125", "ROLE 125", "" },
                    { 126, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 126", "ROLE 126", "" },
                    { 127, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 127", "ROLE 127", "" },
                    { 128, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 128", "ROLE 128", "" },
                    { 129, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 129", "ROLE 129", "" },
                    { 130, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 130", "ROLE 130", "" },
                    { 131, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 131", "ROLE 131", "" },
                    { 132, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 132", "ROLE 132", "" },
                    { 133, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 133", "ROLE 133", "" },
                    { 134, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 134", "ROLE 134", "" },
                    { 135, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 135", "ROLE 135", "" }
                });

            migrationBuilder.InsertData(
                table: "AppRoles_Hist",
                columns: new[] { "Id", "Ver", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions" },
                values: new object[,]
                {
                    { 136, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 136", "ROLE 136", "" },
                    { 137, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 137", "ROLE 137", "" },
                    { 138, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 138", "ROLE 138", "" },
                    { 139, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 139", "ROLE 139", "" },
                    { 140, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 140", "ROLE 140", "" },
                    { 141, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 141", "ROLE 141", "" },
                    { 142, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 142", "ROLE 142", "" },
                    { 143, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 143", "ROLE 143", "" },
                    { 144, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 144", "ROLE 144", "" },
                    { 145, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 145", "ROLE 145", "" },
                    { 146, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 146", "ROLE 146", "" },
                    { 147, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 147", "ROLE 147", "" },
                    { 148, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 148", "ROLE 148", "" },
                    { 149, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 149", "ROLE 149", "" },
                    { 150, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 150", "ROLE 150", "" },
                    { 151, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 151", "ROLE 151", "" },
                    { 152, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 152", "ROLE 152", "" },
                    { 153, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 153", "ROLE 153", "" },
                    { 154, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 154", "ROLE 154", "" },
                    { 155, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 155", "ROLE 155", "" },
                    { 156, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 156", "ROLE 156", "" },
                    { 157, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 157", "ROLE 157", "" },
                    { 158, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 158", "ROLE 158", "" },
                    { 159, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 159", "ROLE 159", "" },
                    { 160, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 160", "ROLE 160", "" },
                    { 161, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 161", "ROLE 161", "" },
                    { 162, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 162", "ROLE 162", "" },
                    { 163, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 163", "ROLE 163", "" },
                    { 164, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 164", "ROLE 164", "" },
                    { 165, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 165", "ROLE 165", "" },
                    { 166, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 166", "ROLE 166", "" },
                    { 167, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 167", "ROLE 167", "" },
                    { 168, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 168", "ROLE 168", "" },
                    { 169, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 169", "ROLE 169", "" },
                    { 170, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 170", "ROLE 170", "" },
                    { 171, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 171", "ROLE 171", "" },
                    { 172, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 172", "ROLE 172", "" },
                    { 173, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 173", "ROLE 173", "" },
                    { 174, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 174", "ROLE 174", "" },
                    { 175, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 175", "ROLE 175", "" },
                    { 176, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 176", "ROLE 176", "" },
                    { 177, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 177", "ROLE 177", "" }
                });

            migrationBuilder.InsertData(
                table: "AppRoles_Hist",
                columns: new[] { "Id", "Ver", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions" },
                values: new object[,]
                {
                    { 178, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 178", "ROLE 178", "" },
                    { 179, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 179", "ROLE 179", "" },
                    { 180, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 180", "ROLE 180", "" },
                    { 181, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 181", "ROLE 181", "" },
                    { 182, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 182", "ROLE 182", "" },
                    { 183, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 183", "ROLE 183", "" },
                    { 184, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 184", "ROLE 184", "" },
                    { 185, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 185", "ROLE 185", "" },
                    { 186, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 186", "ROLE 186", "" },
                    { 187, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 187", "ROLE 187", "" },
                    { 188, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 188", "ROLE 188", "" },
                    { 189, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 189", "ROLE 189", "" },
                    { 190, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 190", "ROLE 190", "" },
                    { 191, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 191", "ROLE 191", "" },
                    { 192, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 192", "ROLE 192", "" },
                    { 193, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 193", "ROLE 193", "" },
                    { 194, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 194", "ROLE 194", "" },
                    { 195, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 195", "ROLE 195", "" },
                    { 196, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 196", "ROLE 196", "" },
                    { 197, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 197", "ROLE 197", "" },
                    { 198, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 198", "ROLE 198", "" },
                    { 199, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 199", "ROLE 199", "" },
                    { 200, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 200", "ROLE 200", "" },
                    { 201, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 201", "ROLE 201", "" },
                    { 202, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 202", "ROLE 202", "" },
                    { 203, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 203", "ROLE 203", "" },
                    { 204, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 204", "ROLE 204", "" },
                    { 205, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 205", "ROLE 205", "" },
                    { 206, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 206", "ROLE 206", "" },
                    { 207, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 207", "ROLE 207", "" },
                    { 208, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 208", "ROLE 208", "" },
                    { 209, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 209", "ROLE 209", "" },
                    { 210, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 210", "ROLE 210", "" },
                    { 211, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 211", "ROLE 211", "" },
                    { 212, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 212", "ROLE 212", "" },
                    { 213, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 213", "ROLE 213", "" },
                    { 214, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 214", "ROLE 214", "" },
                    { 215, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 215", "ROLE 215", "" },
                    { 216, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 216", "ROLE 216", "" },
                    { 217, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 217", "ROLE 217", "" },
                    { 218, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 218", "ROLE 218", "" },
                    { 219, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 219", "ROLE 219", "" }
                });

            migrationBuilder.InsertData(
                table: "AppRoles_Hist",
                columns: new[] { "Id", "Ver", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions" },
                values: new object[,]
                {
                    { 220, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 220", "ROLE 220", "" },
                    { 221, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 221", "ROLE 221", "" },
                    { 222, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 222", "ROLE 222", "" },
                    { 223, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 223", "ROLE 223", "" },
                    { 224, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 224", "ROLE 224", "" },
                    { 225, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 225", "ROLE 225", "" },
                    { 226, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 226", "ROLE 226", "" },
                    { 227, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 227", "ROLE 227", "" },
                    { 228, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 228", "ROLE 228", "" },
                    { 229, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 229", "ROLE 229", "" },
                    { 230, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 230", "ROLE 230", "" },
                    { 231, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 231", "ROLE 231", "" },
                    { 232, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 232", "ROLE 232", "" },
                    { 233, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 233", "ROLE 233", "" },
                    { 234, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 234", "ROLE 234", "" },
                    { 235, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 235", "ROLE 235", "" },
                    { 236, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 236", "ROLE 236", "" },
                    { 237, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 237", "ROLE 237", "" },
                    { 238, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 238", "ROLE 238", "" },
                    { 239, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 239", "ROLE 239", "" },
                    { 240, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 240", "ROLE 240", "" },
                    { 241, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 241", "ROLE 241", "" },
                    { 242, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 242", "ROLE 242", "" },
                    { 243, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 243", "ROLE 243", "" },
                    { 244, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 244", "ROLE 244", "" },
                    { 245, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 245", "ROLE 245", "" },
                    { 246, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 246", "ROLE 246", "" },
                    { 247, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 247", "ROLE 247", "" },
                    { 248, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 248", "ROLE 248", "" },
                    { 249, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 249", "ROLE 249", "" },
                    { 250, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 250", "ROLE 250", "" },
                    { 251, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 251", "ROLE 251", "" },
                    { 252, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 252", "ROLE 252", "" },
                    { 253, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 253", "ROLE 253", "" },
                    { 254, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 254", "ROLE 254", "" },
                    { 255, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 255", "ROLE 255", "" },
                    { 256, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 256", "ROLE 256", "" },
                    { 257, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 257", "ROLE 257", "" },
                    { 258, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 258", "ROLE 258", "" },
                    { 259, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 259", "ROLE 259", "" },
                    { 260, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 260", "ROLE 260", "" },
                    { 261, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 261", "ROLE 261", "" }
                });

            migrationBuilder.InsertData(
                table: "AppRoles_Hist",
                columns: new[] { "Id", "Ver", "Description", "when", "when_str", "whoName", "who", "what", "Name", "NormalizedName", "Permissions" },
                values: new object[,]
                {
                    { 262, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 262", "ROLE 262", "" },
                    { 263, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 263", "ROLE 263", "" },
                    { 264, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 264", "ROLE 264", "" },
                    { 265, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 265", "ROLE 265", "" },
                    { 266, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 266", "ROLE 266", "" },
                    { 267, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 267", "ROLE 267", "" },
                    { 268, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 268", "ROLE 268", "" },
                    { 269, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 269", "ROLE 269", "" },
                    { 270, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 270", "ROLE 270", "" },
                    { 271, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 271", "ROLE 271", "" },
                    { 272, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 272", "ROLE 272", "" },
                    { 273, 1, "", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "Role 273", "ROLE 273", "" }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "Id", "AppRoleId", "AppRoleVer", "AppUserId", "AppUserVer", "when", "when_str", "whoName", "who", "what", "Ver" },
                values: new object[] { 1, 1, 1, 1, 1, new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", 1 });

            migrationBuilder.InsertData(
                table: "AppUserRoles_Hist",
                columns: new[] { "Id", "Ver", "AppRoleId", "AppRoleVer", "AppUserId", "AppUserVer", "when", "when_str", "whoName", "who", "what" },
                values: new object[] { 1, 1, 1, 1, 1, 1, new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Email", "EmailConfirmed", "FirstName", "when", "when_str", "whoName", "who", "what", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleName", "SecurityStamp", "TwoFactorEnabled", "TwoFactorRequired", "UserName", "Ver" },
                values: new object[] { 1, 0, "admin@demo.test", true, "Admin", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "...", false, null, "ADMIN@DEMO.TEST", "ADMIN@DEMO.TEST", "AQAAAAEAACcQAAAAEEWSge3/vKpROE//0Nm9JClMuPvYhPdO/9nCpSNgU7TelfRBy5lZhAjb4t2f5NZQnA==", null, false, "Administrator", "c6c51456-3c4f-4d46-97e4-6b3dbf0bc74b", false, false, "admin@demo.test", 1 });

            migrationBuilder.InsertData(
                table: "AppUsers_Hist",
                columns: new[] { "Id", "Ver", "AccessFailedCount", "Email", "EmailConfirmed", "FirstName", "when", "when_str", "whoName", "who", "what", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleName", "SecurityStamp", "TwoFactorEnabled", "TwoFactorRequired", "UserName" },
                values: new object[] { 1, 1, 0, "admin@demo.test", true, "Admin", new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "26/09/2022 00:00:00", "admin@demo.test", 1, "i", "...", false, null, "ADMIN@DEMO.TEST", "ADMIN@DEMO.TEST", "AQAAAAEAACcQAAAAEEWSge3/vKpROE//0Nm9JClMuPvYhPdO/9nCpSNgU7TelfRBy5lZhAjb4t2f5NZQnA==", null, false, "Administrator", "c6c51456-3c4f-4d46-97e4-6b3dbf0bc74b", false, false, "admin@demo.test" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppClaimTypes");

            migrationBuilder.DropTable(
                name: "AppClaimTypes_Hist");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoleClaims_Hist");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppRoles_Hist");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserClaims_Hist");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserLogins_Hist");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserRoles_Hist");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "AppUsers_Hist");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "AppUserTokens_Hist");

            migrationBuilder.DropTable(
                name: "ListSettingsForUsers");

            migrationBuilder.DropTable(
                name: "ListSettingsForUsers_Hist");
        }
    }
}
