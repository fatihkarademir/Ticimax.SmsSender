using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ticimax.SmsSender.Service.Abstract;

namespace Ticimax.SmsSender.Service
{
    public class MessageSender : ISmsSendable
    {
        public string SmsGonder(BaseServiceProvider serviceProvider)
        {
            string returnValue = string.Empty;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            returnValue = serviceProvider.SendSms(serviceProvider.SpRequestModel);

            return returnValue;
        }
    }
}
