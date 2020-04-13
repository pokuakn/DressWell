using System;
using DressWell.Presentation.Interfaces;
using Ninject;

namespace DressWell
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Initialize Bootstrapper for dependency injection
                var container = Bootstrapper.Init();

                //Get context (from PresentationServicesLayer)
                IDressWellPresentationServices context = container.Get<IDressWellPresentationServices>();

                //Accept user input
                Console.Write("Input: ");
                string input = Console.ReadLine();
                context.AcceptUserInput(input);

                //Display result
                Console.WriteLine("Output: {0}", context.GetDressingOrder());
                Console.ReadLine();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }
    }
}
