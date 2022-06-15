using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ticimax.SmsSender.Models;
using Ticimax.SmsSender.Service.Abstract;

namespace Ticimax.SmsSender.Service.Concrete
{
    public class GuvenTelekom : BaseServiceProvider
    {
        private const string requestUrl = "http://api.guventelekom.net:8080/api/smspost/v1";

        public override string SendSms(ServiceProviderRequestModel spRequestModel)
        {

            string returnValue = string.Empty;

            HttpWebRequest request = WebRequest.Create(new Uri(requestUrl)) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 5000;
            byte[] data = UTF8Encoding.UTF8.GetBytes(CreateXml(spRequestModel.Telephone, spRequestModel.Message)); request.ContentLength = data.Length;
            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(data, 0, data.Length);
            }
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                returnValue = reader.ReadToEnd();
            }
            return returnValue;
        }

        #region Private Methods

        private string CreateXml(string tel, string mesaj)
        {
            var ayarlar = AppSettingsHelper.GetAppSettingsModel();

            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;
            settings.Indent = true;
            settings.IndentChars = ("	");
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                writer.WriteStartElement("sms");
                writer.WriteElementString("username", ayarlar.Kullanici);
                writer.WriteElementString("password", ayarlar.Sifre);
                writer.WriteElementString("header", ayarlar.Orginator);
                writer.WriteElementString("validity", "2880");
                writer.WriteStartElement("message");
                writer.WriteStartElement("gsm");
                writer.WriteElementString("no", tel);
                writer.WriteEndElement(); //gsm
                writer.WriteStartElement("msg");
                //writer.WriteCData(Statik.ReplaceTRChar(mesaj));
                writer.WriteEndElement(); //msg 
                writer.WriteEndElement(); //message 
                writer.WriteEndElement(); // sms 
                writer.Flush();
            }
            return sb.ToString();
        }
        #endregion

    }
}
