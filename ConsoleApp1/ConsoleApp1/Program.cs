using System;
// add additional classes required for Regex validation
using System.Text.RegularExpressions;
using System.Globalization;

namespace StudentsGrades
{
    class Program
    {
        static void Main(string[] args)
        {



            // declare variables
            String sStudentName, iStudentNumber, sMarks;
            double dGrades = 0; // initialise grades
            string[] Module = new string[6];
            double[] aDoubleGrades = new double[6];
            string title = @"
   _____ _             _            _                          _                    _            _       _            
  / ____| |           | |          | |                        | |                  | |          | |     | |            
 | (___ | |_ _   _  __| | ___ _ __ | |_   _ __ ___   __ _ _ __| | _____    ___ __ _| | ___ _   _| | __ _| |_ ___  _ __
  \___ \| __| | | |/ _` |/ _ \ '_ \| __| | '_ ` _ \ / _` | '__| |/ / __|  / __/ _` | |/ __| | | | |/ _` | __/ _ \| '__|
  ____) | |_| |_| | (_| |  __/ | | | |_  | | | | | | (_| | |  |   <\__ \ | (_| (_| | | (__| |_| | | (_| | || (_) | |  
 |_____/ \__|\__,_|\__,_|\___|_| |_|\__| |_| |_| |_|\__,_|_|  |_|\_\___/  \___\__,_|_|\___|\__,_|_|\__,_|\__\___/|_|                                                                                        
";


            string title1 = @"
   _____  _               _               _     _          __                                _    _              
  / ____|| |             | |             | |   (_)        / _|                              | |  (_)              
 | (___  | |_  _   _   __| |  ___  _ __  | |_   _  _ __  | |_  ___   _ __  _ __ ___    __ _ | |_  _   ___   _ __  
  \___ \ | __|| | | | / _` | / _ \| '_ \ | __| | || '_ \ |  _|/ _ \ | '__|| '_ ` _ \  / _` || __|| | / _ \ | '_ \
  ____) || |_ | |_| || (_| ||  __/| | | || |_  | || | | || | | (_) || |   | | | | | || (_| || |_ | || (_) || | | |
 |_____/  \__| \__,_| \__,_| \___||_| |_| \__| |_||_| |_||_|  \___/ |_|   |_| |_| |_| \__,_| \__||_| \___/ |_| |_|                                                                                                                                                                                                                                                                
";
            

            


            // Display "CET133 assignment 1" in the center of the console

            string s = "CET133 assignment 1";
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(s);
            Console.WriteLine();

            // ask student for name

            Console.WriteLine("Please enter your full name: \n");
            sStudentName = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();

            // make sure to validate only entering letters and spaces

            while (!Regex.IsMatch(sStudentName, @"^[a-zA-Z\s]*$"))
            {
                Console.WriteLine("Please enter your profile name using only letters.\n");
                sStudentName = Console.ReadLine()?? string.Empty;
                Console.WriteLine();
            }

            // ask user for number

            Console.WriteLine("Please enter a 9 digit number : \n");
            iStudentNumber = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();

            // make sure its a 9 digit number
            while (!Regex.IsMatch(iStudentNumber, "^\\d{9}$"))
            {
                Console.WriteLine("You don't have a match, please try again \n");
                iStudentNumber = Console.ReadLine() ?? string.Empty;
                Console.WriteLine();
            }

            // clear screen

            Console.Clear();

            // calculate student marks


            Console.WriteLine(title);

            for (int i = 0; i < 6; i++)
            {
                // ask user for module name
                Console.Write("What's your #" + (i + 1) + " module name : ");

                Module[i] = Console.ReadLine() ?? string.Empty;


                Console.Write("\n");

                // make sure validate characters and spaces

                while (!Regex.IsMatch(Module[i], @"^[\w\s]*$"))
                {
                    Console.WriteLine("Please enter a valid module using letters and numbers : ");
                    Console.WriteLine();
                    Module[i] = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine();
                }

                //ask user for module grade
                Console.Write("How many marks have you got for " + Module[i] + " : ");
                sMarks = Console.ReadLine() ?? string.Empty;

                Console.Write("\n");

                // make sure its between 0-100 digit number

                while (!Regex.IsMatch(sMarks, @"^0*(?:[0-9][0-9]?([\.]\d+)?|100?)$"))
                {

                    Console.Write("please enter a number between 0-100\n");
                    Console.WriteLine();
                    Console.Write(Module[i] + " : ");
                    sMarks = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine();
                }

                // store the results in a table

                aDoubleGrades[i] = Convert.ToDouble(sMarks);


                dGrades = dGrades + aDoubleGrades[i];

            }
            // calculate grade

            dGrades = dGrades / 6.0;

            dGrades = Math.Round(dGrades, 2);

            //clear screen

            Console.Clear();

            /* Display  summary of the student, including ID details, individual module marks and overall course result */
            Console.WriteLine(title1);

            
            // Display student name and number
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 3, Console.CursorTop);
            Console.WriteLine("Hello " + sStudentName + " ! Your student number is : " + iStudentNumber + "\n");
            Console.WriteLine();

            //Display Student Grades
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine("Student grades  \n");
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("For " + Module[i] + " you got : " + aDoubleGrades[i] + "\n");
            }
            //Display Student average rade
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine("Average grade : " + dGrades + "\n");

            // make sure if the user fail or pass
            if (dGrades >= 40)
            {
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 3, Console.CursorTop);
                Console.Write("Congratulations! You have successfully completed this course!\n");
            }

            else
            {
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 6, Console.CursorTop);
                Console.Write("Sorry you failed your course\n");
            }


            // press the enter key to exit

            Console.Write("\nPress 'Enter' to exit the process...");

            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
            }
        }
    }
}