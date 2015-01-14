using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace speed_trapper_app.Models
{
    public class SpeedTrapperModel
    {
        private static string _SpeedUnit { get; set; }

        public readonly string SpeedUnit;

         public SpeedTrapperModel()
        {
            SpeedUnit = SpeedTrapperModel._SpeedUnit;
        }

         static SpeedTrapperModel()
        {
             
             _SpeedUnit = System.Configuration.ConfigurationManager.AppSettings["SpeedUnit"];
        }
    }
}