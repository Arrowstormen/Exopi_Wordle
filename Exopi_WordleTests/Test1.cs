
using ExopiWordle;
using ExopiWordle.Consoles;

namespace ExopiWordleTests
{
    [TestClass]
    public sealed class TestConsole : IConsole
    {
        public List<String> output = new List<String>();
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
        public void TestMethod1()
        {
        }
    }
}
