using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticimax.SmsSender.Models;
using Ticimax.SmsSender.Service.Abstract;

namespace Ticimax.SmsSender.Service.Concrete
{
    public class SmartMessage : BaseServiceProvider
    {
        private const string requestUrl = "http://api2.smartmessage-engage.com/GET/SMS";

        public override string SendSms(ServiceProviderRequestModel spRequestModel)
        {
            var ayarlar = AppSettingsHelper.GetAppSettingsModel();

            string returnValue = string.Empty;
            List<string> Params = new List<string>();
            Params.Add("UserName=" + ayarlar.Kullanici);
            Params.Add("Password=" + ayarlar.Sifre);
            Params.Add("JobId=" + ayarlar.Orginator.Split('|')[1]);
            Params.Add("Message=" + spRequestModel.Message);
            Params.Add("MobilePhone=" + spRequestModel.Telephone);
            Params.Add("CustomerNo=" + ayarlar.Orginator.Split('|')[0]);
            Params.Add("PlannedSendingDate=" + DateTime.Now.AddMinutes(1));
            string postData = String.Join("&", Params.ToArray());
            returnValue = WebRequestHelper.CreateWebRequest(requestUrl, postData, "GET", "", new List<KeyValuePair<string, string>>());
            return returnValue;
        }
    }
}
