using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MrGroupDataLoader
{
    public static class Helper
    {
        public static int TryParseIntLog(string paramValue, string paramName, Logger logger)
        {
            if (int.TryParse(paramValue, out int currentValue))
            {
                return currentValue;
            }
            else if (paramValue == null || string.IsNullOrEmpty(paramValue))
            {
                return 0;
            }
            else
            {
                logger.Warning($"not supported param: {paramName}, value: {paramValue}");
                return 0;
            }
        }
    }
}
