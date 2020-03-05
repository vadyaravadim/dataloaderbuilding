using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;

namespace MrGroupDataLoader
{
    public class DataModel
    {
        static Logger logger = new LoggerConfiguration().WriteTo.File($@"{Environment.CurrentDirectory}\Logs\Log_{DateTime.Now.ToString("d")}.txt").CreateLogger();

        #region Request JSON
        public class Request<T> where T : class
        {
            public Request (string _method, T _params)
            {
                Method = _method;
                Params = _params;
            }

            [JsonProperty("method")]
            public string Method { get; set; }
            [JsonProperty("params")]
            public T Params { get; set; }
        }

        public class ParamsRequest
        {
            public ParamsRequest(string token, string[] developmentProjects)
            {
                Token = token ?? throw new ArgumentNullException(nameof(token));
                DevelopmentProjects = developmentProjects ?? throw new ArgumentNullException(nameof(developmentProjects));
            }

            [JsonProperty("token")]
            public string Token { get; set; }
           
            [JsonProperty("developmentProjects")]
            public string[] DevelopmentProjects { get; set; }
        }
        #endregion

        #region Response JSON
        public class Response
        {
            [JsonProperty("result")]
            public RealtyObjectsArray Result { get; set; }
        }
        public class RealtyObjectsArray
        {
            [JsonProperty("realtyobjects")]
            public RealtyObjectsResponse[] RealtyObjectsReponses { get; set; }
        }

        public class RealtyObjectsResponse
        {
            [JsonProperty("realtyobjectid")]
            public string RealtyObjectId { get; set; }
            [JsonProperty("number_on_floorName")]
            public string FloorName { get; set; }
            [JsonProperty("amount")]
            public decimal Amount { get; set; }
            [JsonProperty("sectionName")]
            public string SectionName { get; set; }
            [JsonProperty("floor")]
            public int Floor { get; set; }
            [JsonProperty("number")]
            public string Number { get; set; }
            [JsonProperty("area")]
            public decimal Area { get; set; }
            [JsonProperty("price")]
            public decimal Price { get; set; }
            [JsonProperty("realtyobjecttypeId")]
            public string RealtyObjectTypeId { get; set; }
            [JsonProperty("realtyobjectstatusId")]
            public string RealtyObjectStatusId { get; set; }
            [JsonProperty("room_quantityName")]
            public string RoomQuantityName { get; set; }
            [JsonProperty("area_project")]
            public decimal AreaProject { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("bti_number")]
            public string NumberBTI { get; set; }
            [JsonProperty("area_bti")]
            public string AreaBTI { get; set; }
        }
        #endregion

        #region Upload JSON CRM 
        public class PropertyUpdate
        {
            int _status;
            public PropertyUpdate(RealtyObjectsResponse realtyObjects, int? status = 0, string propertyId = "")
            {
                PropertyId = propertyId;
                Quantity = Convert.ToInt32(realtyObjects.Area);
                Cost = realtyObjects.Price;
                Price = realtyObjects.Amount;
                SpaceDesign = Convert.ToInt32(realtyObjects.AreaProject);
                Name = realtyObjects.Name;
                NumberBTI = realtyObjects.NumberBTI;

                if (Decimal.TryParse(realtyObjects.AreaBTI, out decimal areaBTI))
                {
                    AreaBTI = areaBTI;
                }

                if (status != null)
                {
                    _status = status.Value;
                }

                PropertyStatusCode = realtyObjects.RealtyObjectStatusId;
            }

            [JsonProperty("mtr_quantity")]
            public decimal Quantity { get; set; }
            [JsonProperty("mtr_cost")]
            public decimal Cost { get; set; }
            [JsonProperty("mtr_price")]
            public decimal Price { get; set; }
            [JsonProperty("mtr_spacedesign")]
            public decimal SpaceDesign { get; set; }
            [JsonProperty("mtr_name")]
            public string Name { get; set; }
            [JsonProperty("mtr_bti_number")]
            public string NumberBTI { get; set; }
            [JsonProperty("mtr_spacebti")]
            public decimal AreaBTI { get; set; }
            [JsonProperty("statuscode", NullValueHandling = NullValueHandling.Ignore)]
            public virtual dynamic PropertyStatusCode
            {
                get
                {
                    return _propertyStatusCode;
                }
                set
                {
                    if (Guid.TryParse(value, out Guid parseValue))
                    {
                        StatusModel(value);
                    }
                }
            }

            internal virtual void StatusModel(string value)
            {
                if (value == "2420287F-D58D-E911-B714-00155D5AD641")
                {
                    if (_status == 962090001)
                    {
                        _propertyStatusCode = 962090005;
                    }

                    if (_status == 962090004)
                    {
                        _propertyStatusCode = 962090005;
                    }

                    if (_status == 962090000)
                    {
                        _propertyStatusCode = 962090005;
                    }

                    if (_status == 962090003)
                    {
                        _propertyStatusCode = 962090003;
                    }

                    if (_status == 962090002)
                    {
                        _propertyStatusCode = 962090002;
                    }

                    if (_status == 1)
                    {
                        _propertyStatusCode = 1;
                    }

                    if (_status == 962090005)
                    {
                        _propertyStatusCode = 962090005;
                    }
                }

                if (value == "95630C16-D28D-E911-B714-00155D5AD641")
                {
                    if (_status == 962090001)
                    {
                        _propertyStatusCode = 962090000;
                    }

                    if (_status == 962090003)
                    {
                        _propertyStatusCode = 962090003;
                    }

                    if (_status == 962090002)
                    {
                        _propertyStatusCode = 962090002;
                    }

                    if (_status == 1)
                    {
                        _propertyStatusCode = 1;
                    }

                    if (_status == 962090004)
                    {
                        _propertyStatusCode = 962090000;
                    }

                    if (_status == 962090000)
                    {
                        _propertyStatusCode = 962090000;
                    }

                    if (_status == 962090005)
                    {
                        _propertyStatusCode = 962090000;
                    }
                }

                if (value == "31878D68-D58D-E911-B714-00155D5AD641" || value == "8685BFE7-D18D-E911-B714-00155D5AD641" || value == "29A64200-8105-EA11-B719-00155D5AD641" || value == "DFAE9F1B-8105-EA11-B719-00155D5AD641")
                {
                    if (_status == 962090001)
                    {
                        _propertyStatusCode = 962090001;
                    }

                    if (_status == 962090003)
                    {
                        _propertyStatusCode = 962090003;
                    }

                    if (_status == 962090002)
                    {
                        _propertyStatusCode = 962090002;
                    }

                    if (_status == 1)
                    {
                        _propertyStatusCode = 1;
                    }

                    if (_status == 962090004)
                    {
                        _propertyStatusCode = 962090001;
                    }

                    if (_status == 962090000)
                    {
                        _propertyStatusCode = 962090001;
                    }

                    if (_status == 962090005)
                    {
                        _propertyStatusCode = 962090001;
                    }
                }

                if (value == "0F52CC9F-D18D-E911-B714-00155D5AD641" || value == "D8B37395-D58D-E911-B714-00155D5AD641" || value == "AAFD9D68-8105-EA11-B719-00155D5AD641" || value == "C67CF48D-8105-EA11-B719-00155D5AD641")
                {
                    if (_status == 962090001)
                    {
                        _propertyStatusCode = 962090004;
                    }

                    if (_status == 962090003)
                    {
                        _propertyStatusCode = 962090003;
                    }

                    if (_status == 962090002)
                    {
                        _propertyStatusCode = 962090002;
                    }

                    if (_status == 1)
                    {
                        _propertyStatusCode = 1;
                    }

                    if (_status == 962090004)
                    {
                        _propertyStatusCode = 962090004;
                    }

                    if (_status == 962090000)
                    {
                        _propertyStatusCode = 962090004;
                    }

                    if (_status == 962090005)
                    {
                        _propertyStatusCode = 962090004;
                    }
                }
            }

            [JsonIgnore]
            public string PropertyId { get; set; }
            private int? _propertyStatusCode;
        }

        public class PropertyUpload : PropertyUpdate
        {
            public PropertyUpload(RealtyObjectsResponse realtyObjects) : base(realtyObjects)
            {
                PropertyId = realtyObjects.RealtyObjectId;
                SectionNumber = Helper.TryParseIntLog(realtyObjects.SectionName, "SectionName", logger);
                Floor = realtyObjects.Floor;
                PlatformNumber = Helper.TryParseIntLog(realtyObjects.FloorName, "FloorName", logger);
                BeforeBTINumber = realtyObjects.Number;
                ProprtyTypeCode = realtyObjects.RealtyObjectTypeId;
                Rooms = realtyObjects.RoomQuantityName;
                PropertyStatusCode = realtyObjects.RealtyObjectStatusId;
            }
            [JsonProperty("mtr_propertyid")]
            public new string PropertyId { get; set; }
            [JsonProperty("@odata.type")]
            public string ODataType = "#Microsoft.Dynamics.CRM.mtr_property";
            [JsonProperty("@odata.context")]
            public string ODataConetext = @"https://org0063f16e.crm4.dynamics.com/api/data/v9.0/$metadata#mtr_properties/$entity";
            [JsonProperty("ownerid@odata.bind")]
            public string Owner = "/systemusers(9B6A8604-D0FE-E811-A95A-000D3A464F85)";
            [JsonProperty("mtr_sectionnumber")]
            public int SectionNumber { get; set; }
            [JsonProperty("mtr_floor")]
            public int Floor { get; set; }
            [JsonProperty("mtr_platform_number")]
            public int PlatformNumber { get; set; }
            [JsonProperty("mtr_before_bti_number")]
            public string BeforeBTINumber { get; set; }
            [JsonProperty("mtr_property_type_code")]
            public dynamic ProprtyTypeCode {
                get
                {
                    return _propertyTypeCode;
                }
                set
                {
                    if (Guid.TryParse(value, out Guid parseValue))
                    {
                        switch (value)
                        {
                            case "04EF0F90-F391-E911-B714-00155D5AD641":
                                _propertyTypeCode = 962090000;
                                break;
                            case "7394441E-F391-E911-B714-00155D5AD641":
                                _propertyTypeCode = 962090001;
                                break;
                            case "44D599DA-EC91-E911-B714-00155D5AD641":
                                _propertyTypeCode = 962090002;
                                break;
                            case "A3CCFED6-EE91-E911-B714-00155D5AD641":
                                _propertyTypeCode = 962090004;
                                break;
                            case "8963039C-F391-E911-B714-00155D5AD641":
                                _propertyTypeCode = 962090003;
                                break;
                            case "D3EF99A0-F291-E911-B714-00155D5AD641":
                                _propertyTypeCode = 962090005;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            [JsonProperty("statuscode", NullValueHandling = NullValueHandling.Ignore)]
            public override dynamic PropertyStatusCode
            {
                get
                {
                    return _propertyStatusCode;
                }
                set
                {
                    if (Guid.TryParse(value, out Guid parseValue))
                    {
                        StatusModel(value);
                    }
                }
            }

            internal override void StatusModel(string value)
            {
                switch (value)
                {
                    case "2420287F-D58D-E911-B714-00155D5AD641":
                        _propertyStatusCode = 962090005;
                        break;
                    case "95630C16-D28D-E911-B714-00155D5AD641":
                        _propertyStatusCode = 962090000;
                        break;
                    case "31878D68-D58D-E911-B714-00155D5AD641":
                        _propertyStatusCode = 962090001;
                        break;
                    default:
                        _propertyStatusCode = 962090004;
                        break;
                }
            }

            [JsonProperty("mtr_rooms")]
            public dynamic Rooms {
                get
                {
                    return _roomName;
                }
                set
                {
                    switch (value)
                    {
                        case "Студия":
                            _roomName = 0;
                            break;
                        case "Евро-2":
                            _roomName = 2;
                            break;
                        case "Евро-3":
                            _roomName = 3;
                            break;
                        case "Евро-4":
                            _roomName = 4;
                            break;
                        default:
                            _roomName = Helper.TryParseIntLog(value, "RoomQuantityName", logger);
                            break;
                    }
                }
            }

            private int _propertyTypeCode;
            private int? _propertyStatusCode;
            private int _roomName;
        }
        #endregion

        #region OData Response model
        public class ODataSelectPropertyRecord
        {
            [JsonProperty("value")]
            public ODataSelectRecord[] Value { get; set; }
        }

        public class ODataSelectRecord
        {
            [JsonProperty("mtr_propertyid")]
            public string Property { get; set; }
            [JsonProperty("statuscode")]
            public int? Status { get; set; }
        }
        #endregion
    }
}
