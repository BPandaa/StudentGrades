using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;

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

        // Parameterless constructor needed for deserialization
        public Student() { }

        public Student(string name, int age, string studentNumber, string password)
        {
            Name = name;
            Age = age;
            StudentNumber = studentNumber;
            Password = SecurityHelper.ComputeSha256Hash(password); // Hash the password upon creation
            StudentGrades = new double[6];
            StudentModules = new string[6];
        }

        public void UpdateInfo()
        {
            while (true)
            {
                Console.WriteLine("\nWhat would you like to update?");
                Console.WriteLine("1. Name");
                Console.WriteLine("2. Age");
                Console.WriteLine("3. Password");
                Console.WriteLine("4. Modules");
                Console.WriteLine("5. Go Back");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter new name: ");
                        string newName = Console.ReadLine()?.Trim() ?? string.Empty;
                        if (!string.IsNullOrEmpty(newName))
                        {
                            Name = newName;
                            Console.WriteLine("Name updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Name cannot be empty.");
                        }
                        break;
                    case "2":
                        Console.Write("Enter new age: ");
                        string ageInput = Console.ReadLine()?.Trim() ?? string.Empty;
                        if (int.TryParse(ageInput, out int newAge) && newAge >= 18 && newAge <= 100)
                        {
                            Age = newAge;
                            Console.WriteLine("Age updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid age. Please enter a value between 18 and 100.");
                        }
                        break;
                    case "3":
                        Console.Write("Enter new password: ");
                        string newPassword = Console.ReadLine() ?? string.Empty;
                        if (!string.IsNullOrEmpty(newPassword))
                        {
                            Password = SecurityHelper.ComputeSha256Hash(newPassword);
                            Console.WriteLine("Password updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Password cannot be empty.");
                        }
                        break;
                    case "4":
                        UpdateModules();
                        break;
                    case "5":
                        return; // Exit the update info menu
                    default:
                        Console.WriteLine("Invalid option. Please enter 1, 2, 3, 4, or 5.");
                        continue; // Prompt again
                }
            }
        }

        public void UpdateGrades()
        {
            while (true)
            {
                Console.WriteLine("\nWhich module would you like to update?");
                for (int i = 0; i < StudentModules.Length; i++)
                {
                    string moduleName = !string.IsNullOrEmpty(StudentModules[i]) ? StudentModules[i] : $"Module {i + 1}";
                    Console.WriteLine($"{i + 1}. {moduleName}: Current Grade - {StudentGrades[i]}");
                }
                Console.WriteLine($"{StudentModules.Length + 1}. Go Back");
                Console.Write("Select a module to update (choose the number): ");
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (int.TryParse(input, out int selection))
                {
                    if (selection >= 1 && selection <= StudentModules.Length)
                    {
                        string selectedModule = StudentModules[selection - 1];
                        Console.Write($"Enter new grade for {selectedModule}: ");
                        string gradeInput = Console.ReadLine()?.Trim() ?? string.Empty;
                        if (double.TryParse(gradeInput, out double newGrade) && newGrade >= 0 && newGrade <= 100)
                        {
                            StudentGrades[selection - 1] = newGrade;
                            Console.WriteLine("Grade updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid grade. Please enter a number between 0 and 100.");
                        }
                    }
                    else if (selection == StudentModules.Length + 1)
                    {
                        break; // Go back to previous menu
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number corresponding to the module.");
                }
            }
        }

        // New method to update modules
        public void UpdateModules()
        {
            Console.WriteLine("\n===== Update Modules =====");

            for (int i = 0; i < StudentModules.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {StudentModules[i]}");
            }

            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Update a module name");
            Console.WriteLine("2. Update all module names");
            Console.WriteLine("3. Go Back");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine()?.Trim() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    UpdateSingleModuleName();
                    break;
                case "2":
                    UpdateAllModuleNames();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                    break;
            }
        }

        private void UpdateSingleModuleName()
        {
            Console.Write("Enter the number of the module you want to update: ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;
            if (int.TryParse(input, out int moduleNumber) && moduleNumber >= 1 && moduleNumber <= StudentModules.Length)
            {
                Console.Write($"Enter new name for module #{moduleNumber}: ");
                string newModuleName = Console.ReadLine()?.Trim() ?? string.Empty;
                while (!Regex.IsMatch(newModuleName, @"^[a-zA-Z\s]{2,}$"))
                {
                    Console.WriteLine("Invalid module name. Please use at least two letters and spaces only.");
                    Console.Write($"Enter new name for module #{moduleNumber}: ");
                    newModuleName = Console.ReadLine()?.Trim() ?? string.Empty;
                }

                // Check for duplicate module names
                for (int i = 0; i < StudentModules.Length; i++)
                {
                    if (i != (moduleNumber - 1) && StudentModules[i].Equals(newModuleName, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Duplicate module name detected. Please enter a unique name.");
                        return;
                    }
                }

                StudentModules[moduleNumber - 1] = newModuleName;
                Console.WriteLine("Module name updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid module number.");
            }
        }

        private void UpdateAllModuleNames()
        {
            Console.WriteLine("Enter new names for all modules:");
            HashSet<string> moduleNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < StudentModules.Length; i++)
            {
                string newModuleName;
                do
                {
                    Console.Write($"Enter new name for module #{i + 1}: ");
                    newModuleName = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (!Regex.IsMatch(newModuleName, @"^[a-zA-Z\s]{2,}$"))
                    {
                        Console.WriteLine("Invalid module name. Please use at least two letters and spaces only.");
                        newModuleName = null;
                        continue;
                    }

                    if (moduleNames.Contains(newModuleName))
                    {
                        Console.WriteLine("Duplicate module name detected. Please enter a unique name.");
                        newModuleName = null;
                        continue;
                    }

                    moduleNames.Add(newModuleName);
                } while (newModuleName == null);

                StudentModules[i] = newModuleName;
            }

            Console.WriteLine("All module names updated successfully.");
        }

        public void DisplayInfo()
        {
            Console.WriteLine("\n===== Student Information =====");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Student Number: {StudentNumber}");
            Console.WriteLine("Modules and Grades:");
            for (int i = 0; i < StudentModules.Length; i++)
            {
                string moduleName = !string.IsNullOrEmpty(StudentModules[i]) ? StudentModules[i] : $"Module {i + 1}";
                Console.WriteLine($"- {moduleName}: {StudentGrades[i]}");
            }
            Console.WriteLine($"Average Score: {CalculateAverage():F2}");
            Console.WriteLine($"Result: {GetResult()}");
            Console.WriteLine();
        }

        public void DisplayInfoForAdmin()
        {
            DisplayInfo();
            Console.WriteLine($"Password (hashed): {Password}");
            Console.WriteLine();
        }

        public void SaveToFile()
        {
            try
            {
                string filePath = Path.Combine(Program.FolderPath, $"{StudentNumber}.json");
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving student data: {ex.Message}");
            }
        }

        // Calculate average grade
        public double CalculateAverage()
        {
            double sum = 0;
            foreach (var grade in StudentGrades)
            {
                sum += grade;
            }
            return sum / StudentGrades.Length;
        }

        // Determine pass or fail based on average
        public string GetResult()
        {
            return CalculateAverage() >= 50 ? "Passed" : "Failed";
        }
    }
}
