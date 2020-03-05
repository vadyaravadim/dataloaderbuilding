using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MrGroupDataLoader.IntegrationMethods.Helper;
using MrGroupDataLoader.Uploader;
using Serilog.Core;

namespace MrGroupDataLoader
{
    /// <summary>
    /// Class for request api, contains implementains processing data
    /// </summary>
    public class Methods
    {
        UploaderProperty _uploader;
        string[] _properties;
        Logger _logger;
        public Methods(UploaderProperty uploader, string[] properties, Logger logger)
        {
            _uploader = uploader ?? throw new ArgumentNullException(nameof(uploader));
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ProcessingRealtyObjects()
        {
            foreach(string developmentProject in _properties)
            {
                string preparingString = developmentProject.Trim(' ', '\n', '\r');
                Request<DataModel.ParamsRequest> request = HelperProcessing.SetupRequestRealtyObjects(new string[] { preparingString });
                DataModel.RealtyObjectsResponse[] realtyObjects;
                try
                {
                    realtyObjects = await HelperProcessing.GetRealtyObjects(request);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "throwing an error when querying mr-group");
                    throw new Exception(ex.Message);
                }
                await ProcessingCollection(realtyObjects).ConfigureAwait(false);
                _logger.Information($"Finished updating: {realtyObjects.Length} properties");
            }
        }

        private async Task ProcessingCollection(DataModel.RealtyObjectsResponse[] realtyObjects)
        {
            foreach (DataModel.RealtyObjectsResponse realty in realtyObjects)
            {
                await _uploader.UpdateOrUploadAsync(realty).ConfigureAwait(false);
            }
        }
    }
}
