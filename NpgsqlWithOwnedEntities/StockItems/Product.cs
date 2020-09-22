using System.Collections.Generic;
using JetBrains.Annotations;
using NpgsqlWithOwnedEntities.Lib;

namespace NpgsqlWithOwnedEntities.StockItems
{
        public class Product : ValueObject
    {
        public Product()
        { }

        public Product(string name, string description, string producer, string producerItemNumber, decimal? purity, string unspsc, string url)
        {
            Producer = producer;
            ProducerItemNumber = producerItemNumber;
            Purity = purity;
            Url = url;
            Unspsc = unspsc;
            Name = name;
            Description = description;
        }

        public Product(Product product)
        {
            Description = product.Description;
            Producer = product.Producer;
            ProducerItemNumber = product.ProducerItemNumber;
            Url = product.Url;
            Unspsc = product.Unspsc;
            Name = product.Name;
        }

        public string Description { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string ProducerItemNumber { get; set; }
        public decimal? Purity { get; set; }
        public string Url { get; set; }
        public string Unspsc { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Producer;
            yield return ProducerItemNumber;
            yield return Url;
            yield return Unspsc;
            yield return Name;
        }
    }

}