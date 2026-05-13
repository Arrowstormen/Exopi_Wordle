
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
        public void GameLoop_Ends_After_Correct_Guess_Is_Made_With_Capital_Letters()
        {
            //Arrange
            string answer = "crash";
            var mockConsole = new Mock<IConsole>();
            List<string> dictionary = new List<string>(["crash"]);
            Wordle wordle = new Wordle(mockConsole.Object, answer, dictionary);
            mockConsole.Setup(m => m.ReadLine()).Returns("CrAsH");

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

        [TestMethod]
        public void IsGuessValid_Returns_False_When_Less_Than_Five_Characters()
        {
            //Arrange
            string answer = "crash";
            var testConsole = new TestConsole();
            List<string> dictionary = new List<string>(["smash", "crash"]);
            Wordle wordle = new Wordle(testConsole, answer, dictionary);
            string guess = "fun";

            //Act
            var result = wordle.IsGuessValid(guess);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsGuessValid_Returns_Expected_Message_When_More_Than_Five_Characters()
        {
            //Arrange
            string answer = "crash";
            var testConsole = new TestConsole();
            List<string> dictionary = new List<string>(["smash", "crash"]);
            Wordle wordle = new Wordle(testConsole, answer, dictionary);
            string guess = "fungus";

            //Act
            wordle.IsGuessValid(guess);

            //Assert
            string expected = "A guess must be a word of exactly five letters.";
            Assert.AreEqual(expected, testConsole.output[0]);
        }

        [TestMethod]
        public void IsGuessValid_Returns_Expected_Message_When_Less_Than_Five_Characters()
        {
            //Arrange
            string answer = "crash";
            var testConsole = new TestConsole();
            List<string> dictionary = new List<string>(["smash", "crash"]);
            Wordle wordle = new Wordle(testConsole, answer, dictionary);
            string guess = "fun";

            //Act
            wordle.IsGuessValid(guess);

            //Assert
            string expected = "A guess must be a word of exactly five letters.";
            Assert.AreEqual(expected, testConsole.output[0]);
        }

        [TestMethod]
        public void IsGuessValid_Returns_False_When_Not_In_Dictionary()
        {
            //Arrange
            string answer = "crash";
            var testConsole = new TestConsole();
            List<string> dictionary = new List<string>(["smash", "crash"]);
            Wordle wordle = new Wordle(testConsole, answer, dictionary);
            string guess = "flash";

            //Act
            var result = wordle.IsGuessValid(guess);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsGuessValid_Returns_Expected_Message_When_Not_In_Dictionary()
        {
            //Arrange
            string answer = "crash";
            var testConsole = new TestConsole();
            List<string> dictionary = new List<string>(["smash", "crash"]);
            Wordle wordle = new Wordle(testConsole, answer, dictionary);
            string guess = "flash";

            //Act
            wordle.IsGuessValid(guess);

            //Assert
            string expected = "The guess is not in the game's dictionary; try again.";
            Assert.AreEqual(expected, testConsole.output[0]);
        }

        [TestMethod]
        public void IsGuessValid_Returns_True_When_Valid_Guess()
        {
            //Arrange
            string answer = "crash";
            var testConsole = new TestConsole();
            List<string> dictionary = new List<string>(["smash", "crash"]);
            Wordle wordle = new Wordle(testConsole, answer, dictionary);
            string guess = "smash";

            //Act
            var result = wordle.IsGuessValid(guess);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsGuessCorrect_Returns_False_When_Guess_Incorrect()
        {
            //Arrange
            string answer = "crash";
            var testConsole = new TestConsole();
            List<string> dictionary = new List<string>(["smash", "crash"]);
            Wordle wordle = new Wordle(testConsole, answer, dictionary);
            string guess = "smash";

            //Act
            var result = wordle.IsGuessCorrect(guess);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsGuessCorrect_Returns_True_When_Guess_Correct()
        {
            //Arrange
            string answer = "crash";
            var testConsole = new TestConsole();
            List<string> dictionary = new List<string>(["smash", "crash"]);
            Wordle wordle = new Wordle(testConsole, answer, dictionary);
            string guess = "crash";

            //Act
            var result = wordle.IsGuessCorrect(guess);

            //Assert
            Assert.IsTrue(result);
        }

    }

    }

        
        
     
    

