/*
 * Name: Christian Lachapelle
 * Student #: A00230066
 * 
 * Title: Lab1 - I/O and other Methods
 * Version: 1.0
 * 
 * Description: Collect a meal price from the user and apply the tax and tip.
 *              The program will provide the calculated the amount divided by
 *              the # of people.
 */


using System;
using System.Globalization;

namespace Lab1
{
    public class Bill
    {
        private double _price;
        private double _tax = 0.13; // Default tax rate of 13%
        private double _tip;
        private double _total;
        private byte _numPeople = 1; // Default number of people sharing the bill
        private byte _attempts; // Keeps track of user input attempts


        // This method gets the initial price to work with
        private double getPrice()
        {
            _attempts = 1; // Reset attempts

            Console.Write("Enter the price of your meal: ");

            /*
             * If the input isn't a valid double or the value is < 0
             * Give the user 3 attemps to get it right 
             * before the application exits
             */
            while (!double.TryParse(Console.ReadLine(), out _price) | _price < 0.00)
            {
                if (_attempts < 3)
                {
                    Console.WriteLine("ERROR: Invalid entry - Please try again");
                    Console.Write("Enter the price of your meal: ");
                    _attempts++;
                }
                else
                {
                    // If price is invalid after 3 attempts, exit program
                    Console.WriteLine("\nERROR: Too many invalid entries - Exiting");
                    Environment.Exit(1); // Exit application with error code 1
                }   
            }
            return _price;
        }

        // This method gets the tip value if any
        private double getTip()
        {
            _attempts = 1; // Reset attempts
            char ynAnswer; // Hold answer if the user wants to tip

            Console.Write("Do you wish to add a tip [Y/N]: ");

            /*
             * If the input isn't a character
             * Give the user 3 attemps to get it right 
             * before the application defaults the tip to 20%
             */
            while (!char.TryParse(Console.ReadLine().ToLower(), out ynAnswer))
            {
                if (_attempts < 3)
                {
                    Console.WriteLine("ERROR: Invalid entry - Please try again");
                    Console.Write("Do you wish to add a tip [Y/N]: ");
                    _attempts++;
                }
                else
                {
                    _tip = 0.20; //Gives a 20% tip. A user's incompetence fee.
                }
            }

            switch (ynAnswer)
            {
                case 'y':
                    Console.Write(@"
How much would you like to tip?

            1) 10%
            2) 15%
            3) 20%
            4) Other
            0) Changed my mind

 Selection: ");

                    byte tipAnswer; // Holds the user's tip amount

                    /*
                     * If the input isn't a byte
                     * Give the user 3 attemps to get it right 
                     * before the application defaults the tip to 20%
                     */
                    while (!byte.TryParse(Console.ReadLine(), out tipAnswer))
                    {
                        if (_attempts < 3)
                        {
                            Console.Write("ERROR: Invalid entry - Please try again");
                            Console.Write(@"
How much would you like to tip?

            1) 10%
            2) 15%
            3) 20%
            4) Other
            0) Changed my mind

 Selection: ");
                            _attempts++;
                        }
                        else
                        {
                            _tip = 0.20; //Gives a 20% tip. A user's incompetence fee.
                        }
                    }

                    switch (tipAnswer)
                    {
                        case 1:
                            _tip = 0.10; // 10% tip
                            break;

                        case 2:
                            _tip = 0.15; // 15% tip
                            break;

                        case 3:
                            _tip = 0.20; // 20% tip
                            break;

                        case 4:
                            _attempts = 1; // Reset attempts

                            Console.Write("Please enter a tip %: ");

                            /*
                             * If the input isn't a double
                             * Give the user 3 attemps to get it right 
                             * before the application defaults the tip to 20%
                             */
                            while (!double.TryParse(Console.ReadLine(), out _tip))
                            {
                                if (_attempts < 3)
                                {
                                    Console.WriteLine("ERROR: Invalid entry - Please try again");
                                    Console.Write("Please enter a tip %: ");
                                    _attempts++;
                                }
                                else
                                {
                                    _tip = 0.20; //Gives a 20% tip. A user's incompetence fee.
                                }
                            }
                            _tip /= 100; // Convert whole number to decimal. IE 40% -> 0.40
                            break;

                        default:
                            _tip = 0.00; // The user changed their minds about tipping
                            break;
                    }
                    break;

                default:
                    _tip = 0.00; // The user chose not to tip. Cheap bastard!
                    break;
            }
            return _tip;
        }

        // This method determins how many people are sharing the bill
        private byte goingDutch()
        {
            _attempts = 1; // Reset attempts
            char ynAnswer; // Holds user's answer

            Console.Write("Are you sharing the bill? [Y/n]: ");

            /*
            * If the input isn't a char
            * Give the user 3 attemps to get it right 
            * before the application defaults 1 person
            */
            while (!char.TryParse(Console.ReadLine().ToLower(), out ynAnswer))
            {
                if (_attempts < 3)
                {
                    Console.WriteLine("ERROR: Invalid entry - Please try again");
                    Console.Write("Are you sharing the bill? [Y/n]: ");
                    _attempts++;
                }
                else
                {
                    _numPeople = 1; // If the user failed to answer it will default to 1
                }
            }

            switch (ynAnswer)
            {
                case 'y':
                    Console.Write("How many people?: ");

                    byte numPeople = 0;

                    /*
                    * If the input isn't a byte or number of people < 1
                    * Give the user 3 attemps to get it right 
                    * before the application defaults 1 person
                    */
                    while (!byte.TryParse(Console.ReadLine(), out numPeople) | numPeople < 1)
                    {
                        if (_attempts < 3)
                        {
                            Console.WriteLine("ERROR: Invalid entry - Please try again");
                            Console.Write("How many people?: ");
                                _attempts++;
                        }
                        else
                        {
                            _numPeople = numPeople; // Default to 1 person
                        }
                    }
                    _numPeople = numPeople;
                    break;

                default:
                    _numPeople = 1; // Default to 1 person
                    break;
            }
            return _numPeople;
        }

        // This method calculates the total
        private double calculateTotal(double price, double tip)
        {
            _total = price + price * _tax + price * tip;
            return _total;
        }

        // This method sends formatted results to the screen
        private string sendResults(double total, byte dutch)
        {
            string stringBlock =
                @"
        Summary

      Price:    {0, 0:C2}
Tax @{1, 0:F2}%:    {2, 0:C2}
Tip @{3, 0:F2}%:    {4, 0:C2}
      Total:    {5, 0:C2}

Number of people sharing the bill: {6}
The total per person is: {7, 0:C2}"; // Pre-formatted string block to display tot he user

            string result = String.Format($"{stringBlock}", _price, (_tax * 100), (_price * _tax), (_tip * 100), (_price * _tip), total, _numPeople, (total/dutch));
            return result;
        }

        // This method is the main entry point to the class
        public void run()
        {
            Console.Clear(); // Clear the screen on startup
            Console.WriteLine(sendResults(calculateTotal(getPrice(), getTip()), goingDutch()));
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Bill bill = new Bill();
            bill.run();
        }
    }
}
