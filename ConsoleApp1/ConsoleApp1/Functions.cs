using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace StudentsGrades
{
    public static class ValidationFunction
    {
        public static string ValidateStudentName()
        {
            Console.WriteLine("Please enter your full name: ");
            string name = Console.ReadLine() ?? string.Empty;
            while (!Regex.IsMatch(name, @"^[a-zA-Z\s]*$"))
            {
                Console.WriteLine("Please enter your profile name using only letters.");
                name = Console.ReadLine() ?? string.Empty;
            }
            return name;
        }

        public static int ValidateStudentAge()
        {
            int age;
            Console.WriteLine("Please enter an age between 18-100: ");
            string ageInput = Console.ReadLine() ?? string.Empty;
            while (!Regex.IsMatch(ageInput, @"^(1[89]|[2-9][0-9]|100)$") || !int.TryParse(ageInput, out age))
            {
                Console.WriteLine("Invalid age. Please enter an age between 18-100: ");
                ageInput = Console.ReadLine() ?? string.Empty;
            }
            return age;
        }

       public static string ValidateStudentNumber()
{
    Console.WriteLine("Please enter a 9 digit student number: ");
    string number = Console.ReadLine() ?? string.Empty;
    while (!Regex.IsMatch(number, @"^\d{9}$"))
    {
        Console.WriteLine("Invalid number. Please enter a 9 digit number: ");
        number = Console.ReadLine() ?? string.Empty;
    }
    return number;
}
    }

    public static class CalculationFunction
    {
        public static (string[] modules, double[] grades) CalculateStudentGrades()
        {
            string[] modules = new string[6];
            double[] grades = new double[6];
            HashSet<string> moduleNames = new HashSet<string>();

            for (int i = 0; i < 6; i++)
            {
                string moduleName;
                do
                {
                    Console.Write($"Enter the name of module #{i + 1}: ");
                    moduleName = Console.ReadLine() ?? string.Empty;

                    if (!Regex.IsMatch(moduleName, @"^[\w\s]*$") || !moduleNames.Add(moduleName))
                    {
                        Console.WriteLine("Invalid or duplicate module name. Please enter a valid, unique name using letters and numbers.");
                        moduleName = null;
                    }

                } while (moduleName == null);

                modules[i] = moduleName;

                double grade;
                string gradeInput;
                do
                {
                    Console.Write($"Enter the grade for {moduleName}: ");
                    gradeInput = Console.ReadLine() ?? string.Empty;

                    if (!Regex.IsMatch(gradeInput, @"^0*(?:[0-9][0-9]?(\.\d+)?|100)$") || !double.TryParse(gradeInput, out grade) || grade < 0 || grade > 100)
                    {
                        Console.WriteLine("Invalid grade. Please enter a number between 0-100.");
                        grade = -1;
                    }

                } while (grade == -1);

                grades[i] = grade;
            }

            return (modules, grades);
        }
    } 
}