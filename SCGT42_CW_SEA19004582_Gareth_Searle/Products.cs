using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWW
{
    internal class Products //used for creating products
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public Products(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Status = true;
        }

        public override string? ToString()
        {
            return $"Product ID: {Id}\nProduct Name: {Name}\nAvailibility: {Status}";
        }
    }
}

