using System.Threading.Tasks;
using System.Configuration;

namespace MrGroupDataLoader.IntegrationMethods.Helper
{
    /// <summary>
    /// Describing Class configuration setuping json for request and
    /// Wrapper request to api
    /// </summary>
    public class HelperProcessing
    {
        #region Integration for Api GetRealtyObjects

        /* 
            * This is example for setting json body request 
            * Objects "params" for request, implemenating in DataModel class 
            * and transmitted generic class
            * 
            * {
            *     "method":"getRealtyObjects",
            *     "params":{
            *          "token":"vT0mfKrhvVeBMe1swmUP",
            *          "developmentprojects":["29c6ab02-5ea5-e811-94a0-00155d000e0e"]
            *      }
            *  }
            *  
        */

        public static Request<DataModel.ParamsRequest> SetupRequestRealtyObjects(string[] developmentProjects)
        {
            DataModel.ParamsRequest paramsRequest = new DataModel.ParamsRequest(ConfigurationManager.AppSettings.Get("Token"), developmentProjects);
            Request<DataModel.ParamsRequest> request = new Request<DataModel.ParamsRequest>(ConfigurationManager.AppSettings.Get("MethodRealtyObjects"), paramsRequest);
            return request;
        }

        /// <summary>
        /// This a wrapper for GetRealtyObjects request to the api mr-group
        /// </summary>
        /// <param name="request">Generated base class for the query</param>
        /// <returns>Result request</returns>
        public async static Task<DataModel.RealtyObjectsResponse[]> GetRealtyObjects(Request<DataModel.ParamsRequest> request)
        {
            DataModel.RealtyObjectsResponse[] realtyObjects = await request.GetRealtyObjects().ConfigureAwait(false);
            return realtyObjects;
        }
        #endregion
    }
}
