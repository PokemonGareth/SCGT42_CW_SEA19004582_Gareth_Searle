using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWW
{
    internal class Account // used to create an Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Account(
                      string name,
                      string email,
                      string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Address = address;
        }

        public override string? ToString()
        {
            return $"Account ID: { Id }\nDriver Name: {Name}\nEmail: { Email }\nAddress: {Address}";
        }
    }
}
