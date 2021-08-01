using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerribleCQRS.Orders.ValueTypes
{
    public enum OrderStatus
    {
        InProgress,
        PendingPayment,
        Paid,
        Complete
    }
}
