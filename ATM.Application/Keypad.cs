using static System.Console;

namespace ATM.Application
{
    public class Keypad
    {
        public int GetInput()
        {
            int.TryParse(ReadLine(), out int output);
            return output;
        }
    }
}
