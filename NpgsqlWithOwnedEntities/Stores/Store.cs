using System;
using JetBrains.Annotations;
using NpgsqlWithOwnedEntities.Lib;

namespace NpgsqlWithOwnedEntities.Stores
{
    public class Store : AggregateRoot
    {
        [UsedImplicitly]
        private Store()
        { }

        public Store(int id,
                     [NotNull] string name,
                     string site,
                     [NotNull] string building,
                     string room,
                     string defaultLocation,
                     TemperatureRange coolingTemperatureRange)
            : base(id)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Site = site;
            Building = building ?? throw new ArgumentNullException(nameof(building));
            Room = room;
            DefaultLocation = defaultLocation;
            CoolingTemperature = coolingTemperatureRange;
        }

        public string Name { get; private set; }

        public string Site { get; private set; }

        public string Building { get; private set; }

        public string Room { get; private set; }

        public string DefaultLocation { get; private set; }

        public TemperatureRange CoolingTemperature { get; private set; }

        public int CheckedOutItemCount { get; private set; }
        public int CheckedInItemCount { get; private set; }
        public int DiscardedItemCount { get; private set; }
        public int ItemsToBeCheckedOutCount { get; set; }

        
    }
}