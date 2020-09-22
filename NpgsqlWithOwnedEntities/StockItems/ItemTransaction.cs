using System;
using JetBrains.Annotations;
using NpgsqlWithOwnedEntities.Lib;

namespace NpgsqlWithOwnedEntities.StockItems
{
    public class CheckingOut : ItemTransaction
    {
        public CheckingOut(int id, Employee actor, DateTime fulfilledOn, DateTime? requestedOn, Employee recipient, StockItem stockItem, string note)
            : base(id, actor, fulfilledOn, requestedOn, note, stockItem)
        {
            Recipient = recipient;
        }

        [UsedImplicitly]
        private CheckingOut()
        { }

        [CanBeNull]
        public string RecipientEmployeeId { get; set; }

        [CanBeNull]
        public string RecipientName { get; set; }

        [CanBeNull]
        public string RecipientPhoneNumer { get; set; }

        [CanBeNull]
        public string RecipientDepartment { get; set; }

        public Employee Recipient
        {
            get => new Employee(RecipientEmployeeId, RecipientName, RecipientPhoneNumer, RecipientDepartment);

            set
            {
                RecipientEmployeeId = value.EmployeeId;
                RecipientName = value.Name;
                RecipientPhoneNumer = value.PhoneNumber;
                RecipientDepartment = value.Department;
            }
        }
    }

    
    public class CheckingIn : ItemTransaction
    {
        public CheckingIn(
            int id,
            Employee actor,
            DateTime fulfilledOn,
            StockItem stockItem, string note)
            : base(id, actor, fulfilledOn, null, note, stockItem)
        { }

        [UsedImplicitly]
        private CheckingIn()
        { }
    }
    
        public abstract class ItemTransaction : Entity
    {
        protected ItemTransaction(int id, [NotNull] Employee actor, DateTime fulfilledOn, DateTime? requestedOn,
                                  [CanBeNull] string note, [NotNull] StockItem stockItem)
            : base(id)
        {
            if (actor == null)
            {
                throw new ArgumentNullException(nameof(actor));
            }

            if (stockItem == null)
            {
                throw new ArgumentNullException(nameof(stockItem));
            }

            ActorEmployeeId = actor.EmployeeId;
            ActorName = actor.Name;
            ActorPhoneNumer = actor.PhoneNumber;
            ActorDepartment = actor.Department;
            FulfilledOn = fulfilledOn;
            RequestedOn = requestedOn;
            Note = note;
            StockItemId = stockItem.Id;
            StockItem = stockItem;
        }

        [UsedImplicitly]
        protected ItemTransaction()
        { }

        public Employee Actor => new Employee(ActorEmployeeId, ActorName, ActorPhoneNumer, ActorDepartment);

        [CanBeNull]
        public string ActorEmployeeId { get; set; }

        [CanBeNull]
        public string ActorName { get; set; }

        [CanBeNull]
        public string ActorPhoneNumer { get; set; }

        [CanBeNull]
        public string ActorDepartment { get; set; }

        public DateTime FulfilledOn { get; set; }
        public DateTime? RequestedOn { get; set; }
        public string Note { get; set; }
        public int StockItemId { get; set; }

        [UsedImplicitly]
        public StockItem StockItem { get; set; }

        public decimal? InventoryBeforeBaseUnitValue { get; set; }
        public string InventoryBeforeBaseUnit { get; set; }
        public string InventoryBeforeDisplayUnit { get; set; }

        
        public decimal? InventoryNowBaseUnitValue { get; set; }
        public string InventoryNowBaseUnit { get; set; }
        public string InventoryNowDisplayUnit { get; set; }
    }

}