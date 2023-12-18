using Newtonsoft.Json;
using System;
using System.IO;
using StudentsGrades;

namespace StudentsGrades
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string StudentNumber { get; set; }
        public string Password { get; set; }
        public double[] StudentGrades { get; set; }
        public string[] StudentModules { get; set; }

        public Student(string name, int age, string studentNumber, string password)
        {
            Name = name;
            Age = age;
            StudentNumber = studentNumber;
            Password = password;
            StudentGrades = new double[6]; // Initialize with default size of 6
            StudentModules = new string[6]; // Initialize with default size of 6
        }

       
public void UpdateInfo()
{
    Console.WriteLine("What would you like to update?");
    Console.WriteLine("1. Name");
    Console.WriteLine("2. Age");
    Console.WriteLine("3. Password");
    Console.Write("Select an option: ");
    string choice = Console.ReadLine() ?? string.Empty;

    switch (choice)
    {
        case "1":
            Console.Write("Enter new name: ");
            Name = Console.ReadLine()?? string.Empty;
            break;
        case "2":
            Console.Write("Enter new age: ");
            if (int.TryParse(Console.ReadLine(), out int newAge))
            {
                Age = newAge;
            }
            break;
        case "3":
            Console.Write("Enter new password: ");
            Password = Console.ReadLine() ?? string.Empty;
            break;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

public double CalculateAverageScore()
        {
            if (StudentGrades == null || StudentGrades.Length == 0)
                return 0;

            double sum = 0;
            foreach (var grade in StudentGrades)
            {
                sum += grade;
            }
            return sum / StudentGrades.Length;
        }

        public bool DidPass(double passingScore = 40.0)
        {
            return CalculateAverageScore() >= passingScore;
        }



public void UpdateGrades()
{
    Console.WriteLine("Current module grades:");
    for (int i = 0; i < StudentModules.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {StudentModules[i]}: Current Grade - {StudentGrades[i]}");
    }

    Console.Write("\nSelect a module to update (choose the number): ");
    if (int.TryParse(Console.ReadLine(), out int moduleChoice) && moduleChoice >= 1 && moduleChoice <= StudentModules.Length)
    {
        int index = moduleChoice - 1;
        Console.Write($"Enter the new grade for {StudentModules[index]}: ");
        if (double.TryParse(Console.ReadLine(), out double newGrade) && newGrade >= 0 && newGrade <= 100)
        {
            StudentGrades[index] = newGrade;
            Console.WriteLine($"Updated {StudentModules[index]} grade to {newGrade}.");
        }
        else
        {
            Console.WriteLine("Invalid grade. Please enter a number between 0-100.");
        }
    }
    else
    {
        Console.WriteLine("Invalid module selection.");
    }
}



        public void DisplayInfo()
        {
            Console.WriteLine($"Student Name: {Name}");
            Console.WriteLine($"Student Age: {Age}");
            Console.WriteLine($"Student Number: {StudentNumber}");
            for (int i = 0; i < StudentGrades.Length; i++)
            {
                Console.WriteLine($"Module {i + 1} ({StudentModules[i]}): {StudentGrades[i]}");
            }
             double averageScore = CalculateAverageScore();
            Console.WriteLine($"Average Score: {averageScore}");
            Console.WriteLine($"Result: {(DidPass() ? "Passed" : "Failed")}");
        }

         public void DisplayInfoForAdmin()
    {
        Console.WriteLine($"Student Name: {Name}");
        Console.WriteLine($"Student Age: {Age}");
        Console.WriteLine($"Student Number: {StudentNumber}");
        Console.WriteLine($"Password: {Password}"); // Display password
        for (int i = 0; i < StudentGrades.Length; i++)
        {
            Console.WriteLine($"Module {i + 1} ({StudentModules[i]}): {StudentGrades[i]}");
        }
            double averageScore = CalculateAverageScore();
            Console.WriteLine($"Average Score: {averageScore}");
            Console.WriteLine($"Result: {(DidPass() ? "Passed" : "Failed")}");
    }

        public void SaveToFile()
        {
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            string filePath = Path.Combine(Program.folderPath, $"{StudentNumber}.json");
            File.WriteAllText(filePath, jsonString);
        }
    }
}

