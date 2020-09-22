using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using NpgsqlWithOwnedEntities.Lib;

namespace NpgsqlWithOwnedEntities.StockItems
{
    public class StockItem : AggregateRoot
    {

        [UsedImplicitly]
        protected StockItem()
        { }

        public StockItem(int id, int storeId, string storeName, string barcode, string compartment, string department, 
            string note, Employee responsibleOwner, Substance substance, [NotNull] Product product, CatalogItemInfo catalogItem)
            : base(id)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            StoreId = storeId;
            StoreName = storeName;

            Barcode = barcode;
            Compartment = compartment;
            Department = department;
            Note = note;
            ResponsibleOwner = responsibleOwner;

            Substance = substance == null
                            ? null
                            : new Substance(substance);
            Product = new Product(product);
            CatalogItem = catalogItem;

            Status = StockItemStatus.New;
        }


        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Barcode { get; set; }
        public string Compartment { get; set; }
        public string Department { get; set; }

        

        public decimal? InventoryBaseUnitValue { get; set; }
        public string InventoryBaseUnit { get; set; }
        public string InventoryDisplayUnit { get; set; }

        

        public decimal? MinimumInventoryBaseUnitValue { get; set; }
        public string MinimumInventoryBaseUnit { get; set; }
        public string MinimumInventoryDisplayUnit { get; set; }
        public string Note { get; set; }
        public Employee ResponsibleOwner { get; set; }
        public Reservation Reservation { get; set; }
        public StockItemStatus Status { get; set; }
        public Employee TransferredFor { get; set; }

        public CatalogItemInfo CatalogItem { get; set; }
        public virtual Product Product { get; set; }

        [CanBeNull]
        public virtual Substance Substance { get; set; }

        public ISet<ItemTransaction> ItemTransactions { get; } = new SortedSet<ItemTransaction>(new ItemTransactionSortComparer());

    }

    public class ItemTransactionSortComparer : IComparer<ItemTransaction>
    {
        public int Compare(ItemTransaction x, ItemTransaction y)
        {
            return (x?.FulfilledOn ?? DateTime.MinValue).CompareTo(y?.FulfilledOn ?? DateTime.MinValue);
        }
    }
}