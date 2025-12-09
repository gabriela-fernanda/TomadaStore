using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TomadaStore.Models.Models
{
    public class Sale
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get;  set; }
        public Customer Customer { get;  set; }
        public List<Product> Products { get;  set; }
        public DateTime SaleDate { get;  set; }
        public decimal TotalPrice { get;  set; }
        public bool? Aproved { get; set; }

        public Sale(Customer customer, List<Product> products, decimal totalPrice, bool? aproved)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Customer = customer;
            Products = products;
            SaleDate = DateTime.UtcNow;
            TotalPrice = totalPrice;
            Aproved = aproved;
        }
        public Sale() { }
    }
}
