using System;
using System.IO;
using Newtonsoft.Json;

namespace StudentsGrades
{
    class Program
    {
        // Adjust folder path to be relative to the project root, not the output directory.
        public static string FolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Student Numbers");

        static void Main(string[] args)
        {
            // Display the program title
            DisplayFunctions.DisplayCenteredTitle(DisplayFunctions.title);

            // Resolve the relative path and convert it to an absolute path
            FolderPath = Path.GetFullPath(FolderPath);

            // Initialize the folder if necessary
            InitializeFolder();

            // Main loop for the menu system
            while (true)
            {
                DisplayMainMenu();
                string option = (Console.ReadLine() ?? string.Empty).ToLower();
                HandleOption(option);
            }
        }

        // Initialize folder (check if it exists, otherwise create it)
        static void InitializeFolder()
        {
            try
            {
                if (!Directory.Exists(FolderPath))
                {
                    Console.WriteLine("Folder not found, creating 'Student Numbers' folder...");
                    Directory.CreateDirectory(FolderPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing folder: {ex.Message}");
                Environment.Exit(1); // Exit program if folder cannot be created.
            }
        }

        // Display the main menu for user and admin options
        static void DisplayMainMenu()
        {
            DisplayFunctions.DisplayCenteredTitle(DisplayFunctions.title1);
            Console.WriteLine("\n===== Main Menu =====");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. User");
            Console.WriteLine("3. Register New Student");
            Console.WriteLine("4. Quit");
            Console.Write("Select an option: ");
        }

        // Handle the user's menu selection
        static void HandleOption(string option)
        {
            switch (option)
            {
                case "1":
                    Admin.Login();  // Admin class handles login and subsequent actions
                    break;
                case "2":
                    UserLogin();  // Handle user login
                    break;
                case "3":
                    RegisterNewStudent();  // Handle student registration
                    break;
                case "4":
                    Console.WriteLine("Exiting the application...");
                    Environment.Exit(0);  // Exit the application
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }

        // Handle the user login functionality
        static void UserLogin()
        {
            Console.Write("\nEnter student number: ");
            string studentNumber = ValidationFunction.ValidateStudentNumber();
            string filePath = Path.Combine(FolderPath, $"{studentNumber}.json");

            if (File.Exists(filePath))
            {
                try
                {
                    string data = File.ReadAllText(filePath);
                    Student student = JsonConvert.DeserializeObject<Student>(data);

                    if (student != null)
                    {
                        Console.Write("Enter password: ");
                        string inputPassword = Console.ReadLine() ?? string.Empty;
                        string hashedInput = SecurityHelper.ComputeSha256Hash(inputPassword);

                        if (student.Password == hashedInput)
                        {
                            student.DisplayInfo();
                        }
                        else
                        {
                            Console.WriteLine("Incorrect password.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to load student data.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading student data: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        // Handle registering a new student
        static void RegisterNewStudent()
        {
            Console.WriteLine("\n===== Register New Student =====");
            string name = ValidationFunction.ValidateStudentName();
            int age = ValidationFunction.ValidateStudentAge();
            string studentNumber = ValidationFunction.ValidateStudentNumber();

            // Check if student number already exists
            string filePath = Path.Combine(FolderPath, $"{studentNumber}.json");
            if (File.Exists(filePath))
            {
                Console.WriteLine("A student with this number already exists.");
                return;
            }

            Console.Write("Enter password: ");
            string password = Console.ReadLine()?.Trim() ?? string.Empty;
            while (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Password cannot be empty.");
                Console.Write("Enter password: ");
                password = Console.ReadLine()?.Trim() ?? string.Empty;
            }
            string hashedPassword = SecurityHelper.ComputeSha256Hash(password);

            var (modules, grades) = CalculationFunction.CalculateStudentGrades();

            Student newStudent = new Student
            {
                Name = name,
                Age = age,
                StudentNumber = studentNumber,
                Password = hashedPassword,
                StudentModules = modules,
                StudentGrades = grades
            };

            newStudent.SaveToFile();

            Console.WriteLine("Student registered successfully.\n");
        }
    }
}
