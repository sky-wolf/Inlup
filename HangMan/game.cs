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
            this.ReadList();
        }

        public void Run()
        {
            int left = 10;
            bool win = false;
            
            this.ValjOrd();

            string responses = new string(this.response);

            while (!win)
            {
                Console.Clear();
                this.Stats(left);

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
                            bool temp = this.GuessLetter(guess);
                            if (temp)
                            {
                                left--;
                            }
                        }
                        break;
                    default:
                        {
                            bool temp = this.GuessWord(guess);
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

                if(this.GuessWord(new string(s_responseList)))
                {
                    win = true;
                }
            }
            Console.Clear();
            if (win)
            {
                Console.WriteLine("We have a Winner");
                Console.WriteLine($"___________________________________\r\n                  \r\n ___________.._______\r\n|  __________))______|\r\n| | / /      ||\r\n| |/ /       ||\r\n| | /        ||\r\n| |/         ||\r\n| |          ||\r\n| |     \r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| | \t{this.PrintWord()}\r\n|_|___________________________________________________________");
            }
            else
            {
                Console.WriteLine("Game over");
                Console.WriteLine($"___________________________________\r\n                  \r\n ___________.._______\r\n|  __________))______|\r\n| | / /      ||\r\n| |/ /       ||\r\n| | /        ||\r\n| |/         ||\r\n| |          ||\r\n| | \r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| | \t{this.PrintWord()}\r\n|_|___________________________________________________________");
                Console.WriteLine($"looking for the word: {this.Printanswer()}");

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
                this.s_OrdList = list.ToArray();
            }

        }

        private void ValjOrd()
        {
            int i = Slummp.Roll((byte)s_OrdList.Length);

            this.response = s_OrdList[i].ToCharArray();
            this.s_responseList = s_OrdList[i].ToCharArray();

            int test = this.response.Length;

            for (int j = 0; j < test; j++)
            {
                this.s_responseList[j] = '_';
            }
        }

        public string Printanswer()
        {
            string res = "";
            for (int i = 0; i < this.response.Length; ++i)
            {
                res += this.response[i] + " " ;
            }
            return res;
        }
        public string PrintWord()
        {
            string res = "";
            for (int i = 0; i < this.s_responseList.Length; ++i)
            {
                res += this.s_responseList[i] + " ";
            }
            return res;
        }
        public bool GuessLetter(string guess)
        {
            bool res = false;
            bool inWord = false;
            if (guess.Length == 1)
            {
                for (int i = 0; i < this.response.Length; ++i)
                {
                    if (this.response[i] == Convert.ToChar(guess))
                    {
                        this.s_responseList[i] = Convert.ToChar(guess);
                        inWord = true;
                    }
                }
                if (inWord)
                {
                    res = true;
                }
                else if (!inWord)
                {
                    if (this.sb.Length == 0)
                    {
                        this.sb.Append(guess);
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
                            this.sb.Append(Convert.ToChar(guess));
                            res = true;
                        }

                    }
                }
            }
            return res;
        }

        public bool GuessWord(string guess)
        {
            bool test = guess.Equals(new string(this.response));

            if (test)
            {
                this.s_responseList = guess.ToCharArray();
            }
            
            return test;
        }

        private void Stats(int gus)
        {
            Console.WriteLine($"\r\n___________________________________\r\n                  Guesses left {gus}\r\n ___________.._______\r\n|  __________))______|\r\n| | / /      ||\r\n| |/ /       ||\r\n| | /        ||\r\n| |/         ||\r\n| |          ||\r\n| |     \r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| |\r\n| | \t{this.PrintWord()}\r\n|_|___________________________________________________________");
            Console.WriteLine($"Incorrect guesses: {this.sb.ToString()}");
        }

    }
}
