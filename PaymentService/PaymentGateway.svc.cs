using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PaymentService
{
    public class PaymentGatewayImpl : PaymentGateway
    {
        public OrderStatus CheckOrderStatus(int orderid)
        {
            return new OrderStatus { OrderId = orderid, Status = "Succeed" };
        }
    }
}
