using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NpgsqlWithOwnedEntities.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "inventory");

            migrationBuilder.CreateTable(
                name: "stores",
                schema: "inventory",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<string>(maxLength: 100, nullable: true),
                    changed_on = table.Column<DateTime>(nullable: true),
                    changed_by = table.Column<string>(maxLength: 100, nullable: true),
                    tenant_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    site = table.Column<string>(maxLength: 100, nullable: true),
                    building = table.Column<string>(maxLength: 100, nullable: false),
                    room = table.Column<string>(maxLength: 100, nullable: true),
                    default_location = table.Column<string>(maxLength: 100, nullable: true),
                    cooling_temperature_max = table.Column<int>(nullable: true),
                    cooling_temperature_min = table.Column<int>(nullable: true),
                    cooling_temperature_unit = table.Column<string>(maxLength: 3, nullable: true),
                    xmin = table.Column<uint>(type: "xid", nullable: true),
                    checked_out_item_count = table.Column<int>(nullable: false),
                    checked_in_item_count = table.Column<int>(nullable: false),
                    discarded_item_count = table.Column<int>(nullable: false),
                    items_to_be_checked_out_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stock_items",
                schema: "inventory",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<string>(maxLength: 100, nullable: true),
                    changed_on = table.Column<DateTime>(nullable: true),
                    changed_by = table.Column<string>(maxLength: 100, nullable: true),
                    tenant_id = table.Column<int>(nullable: false),
                    store_id = table.Column<int>(nullable: false),
                    barcode = table.Column<string>(maxLength: 100, nullable: false),
                    compartment = table.Column<string>(maxLength: 100, nullable: true),
                    department = table.Column<string>(maxLength: 100, nullable: true),
                    inventory_base_unit_value = table.Column<decimal>(nullable: true),
                    inventory_base_unit = table.Column<string>(maxLength: 50, nullable: true),
                    inventory_display_unit = table.Column<string>(maxLength: 50, nullable: true),
                    minimum_inventory_base_unit_value = table.Column<decimal>(nullable: true),
                    minimum_inventory_base_unit = table.Column<string>(maxLength: 50, nullable: true),
                    minimum_inventory_display_unit = table.Column<string>(maxLength: 50, nullable: true),
                    note = table.Column<string>(nullable: true),
                    responsible_owner_employee_id = table.Column<string>(maxLength: 400, nullable: true),
                    responsible_owner_name = table.Column<string>(maxLength: 400, nullable: true),
                    responsible_owner_phone_number = table.Column<string>(maxLength: 200, nullable: true),
                    responsible_owner_department = table.Column<string>(maxLength: 400, nullable: true),
                    xmin = table.Column<uint>(type: "xid", nullable: true),
                    reservation_employee_id = table.Column<string>(maxLength: 400, nullable: true),
                    reservation_name = table.Column<string>(maxLength: 400, nullable: true),
                    reservation_phone_number = table.Column<string>(maxLength: 200, nullable: true),
                    reservation_department = table.Column<string>(maxLength: 400, nullable: true),
                    reservation_reserved_through_order_id = table.Column<int>(nullable: true),
                    reservation_reserved_on = table.Column<DateTime>(nullable: true),
                    status = table.Column<string>(maxLength: 15, nullable: false),
                    transferred_for_employee_id = table.Column<string>(maxLength: 400, nullable: true),
                    transferred_for_name = table.Column<string>(maxLength: 400, nullable: true),
                    transferred_for_phone_number = table.Column<string>(maxLength: 200, nullable: true),
                    transferred_for_department = table.Column<string>(maxLength: 400, nullable: true),
                    catalog_item_supplier_id = table.Column<int>(nullable: true),
                    catalog_item_supplier_name = table.Column<string>(maxLength: 100, nullable: true),
                    catalog_item_supplier_url = table.Column<string>(maxLength: 2000, nullable: true),
                    catalog_item_supplier_phone = table.Column<string>(maxLength: 100, nullable: true),
                    catalog_item_supplier_email = table.Column<string>(maxLength: 200, nullable: true),
                    catalog_item_supplier_city = table.Column<string>(maxLength: 200, nullable: true),
                    catalog_item_supplier_country = table.Column<string>(maxLength: 200, nullable: true),
                    catalog_item_catalog_name = table.Column<string>(maxLength: 100, nullable: true),
                    catalog_item_batch = table.Column<string>(maxLength: 100, nullable: true),
                    catalog_item_description = table.Column<string>(nullable: true),
                    catalog_item_item_number = table.Column<string>(nullable: true),
                    catalog_item_lead_time = table.Column<string>(maxLength: 100, nullable: true),
                    catalog_item_package_amount_base_unit_value = table.Column<decimal>(nullable: true),
                    catalog_item_package_amount_base_unit = table.Column<string>(maxLength: 50, nullable: true),
                    catalog_item_package_amount_display_unit = table.Column<string>(maxLength: 50, nullable: true),
                    catalog_item_package_description = table.Column<string>(maxLength: 100, nullable: true),
                    catalog_item_price_amount = table.Column<decimal>(nullable: true),
                    catalog_item_price_currency = table.Column<string>(maxLength: 5, nullable: true),
                    catalog_item_url = table.Column<string>(maxLength: 2000, nullable: true),
                    product_description = table.Column<string>(nullable: true),
                    product_name = table.Column<string>(maxLength: 2000, nullable: true),
                    product_producer = table.Column<string>(maxLength: 100, nullable: true),
                    product_producer_item_number = table.Column<string>(maxLength: 100, nullable: true),
                    product_purity = table.Column<decimal>(nullable: true),
                    product_url = table.Column<string>(maxLength: 2000, nullable: true),
                    product_unspsc = table.Column<string>(maxLength: 10, nullable: true),
                    substance_anicors_registry_number = table.Column<int>(nullable: true),
                    substance_average_mol_weight = table.Column<decimal>(nullable: true),
                    substance_cas_registry_number = table.Column<string>(maxLength: 100, nullable: true),
                    substance_density_base_unit_value = table.Column<decimal>(nullable: true),
                    substance_density_base_unit = table.Column<string>(maxLength: 50, nullable: true),
                    substance_density_display_unit = table.Column<string>(maxLength: 50, nullable: true),
                    substance_description = table.Column<string>(nullable: true),
                    substance_formula = table.Column<string>(maxLength: 100, nullable: true),
                    substance_hazard_pictograms = table.Column<string>(nullable: true),
                    substance_hazard_statements = table.Column<string>(nullable: true),
                    substance_is_narcotic = table.Column<bool>(nullable: true),
                    substance_is_radioactive = table.Column<bool>(nullable: true),
                    substance_iupac_name = table.Column<string>(maxLength: 500, nullable: true),
                    substance_mol_file = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_stock_items_stores_store_id",
                        column: x => x.store_id,
                        principalSchema: "inventory",
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "item_transactions",
                schema: "inventory",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<string>(maxLength: 100, nullable: true),
                    changed_on = table.Column<DateTime>(nullable: true),
                    changed_by = table.Column<string>(maxLength: 100, nullable: true),
                    actor_employee_id = table.Column<string>(nullable: true),
                    actor_name = table.Column<string>(nullable: true),
                    actor_phone_numer = table.Column<string>(nullable: true),
                    actor_department = table.Column<string>(nullable: true),
                    fulfilled_on = table.Column<DateTime>(nullable: false),
                    requested_on = table.Column<DateTime>(nullable: true),
                    note = table.Column<string>(nullable: true),
                    stock_item_id = table.Column<int>(nullable: false),
                    inventory_before_base_unit_value = table.Column<decimal>(nullable: true),
                    inventory_before_base_unit = table.Column<string>(nullable: true),
                    inventory_before_display_unit = table.Column<string>(nullable: true),
                    inventory_now_base_unit_value = table.Column<decimal>(nullable: true),
                    inventory_now_base_unit = table.Column<string>(nullable: true),
                    inventory_now_display_unit = table.Column<string>(nullable: true),
                    discriminator = table.Column<string>(nullable: false),
                    xmin = table.Column<uint>(type: "xid", nullable: false),
                    recipient_employee_id = table.Column<string>(nullable: true),
                    recipient_name = table.Column<string>(nullable: true),
                    recipient_phone_numer = table.Column<string>(nullable: true),
                    recipient_department = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_item_transactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_item_transactions_stock_items_stock_item_id",
                        column: x => x.stock_item_id,
                        principalSchema: "inventory",
                        principalTable: "stock_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_item_transactions_stock_item_id",
                schema: "inventory",
                table: "item_transactions",
                column: "stock_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_items_store_id",
                schema: "inventory",
                table: "stock_items",
                column: "store_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "item_transactions",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "stock_items",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "stores",
                schema: "inventory");
        }
    }
}
