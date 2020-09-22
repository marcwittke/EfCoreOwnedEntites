using System.Collections.Generic;
using NpgsqlWithOwnedEntities.Lib;

namespace NpgsqlWithOwnedEntities.StockItems
{
        public class CatalogItemInfo : ValueObject
    {
        public CatalogItemInfo()
        { }

        public CatalogItemInfo(SupplierInfo supplier, string catalogName, string batch, string description, string itemNumber,
                               string leadTime, string packageDescription, string url)
        {
            Supplier = supplier;
            CatalogName = catalogName;
            Batch = batch;
            Description = description;
            ItemNumber = itemNumber;
            LeadTime = leadTime;
            PackageDescription = packageDescription;
            Url = url;
        }

        public SupplierInfo Supplier { get; set; }
        public string CatalogName { get; set; }
        public string Batch { get; set; }
        public string Description { get; set; }
        public string ItemNumber { get; set; }
        public string LeadTime { get; set; }

        

        public decimal? PackageAmountBaseUnitValue { get; set; }
        public string PackageAmountBaseUnit { get; set; }
        public string PackageAmountDisplayUnit { get; set; }
        public string PackageDescription { get; set; }

        

        public decimal? PriceAmount { get; set; }
        public string PriceCurrency { get; set; }
        public string Url { get; set; }

       

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Batch;
            yield return CatalogName;
            yield return Description;
            yield return ItemNumber;
            yield return LeadTime;
            yield return PackageDescription;
            yield return PriceAmount;
            yield return PriceCurrency;
            yield return Supplier;
            yield return Url;
        }
    }

}