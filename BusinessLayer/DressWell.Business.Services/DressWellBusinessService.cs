using System;
using DressWell.Business.Interfaces;
using DressWell.Data.Interfaces;
using System.Text;
using DressWell.Data.Repository;
using DressWell.Data.Models;
using System.Collections.Generic;


namespace DressWell.Business.Services
{
    /// <summary>
    /// Class handling all the business logic of dressing up
    /// </summary>
    public class DressWellBusinessService : IDressWellBusinessService
    {
        private IDressWellRepository dbContext;
        private List<string> processedCommands;
        private IDresser dresser;
        private string failureMessage = "fail";

        public DressWellBusinessService(IDressWellRepository iDbRepo)
        {
            dbContext = iDbRepo;
            processedCommands = new List<string>();
        }

        public void AcceptUserInput(string userInput)
        {
            this.dbContext.AcceptUserInput(userInput);
        }

        /// <summary>
        /// Processes the user input commands
        /// </summary>
        /// <returns>Dressing order</returns>
        public string DressWell()
        {
            //Get the model from the Data layer
            UserInputModel uim = ((DressWellRepository)dbContext).Uim;

            //Instantiate dresser based on the first command (weather type) passed by the user
            switch (uim.Input[0].ToUpper())
            {
                case "HOT":
                    dresser = new DressWellForHot();
                    break;
                case "COLD":
                    dresser = new DressWellForCold();
                    break;
                //Invalid First Command
                default:
                    throw new Exception("Invalid weather specified in the first arguement");
            }

            return ProcessCommands(uim);
        }

        private string ProcessCommands(UserInputModel uim)
        {
            StringBuilder dressingOrder = new StringBuilder(string.Empty);
            try
            {
                //Process all the commands passed by the user (except the first one that would be weather type) and build the dressing order
                for (int i = 1; i < uim.Input.Count; i++)
                {
                    string command = uim.Input[i];

                    //Validate the input command to check compliance with the rules before processing it
                    if (IsValid(command))
                    {
                        //Process the command
                        switch (uim.Input[i])
                        {
                            case "1":
                                dressingOrder.Append(", ").Append(dresser.PutOnFootwear());
                                break;
                            case "2":
                                dressingOrder.Append(", ").Append(dresser.PutOnHeadwear());
                                break;
                            case "3":
                                dressingOrder.Append(", ").Append(dresser.PutOnSocks());
                                break;
                            case "4":
                                dressingOrder.Append(", ").Append(dresser.PutOnShirt());
                                break;
                            case "5":
                                dressingOrder.Append(", ").Append(dresser.PutOnJacket());
                                break;
                            case "6":
                                dressingOrder.Append(", ").Append(dresser.PutOnPants());
                                break;
                            case "7":
                                dressingOrder.Append(", ").Append(dresser.LeaveHouse());
                                break;
                            case "8":
                                dressingOrder.Append(", ").Append(dresser.TakeOffPajamas());
                                break;
                            default:
                                throw new Exception("Invalid weather specified in the first arguement");
                        }
                    }

                    //Add command to processed list of commands
                    processedCommands.Add(command);
                }
            }

            catch (Exception ex)
            {
                //If business rules throws exception (exception message will always be "Fail")
                //append the message with the dressing order and return dressing order
                if (ex.Message == failureMessage)
                {
                    return dressingOrder.Append(", ").Append(ex.Message).ToString().Remove(0, 2);       //Remove leading ", " before returning the value
                }

                //Else, throw it back and let the main method handle it
                else
                {
                    throw;
                }
            }

            //Remove leading ", " before returning the value
            return dressingOrder.ToString().Remove(0, 2);
        }
        
        /// <summary>
        /// Validates commands and checks for compliance with the stated rules
        /// </summary>
        /// <param name="command">Command to be validated</param>
        /// <returns>True if the entered command is valid and in compliance with the rules, throws an exception otherwise</returns>
        private bool IsValid(string command)
        {
            if (string.IsNullOrWhiteSpace(command)) throw new Exception("Please enter a command.");

            TakePajamasOffFirst(command);
            PutOnOnePieceNoDupes(command);

            #region Command-wise Checking

            PutOnSocksAndJacketWhenCold(command);
            PutOnSocksAndPantsBeforeShoes(command);
            PutOnShirtBeforeHatAndJacket(command);
            PutOnAllItemsBeforeLeaveHouse(command);

            #endregion

            //If all the conditions are satisfied, return true
            return true;
        }

        private void PutOnSocksAndJacketWhenCold(string command)
        {
            if ((command == "3") || (command == "5"))
            {
                //If command is for putting on socks or jacket and dresser is of type HotDresser (it is hot)
                if (dresser.GetType() == typeof(DressWellForHot))
                {
                    throw new Exception(failureMessage);
                }
            }            
        }

        private void PutOnSocksAndPantsBeforeShoes(string command)
        {
            //In other words, if the command is for shoes/footwear, make sure that socks and pants are already put on
            if (command == "1")
            {
                //if the weather is hot check only for pants
                if (dresser.GetType() == typeof(DressWellForHot))
                {
                    if (!processedCommands.Contains("6"))
                    {
                        throw new Exception(failureMessage);
                    }
                }
                //else, check for both socks and pants
                else
                {
                    if (!processedCommands.Contains("3") || !processedCommands.Contains("6"))
                    {
                        throw new Exception(failureMessage);
                    }
                }
            }
        }

        private void PutOnShirtBeforeHatAndJacket(string command)
        {
            //In other words, if the command is for either the headwear or the jacket, make sure that the shirt is already put on
            if ((command == "2") || (command == "5"))
            {
                if (!processedCommands.Contains("4"))
                {
                    throw new Exception(failureMessage);
                }
            }
        }

        private void PutOnAllItemsBeforeLeaveHouse(string command) 
        {
            //You cannot leave the house until all items of clothing are on (except socks and a jacket when it’s hot)
            //If the command is for leaving the house, make sure that all the other commands have been processed
           if (command == "7")
            {
                //Check if jacket and socks are put on only when the weather is not hot
                if (dresser.GetType() != typeof(DressWellForHot))
                {
                    if (!processedCommands.Contains("3") || !processedCommands.Contains("5"))       //jacket or shoes/footwear not put on
                    {
                        throw new Exception(failureMessage);
                    }
                }

                //Check for other types of clothing irrespective of the weather type
                if (!processedCommands.Contains("1") || !processedCommands.Contains("2") || !processedCommands.Contains("4") || !processedCommands.Contains("6") || !processedCommands.Contains("8"))
                {
                    throw new Exception(failureMessage);
                }
            }
        }
     
        private void PutOnOnePieceNoDupes(string command)
        {
            if (processedCommands.Contains(command))
            {
                throw new Exception(failureMessage);
            }
        }

        private void TakePajamasOffFirst(string command)
        {
            if (processedCommands.Count == 0)
            {
                if (command != "8")
                {
                    throw new Exception(failureMessage);
                }
            }
        }
    }
}
