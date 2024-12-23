using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace StudentsGrades
{
    public static class ValidationFunction
    {
        public static string ValidateStudentName()
        {
            Console.Write("Please enter your full name: ");
            string name = Console.ReadLine()?.Trim() ?? string.Empty;
            while (!Regex.IsMatch(name, @"^[a-zA-Z\s]{2,}$"))
            {
                Console.WriteLine("Invalid input. Please use only letters and spaces, with at least two characters.");
                Console.Write("Please enter your full name: ");
                name = Console.ReadLine()?.Trim() ?? string.Empty;
            }
            return name;
        }

        public static int ValidateStudentAge()
        {
            int age;
            Console.Write("Please enter an age between 18-100: ");
            string ageInput = Console.ReadLine()?.Trim() ?? string.Empty;
            while (!int.TryParse(ageInput, out age) || age < 18 || age > 100)
            {
                Console.WriteLine("Invalid age. Please enter a valid age between 18 and 100.");
                Console.Write("Please enter an age between 18-100: ");
                ageInput = Console.ReadLine()?.Trim() ?? string.Empty;
            }
            return age;
        }

        public static string ValidateStudentNumber()
        {
            Console.Write("Please enter a 9-digit student number: ");
            string number = Console.ReadLine()?.Trim() ?? string.Empty;
            while (!Regex.IsMatch(number, @"^\d{9}$"))
            {
                Console.WriteLine("Invalid number. Please enter exactly 9 digits.");
                Console.Write("Please enter a 9-digit student number: ");
                number = Console.ReadLine()?.Trim() ?? string.Empty;
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
            HashSet<string> moduleNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < 6; i++)
            {
                string moduleName;
                do
                {
                    Console.Write($"Enter the name of module #{i + 1}: ");
                    moduleName = Console.ReadLine()?.Trim() ?? string.Empty;

                    // Enhanced validation: At least 2 characters, no single letters like 'q'
                    if (!Regex.IsMatch(moduleName, @"^[a-zA-Z\s]{2,}$") || !moduleNames.Add(moduleName))
                    {
                        Console.WriteLine("Invalid or duplicate module name. Please enter a unique name using at least two letters and spaces.");
                        moduleName = null;
                    }

                } while (moduleName == null);

                modules[i] = moduleName;

                double grade;
                string gradeInput;
                do
                {
                    Console.Write($"Enter the grade for {moduleName}: ");
                    gradeInput = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (!double.TryParse(gradeInput, out grade) || grade < 0 || grade > 100)
                    {
                        Console.WriteLine("Invalid grade. Please enter a number between 0 and 100.");
                        grade = -1;
                    }

                } while (grade == -1);

                grades[i] = grade;
            }

            return (modules, grades);
        }
    }
}
