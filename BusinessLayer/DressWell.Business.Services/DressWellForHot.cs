using DressWell.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressWell.Business.Services
{
    /// <summary>
    /// Dress Items for HOT weather
    /// </summary>
    public class DressWellForHot : IDresser
    {
        private string failureMessage = "fail";
              
        public string PutOnFootwear()
        {
            return "sandals";
        }

        public string PutOnHeadwear()
        {
            return "sun visor";
        }

        public string PutOnJacket()
        {
            throw new Exception(failureMessage);
        }

        public string PutOnPants()
        {
            return "shorts";
        }

        public string PutOnShirt()
        {
            return "t-shirt";
        }

        public string PutOnSocks()
        {
            throw new Exception(failureMessage);
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
