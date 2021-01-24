using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace BasaDate.Models
{
    public class CourseAPI
    {
        public Parser GetParser() {
            string url = "https://www.cbr-xml-daily.ru/daily_json.js";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream())) {
                response = streamReader.ReadToEnd();
            }
            Parser parser = JsonConvert.DeserializeObject<Parser>(response);
            return parser; 
            
        }
    }
}