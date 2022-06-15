using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ticimax.SmsSender.Models;
using Ticimax.SmsSender.Service.Abstract;

namespace Ticimax.SmsSender.Service.Concrete
{
    public class JetMesaj : BaseServiceProvider
    {
        private const string requestUrl = "http://92.42.35.50:16899/smswebservice.asmx";

        public override string SendSms(ServiceProviderRequestModel spRequestModel)
        {
            var ayarlar = AppSettingsHelper.GetAppSettingsModel();

            string returnValue = "";
            string soapStr = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                  <soap:Body>
                                    <SmsGonder xmlns=""http://tempuri.org/"">
                                      <kullaniciAd>" + ayarlar.Kullanici + @"</kullaniciAd>
                                      <parola>" + ayarlar.Sifre + @"</parola>
                                      <gsmNo>
                                        <string>" + spRequestModel.Telephone + @"</string>
                                      </gsmNo>
                                      <smsText>
                                        <string>" + spRequestModel.Message + @"</string>
                                      </smsText>
                                      <gonderimTarihi>" + DateTime.Now.ToString("ddMMyyyyHHmmss") + @"</gonderimTarihi>
                                      <alfaNumeric>" + ayarlar.Orginator + @"</alfaNumeric>
                                      <chargedNumber></chargedNumber>
                                      <multiSms>false</multiSms>
                                    </SmsGonder>
                                  </soap:Body>
                                </soap:Envelope>";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(requestUrl);
            req.Headers.Add("SOAPAction", "\"http://tempuri.org/SmsGonder\"");
            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "text/xml";
            req.Method = "POST";

            using (Stream stm = req.GetRequestStream())
            {
                ;
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(soapStr);
                }
            }

            using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                string result = responseReader.ReadToEnd();
                XDocument ResultXML = XDocument.Parse(result);
                returnValue = ResultXML.Descendants(XName.Get("SmsGonderResponse", "http://tempuri.org/")).FirstOrDefault().Element(XName.Get("SmsGonderResult", "http://tempuri.org/")).Value.Split(':')[0];
            }
            return returnValue;
        }
    }
}
