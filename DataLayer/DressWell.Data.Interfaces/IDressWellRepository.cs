using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressWell.Data.Interfaces
{
    public interface IDressWellRepository
    {
        /// <summary>
        /// Accepts user input and stores it in the model
        /// </summary>
        /// <param name="userInput">Input passed by the user</param>
        void AcceptUserInput(string userInput);
    }
}
