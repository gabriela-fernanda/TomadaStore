using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomadaStore.Models.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public decimal Price { get;  set; }
        public Category Category { get;  set; }

        public Product() { }

        public Product(string name, string description, decimal price, Category category)
        {
            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }

        public Product(string id,
                        string name,
                        string description,
                        decimal price,
                        Category category)
        {
            Id = ObjectId.Parse(id).ToString();
            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }
    }
}
