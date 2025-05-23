using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Orders.Core.ValueObjects
{
    public class PaymentInfo
    {
        public PaymentInfo(string cardNumber, string fullName, string cvv, string expireAt)
        {
            CardNumber = cardNumber;
            FullName = fullName;
            Cvv = cvv;
            ExpireAt = expireAt;
        }

        public string CardNumber { get; private set; }
        public string FullName { get; private set; }
        public string Cvv { get; private set; }
        public string ExpireAt { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is PaymentInfo info &&
                   CardNumber == info.CardNumber &&
                   FullName == info.FullName &&
                   Cvv == info.Cvv &&
                   ExpireAt == info.ExpireAt;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CardNumber, FullName, Cvv, ExpireAt);
        }
    }
}