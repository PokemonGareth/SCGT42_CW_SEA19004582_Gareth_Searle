using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWW
{
    internal class Products //used for creating products
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public Products(string id, string name, string status, string type)
        {
            Id = id;
            Name = name;
            Status = status;
            Type = type;
        }

        public override string? ToString()
        {
            return $"Product ID: {Id}\nProduct Name: {Name}\nAvailability: {Status}\nProduct Type: {Type}";
        }
    }
}

