﻿using PKMN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Console
{
    public class UserInputManager : IUserInputManager
    {
        public int GetUserSelection(string prompt, int min, int max)
        {
            var userChoice = 0;
            do
            {
                System.Console.WriteLine(prompt);
            } while (!int.TryParse(System.Console.ReadLine(), out userChoice) || userChoice < min || userChoice > max);
            return userChoice;
        }
    }
}
