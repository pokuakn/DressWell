using DressWell.Business.Interfaces;
using System;


namespace DressWell.Business.Services
{
    /// <summary>
    /// Dress Items for COLD weather.
    /// </summary>
    public class DressWellForCold : IDresser
    {
      
       public string PutOnFootwear()
        {
            return "boots";
        }

        public string PutOnHeadwear()
        {
            return "hat";
        }

        public string PutOnJacket()
        {
            return "jacket";
        }

        public string PutOnPants()
        {
            return "pants";
        }

        public string PutOnShirt()
        {
            return "shirt";
        }
    
        public string PutOnSocks()
        {
            return "socks";
        }

        public string LeaveHouse()
        {
            return "leaving house";
        }

        public string TakeOffPajamas()
        {
            return "Removing PJs";
        }
    }
}
