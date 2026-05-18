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
        private int _attemptsLeft = 6;
        public bool Ran { get; private set; }
        public bool AnswerGuessed { get; private set; }

        public Wordle(IConsole console, string answer, List<string> dictionary)
        {
            this._console = console;
            this._answer = answer;
            this._dictionary = dictionary;
            AnswerGuessed = false;
        }

        public void GameLoop()
        {
            StartingGameInfo();
            while (_attemptsLeft > 0 && !AnswerGuessed)
            {
                bool guessValid = false;
                string playerGuess = String.Empty;
                while (!guessValid)
                {
                    playerGuess = _console.ReadLine().ToLower();
                    if (IsGuessValid(playerGuess))
                    {
                        guessValid = true;
                    }
                }

                _console.WriteLine(DetermineHints(playerGuess));
                _attemptsLeft--;
                if (DetermineHints(playerGuess) == "GGGGG")
                {
                    AnswerGuessed = true;
                }
                else
                {
                    _console.WriteLine(_attemptsLeft.ToString() + " attempts left!");
                }

            }
            Ran = true;
            EndGameInfo();
        }

        public string StartingGameInfo()
        {
            return "A NEW GAME HAS STARTED. PLEASE INPUT A FIVE LETTER WORD AS A GUESS.";
        }

        public string EndGameInfo()
        {
            if (AnswerGuessed)
            {
                return "YOU WIN!";
            }
            else return "GAME OVER";
        }

        public bool IsGuessValid(string guess) 
        {
            if (!(guess.Length == 5))
            {
                _console.WriteLine("A guess must be a word of exactly five letters.");
                return false;
            }
            else if (_dictionary.Contains(guess))
            {
                return true;
            }
            else
                _console.WriteLine("The guess is not in the game's dictionary; try again.");
                return false;
        }

        public string DetermineHints(string guess) 
        {
            string res = "";
            int i = 0;
            while (i < 5)
            {
                string hint = String.Empty;
                if (guess[i] == _answer[i])
                {
                    hint = "G";
                } 
                else if (_answer.Contains(guess[i]))
                {
                    hint = "Y";
                } 
                else
                {
                    hint = "R";
                }
                res = string.Concat(res, hint);
                i++;
            }
            return res;
        }

    }
}
