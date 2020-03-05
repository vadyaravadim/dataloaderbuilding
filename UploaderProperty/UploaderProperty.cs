using Flurl.Http;
using Newtonsoft.Json;
using ODataLayerCrm.AuthorizeSpace;
using ODataLayerCrm.Queries;
using Serilog.Core;
using Simple.OData.Client;
using System;
using System.Threading.Tasks;
using static MrGroupDataLoader.DataModel;

namespace MrGroupDataLoader.Uploader
{
    public class UploaderProperty
    {
        Authorize authorize;
        TokenResponse token;
        string _odata;
        Logger _logger;
        public UploaderProperty(string resource, string login, string password, string odata, Logger logger)
        {
            _odata = odata;
            authorize = new Authorize(resource, login, password);
            Task taskAuth = AuthorizeStart();
            _logger = logger;
        }

        async Task AuthorizeStart()
        {
            token = await authorize.GetTokenRequestAsync();

            if (token.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _logger.Information("successfully auth in");
            } 
            else
            {
                _logger.Error("unsuccessful logged in");
                throw new Exception("unsuccessful auth in");
            }
        }

        private async Task<T> SelectAsync<T>(RealtyObjectsResponse data)
        {
            IFlurlResponse responseHttp = await $"{_odata}?$select=mtr_propertyid,statuscode&$filter=mtr_propertyid eq '{data.RealtyObjectId}'".WithOAuthBearerToken(token.Content).GetAsync();
            string responseBody = await responseHttp.GetStringAsync();

            if (!responseHttp.ResponseMessage.IsSuccessStatusCode && responseHttp.StatusCode != 401)
            {
                _logger.Error($"Bad select in the database, Message: {responseBody}, StatusCode: {responseHttp.StatusCode}, Json for request: {JsonConvert.SerializeObject(data)}");
            }
            else if (!responseHttp.ResponseMessage.IsSuccessStatusCode && responseHttp.StatusCode == 401)
            {
                await AuthorizeStart();
                return await SelectAsync<T>(data);
            }

            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        private async Task<IFlurlResponse> UpdateAsync(PropertyUpdate data)
        {
            IFlurlResponse responseHttp = await $"{_odata}({data.PropertyId})".WithOAuthBearerToken(token.Content).PatchJsonAsync(data);
            string responseBody = await responseHttp.GetStringAsync();
            
            if (!responseHttp.ResponseMessage.IsSuccessStatusCode && responseHttp.StatusCode != 401)
            {
                _logger.Error($"Bad update in the database, Message: {responseBody}, StatusCode: {responseHttp.StatusCode}, Json for request: {JsonConvert.SerializeObject(data)}");
            } 
            else if (!responseHttp.ResponseMessage.IsSuccessStatusCode && responseHttp.StatusCode == 401)
            {
                await AuthorizeStart();
                return await UpdateAsync(data);
            }
            
            return responseHttp;
        }
        
        private async Task<IFlurlResponse> UploadAsync(PropertyUpload data)
        {
            IFlurlResponse responseHttp = await _odata.WithOAuthBearerToken(token.Content).PostJsonAsync(data);
            string responseBody = await responseHttp.GetStringAsync();

            if (!responseHttp.ResponseMessage.IsSuccessStatusCode && responseHttp.StatusCode != 401)
            {
                _logger.Error($"Bad insert in the database, Message: {responseBody}, StatusCode: {responseHttp.StatusCode}, Json for request: {JsonConvert.SerializeObject(data)}");
            }
            else if (!responseHttp.ResponseMessage.IsSuccessStatusCode && responseHttp.StatusCode == 401)
            {
                await AuthorizeStart();
                return await UpdateAsync(data);
            }

            return responseHttp;
        }

        public async Task UpdateOrUploadAsync(RealtyObjectsResponse data)
        {
            if (data == null)
            {
                return;
            }

            ODataSelectPropertyRecord flurlSelect = await SelectAsync<ODataSelectPropertyRecord>(data);

            if (flurlSelect == null || flurlSelect?.Value == null || flurlSelect?.Value.Length == 0)
            {
                PropertyUpload propertyUpload = new PropertyUpload(data);
                await UploadAsync(propertyUpload).ConfigureAwait(false);
            } 
            else
            {
                foreach (ODataSelectRecord property in flurlSelect.Value)
                {
                    PropertyUpdate propertyUpdate = new PropertyUpdate(data, status: property.Status, propertyId: property.Property);
                    await UpdateAsync(propertyUpdate).ConfigureAwait(false);
                }
            }
        }
    }

}
