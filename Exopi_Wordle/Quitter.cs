using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExopiWordle.Consoles;

namespace ExopiWordle
{
    public class Quitter
    {
        public bool quit = false;

        IConsole Console;

        public Quitter(IConsole console)
        {
            Console = console;
        }

        public void QuitOrContinue()
        {
                Console.WriteLine("\nDo you wish to continue? (Y/N)");
                bool answerGiven = false;

                while (!answerGiven)
                {
                    switch (Console.ReadLine())
                    {
                        case "Y":
                        case "y":
                        case "YES":
                        case "Yes":
                        case "yes":
                            Console.WriteLine("");
                            answerGiven = true;
                            break;
                        case "N":
                        case "n":
                        case "NO":
                        case "No":
                        case "no":
                            quit = true;
                            answerGiven = true;
                            break;
                        default:
                            Console.WriteLine("Please write a valid answer");
                            break;
                    }
                }
            }
        }
}
