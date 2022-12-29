using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWW
{
    internal class Products
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Products(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public override string? ToString()
        {
            return $"Product ID: {Id}\nProduct Name: {Name}";
        }
    }
}

