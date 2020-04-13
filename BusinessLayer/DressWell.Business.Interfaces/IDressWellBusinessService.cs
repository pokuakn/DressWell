using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressWell.Business.Interfaces
{
    public interface IDressWellBusinessService
    {
        void AcceptUserInput(string userInput);
        string DressWell();
    }
}
