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
    public class TFonTelekom : BaseServiceProvider
    {
        private const string requestUrl = "http://api2.ekomesaj.com/json/syncreply/SendInstantSms";

        public override string SendSms(ServiceProviderRequestModel spRequestModel)
        {
            var ayarlar = AppSettingsHelper.GetAppSettingsModel();
            var Credential = new
            {
                Username = ayarlar.Kullanici,
                Password = ayarlar.Sifre,
                ResellerID = 1111
            };
            var Sms = new
            {
                SmsCoding = "String",
                SenderName = ayarlar.Orginator,
                Route = 0,
                ValidityPeriod = 0,
                DataCoding = "Default",
                ToMsisdns = new
                {
                    Msisdn = spRequestModel.Telephone,
                    Name = "",
                    Surname = "",
                    CustomField1 = "",
                },
                ToGroups = new List<int>(),
                IsCreateFromTeplate = false,
                SmsTitle = ayarlar.Orginator,
                SmsContent = spRequestModel.Message,
                RequestGuid = "",
                CanSendSmsToDuplicateMsisdn = false,
                SmsSendingType = "ByNumber"
            };
            return WebRequestHelper.CreateWebRequest(requestUrl, JsonSerializer.Serialize(new { Credential, Sms }), "POST", "application/json");
        }
    }
}
