using FalloutChat.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FalloutChat.Services
{
    //https://fnvbot.azurewebsites.net/
    //qnamaker/knowledgebases/beaa10bf-eb21-460a-99d3-b9478fff8627/generateAnswer
    //Body: {"question":model.questionsent}
    //Header: Authorization: EndpointKey b16864f1-71d1-4e94-ad72-c6220046789e
    public class QuestionBody
    {
        public string question { get; set; }
    }
    public class FNVBotService
    {
        string _baseUrl = "https://fnvbot.azurewebsites.net/";
        string _route = "qnamaker/knowledgebases/beaa10bf-eb21-460a-99d3-b9478fff8627/generateAnswer";
        string _auth = "b16864f1-71d1-4e94-ad72-c6220046789e";
        
        public ChatHistoryCreate SubmitQuestion(ChatHistoryCreate model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("EndpointKey", _auth);

                QuestionBody qb = new QuestionBody() { question = model.MessageSent };

                //var content = new StringContent(jsonObject.ToString(), Enconding.UTF8, "")

                HttpResponseMessage res = client.PostAsync(_route, new StringContent(new JavaScriptSerializer().Serialize(qb), Encoding.UTF8, "application/json")).Result;
                if (res.IsSuccessStatusCode)
                {
                    List<FNVBotRoot> FNVBotInfo = new List<FNVBotRoot>();
                    var Response = res.Content.ReadAsStringAsync().Result;
                    FNVBotInfo = JsonConvert.DeserializeObject<List<FNVBotRoot>>(Response);
                }
                return model;
            }
        }

    }
}
