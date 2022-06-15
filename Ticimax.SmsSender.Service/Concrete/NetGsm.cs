using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticimax.SmsSender.Models;
using Ticimax.SmsSender.Service.Abstract;

namespace Ticimax.SmsSender.Service.Concrete
{
    public class NetGsm : BaseServiceProvider
    {
        private const string requestUrl = "http://api.guventelekom.net:8080/api/smspost/v1";

        public override string SendSms(ServiceProviderRequestModel spRequestModel)
        {
            var ayarlar = AppSettingsHelper.GetAppSettingsModel();

            string returnValue = string.Empty;
            string requestXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                            <mainbody>
                                <header>
                                    <company dil=""TR"" bayikodu=""11111"">Ticimax</company>
                                    <usercode>" + ayarlar.Kullanici + @"</usercode>
                                    <password>" + ayarlar.Sifre + @"</password>
                                    <startdate></startdate>
                                    <stopdate></stopdate>
                                    <type>1:n</type>
                                    <msgheader>" + ayarlar.Orginator + @"</msgheader>
                                </header>
                                <body>
                                    <msg><![CDATA[" + spRequestModel.Message + @"]]></msg>
                                    <no>" + spRequestModel.Telephone + @"</no>
                                </body>
                            </mainbody>";
            returnValue = WebRequestHelper.CreateWebRequest(requestUrl, requestXml, "POST", "application/x-www-form-urlencoded", new List<KeyValuePair<string, string>>());
            return returnValue;
        }
    }
}
