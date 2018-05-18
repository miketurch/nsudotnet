using System;
using TurchinovichZhuk.Nsudotnet.TicTacToe.Model;


namespace TurchinovichZhuk.Nsudotnet.TicTacToe.View
{
    public class ConsoleView
    {
        private CellState[] _cellStates;
        private int[] _colors = new int[81];
        
        public void SetField(CellState[] states)
        {
            _cellStates = states;
            
        }
        
        public void ShowRules()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hello, lets have some fun!");
            Console.WriteLine("We are going to play in TicTacToe and rules are very simple:");
            Console.WriteLine("We have big field with 9 simple Tic Tac Toe fields.");
            Console.WriteLine("Win little field - get one point on big one!");
            Console.WriteLine("You can go only the same cell that was chosen in little field!");
            Console.WriteLine("Enter your step with two numbers and space between followed dy Enter.");
            Console.WriteLine("Good Luck!");
         
        }

        public void PrintField()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ ");
            for (int k = 0; k <= 54; k += 27)
            {
                for (int i = k; i <= k + 6; i += 3)
                {
                    for (int j = i; j <= i + 18; j += 9)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            if (_colors[j+l] == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                            else if (_colors[j+l] == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else if (_colors[j+l] == 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                            }
                            if (_cellStates[j + l] == CellState.None)
                            {
                                Console.Write("[] ");
                            }
                            else
                            {
                                Console.Write(_cellStates[j + l] + "  ");
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("| ");
                    }
                    Console.WriteLine();
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ ");
            }
            Console.WriteLine();
        }
        
        public int[] GetSteps()
        {
            int[] steps = new int[2];
            string[] stepStrings = new string[3];
            string input = Console.ReadLine();
            try
            {
                stepStrings = input.Split(' ');
            }
            catch (NullReferenceException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong format, lets try again!");
                steps = GetSteps();
            }
            int outsideStep;
            int insideStep;
            if (stepStrings.Length==2 && Int32.TryParse(stepStrings[0], out outsideStep) && Int32.TryParse(stepStrings[1], out insideStep))
            {
                steps[0] = outsideStep;
                steps[1] = insideStep;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong format, lets try again!");
                Console.ForegroundColor = ConsoleColor.White;
                steps = GetSteps();
            }
            return steps;
        }

        public void SetLittleWinner(bool isX, int number)
        {
            int color = isX ? 2 : 1;
            for (int j = number * 9; j < number * 9 + 9; j++)
            {
                _colors[j] = color;
            }
        }

        public void WrongFieldMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You can`t go this field!");
        }
        
        public void BusyCellMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You can`t use this cell!");
        }

        public void Win(bool isX)
        {
            char winner = isX ? 'X' : 'O';
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(winner + " win! Congradulations!");
        }
        
        public void Lose()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game over!");
        }
    }
}