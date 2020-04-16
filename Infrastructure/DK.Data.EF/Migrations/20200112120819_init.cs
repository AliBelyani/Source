using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DK.Data.EF.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "PermissionGroups",
                schema: "Security",
                columns: table => new
                {
                    xID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    xParentID = table.Column<long>(nullable: true),
                    xName = table.Column<string>(maxLength: 100, nullable: true),
                    xControllerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroups", x => x.xID);
                    table.ForeignKey(
                        name: "FK_PermissionGroups_PermissionGroups_xParentID",
                        column: x => x.xParentID,
                        principalSchema: "Security",
                        principalTable: "PermissionGroups",
                        principalColumn: "xID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Security",
                columns: table => new
                {
                    xID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    xName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    xDescription = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.xID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Security",
                columns: table => new
                {
                    xID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    xUsername = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    xEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    xEmailConfirmed = table.Column<bool>(nullable: false),
                    xPasswordHash = table.Column<string>(nullable: true),
                    xSecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    xMobile = table.Column<string>(maxLength: 20, nullable: true),
                    xMobileConfirmed = table.Column<bool>(nullable: false),
                    xTwoFactorEnabled = table.Column<bool>(nullable: false),
                    xLockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    xLockoutEnabled = table.Column<bool>(nullable: false),
                    xAccessFailedCount = table.Column<int>(nullable: false),
                    xIsActive = table.Column<bool>(nullable: false),
                    xRegisterDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.xID);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "Security",
                columns: table => new
                {
                    xID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    xName = table.Column<string>(nullable: true),
                    xController = table.Column<string>(nullable: true),
                    xAction = table.Column<string>(nullable: true),
                    xComment = table.Column<string>(nullable: true),
                    xActionType = table.Column<int>(nullable: false),
                    xPermissionGroupID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.xID);
                    table.ForeignKey(
                        name: "FK_Permissions_PermissionGroups_xPermissionGroupID",
                        column: x => x.xPermissionGroupID,
                        principalSchema: "Security",
                        principalTable: "PermissionGroups",
                        principalColumn: "xID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<long>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "xID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Security",
                columns: table => new
                {
                    xID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    xUserID = table.Column<long>(nullable: false),
                    xClaimType = table.Column<string>(nullable: true),
                    xClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.xID);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_xUserID",
                        column: x => x.xUserID,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "xID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Security",
                columns: table => new
                {
                    xLoginProvider = table.Column<string>(nullable: false),
                    xProviderKey = table.Column<string>(nullable: false),
                    xUserID = table.Column<long>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.xLoginProvider, x.xProviderKey, x.xUserID });
                    table.UniqueConstraint("AK_UserLogins_xLoginProvider_xProviderKey", x => new { x.xLoginProvider, x.xProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_xUserID",
                        column: x => x.xUserID,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "xID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Security",
                columns: table => new
                {
                    xUserID = table.Column<long>(nullable: false),
                    xRoleID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.xUserID, x.xRoleID });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_xRoleID",
                        column: x => x.xRoleID,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "xID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_xUserID",
                        column: x => x.xUserID,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "xID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "xID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionRoles",
                schema: "Security",
                columns: table => new
                {
                    xPermissionID = table.Column<long>(nullable: false),
                    xRoleID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRoles", x => new { x.xPermissionID, x.xRoleID });
                    table.ForeignKey(
                        name: "FK_PermissionRoles_Permissions_xPermissionID",
                        column: x => x.xPermissionID,
                        principalSchema: "Security",
                        principalTable: "Permissions",
                        principalColumn: "xID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRoles_Role_xRoleID",
                        column: x => x.xRoleID,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "xID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroups_xParentID",
                schema: "Security",
                table: "PermissionGroups",
                column: "xParentID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRoles_xRoleID",
                schema: "Security",
                table: "PermissionRoles",
                column: "xRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_xPermissionGroupID",
                schema: "Security",
                table: "Permissions",
                column: "xPermissionGroupID");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Security",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Security",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_xUserID",
                schema: "Security",
                table: "UserClaims",
                column: "xUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_xUserID",
                schema: "Security",
                table: "UserLogins",
                column: "xUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_xRoleID",
                schema: "Security",
                table: "UserRoles",
                column: "xRoleID");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Security",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Security",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionRoles",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "PermissionGroups",
                schema: "Security");
        }
    }
}
