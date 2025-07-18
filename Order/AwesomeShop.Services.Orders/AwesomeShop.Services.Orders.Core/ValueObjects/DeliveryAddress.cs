using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Orders.Core.ValueObjects
{
    public class DeliveryAddress
    {
        public DeliveryAddress(string street, string number, string city, string state, string zipCode, string observation)
        {
            Street = street;
            Number = number;
            City = city;
            State = state;
            ZipCode = zipCode;
            Observation = observation;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
        public string Observation { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is DeliveryAddress address &&
                   Street == address.Street &&
                   Number == address.Number &&
                   City == address.City &&
                   State == address.State &&
                   ZipCode == address.ZipCode &&
                   Observation == address.Observation;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, Number, City, State, ZipCode, Observation);
        }
    }
}