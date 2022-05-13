
using Lombard.Domain.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lombard.Helpers
{
    public class ConnectToApis<T> where T : class
    {
        public string BaseUrl { get; set; }
        public HttpClient client { get; set; }

        public ConnectToApis(string _baseUrl)
        {           
            BaseUrl = _baseUrl;
        }
 

        public async Task<T> PostUserForLogin(string postAsyncUrl, LoginVm user, string apiKey, string token = null)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("api-key", apiKey);
                if (token != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                }
                var data = JsonConvert.SerializeObject(user);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(postAsyncUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<T>(result);
                    return res;
                }
                else
                {
                    throw new Exception();
                }
            }
        }
 


    }
}