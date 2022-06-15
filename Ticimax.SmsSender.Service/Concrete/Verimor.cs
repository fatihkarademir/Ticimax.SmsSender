using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ticimax.SmsSender.Models;
using Ticimax.SmsSender.Service.Abstract;

namespace Ticimax.SmsSender.Service.Concrete
{
    public class Verimor : BaseServiceProvider
    {
        private const string requestUrl = "http://sms.verimor.com.tr/v2/send.json";

        public override string SendSms(ServiceProviderRequestModel spRequestModel)
        {
            var ayarlar = AppSettingsHelper.GetAppSettingsModel();
            var Sms = new
            {
                username = ayarlar.Kullanici,
                password = ayarlar.Sifre,
                source_addr = ayarlar.Orginator,
                valid_for = "24:00",
                datacoding = "1",
                messages = new List<object>
                {
                    new
                    {
                       msg = spRequestModel.Message,
                       dest = spRequestModel.Telephone
                    }
                }
            };
            return WebRequestHelper.CreateWebRequest(requestUrl, JsonSerializer.Serialize(Sms), "POST", "application/json");
        }
    }
}
