using System;
using DressWell.Presentation.Interfaces;
using DressWell.Business.Interfaces;
using DressWell.Presentation.ViewModel;

namespace DressWell.Presentation.Services
{
    public class DressWellPresentationServices : IDressWellPresentationServices
    {
        private IDressWellBusinessService context;
        private DressingOrderViewModel dressingOrder;


        public DressWellPresentationServices(IDressWellBusinessService objDressUpService)
        {
            this.context = objDressUpService;
        }

        public void AcceptUserInput(string userInput)
        {
            this.context.AcceptUserInput(userInput);
        }

        public string GetDressingOrder()
        {
            DressWell();
            return dressingOrder.Order;
        }

        private void DressWell()
        {
            dressingOrder = new DressingOrderViewModel();
            dressingOrder.Order = this.context.DressWell();
        }

    }
}
