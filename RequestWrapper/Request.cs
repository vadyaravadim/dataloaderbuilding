using System;
using System.Net.Http;
using MrGroupDataLoader;
using Flurl.Http;
using System.Threading;
using System.Threading.Tasks;
using static MrGroupDataLoader.DataModel;
using System.Configuration;

namespace MrGroupDataLoader
{
    public class Request<T>
    {
        object _requestObject;
         
        string RequestUrl = ConfigurationManager.AppSettings.Get("mrapi");
        public Request(string _method, T paramsRequest)
        {
            if (string.IsNullOrEmpty(_method))
            {
                throw new ArgumentException("an empty parameter was passed", nameof(_method));
            }

            _requestObject = new DataModel.Request<object>(_method, paramsRequest);
        }

        public async Task<RealtyObjectsResponse[]> GetRealtyObjects()
        {
            FlurlHttp.Configure(setting =>
            {
                setting.Timeout = new TimeSpan(1, 30, 0);
            });
            IFlurlResponse request = await RequestUrl.PostJsonAsync(_requestObject);
            DataModel.Response response = await request.GetJsonAsync<DataModel.Response>();
            return response.Result.RealtyObjectsReponses;
        }
    }
}
