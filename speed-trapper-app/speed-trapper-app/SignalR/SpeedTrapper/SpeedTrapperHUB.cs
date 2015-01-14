using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;

namespace speed_trapper_app.SignalR.SpeedTrapper
{
    [HubName("speedTrapper")]
    public class SpeedTrapperHUB : Hub
    {
        private readonly SpeedTrapper _speedTrapperTicker;

        public SpeedTrapperHUB() :
            this(SpeedTrapper.Instance)
        {

        }

        public SpeedTrapperHUB(SpeedTrapper speedTrapperTicker)
        {
            _speedTrapperTicker = speedTrapperTicker;
        }
        
        public string AddCar(Car car)
        {
            return _speedTrapperTicker.AddCar(car).ToString();
        }
        public string GetOverspeed()
        {
            return _speedTrapperTicker.GetOverspeed().ToString();
        }
        
        public void ApplySpeed(int speed)
        {
            _speedTrapperTicker.ApplySpeed(speed);
        }

       
    }
}