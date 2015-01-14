using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace speed_trapper_app.SignalR.SpeedTrapper
{
    public class SpeedTrapper
    {
         // Singleton instance
        private readonly static Lazy<SpeedTrapper> _instance = new Lazy<SpeedTrapper>(
            () => new SpeedTrapper(GlobalHost.ConnectionManager.GetHubContext<SpeedTrapperHUB>().Clients));

        private readonly object _speedLimitLock = new object();
             
            
        private readonly int _maxSpeedLimit =200;
        private readonly int _minSpeedLimit = 10;
        private readonly int _defaultSpeedLimit = 100;
       
        private volatile int  _speedLimit;

        private SpeedTrapper(IHubConnectionContext<dynamic> clients)
        {  
            Clients = clients;
            if (SpeedLimit == 0)
            {
                lock (_speedLimitLock)
                {
                    SpeedLimit = _defaultSpeedLimit; //Default Speed For The First Time
                }
            }
        }

        public static SpeedTrapper Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public int SpeedLimit
        {
            get { return _speedLimit; }
            private set { _speedLimit = value; }
        }

        public int GetOverspeed()
        {
            
            return SpeedLimit;
        }
        public string AddCar(Car car)
        {
            string returnStatus = string.Empty;
            if (car.Speed > SpeedLimit)
            {              
                returnStatus= "0";
            }
            else
            {                
                returnStatus= "1";
            }
            car.MaxSpeed = SpeedLimit;
            BroadcastCarAdded(car);
            return returnStatus;
          
        }

        public void ApplySpeed(int Speed)
        {
            lock (_speedLimitLock)
            {

                if (_minSpeedLimit <= Speed && Speed <= _maxSpeedLimit)
                {
                    SpeedLimit = Speed;
                    BroadcastApplySpeed(Speed);
                }
                else
                {
                    var ErrorMessage="Speed Limit must be greater than " + _minSpeedLimit + " and less than " + _maxSpeedLimit + ".";
                    BroadcastApplySpeedError(ErrorMessage);
                    
                }

               
               
            }
        }
        private void BroadcastApplySpeedError(string ErrorMessage)
        {
            Clients.All.speedAppliedError(ErrorMessage);
        }

        private void BroadcastApplySpeed(int Speed)
        {
            Clients.All.speedApplied(Speed);
        }
       

        private void BroadcastCarAdded(Car car)
        {
            Clients.All.carAdded(car);
        }
              
    }
}