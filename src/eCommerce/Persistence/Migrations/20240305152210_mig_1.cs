using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AuthenticatorType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityAvailable = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Baskets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailAuthenticators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivationKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAuthenticators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAuthenticators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtpAuthenticators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecretKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpAuthenticators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtpAuthenticators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevokedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonRevoked = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikedProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikedProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikedProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("01627ffa-445c-4111-b0f8-935c96814454"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(819), null, "Fresh Meat", "Fresh Meat", null },
                    { new Guid("2411b221-ec3d-4170-9978-d958440bd35f"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(825), null, "Dried Fruits & Nuts", "Dried Fruits & Nuts", null },
                    { new Guid("2fedbbd7-46a5-487d-bc3a-51243f1280b3"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(822), null, "Vegetables", "Vegetables", null },
                    { new Guid("3acbce70-bf9e-4ed8-bf9f-bb1311a82899"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(830), null, "Oatmeal", "Oatmeal", null },
                    { new Guid("3bbd6712-dba4-40aa-904a-a12898fcdb65"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(828), null, "Butter & Eggs", "Butter & Eggs", null },
                    { new Guid("52021e7d-7e0f-4e09-ae74-8e1891752f1a"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(831), null, "Juices", "Juices", null },
                    { new Guid("77bf0937-7055-47a6-950a-77b2328da3c6"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(823), null, "Fresh Fruits", "Fresh Fruits", null },
                    { new Guid("8e978bdb-2528-48f2-bf61-d7e5c342d5a8"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(826), null, "Ocean Foods", "Ocean Foods", null },
                    { new Guid("e9b8588e-25dd-45a2-926a-ce313035bdbc"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(829), null, "Fastfood", "Fastfood", null }
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Auth.Admin", null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Auth.Read", null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Auth.Write", null },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Auth.RevokeToken", null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OperationClaims.Admin", null },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OperationClaims.Read", null },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OperationClaims.Write", null },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OperationClaims.Create", null },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OperationClaims.Update", null },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OperationClaims.Delete", null },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "UserOperationClaims.Admin", null },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "UserOperationClaims.Read", null },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "UserOperationClaims.Write", null },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "UserOperationClaims.Create", null },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "UserOperationClaims.Update", null },
                    { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "UserOperationClaims.Delete", null },
                    { 18, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Users.Admin", null },
                    { 19, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Users.Read", null },
                    { 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Users.Write", null },
                    { 21, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Users.Create", null },
                    { 22, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Users.Update", null },
                    { 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Users.Delete", null },
                    { 24, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Baskets.Admin", null },
                    { 25, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Baskets.Read", null },
                    { 26, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Baskets.Write", null },
                    { 27, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Baskets.Create", null },
                    { 28, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Baskets.Update", null },
                    { 29, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Baskets.Delete", null },
                    { 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BasketItems.Admin", null },
                    { 31, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BasketItems.Read", null },
                    { 32, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BasketItems.Write", null },
                    { 33, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BasketItems.Create", null },
                    { 34, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BasketItems.Update", null },
                    { 35, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BasketItems.Delete", null },
                    { 36, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categories.Admin", null },
                    { 37, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categories.Read", null },
                    { 38, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categories.Write", null },
                    { 39, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categories.Create", null },
                    { 40, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categories.Update", null },
                    { 41, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categories.Delete", null },
                    { 42, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Comments.Admin", null },
                    { 43, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Comments.Read", null },
                    { 44, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Comments.Write", null },
                    { 45, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Comments.Create", null },
                    { 46, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Comments.Update", null },
                    { 47, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Comments.Delete", null },
                    { 48, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LikedProducts.Admin", null },
                    { 49, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LikedProducts.Read", null },
                    { 50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LikedProducts.Write", null },
                    { 51, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LikedProducts.Create", null },
                    { 52, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LikedProducts.Update", null },
                    { 53, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LikedProducts.Delete", null },
                    { 54, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.Admin", null },
                    { 55, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.Read", null },
                    { 56, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.Write", null },
                    { 57, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.Create", null },
                    { 58, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.Update", null },
                    { 59, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.Delete", null },
                    { 60, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OrderItems.Admin", null },
                    { 61, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OrderItems.Read", null },
                    { 62, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OrderItems.Write", null },
                    { 63, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OrderItems.Create", null },
                    { 64, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OrderItems.Update", null },
                    { 65, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OrderItems.Delete", null },
                    { 66, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Products.Admin", null },
                    { 67, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Products.Read", null },
                    { 68, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Products.Write", null },
                    { 69, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Products.Create", null },
                    { 70, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Products.Update", null },
                    { 71, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Products.Delete", null },
                    { 72, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductImages.Admin", null },
                    { 73, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductImages.Read", null },
                    { 74, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductImages.Write", null },
                    { 75, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductImages.Create", null },
                    { 76, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductImages.Update", null },
                    { 77, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductImages.Delete", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("0e31726f-1fd1-429d-b486-e618086bebe6"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@alikemaluysal.com", new byte[] { 140, 36, 124, 107, 147, 26, 126, 77, 253, 159, 173, 142, 196, 155, 186, 251, 237, 64, 3, 24, 188, 130, 110, 9, 44, 37, 21, 139, 64, 119, 94, 55, 47, 198, 32, 51, 10, 96, 4, 168, 108, 120, 167, 67, 185, 202, 184, 211, 93, 97, 89, 185, 202, 135, 27, 109, 100, 196, 52, 215, 96, 209, 233, 158 }, new byte[] { 182, 127, 184, 207, 70, 119, 245, 33, 189, 195, 248, 182, 174, 143, 198, 5, 80, 209, 167, 148, 215, 248, 222, 4, 254, 216, 133, 6, 84, 88, 113, 79, 181, 250, 77, 194, 67, 172, 11, 40, 65, 184, 244, 227, 120, 209, 251, 247, 87, 82, 174, 70, 18, 246, 223, 209, 22, 211, 248, 165, 165, 220, 134, 120, 253, 63, 190, 135, 231, 170, 171, 227, 107, 19, 122, 48, 157, 137, 33, 183, 181, 23, 146, 45, 173, 103, 109, 38, 197, 183, 178, 125, 214, 119, 161, 168, 52, 87, 142, 118, 153, 201, 97, 187, 56, 142, 35, 242, 162, 129, 65, 70, 238, 175, 231, 142, 108, 220, 132, 156, 133, 246, 153, 25, 74, 129, 168, 69 }, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "DeletedDate", "Description", "Name", "Price", "QuantityAvailable", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("00c11dca-8f36-4c70-85fd-0dbdd09d7f8d"), new Guid("8e978bdb-2528-48f2-bf61-d7e5c342d5a8"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(908), null, "Raisins", "Raisins", 366m, 100, null },
                    { new Guid("0e54932e-e776-4a7a-b1b9-d051238627f0"), new Guid("77bf0937-7055-47a6-950a-77b2328da3c6"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(913), null, "Spinach", "Spinach", 180m, 100, null },
                    { new Guid("140aa7e9-01df-4036-96e3-01d0784cd709"), new Guid("2411b221-ec3d-4170-9978-d958440bd35f"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(897), null, "Banana", "Banana", 137m, 75, null },
                    { new Guid("192a67be-09e7-4056-b18d-23ea78d54191"), new Guid("2411b221-ec3d-4170-9978-d958440bd35f"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(904), null, "Grapes", "Grapes", 269m, 100, null },
                    { new Guid("1fd43c98-1bfb-4a83-914c-9bad663eb0ef"), new Guid("3acbce70-bf9e-4ed8-bf9f-bb1311a82899"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(894), null, "Hamburger", "Hamburger", 494m, 20, null },
                    { new Guid("530f39c4-569e-400b-8565-2f4a339682e1"), new Guid("77bf0937-7055-47a6-950a-77b2328da3c6"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(915), null, "Bell Pepper", "Bell Pepper", 136m, 100, null },
                    { new Guid("6db7024e-aa6e-4cb9-89eb-cc877e0f7042"), new Guid("52021e7d-7e0f-4e09-ae74-8e1891752f1a"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(888), null, "Mixed Fruit Juice", "Mixed Fruit Juice", 410m, 100, null },
                    { new Guid("73e248a6-0f66-4640-91a1-61881e1f9a50"), new Guid("2411b221-ec3d-4170-9978-d958440bd35f"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(902), null, "Apple", "Apple", 161m, 80, null },
                    { new Guid("8a15110d-2f57-4d49-b983-8adff3e3574a"), new Guid("2411b221-ec3d-4170-9978-d958440bd35f"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(891), null, "Mango", "Mango", 112m, 50, null },
                    { new Guid("a10c4411-9591-44a2-bb0c-27d700a116ed"), new Guid("52021e7d-7e0f-4e09-ae74-8e1891752f1a"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(910), null, "Orange Juice", "Orange Juice", 456m, 100, null },
                    { new Guid("c375b4cd-f41a-4f64-87a0-791c20d6de37"), new Guid("2411b221-ec3d-4170-9978-d958440bd35f"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(900), null, "Fig", "Fig", 464m, 100, null },
                    { new Guid("c5af10c3-c4ec-47e7-a6d4-4da0a8cd4df2"), new Guid("3acbce70-bf9e-4ed8-bf9f-bb1311a82899"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(917), null, "Fried Chicken", "Fried Chicken", 228m, 20, null },
                    { new Guid("d5236461-b469-4725-a54c-4ce547a3b90f"), new Guid("2411b221-ec3d-4170-9978-d958440bd35f"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(912), null, "Mixed Fruits", "Mixed Fruits", 229m, 100, null },
                    { new Guid("e539d788-9cfd-419d-a6c0-7c9ecced74f8"), new Guid("2411b221-ec3d-4170-9978-d958440bd35f"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(906), null, "Watermelon", "Watermelon", 396m, 20, null },
                    { new Guid("f1f1639a-d3e0-4ec9-9d47-cde00d43e44f"), new Guid("2fedbbd7-46a5-487d-bc3a-51243f1280b3"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(896), null, "Meat", "Red Meat", 96m, 50, null }
                });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("3859ee0a-9acc-4d20-8299-93b6ad6ff0b0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("0e31726f-1fd1-429d-b486-e618086bebe6") });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "ImageUrl", "ProductId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0950f706-9920-4f40-a435-3908e0b2ba89"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(965), null, "/theme/img/latest-product/lp-1.jpg", new Guid("0e54932e-e776-4a7a-b1b9-d051238627f0"), null },
                    { new Guid("29f9a3c7-df9c-4616-b89e-335ca0e9f568"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(966), null, "/theme/img/product/details/product-details-2.jpg", new Guid("530f39c4-569e-400b-8565-2f4a339682e1"), null },
                    { new Guid("32bf6d1b-423a-496b-855b-73d66580dd70"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(947), null, "/theme/img/product/discount/pd-5.jpg", new Guid("1fd43c98-1bfb-4a83-914c-9bad663eb0ef"), null },
                    { new Guid("6a94cf8f-8a62-4666-8312-b0dcac7b6b46"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(942), null, "/theme/img/product/discount/pd-3.jpg", new Guid("6db7024e-aa6e-4cb9-89eb-cc877e0f7042"), null },
                    { new Guid("713da0e4-2249-4a78-9b3c-a206f171389e"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(956), null, "/theme/img/product/product-4.jpg", new Guid("192a67be-09e7-4056-b18d-23ea78d54191"), null },
                    { new Guid("7b030ee2-e806-4dc5-84e5-437e2670acb0"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(962), null, "/theme/img/product/product-11.jpg", new Guid("a10c4411-9591-44a2-bb0c-27d700a116ed"), null },
                    { new Guid("902a956f-1114-43ed-a4fd-631679babcc8"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(955), null, "/theme/img/product/product-8.jpg", new Guid("73e248a6-0f66-4640-91a1-61881e1f9a50"), null },
                    { new Guid("9ef744b6-139e-491f-86cb-bfd05ac8076e"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(949), null, "/theme/img/product/product-1.jpg", new Guid("f1f1639a-d3e0-4ec9-9d47-cde00d43e44f"), null },
                    { new Guid("be6535b2-d9c3-47de-9eea-a5a38bf14947"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(946), null, "/theme/img/product/discount/pd-4.jpg", new Guid("8a15110d-2f57-4d49-b983-8adff3e3574a"), null },
                    { new Guid("df7442c9-d07b-4625-a9c0-f844d15bb28c"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(968), null, "/theme/img/product/product-10.jpg", new Guid("c5af10c3-c4ec-47e7-a6d4-4da0a8cd4df2"), null },
                    { new Guid("e482e356-7be9-4c43-972e-606b95102ff8"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(957), null, "/theme/img/product/product-7.jpg", new Guid("e539d788-9cfd-419d-a6c0-7c9ecced74f8"), null },
                    { new Guid("ea29efed-80ff-4319-9fea-2e01f3b914c2"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(951), null, "/theme/img/product/product-2.jpg", new Guid("140aa7e9-01df-4036-96e3-01d0784cd709"), null },
                    { new Guid("ec47585b-667a-45f2-b652-6c37bcd6f77a"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(964), null, "/theme/img/product/product-12.jpg", new Guid("d5236461-b469-4725-a54c-4ce547a3b90f"), null },
                    { new Guid("f35949b7-d780-4f25-9b27-320469241496"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(953), null, "/theme/img/product/product-3.jpg", new Guid("c375b4cd-f41a-4f64-87a0-791c20d6de37"), null },
                    { new Guid("fd9d6516-de9f-4144-8abb-42e3172597b1"), new DateTime(2024, 3, 5, 15, 22, 9, 836, DateTimeKind.Utc).AddTicks(961), null, "/theme/img/product/product-9.jpg", new Guid("00c11dca-8f36-4c70-85fd-0dbdd09d7f8d"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId",
                table: "BasketItems",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_UserId",
                table: "BasketItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAuthenticators_UserId",
                table: "EmailAuthenticators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedProducts_ProductId",
                table: "LikedProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedProducts_UserId",
                table: "LikedProducts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OtpAuthenticators_UserId",
                table: "OtpAuthenticators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_OperationClaimId",
                table: "UserOperationClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_UserId",
                table: "UserOperationClaims",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "EmailAuthenticators");

            migrationBuilder.DropTable(
                name: "LikedProducts");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "OtpAuthenticators");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserOperationClaims");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
