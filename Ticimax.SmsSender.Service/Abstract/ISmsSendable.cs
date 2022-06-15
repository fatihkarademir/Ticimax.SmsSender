using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticimax.SmsSender.Service.Abstract
{
    public interface ISmsSendable
    {
        public string SmsGonder(BaseServiceProvider serviceProvider);
    }
}
