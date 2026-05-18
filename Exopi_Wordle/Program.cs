// Display title as the C# console Wordle app.
using ExopiWordle;
using ExopiWordle.Consoles;

StandardConsole console = new StandardConsole();
Quitter quitter = new Quitter(console);
string fileName = "../../../Data/Words.txt";
List<string> dictionary = File.ReadAllLines(fileName).ToList();

console.WriteLine("Console Wordle in C#\r");
console.WriteLine("------------------------\n");
console.WriteLine("On hints:");
console.WriteLine("G (short for Green) means the letter in this position is correct.");
console.WriteLine("Y (short for Yellow) means the letter appears in the word, but not in this position.");
console.WriteLine("R (short for Red) means the letter does not appear in the word at all.");
console.WriteLine("You have a total of 6 attempts each game.\n");

while (!quitter.quit)
{
    string answer = dictionary[new Random().Next(dictionary.Count)];
    Wordle wordle = new Wordle(console, answer, dictionary);
    wordle.GameLoop();
    quitter.QuitOrContinue();
}
   
