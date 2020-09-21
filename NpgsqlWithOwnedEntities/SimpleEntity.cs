using System;

namespace NpgsqlWithOwnedEntities
{
    public class SimpleEntity : Entity
    {
        public string Name { get; set; }

        public DateTime Timestamp { get; set; }

        //public OwnedOne OwnedOne { get; set; }
    }
}