using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace loyaltycard.Migrations
{
    /// <inheritdoc />
    public partial class Initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    adress_line = table.Column<string>(type: "TEXT", nullable: false),
                    district = table.Column<string>(type: "TEXT", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    last_name = table.Column<string>(type: "TEXT", nullable: false),
                    day_of_birth = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    mail = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    category = table.Column<string>(type: "TEXT", nullable: false),
                    brand = table.Column<string>(type: "TEXT", nullable: false),
                    price = table.Column<int>(type: "INTEGER", nullable: false),
                    type = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "branchAdresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    branchId = table.Column<int>(type: "INTEGER", nullable: false),
                    adressId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branchAdresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_branchAdresses_adresses_adressId",
                        column: x => x.adressId,
                        principalTable: "adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_branchAdresses_branches_branchId",
                        column: x => x.branchId,
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    branchId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employees_branches_branchId",
                        column: x => x.branchId,
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customerAdresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    customerId = table.Column<int>(type: "INTEGER", nullable: false),
                    adressId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerAdresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customerAdresses_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customerAdresses_adresses_adressId",
                        column: x => x.adressId,
                        principalTable: "adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loyaltyCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    customerId = table.Column<int>(type: "INTEGER", nullable: false),
                    loyaltycard_no = table.Column<string>(type: "TEXT", nullable: false),
                    amount = table.Column<int>(type: "INTEGER", nullable: false),
                    register_date = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loyaltyCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_loyaltyCards_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    branchId = table.Column<int>(type: "INTEGER", nullable: false),
                    employeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    customerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sales_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sales_branches_branchId",
                        column: x => x.branchId,
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sales_employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "salesProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    salesId = table.Column<string>(type: "TEXT", nullable: false),
                    productId = table.Column<int>(type: "INTEGER", nullable: false),
                    cost = table.Column<int>(type: "INTEGER", nullable: false),
                    bonus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salesProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_salesProducts_products_productId",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_salesProducts_sales_salesId",
                        column: x => x.salesId,
                        principalTable: "sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_branchAdresses_adressId",
                table: "branchAdresses",
                column: "adressId");

            migrationBuilder.CreateIndex(
                name: "IX_branchAdresses_branchId",
                table: "branchAdresses",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_customerAdresses_adressId",
                table: "customerAdresses",
                column: "adressId");

            migrationBuilder.CreateIndex(
                name: "IX_customerAdresses_customerId",
                table: "customerAdresses",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_branchId",
                table: "employees",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_loyaltyCards_customerId",
                table: "loyaltyCards",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_sales_branchId",
                table: "sales",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_sales_customerId",
                table: "sales",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_sales_employeeId",
                table: "sales",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_salesProducts_productId",
                table: "salesProducts",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_salesProducts_salesId",
                table: "salesProducts",
                column: "salesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "branchAdresses");

            migrationBuilder.DropTable(
                name: "customerAdresses");

            migrationBuilder.DropTable(
                name: "loyaltyCards");

            migrationBuilder.DropTable(
                name: "salesProducts");

            migrationBuilder.DropTable(
                name: "adresses");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "sales");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "branches");
        }
    }
}
