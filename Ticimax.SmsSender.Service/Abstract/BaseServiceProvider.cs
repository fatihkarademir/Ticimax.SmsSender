using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticimax.SmsSender.Models;

namespace Ticimax.SmsSender.Service.Abstract
{
    public abstract class BaseServiceProvider
    {
        public ServiceProviderRequestModel SpRequestModel { get; private set; }

        public abstract string SendSms(ServiceProviderRequestModel spRequestModel);

        public ServiceProviderRequestModel SetSpRequestModel(string telephone, string message)
        {
          
            if (!string.IsNullOrEmpty(telephone))
            {
                telephone = telephone.Replace("+", string.Empty).Replace(" ", string.Empty);

                if (telephone.Length == 10)
                {
                    telephone = "90" + telephone;
                }
                else if (telephone.Length == 11)
                {
                    telephone = "9" + telephone;
                }
            }

            SpRequestModel = new ServiceProviderRequestModel()
            {
                Telephone = telephone,
                Message = message
            };

            return SpRequestModel;
        }
    }
}
