using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NpgsqlWithOwnedEntities.Lib;

namespace NpgsqlWithOwnedEntities.StockItems
{
    public class Reservation : ValueObject
    {
        public Reservation(string employeeId, string name, string phoneNumber, string department, int? reservedThroughOrderId, DateTime? reservedOn)
        {
            EmployeeId = employeeId;
            Name = name;
            PhoneNumber = phoneNumber;
            Department = department;
            ReservedThroughOrderId = reservedThroughOrderId;
            ReservedOn = reservedOn;
        }


        [UsedImplicitly]
        private Reservation()
        { }

        [CanBeNull]
        public string EmployeeId { get; set; }

        [CanBeNull]
        public string Name { get; set; }

        [CanBeNull]
        public string PhoneNumber { get; set; }

        [CanBeNull]
        public string Department { get; set; }

        public int? ReservedThroughOrderId { get; set; }
        public DateTime? ReservedOn { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmployeeId;
            yield return Name;
            yield return PhoneNumber;
            yield return Department;
        }
    }

}