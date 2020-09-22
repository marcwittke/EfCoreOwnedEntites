using System;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlWithOwnedEntities.StockItems;
using NpgsqlWithOwnedEntities.Stores;
using Xunit;
using Xunit.Abstractions;

namespace NpgsqlWithOwnedEntities.Tests
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private static int nextId = 123;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            using (var conn = new NpgsqlConnection("Host=localhost;Username=anicors;Password=P0stgr3s;"))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DROP DATABASE IF EXISTS testdb";
                    cmd.ExecuteNonQuery();
                }
            }
            
            using (var dbc = new MyDbContext())
            {
                dbc.Database.Migrate();
            }
        }

        [Fact]
        public void Test1()
        {
            try
            {
                using (var dbc = new MyDbContext())
                {
                    int existingStoreCount = dbc.Stores.Count();

                    for (var i = 0; i < 10 - existingStoreCount; i++)
                    {
                        TemperatureRange temperaturRange = new TemperatureRange(1, 2, TemperatureRangeUnit.C);
                        var store = new Store(
                            nextId++,
                            "Store " + new Random().Next(10000, 90000),
                            "Site " + new Random().Next(1000, 9999),
                            "Building " + new Random().Next(10000, 90000),
                            "Room " + new Random().Next(10000, 90000),
                            "Default Location " + new Random().Next(10000, 90000),
                            temperaturRange
                        );
                        dbc.Add(store);
                    }

                    dbc.SaveChanges();
                }

                int stockItemId;
using (var dbc = new MyDbContext())
{
    var store = dbc.Stores.ToArray()[0];
    var stockItem = GenerateStockItem(nextId++, store.Id, store.Name);
    dbc.Add(stockItem);
    dbc.SaveChanges();
    
    Assert.Null(stockItem.Reservation);
    stockItemId = stockItem.Id;
}

using (var dbc = new MyDbContext())
{
    var stockItem = dbc.Find<StockItem>(stockItemId);
    Assert.Null(stockItem.Reservation);
}
            }
            catch (Exception ex)
            {
                _testOutputHelper.WriteLine(ex.ToString());
                throw;
            }
        }
        
        public static StockItem GenerateStockItem(int id, int storeId, string storeName, Substance substance = null)
        {
            substance ??= DemoSubstances.Cyclohexane;

            var product = new Product
            {
                Description = substance.Description,
                Name = substance.IupacName,
                Producer = "ACME Corp.",
                Purity = 21,
                ProducerItemNumber = "34.5435.2343124",
                Unspsc = "3454231",
                Url = "http://www.googlw.com?q="+substance.IupacName
            };
            var catalogItem = new CatalogItemInfo
            {
                Batch = "wg",
                CatalogName = "dgdfg",
                Description =  "dgdfg",
                 ItemNumber= "dgdfg",
                LeadTime = "dgdfg",
                PackageDescription = "dgdfg",
                Supplier = new SupplierInfo
                {
                    Name = "supp"
                },
                Url = "dgdfg",
                PriceCurrency = "EUR"
            };
            
            var stockItem = new StockItem(
                id,
                storeId,
                storeName,
                "345345345",
                "2354234234",
                "CHEM",
                "inventory",
                null,
                substance,
                product,
                catalogItem);
            return stockItem;
        }
    }
}