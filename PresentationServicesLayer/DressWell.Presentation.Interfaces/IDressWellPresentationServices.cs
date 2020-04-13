using System;

namespace DressWell.Presentation.Interfaces
{
    public interface IDressWellPresentationServices
    {
        void AcceptUserInput(string userInput);
        string GetDressingOrder();
    }
}
