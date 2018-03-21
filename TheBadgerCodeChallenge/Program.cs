using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBadgerCodeChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();

            Console.ReadLine();

        }

        public static void Start()
        {
            Random rng = new Random();
            string[] seatLetters = { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K" };
            int numberOfRows = GetNumberOfRows();
            List<string> reservedSeatsList = RandomReservedArray(seatLetters, numberOfRows, rng);
            int openSeatingOptions = CheckAvailableSeats(seatLetters, reservedSeatsList, numberOfRows);

            PrintResults(seatLetters, reservedSeatsList, numberOfRows, openSeatingOptions);

            Again();
        }

        public static int GetNumberOfRows()
        {
            Console.WriteLine("Please enter how many rows are available (1-50):");
            if (int.TryParse(Console.ReadLine(), out int rows) && rows>0 && rows<=50)
            {
                return rows;
            }
            else
            {
                Console.WriteLine("Input must be an integer between 1-50.");
                return GetNumberOfRows();
            }
        }

        public static List<string> RandomReservedArray(string[] seatLetters, int rows, Random rng)
        {
            List<string> possibleSeats = new List<string>();
            List<string> reservedSeatsList = new List<string>();
            int filledSeats = rng.Next(0, (rows * 10) + 1);

            foreach (string letter in seatLetters)
            {
                for (int i = 1; i <= rows; i++)
                {
                    possibleSeats.Add(string.Format("{0}{1}", letter, i));
                }
            }
            
            while (reservedSeatsList.Count < filledSeats)
            {
                int randomSeat = rng.Next(0, possibleSeats.Count);

                if (!reservedSeatsList.Contains(possibleSeats[randomSeat]))
                {
                    reservedSeatsList.Add(possibleSeats[randomSeat]);
                    possibleSeats.Remove(possibleSeats[randomSeat]);
                }

            }

            return reservedSeatsList;

        }

        public static int CheckAvailableSeats(string[] seatLetters, List<string> reservedSeatsList, int numberOfRows)
        {
            int openSeatingOptions = 0;

            for (int i = 0; i < numberOfRows; i++)
            {
                int consecutiveOpenSeats = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (!reservedSeatsList.Contains(string.Format("{0}{1}", seatLetters[j], i+1)))
                    {
                        consecutiveOpenSeats++;
                        if (consecutiveOpenSeats==3)
                        {
                            openSeatingOptions++;
                        }
                    }
                    else
                    {
                        consecutiveOpenSeats = 0;
                    }
                }

                consecutiveOpenSeats = 0;

                for (int j = 3; j < 7; j++)
                {
                    if (!reservedSeatsList.Contains(string.Format("{0}{1}", seatLetters[j], i+1)))
                    {
                        consecutiveOpenSeats++;
                        if (consecutiveOpenSeats == 3)
                        {
                            openSeatingOptions++;
                        }
                    }
                    else
                    {
                        consecutiveOpenSeats = 0;
                    }
                }

                consecutiveOpenSeats = 0;

                for (int j = 7; j < 10; j++)
                {
                    if (!reservedSeatsList.Contains(string.Format("{0}{1}", seatLetters[j], i + 1)))
                    {
                        consecutiveOpenSeats++;
                        if (consecutiveOpenSeats == 3)
                        {
                            openSeatingOptions++;
                        }
                    }
                    else
                    {
                        consecutiveOpenSeats = 0;
                    }
                }
            }

            return openSeatingOptions;
        }

        public static void PrintResults(string[] seatLetters, List<string> reservedSeatsList, int numberOfRows, int openSeatingOptions)
        {
            Console.WriteLine("\n     A   B   C       D   E   F   H       J   K   L");

            for (int i = 0; i < numberOfRows; i++)
            {
                Console.Write("{0, -2}  ", i+1);

                for (int j = 0; j < 3; j++)
                {
                    if (!reservedSeatsList.Contains(string.Format("{0}{1}", seatLetters[j], i + 1)))
                    {
                        Console.Write("[ ] ");
                    }
                    else
                    {
                        Console.Write("[x] ");
                    }
                }

                Console.Write("    ");

                for (int j = 3; j < 7; j++)
                {
                    if (!reservedSeatsList.Contains(string.Format("{0}{1}", seatLetters[j], i + 1)))
                    {
                        Console.Write("[ ] ");
                    }
                    else
                    {
                        Console.Write("[x] ");
                    }
                }

                Console.Write("    ");

                for (int j = 7; j < 10; j++)
                {
                    if (!reservedSeatsList.Contains(string.Format("{0}{1}", seatLetters[j], i + 1)))
                    {
                        Console.Write("[ ] ");
                    }
                    else
                    {
                        Console.Write("[x] ");
                    }
                }

                Console.WriteLine();
            }
            Console.WriteLine("\n{0} possible seating option(s) available.", openSeatingOptions);
            Console.WriteLine("Refer to the chart above to see open seats.");
            Console.WriteLine("Those marked with an 'x' have been reserved.\n");
        }

        public static void Again()
        {
            Console.WriteLine("Press 'Y' to search again or any other key to exit.");
            ConsoleKeyInfo answer = Console.ReadKey();

            if (answer.KeyChar=='y' || answer.KeyChar=='Y')
            {
                Console.WriteLine();
                Console.WriteLine();
                Start();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Goodbye!");
            }
        }
    }
}
