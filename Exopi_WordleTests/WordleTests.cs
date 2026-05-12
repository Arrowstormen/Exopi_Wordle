
using ExopiWordle;
using ExopiWordle.Consoles;
using Moq;

namespace ExopiWordleTests
{
    [TestClass]
    public sealed class TestConsole : IConsole
    {
        public List<string> output = new List<string>();

        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        public void Write(string message)
        {
            output.Add(message);
        }

        public void WriteLine(string message)
        {
            output.Add(message);
        }
    }

    [TestClass]
    public sealed class WordleTests
    {
        [TestMethod]
        public void StartingGameInfo_Is_As_Expected()
        {
            //SHOULD PROBABLY JUST BE A CONSTANT UNLESS MORE THAN ONE LINE, OR LOGIC IS INTRODUCED.

            //Arrange
            string answer = "crash";
            TestConsole console = new TestConsole();
            List<string> dictionary = new List<string>();
            Wordle wordle = new Wordle(console, answer, dictionary);

            //Act
            wordle.StartingGameInfo();

            //Assert
            string expected = "A NEW GAME HAS STARTED. PLEASE INPUT A FIVE LETTER WORD AS A GUESS.";
            string actual = console.output[0];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GameLoop_Ends_After_Six_Attempts_Are_Made()
        {
            //Arrange
            string answer = "crash";
            var mockConsole = new Mock<IConsole>();
            List<string> dictionary = new List<string>(["smash"]);
            Wordle wordle = new Wordle(mockConsole.Object, answer, dictionary);
            mockConsole.SetupSequence(m => m.ReadLine()).Returns("smash").Returns("smash").Returns("smash")
                .Returns("smash").Returns("smash").Returns("smash");

            //Act
            wordle.GameLoop();

            //Assert
            Assert.IsTrue(wordle.Ran);
            mockConsole.VerifyAll();
        }


        [TestMethod]
        public void GameLoop_Ends_After_Correct_Guess_Is_Made_First_Try()
        {
            //Arrange
            string answer = "crash";
            var mockConsole = new Mock<IConsole>();
            List<string> dictionary = new List<string>(["crash"]);
            Wordle wordle = new Wordle(mockConsole.Object, answer, dictionary);
            mockConsole.Setup(m => m.ReadLine()).Returns("crash");

            //Act
            wordle.GameLoop();

            //Assert
            Assert.IsTrue(wordle.Ran);
            mockConsole.VerifyAll();
        }

        [TestMethod]
        public void GameLoop_Ends_After_Correct_Guess_Is_Made_Second_Try()
        {
            //Arrange
            string answer = "crash";
            var mockConsole = new Mock<IConsole>();
            List<string> dictionary = new List<string>(["smash", "crash"]);
            Wordle wordle = new Wordle(mockConsole.Object, answer, dictionary);
            mockConsole.SetupSequence(m => m.ReadLine()).Returns("smash").Returns("crash");

            //Act
            wordle.GameLoop();

            //Assert
            Assert.IsTrue(wordle.Ran);
            mockConsole.VerifyAll();
        }

        [TestMethod]
        public void GameLoop_Ends_After_Correct_Guess_Is_Made_Fifth_Try()
        {
            //Arrange
            string answer = "crash";
            var mockConsole = new Mock<IConsole>();
            List<string> dictionary = new List<string>(["smash", "crash"]);
            Wordle wordle = new Wordle(mockConsole.Object, answer, dictionary);
            mockConsole.SetupSequence(m => m.ReadLine()).Returns("smash").Returns("smash")
                .Returns("smash").Returns("smash").Returns("crash");

            //Act
            wordle.GameLoop();

            //Assert
            Assert.IsTrue(wordle.Ran);
            mockConsole.VerifyAll();
        }

        [TestMethod]
        public void IsGuessValid_Returns_False_When_More_Than_Five_Characters()
        {
            //Arrange
            string answer = "crash";
            var testConsole = new TestConsole();
            List<string> dictionary = new List<string>(["smash", "crash"]);
            Wordle wordle = new Wordle(testConsole, answer, dictionary);
            string guess = "fungus";

            //Act
            var result = wordle.IsGuessValid(guess);

            //Assert
            Assert.IsFalse(result);
        }


    }

    }

        
        
     
    

