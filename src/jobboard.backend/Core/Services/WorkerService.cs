using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace jobboard.backend.Core.Services
{
    public class WorkerService : IWorkerService
    {
        private const string _url = "http://127.0.0.1:5007/api/tasks"; 
        public void RegisterTask(string taskType, int taskId)
        {
            JsonContent content = new JsonContent(new { type = taskType, id = taskId });

            HttpClient client = new HttpClient();
            client.PostAsync(_url, content);
        }
    }

    public class JsonContent : StringContent
    {
        public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }
    }
}
