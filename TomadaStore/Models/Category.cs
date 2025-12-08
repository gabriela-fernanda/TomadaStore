using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomadaStore.Models.Models
{
    public class Category
    {
        public ObjectId Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Category() { }

        public Category(
                        string name,
                        string description)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            Description = description;
        }
    }
}
