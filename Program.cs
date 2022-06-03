using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Assignment11
{
    class Program
    {
        public static void Error(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(s);
            Console.ResetColor();
        }
        
        public static bool IsDigigt(string s)
        {
            try
            {
                Int64.Parse(s);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            
        }
        
        static void Main(string[] args)
        {
            // ==========================================
            // change the .csv file directory in line 357
            // ==========================================

            string[] menu = 
            {
                "Genre of the movies with the duration less than 100 min",
                "Director of the movies played by Vin Diesel",
                "Most voted movie in 2016",
                "Title and revenue of the movies of Bryan Singer ordered by descending revenue",
                "Sum revenue of the movies produced in year 2011",
                "Average revenue of all the movies produced in year 2014",
                "Top 10 most revenue action movies with more than 120 min duration",
                "Movies with numbers in them",
                "Actor's movies ordered by descending rating and from oldest to newest",
                "Compare number drama and comedy movies with rating of more than 8",
                "Actor with the most bad movies (rating under 7)",
                "Number of movies that have more letter than 'Prometheus'",
                "Action movies or movies produced in year 2014",
                "Top 10 best comedy movies except first 3 ones",
                "Exit"
            };
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("____________Filter___________");
            Console.ResetColor();
            ShowMenu(menu);
            var data = new IMDB();
            
            int command = GetCommand(menu);
            while (command != 15)
            {
                if (command == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("____________Filter___________");
                    Console.ResetColor();
                    ShowMenu(menu);
                    command = GetCommand(menu);
                }
                else if (command == 1)
                {
                    data.Q1();
                    command = GetCommand(menu);
                }
                else if (command == 2)
                {
                    data.Q2();
                    command = GetCommand(menu);
                }
                else if (command == 3)
                {
                    data.Q3();
                    command = GetCommand(menu);
                }
                else if (command == 4)
                {
                    data.Q4();
                    command = GetCommand(menu);
                }
                else if (command == 5)
                {
                    data.Q5();
                    command = GetCommand(menu);
                }
                else if (command == 6)
                {
                    data.Q6();
                    command = GetCommand(menu);
                }
                else if (command == 7)
                {
                    data.Q7();
                    command = GetCommand(menu);
                }
                else if (command == 8)
                {
                    data.Q8();
                    command = GetCommand(menu);
                }
                else if (command == 9)
                {
                    Console.Write("Enter the name of the actor: ");
                    string name = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("_________________Sort by Rating________________");
                    Console.ResetColor();
                    data.Q9(name,true);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("_________________Sort by Date________________");
                    Console.ResetColor();
                    data.Q9(name, date:true);
                    command = GetCommand(menu);
                }
                else if (command == 10)
                {
                    data.Q10();
                    command = GetCommand(menu);
                }
                else if (command == 11)
                {
                    data.Q11();
                    command = GetCommand(menu);
                }
                else if (command == 12)
                {
                    data.Q12();
                    command = GetCommand(menu);
                }
                else if (command == 13)
                {
                    data.Q13();
                    command = GetCommand(menu);
                }
                else
                {
                    data.Q14();
                    command = GetCommand(menu);
                }
            }
            

        }
        
        public static void ShowMenu(string[] s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                Console.WriteLine($"{i + 1}- {s[i]}");
            }
        }
        
        public static int GetCommand(string[] s)
        {
            Console.Write("Please enter your command (press Enter if you want to see the menu again): ");
            string command = Console.ReadLine();
            if (command == "")
            {
                return 0;
            }
            int c = 0;
            if (IsDigigt(command))
            {
                c = int.Parse(command);
                if (c >= 1 && c <= s.Length)
                {
                    return c;
                }
                Error("Invalid Command\n");
                return GetCommand(s);
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].ToLower() == command.ToLower())
                {
                    return i + 1;
                }
            }
            Error("Invalid Command\n");
            return GetCommand(s);

        }
    }

    public static class Extensions
    {
        public static Nullable<int> ParseIntOrNull(this string str)
            => !string.IsNullOrEmpty(str) ? int.Parse(str) as Nullable<int> : null;
        
        public static Nullable<double> ParseDoubleOrNull(this string str)
            => !string.IsNullOrEmpty(str) ? double.Parse(str) as Nullable<double> : null;
        public static string ParseStringOrNull(this string str)
            => !string.IsNullOrEmpty(str) ? str : null;

        //For example
        public static IMDBData GetHighestMetascore(this IEnumerable<IMDBData> data)
            => data.OrderByDescending(x => x.Metascore).First();

        /// <summary>
        /// you must modify the name of this method and its 
        /// implementation to fit your need and create more methods like this
        public static IMDBData ExtensionMethodPlaceHolder(this IEnumerable<IMDBData> data)
            => data.First();

        public static string NameModify(this string s)
        {
            string out1 = "";
            if (s[0] == ' ' || s[0] == '"')
            {
                out1 += s.Substring(1, s.Length - 1);
            }
            else
            {
                out1 += s;
            }

            if (s[s.Length - 1] == ' ' || s[s.Length - 1] == '"')
            {
                return out1.Substring(0,out1.Length - 2);
            }
            return out1;
        }

    }
    
    
    

    public class IMDBData
    {
        public IMDBData(string line)
        {
            var toks = line.Split(',');
            Rank = int.Parse(toks[0]);
            Title = toks[1];
            Genre = toks[2];
            Director = toks[3];
            Actor1 = toks[4].NameModify();
            Actor2 = toks[5].NameModify();
            Actor3 = toks[6].NameModify();
            Actor4 = toks[7].NameModify();
            Year = int.Parse(toks[8]);
            Runtime = int.Parse(toks[9]);
            Rating = toks[10].ParseDoubleOrNull();
            Votes = int.Parse(toks[11]);
            Revenue = toks[12].ParseDoubleOrNull();
            Metascore = toks[13].ParseIntOrNull();
        }
        public int Rank;
        public string Title;
        public string Genre;
        public string Director;
        public string Actor1;
        public string Actor2;
        public string Actor3;
        public string Actor4;
        public int Year;
        public int Runtime;
        public Nullable<double> Rating;
        public int Votes;
        public Nullable<double> Revenue;
        public Nullable<int> Metascore;
        
    }


    public class IMDB : IEnumerable<IMDBData>
    {
        public static void RedPrint(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(s);
            Console.ResetColor();
        }
        
        public static bool IsDigigt(char c)
        {
            try
            {
                int.Parse(c.ToString());
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            
        }

        public static bool HasNumber(string name)
        {
            string[] numbers = new[]
            {
                "zero", " one", "two", "three", "four", "five", "six", "seven",
                "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fif", "twenty", "hundred", "thousand",
                "million", "billion"
            };
            foreach (char c in name)
            {
                if (IsDigigt(c))
                {
                    return true;
                }
            }

            foreach (string s in numbers)
            {
                if (name.ToLower().IndexOf(s) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        public static int ValidNumOfLetters(string s)
        {
            int n = s.Length;
            int counter = 0;
            foreach (var c in s)
            {
                if ((c >= 65 && c <= 90) || (c >= 97 || c <= 122))
                {
                    counter++;
                }
            }

            return counter;
        }

        public static bool ActionOr2014(IMDBData movie)
        {
            if (movie.Year == 2014)
            {
                return true;
            }

            if (movie.Genre.ToLower() == "action")
            {
                return true;
            }

            return false;
        }
        
        private IEnumerable<IMDBData> _datas = File.ReadAllLines(@"C:\Users\Armageddon\Desktop\CSVParser\CSVParser\IMDB-Movie-Data.csv")
            .Skip(1)
            .Select(line => new IMDBData(line));
        
        public IEnumerator<IMDBData> GetEnumerator()
        {
            foreach (IMDBData movie in _datas)
            {
                yield return movie;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Q1()
        {
            var resutlt = from d in _datas where d.Runtime < 100 select d;
            int counter = 1;
            foreach (IMDBData d in resutlt)
            {
                Console.Write($"{counter}. ");
                counter++;
                RedPrint("Title: ");
                Console.Write($"{d.Title}   ");
                RedPrint("Genre: ");
                Console.Write($"{d.Genre}   ");
                RedPrint("Duration: ");
                Console.WriteLine($"{d.Runtime} min");
                Console.WriteLine("___________________________");
            }
        }
        
        public void Q2()
        {
            var data = _datas.Where(x => x.Actor1.ToLower() == "vin diesel" ||
                                         x.Actor2.ToLower() == "vin diesel" ||
                                         x.Actor3.ToLower() == "vin diesel" ||
                                         x.Actor4.ToLower() == "vin diesel");
            int counter = 1;
            foreach (IMDBData movie in data)
            {
                Console.Write($"{counter}. ");
                counter++;
                RedPrint("Title: ");
                Console.Write($"{movie.Title}   ");
                RedPrint("Director: ");
                Console.WriteLine(movie.Director);
                Console.WriteLine("___________________________");
            }
        }

        public void Q3()
        {
            var data = _datas.GroupBy(x => x.Year).Where(x => x.Key == 2016)
                .SelectMany(x=>x).OrderByDescending(x => x.Votes).First();
                
            RedPrint("Title: ");
            Console.WriteLine($"{data.Title}");
            RedPrint("Genre: ");
            Console.WriteLine($"{data.Genre}");
            RedPrint("Director: ");
            Console.WriteLine($"{data.Director}");
            RedPrint("Actors: ");
            Console.WriteLine($"{data.Actor1}, {data.Actor2}, {data.Actor3}, {data.Actor4}");
            RedPrint("Year: ");
            Console.WriteLine($"{data.Year}");
            RedPrint("Runtime: ");
            Console.WriteLine($"{data.Runtime}");
            RedPrint("Rating: ");
            Console.WriteLine($"{data.Rating}");
            RedPrint("Votes: ");
            Console.WriteLine($"{data.Votes}");
            RedPrint("Revenue: ");
            Console.WriteLine($"{data.Revenue}");
            RedPrint("Metascore: ");
            Console.WriteLine($"{data.Metascore}");
            Console.WriteLine("_________________________");

        }

        public void Q4()
        {
            var data = _datas.Where(x => x.Director == "Bryan Singer")
                .OrderByDescending(x => x.Revenue);
            
            int counter = 1;
            foreach (IMDBData movie in data)
            {
                Console.Write($"{counter}. ");
                counter++;
                RedPrint("Title: ");
                Console.Write($"{movie.Title}   ");
                RedPrint("Revenue: ");
                Console.WriteLine(movie.Revenue);
                Console.WriteLine("__________________________");
            }
        }

        public void Q5()
        {
            var data = _datas.Where(x => x.Year == 2011).Select(x => x.Revenue).Sum();
            RedPrint("Sum of revenues of: ");
            Console.WriteLine(data);
            Console.WriteLine("____________________________");
        }

        public void Q6()
        {
            var data = _datas.Where(x => x.Year == 2014).Select(x => x.Revenue);
            if (data.Count() == 0)
            {
                Console.WriteLine("No Movie in 2011");
                return;
            }
            RedPrint("Average Revenue: ");
            Console.WriteLine(data.Sum()/data.Count());
            Console.WriteLine("____________________________");
        }

        public void Q7()
        {
            var data = _datas.Where(x => x.Genre.ToLower() == "action" && x.Runtime > 120)
                .OrderByDescending(x => x.Rating);

            int counter = 1;
            foreach (var movie in data)
            {
                Console.Write($"{counter}. ");
                counter++;
                RedPrint("Title: ");
                Console.Write($"{movie.Title}   ");
                RedPrint("Runtime: ");
                Console.Write($"{movie.Runtime}    ");
                RedPrint("Rating: ");
                Console.WriteLine(movie.Rating);
                Console.WriteLine("_________________________");
                if (counter == 11)
                {
                    return;
                }
            }
        }
        
        public void Q8()
        {
            var data = _datas.Where(x => HasNumber(x.Title)).Select(x => x.Title);
            int counter = 1;
            foreach (string title in data)
            {
                Console.Write($"{counter}. ");
                counter++;
                RedPrint("Title: ");
                Console.WriteLine(title);
                Console.WriteLine("_________________________");
            }
        }
        
        public void Q9(string name, bool rating = false, bool date = false)
        {
            if (rating)
            {
                var data = _datas.Where(x => x.Actor1.ToLower() == name.ToLower() ||
                                             x.Actor2.ToLower() == name.ToLower() ||
                                             x.Actor3.ToLower() == name.ToLower() || 
                                             x.Actor4.ToLower() == name.ToLower()).OrderByDescending(x => x.Rating);
                int counter = 1;
                foreach (IMDBData movie in data)
                {
                    Console.Write($"{counter}. ");
                    counter++;
                    RedPrint("Title: ");
                    Console.Write($"{movie.Title}   ");
                    RedPrint("Rating: ");
                    Console.WriteLine(movie.Rating);   
                    Console.WriteLine("_________________________");
                }
                return;
            } 
            if (date)
            {
                var data = _datas.Where(x => x.Actor1.ToLower() == name.ToLower() ||
                                             x.Actor2.ToLower() == name.ToLower() ||
                                             x.Actor3.ToLower() == name.ToLower() || 
                                             x.Actor4.ToLower() == name.ToLower()).OrderBy(x => x.Year);
                int counter = 1;
                foreach (IMDBData movie in data)
                {
                    Console.Write($"{counter}. ");
                    counter++;
                    RedPrint("Title: ");
                    Console.Write($"{movie.Title}   ");
                    RedPrint("Year: ");
                    Console.WriteLine(movie.Year);
                    Console.WriteLine("_________________________");
                }
            } 
        }
        
        
        public void Q10()
        {
            var comedy = _datas.Where(x => x.Genre.ToLower() == "comedy" && x.Rating > 8);
            var drarma = _datas.Where(x => x.Genre.ToLower() == "drama" && x.Rating > 8);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("______________Comedy______________");
            Console.WriteLine($"Number of dramas: {comedy.Count()}");
            int counter = 1; 
            foreach (var movie in comedy)
            {
                Console.Write($"{counter}. ");
                counter++;
                RedPrint("Title: ");
                Console.Write($"{movie.Title}   ");
                RedPrint("Rating: ");
                Console.WriteLine(movie.Rating);
                Console.WriteLine("____________________________");
            }

            counter = 1;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("__________________Drama_________________");
            Console.WriteLine($"Number of dramas: {drarma.Count()}");
            foreach (var movie in drarma)
            {
                Console.Write($"{counter}. ");
                counter++;
                RedPrint("Title: ");
                Console.Write($"{movie.Title}   ");
                RedPrint("Rating: ");
                Console.WriteLine(movie.Rating);
                Console.WriteLine("____________________________");
            }
        }
        
        public void Q11()
        {
            IDictionary<string, int> BadMoviePerAtor = new Dictionary<string, int>();
            var data = _datas.Where(x => x.Rating < 7);
            foreach (var movie in data)
            {
                if (BadMoviePerAtor.ContainsKey(movie.Actor1))
                {
                    BadMoviePerAtor[movie.Actor1]++;
                }
                else
                {
                    BadMoviePerAtor.Add(movie.Actor1,1);
                }
                
                if (BadMoviePerAtor.ContainsKey(movie.Actor2))
                {
                    BadMoviePerAtor[movie.Actor2]++;
                }
                else
                {
                    BadMoviePerAtor.Add(movie.Actor2,1);
                }
                
                if (BadMoviePerAtor.ContainsKey(movie.Actor3))
                {
                    BadMoviePerAtor[movie.Actor3]++;
                }
                else
                {
                    BadMoviePerAtor.Add(movie.Actor3,1);
                }
                
                if (BadMoviePerAtor.ContainsKey(movie.Actor4))
                {
                    BadMoviePerAtor[movie.Actor4]++;
                }
                else
                {
                    BadMoviePerAtor.Add(movie.Actor4,1);
                }
            }

            int max = 0;
            string name = "";
            foreach (KeyValuePair<string,int> kvp in BadMoviePerAtor)
            {
                if (kvp.Value > max)
                {
                    max = kvp.Value;
                    name = kvp.Key;
                }
            }

            RedPrint($"Actor's name with the most bad movies is: {name}        Number of Bad Movies: {max}\n");
            Console.WriteLine("_______________________________");
            
        }


        public void Q12()
        {
            var data = _datas.Where(x => ValidNumOfLetters(x.Title) > "prometheus".Length);
            int counter = 1;
            foreach (var movie in data)
            {
                counter++;
                RedPrint($"{counter}.Title: {movie.Title}     Number of Letters: {ValidNumOfLetters(movie.Title)}");
                Console.WriteLine("\n____________________________");
            }
        }
        
        public void Q13()
        {
            var data = _datas.Where(x => ActionOr2014(x)).Select(x => x.Title);
            int counter = 1;
            foreach (var name in data)
            {
                Console.Write($"{counter}. ");
                counter++;
                RedPrint("Title: ");
                Console.WriteLine(name);
                Console.WriteLine("__________________________");
            }
        }


        public void Q14()
        {
            var data = _datas.Where(x => x.Genre.ToLower() == "comedy").OrderByDescending(x => x.Rating);
            int counter = 1;
            foreach (var movie in data)
            {
                if (counter < 4)
                {
                    counter++;
                    continue;
                }
                Console.Write($"{counter}. ");
                RedPrint("Title: ");
                Console.Write($"{movie.Title}   ");
                RedPrint("Rating: ");
                Console.WriteLine(movie.Rating);
                Console.WriteLine("_____________________________");
                counter++;
                if (counter == 11)
                {
                    break;
                }
            }
        }
    }
    
    
}
