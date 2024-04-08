using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class SendCode
    {
        private  string _code;
        private  string _mobile;
        public SendCode(string cobe , string mobile )
        {
            _code = cobe;
            _mobile = mobile;

        }
        public void Send()
        {
            var client = new RestClient("https://api2.ippanel.com/api/v1");
            var request = new RestRequest("https://api2.ippanel.com/api/v1/sms/pattern/normal/send", RestSharp.Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("apikey", "Ofh2i9NB7pMYW-7_5fMa5FJYYYwuifyHDBHg6ZKzSGA=");
            request.AddParameter("application/json", JsonConvert.SerializeObject(new
            {
                code = "7llwjtdei2c0z6k",
                sender = "+983000505",
                recipient = "+98" + _mobile,
                variable = new
                {
                    code = _code
                }
            }), ParameterType.RequestBody);
            var response = client.Execute(request);
        }
    }
}
