using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace car_app.Models
{
    public class CarModel
    {
        public string Name { get; set; }

        public int Speed { get; set; }

        private static string _SpeedUnit { get;  set; }

        public readonly string SpeedUnit;

        private static Random RandomNoGenerator { get;  set; }

        private static string[] CarNames { get;  set; }

        public CarModel()
        {
            SpeedUnit = CarModel._SpeedUnit;
        }

        static CarModel()
        {
             RandomNoGenerator = new Random();
             CarNames = System.IO.File.ReadAllLines(System.Web.HttpContext.Current.Request.MapPath(@"Resources\car-names.txt"));
             _SpeedUnit = System.Configuration.ConfigurationManager.AppSettings["SpeedUnit"];
        }
        public static CarModel GetRandomCar()
        {             

              var RandomNo = RandomNoGenerator.Next(0,CarNames.Length);

              CarModel Car = new CarModel();
              Car.Name = CarNames[RandomNo];
              Car.Speed = GetRendomSpeed(RandomNo);
              return Car;
              
              
        }

        private static int GetRendomSpeed(int RandomNo)
        {
            var RendomSpeed = (DateTime.Now.Second * RandomNo).ToString();

            if (RendomSpeed.Length>=3)
	        {
                var Temp = RendomSpeed.Take(3).ToArray();
                if (Convert.ToInt32(Temp[0]) > 3)
                {
                    Temp[0] = RandomNoGenerator.Next(0, 3).ToString().FirstOrDefault();
                }
                RendomSpeed = new string(Temp);
	        }


            return Convert.ToInt32(RendomSpeed);
        }
    }
}