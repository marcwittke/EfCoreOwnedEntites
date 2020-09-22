using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NpgsqlWithOwnedEntities.Lib;

namespace NpgsqlWithOwnedEntities.Stores
{
    public enum TemperatureRangeUnit
    {
        C,
        F
    }

    public class TemperatureRange : ValueObject
    {
        [UsedImplicitly]
        private TemperatureRange()
        { }

        public TemperatureRange(int min, int max, TemperatureRangeUnit unit)
        {
            if (min > max)
            {
                throw new Exception($"Invalid temperature range: minimum value {min} is greater than {max}");
            }

            Min = min;
            Max = max;
            Unit = unit;
        }

        public int? Max { get; set; }

        public int? Min { get; set; }
        public TemperatureRangeUnit Unit { get; set; }

       

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Max;
            yield return Min;
            yield return Unit;
        }
    }
}