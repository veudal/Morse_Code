using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Morse_Code
{
    internal class Program
    {
        
        static bool shouldAccept = false;

        static void Main(string[] args)
        {

            Dictionary<char, string> dict = new Dictionary<char, string>
            {
                { 'a', ". -" },
                { 'b', "- . . ." },
                { 'c', "- . - ." },
                { 'd', "- . ." },
                { 'e', "." },
                { 'f', ". . - ." },
                { 'g', "- - ." },
                { 'h', ". . . ." },
                { 'i', ". ." },
                { 'j', ". - - -" },
                { 'k', "- . -" },
                { 'l', ". - . ." },
                { 'm', "- -" },
                { 'n', "- ." },
                { 'o', "- - -" },
                { 'p', ". - - ." },
                { 'q', "- - . -" },
                { 'r', ". - ." },
                { 's', ". . ." },
                { 't', "-" },
                { 'u', ". . -" },
                { 'v', ". . . -" },
                { 'w', ". - -" },
                { 'x', "- . . -" },
                { 'y', "- . - -" },
                { 'z', "- - . ." },
                { '1', ". - - - -" },
                { '2', ". . - - -" },
                { '3', ". . . - -" },
                { '4', ". . . . -" },
                { '5', ". . . . ." },
                { '6', "- . . . ." },
                { '7', "- - . . ." },
                { '8', "- - - . ." },
                { '9', "- - - - ." },
                { '0', "- - - - -" }
            };
            bool visible = ConfigureMode();
            MainLoop(dict, visible);
        }

        private static void MainLoop(Dictionary<char, string> dict, bool visible)
        {
            while (true)
            {
                Random random = new Random();
                var element = dict.ElementAt(random.Next(dict.Count));
                AskUser(visible, element);
            }
        }

        private static void AskUser(bool visible, KeyValuePair<char, string> element)
        {
            while (true)
            {
                shouldAccept = false;
                Thread thread = new Thread(() => PresentInformation(visible, element));
                thread.Start();
                ConsoleKeyInfo userInput = new ConsoleKeyInfo();
                while (shouldAccept == false)
                {
                    userInput = Console.ReadKey(true);
                }
                if (userInput.KeyChar.ToString().ToLower() == element.Key.ToString())
                {
                    CorrectInput();
                    break;
                }
                else
                {
                    WrongInput();
                }
            }
        }

        private static void PresentInformation(bool visible, KeyValuePair<char, string> element)
        {
            if (visible)
            {
                string[] resultArr = new string[2];
                for (int i = 0; i < element.Value.Length; i++)
                {
                    if (element.Value[i] == '-')
                    {
                        resultArr[0] += "████████████";
                        resultArr[1] += "████████████";
                    }
                    else if(element.Value[i] == '.')
                    {
                        resultArr[0] += "█████";
                        resultArr[1] += "█████";
                    }
                    else
                    {
                        for (int j = 0; j < resultArr.Length; j++)
                        {
                            resultArr[j] += "         ";

                        }
                    }
                }
                Console.WriteLine();
                int index = element.Key;
                while(index > 14)
                {
                    index -= 14;
                }
                Console.ForegroundColor = (ConsoleColor)index;
                foreach (var str in resultArr)
                {
                    Console.WriteLine(str);
                }
                Console.WriteLine();
                Thread.Sleep(200);
            }
            else
            {
                Console.WriteLine("Carefully listen!");
                var data = element.Value.Split(' ');
                foreach (var item in data)
                {
                    Thread.Sleep(400);
                    if (item == ".")
                    {
                        Console.Beep(300, 200);
                    }
                    else
                    {
                        Console.Beep(300, 600);
                    }
                }
            }
            shouldAccept = true;
        }

        private static bool ConfigureMode()
        {
            Console.WriteLine("Sound mode? (Enter 'true' or 'false')");
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (input == "true" || input == "false")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            Console.Clear();
            return !Convert.ToBoolean(input);
        }

        private static void WrongInput()
        {
            Console.Write("Wrong\n");
            Console.Beep(160, 700);
            Console.Clear();
        }

        private static void CorrectInput()
        {
            Console.Write("Correct");
            Console.Beep(700, 300);
            Thread.Sleep(500);
            Console.Clear();
        }
    }
}
