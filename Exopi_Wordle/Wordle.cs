using ExopiWordle.Consoles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExopiWordle
{
    public class Wordle
    {
        private IConsole _console;
        private string _answer;
        private List<string> _dictionary;
        private int _attempts = 0;
        public bool Running = false;

        public Wordle(IConsole console, string answer, List<string> dictionary)
        {
            this._console = console;
            this._answer = answer;
            this._dictionary = dictionary;

        }

        public void GameLoop()
        {

        }

        public void StartingGameInfo()
        {

        }

        public bool IsGuessValid(string guess) 
        {
            return true;
        }
    }
}
