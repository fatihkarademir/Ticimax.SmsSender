using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticimax.SmsSender.Models;
using Ticimax.SmsSender.Service.Abstract;

namespace Ticimax.SmsSender.Service.Concrete
{
    public class Telsam : BaseServiceProvider
    {
        private const string requestUrl = "http://websms.telsam.com.tr/xmlapi/sendsms";

        public override string SendSms(ServiceProviderRequestModel spRequestModel)
        {
            var ayarlar = AppSettingsHelper.GetAppSettingsModel();
            string returnValue = string.Empty;
            var tel = TelsamTelFix(spRequestModel.Telephone);
            string requestXml = @"<?xml version=""1.0""?>
                                <SMS>
                                  <authentication>
                                    <username>" + ayarlar.Kullanici + @"</username>
                                    <password>" + ayarlar.Sifre + @"</password>
                                  </authentication>
                                  <message>
                                    <originator>" + ayarlar.Orginator + @"</originator>
                                    <text>" + spRequestModel.Message + @"</text>
                                    <unicode></unicode>
                                    <international></international>
                                    <canceltext></canceltext>
                                  </message>
                                  <receivers>
                                    <receiver>" + tel + @"</receiver>
                                  </receivers>
                                </SMS>";
            returnValue = WebRequestHelper.CreateWebRequest(requestUrl, requestXml, "POST", "application/x-www-form-urlencoded", new List<KeyValuePair<string, string>>());
            return returnValue;
        }

        #region Private Methods

        private string TelsamTelFix(string telefon)
        {
            if (telefon.Length == 12)
            {
                telefon = telefon.Substring(2, 10);
            }
            else if (telefon.Length == 11)
            {
                telefon = telefon.Substring(1, 10);
            }
            return telefon;
        }

        #endregion

    }
}
