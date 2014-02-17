using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentServiceClient.PaymentService;

namespace PaymentServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new PaymentGatewayClient();
            var result = client.CheckOrderStatus(10);
            Console.WriteLine(result.Status);
        }
    }
}
