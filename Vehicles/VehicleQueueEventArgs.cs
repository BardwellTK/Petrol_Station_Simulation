using System;

namespace Petrol_Somewhat_Unlimited_Ltd.Vehicles
{
    public class VehicleQueueEventArgs : EventArgs
    {
        public Vehicle Vehicle { get; set; }
    }
}
