using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HangMan
{
    public class Game
    {
        private string[] s_OrdList;
        public char[] s_responseList;
        public char[] response;
        private StringBuilder sb = new StringBuilder();

        public void Init()
        {
            ReadList();
        }

        public void Run()
        {
            int left = 10;
            bool win = false;
            
            ValjOrd();

            string responses = new string(response);

            while (!win)
            {
                Console.Clear();
                Stats(left);

                //read only letters
                string guess;
                do
                {
                    guess = Console.ReadLine();
                    if (true)
                    {

                    }
                } while (guess.Any(c => !char.IsLetter(c)) || string.IsNullOrWhiteSpace(guess));

                if(s_responseList.Length != response.Length)
                {
                    break;
                }

                switch (guess.Length)
                {
                    case 1:
                        {
                            bool temp = GuessLetter(guess);
                            if (temp)
                            {
                                left--;
                            }
                        }
                        break;
                    default:
                        {
                            bool temp = GuessWord(guess);
                            if(temp)
                            {
                                win = true;
                            }else
                            {
                                left--;
                            }

                        }
                        break;
                }
              
                if(left == 0)
                    break;

                if(GuessWord(new string(s_responseList)))
                {
                    win = true;
                }
            }
            Console.Clear();
            if (win)
            {
                Console.WriteLine("We have a Winner");
                Console.WriteLine($"___________________________________\r\n                  \r\n ___________.._______\r\n|  __________))______|\r\n| | / /      ||\r\n| |/ /       ||\r\n| | /        ||\r\n| |/         ||\r\n| |          ||\r\n| |     \r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| | \t{PrintWord()}\r\n|_|___________________________________________________________");
            }
            else
            {
                Console.WriteLine("Game over");
                Console.WriteLine($"___________________________________\r\n                  \r\n ___________.._______\r\n|  __________))______|\r\n| | / /      ||\r\n| |/ /       ||\r\n| | /        ||\r\n| |/         ||\r\n| |          ||\r\n| | \r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| | \t{PrintWord()}\r\n|_|___________________________________________________________");
                Console.WriteLine($"looking for the word: {Printanswer()}");

            }
            Console.ReadKey(true);
        }

         private void ReadList()
        {
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\ord.txt";
            List<string> list = new List<string>();

            String line;
            try
            {
                StreamReader sr = new StreamReader(path);

                line = sr.ReadLine();

                while (line != null)
                {
                    list.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
               s_OrdList = list.ToArray();
            }

        }

        private void ValjOrd()
        {
            int i = Slummp.Roll((byte)s_OrdList.Length);

            response = s_OrdList[i].ToCharArray();
            s_responseList = s_OrdList[i].ToCharArray();

            int test = response.Length;

            for (int j = 0; j < test; j++)
            {
                s_responseList[j] = '_';
            }
        }

        public string Printanswer()
        {
            string res = "";
            for (int i = 0; i < response.Length; ++i)
            {
                res += response[i] + " " ;
            }
            return res;
        }
        public string PrintWord()
        {
            string res = "";
            for (int i = 0; i < s_responseList.Length; ++i)
            {
                res += s_responseList[i] + " ";
            }
            return res;
        }
        public bool GuessLetter(string guess)
        {
            bool res = false;
            bool inWord = false;
            if (guess.Length == 1)
            {
                for (int i = 0; i < response.Length; ++i)
                {
                    if (response[i] == Convert.ToChar(guess))
                    {
                        s_responseList[i] = Convert.ToChar(guess);
                        inWord = true;
                    }
                }
                if (inWord)
                {
                    res = true;
                }
                else if (!inWord)
                {
                    if (sb.Length == 0)
                    {
                        sb.Append(guess);
                        res = true;
                    }
                    else
                    {
                        bool duplicat = false;
                        for (int j = 0; j < sb.Length; j++)
                        {
                            char test = sb[j];
                            if (test == Convert.ToChar(guess))
                            {
                                duplicat = true;
                            }
                        }
                        if (!duplicat)
                        {
                            sb.Append(Convert.ToChar(guess));
                            res = true;
                        }

                    }
                }
            }
            return res;
        }

        public bool GuessWord(string guess)
        {
            bool test = guess.Equals(new string(response));

            if (test)
            {
               s_responseList = guess.ToCharArray();
            }
            
            return test;
        }

        private void Stats(int gus)
        {
            Console.WriteLine($"\r\n___________________________________\r\n                  Guesses left {gus}\r\n ___________.._______\r\n|  __________))______|\r\n| | / /      ||\r\n| |/ /       ||\r\n| | /        ||\r\n| |/         ||\r\n| |          ||\r\n| |     \r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| | \t{PrintWord()}\r\n|_|___________________________________________________________");
            Console.WriteLine($"Incorrect guesses: {sb.ToString()}");
        }

    }
}
