using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PaymentService
{
    [ServiceContract]
    public interface PaymentGateway
    {
        [OperationContract]
        OrderStatus CheckOrderStatus(int orderid);
    }

    [DataContract]
    public class OrderStatus
    {
        int orderid;
        string status;

        [DataMember]
        public int OrderId
        {
            get { return orderid; }
            set { orderid = value; }
        }

        [DataMember]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
