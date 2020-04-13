using System;
using DressWell.Data.Interfaces;
using DressWell.Data.Models;
using System.Collections.Generic;

namespace DressWell.Data.Repository
{
    /// <summary>
    /// Repository handling all the data access logic
    /// </summary>
    public class DressWellRepository : IDressWellRepository
    {
        /// <summary>
        /// Instance of UserInputModel
        /// </summary>
        public UserInputModel Uim;      

        /// <summary>
        /// Accepts user input and stores it in the model
        /// </summary>
        /// <param name="userInput">Input passed by the user</param>
        public void AcceptUserInput(string userInput)
        {
            Uim = new UserInputModel();

            //Removing spaces and commas from the input
            string[] userInputArray = userInput.Split(',', ' ');

            Uim.Input = new List<string>();

            //Store all the input commands passed by the user in the data model, one-by-one
            foreach (string input in userInputArray)
            {
                if (input.Trim() != string.Empty)
                {
                    Uim.Input.Add(input);
                }
            }
        }
    }
}
