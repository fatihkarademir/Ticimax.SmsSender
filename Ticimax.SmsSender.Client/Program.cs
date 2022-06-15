using System;
using Ticimax.SmsSender.Service.Abstract;
using Ticimax.SmsSender.Service.Concrete;

namespace Ticimax.SmsSender.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseServiceProvider serviceProvider = new GuvenTelekom();
            serviceProvider.SetSpRequestModel("+905457153283", "test");
            
            Service.MessageSender messageSender = new Service.MessageSender();
            messageSender.SmsGonder(serviceProvider);

        }



    }
}
