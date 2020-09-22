using System.Collections.Generic;
using JetBrains.Annotations;
using NpgsqlWithOwnedEntities.Lib;

namespace NpgsqlWithOwnedEntities.StockItems
{
    public class Employee : ValueObject
    {
        public Employee(string employeeId, string name, string phoneNumber, string department)
        {
            EmployeeId = employeeId;
            Name = name;
            PhoneNumber = phoneNumber;
            Department = department;
        }

        [UsedImplicitly]
        private Employee()
        { }

        [CanBeNull]
        public string EmployeeId { get; set; }

        [CanBeNull]
        public string Name { get; set; }

        [CanBeNull]
        public string PhoneNumber { get; set; }

        [CanBeNull]
        public string Department { get; set; }

        public Employee Delete()
        {
            EmployeeId = null;
            Name = null;
            PhoneNumber = null;
            Department = null;
            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmployeeId;
            yield return Name;
            yield return PhoneNumber;
            yield return Department;
        }
    }
}