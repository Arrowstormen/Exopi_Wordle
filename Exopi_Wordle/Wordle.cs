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
        private int _attemptsLeft = 6; //Change to make harder/easier.
        public readonly bool Ran = false;
        public readonly bool AnswerGuessed = false;

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
            throw new NotImplementedException();
        }

        public bool IsGuessCorrect(string guess)
        {
            throw new NotImplementedException();
        }

        public string DetermineHints(string guess) 
        {
            return "";
        }

    }
}
