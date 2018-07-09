using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnBoardingTask2018Jun.Models.New_Models
{   
    public class CustomerControllerViewDataTransferModel
    {
        public IEnumerable<Customer> DbCustomerSet { get; set; }
        public Customer TransferExtraCustomer { get; set; }
        public int ActionState { get; set; }
        public CustomerControllerViewDataTransferModel()
        {
            DbCustomerSet = null;
            TransferExtraCustomer = null;
            ActionState = (int)ActionStates.Initial;
            // enum ActionStates is in Model - New Model - ActionStates.cs
        }
    }
}