using System;
using System.Collections.Generic;
using NpgsqlWithOwnedEntities.Lib;

namespace NpgsqlWithOwnedEntities.StockItems
{
    public class SupplierInfo : ValueObject
    {
        public SupplierInfo(int id, string name, string url, string phone, string email, string city, string country)
        {
            Id = id;
            Name = name;
            Url = url;
            Phone = phone;
            Email = email;
            City = city;
            Country = country;
        }

        public SupplierInfo()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        private static string Sanitize(string companyName)
        {
            var invalidChars = new[]
                {'!', '"', '§', '$', '%', '&', '/', '(', ')', '=', '?', '*', '+', '#', '~', '@', '^', '°', '\\', '.'};
            foreach (char invalidChar in invalidChars)
            {
                companyName = companyName.Replace(invalidChar, '#');
            }

            var replacements = new[]
            {
                new Tuple<string, string>("ä", "ae"),
                new Tuple<string, string>("ö", "oe"),
                new Tuple<string, string>("ü", "ue"),
                new Tuple<string, string>("ß", "ss"),
                new Tuple<string, string>("#", ""),
                new Tuple<string, string>(" ", "-")
            };

            foreach ((string badChars, string goodChars) in replacements)
            {
                companyName = companyName.Replace(badChars, goodChars);
            }

            return companyName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
            yield return Url;
            yield return Phone;
            yield return Email;
            yield return City;
            yield return Country;
        }
    }
}