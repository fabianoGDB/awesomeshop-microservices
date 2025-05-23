using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Orders.Core.Entitties
{
    public interface IEntityBase
    {
        Guid Id { get; }
    }
}