using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace StudentsGrades
{
    class Program
    {
       
        public static readonly string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Student Numbers");
        static void Main(string[] args)
        { 
            while (true)
            {
                
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. User");
                Console.WriteLine("3. Quit");
                Console.Write("Select an option: ");

                string option = Console.ReadLine() ?? string.Empty;

                switch (option)
                {
                    case "1":
                        AdminLogin();
                        break;
                    case "2":
                        UserLogin();
                        break;
                    case "3":
                        Console.WriteLine("Goodbye");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

            static void AdminLogin()
{
    Console.Write("Enter admin password: ");
    string password = Console.ReadLine() ?? string.Empty;

    if (password == "adminpassword")
    {
        Console.Clear();
        string[] files = Directory.GetFiles(folderPath, "*.json");
        if (files.Length == 0)
        {
            Console.WriteLine("No student records found.");
            return;
        }

        while (true) // Loop until a valid student number is entered or the admin chooses to exit
        {
            Console.WriteLine("Available student records:");
            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileNameWithoutExtension(file));
            }

            Console.WriteLine("\nEnter the student number to view or edit, or type 'exit' to go back:");
            string studentNumber = Console.ReadLine() ?? string.Empty;
            Console.Clear();
            if (studentNumber.ToLower() == "exit")
            {
                break; // Exit the loop and go back
            }

            string filePath = Path.Combine(folderPath, $"{studentNumber}.json");

            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath);
                Student student = JsonConvert.DeserializeObject<Student>(data);
                if (student != null)
                {
                    AdminEditStudent(student);
                    break; // Exit the loop after editing
                }
                else
                {
                    Console.WriteLine("Failed to deserialize student data.");
                }
            }
            else
            {
                Console.WriteLine("Student record not found. Please try again.");
            }
        }
    }
    else
    {
        Console.Clear();
        Console.WriteLine("Incorrect password.");
    }
}
static void AdminEditStudent(Student student)
{
    // Display complete student information for admin
    student.DisplayInfoForAdmin();

    Console.WriteLine("Do you want to edit this student's information? (yes/no)");
    string editChoice = Console.ReadLine()?.ToLower() ?? string.Empty;

    if (editChoice == "yes" || editChoice == "y")
    {
        Console.WriteLine("Select what to edit:");
        Console.WriteLine("1. Personal Information");
        Console.WriteLine("2. Password");
        Console.WriteLine("3. Module Grades");
        Console.Write("Enter your choice: ");
        string adminEditChoice = Console.ReadLine() ?? string.Empty;

        switch (adminEditChoice)
        {
            case "1":
                student.UpdateInfo();
                student.SaveToFile();
                Console.Clear();
                Console.WriteLine("Student information updated successfully.");
                AdminEditStudent(student);;
                break;
            case "2":
                 Console.Write("Enter a new password: ");
                 student.Password = Console.ReadLine() ?? string.Empty;
                 student.SaveToFile();
                Console.Clear();
                Console.WriteLine("Student information updated successfully.");
                AdminEditStudent(student);
                break;
            case "3":
                 student.UpdateGrades();
                 student.SaveToFile();
                 Console.Clear();
                 Console.WriteLine("Student information updated successfully."); 
                 AdminEditStudent(student);
                break;
            default:
                Console.WriteLine("Invalid option selected.");
                break;
        }

       
    }
}

        static void UserLogin()
{
    Console.Write("Enter your student number: ");
    string studentNumber = ValidationFunction.ValidateStudentNumber();
    string filePath = Path.Combine(folderPath, $"{studentNumber}.json");

    if (File.Exists(filePath))
    {
        string data = File.ReadAllText(filePath);
        Student student = JsonConvert.DeserializeObject<Student>(data);

        if (student != null)
        {
            Console.Write("Enter your password: ");
            string password = Console.ReadLine() ?? string.Empty;

            if (student.Password == password)
            {
                Console.Clear();
                EditStudentInfo(student);
            }
            else
            {
                Console.WriteLine("Incorrect password. Please contact an admin.");
            }
        }
        else
        {
            Console.WriteLine("Failed to load student data.");
        }
    }
   
   else
{
    Console.Clear();
    Console.WriteLine("No existing record found with the number "+ studentNumber +  " Let's create a new student profile.");
    string name = ValidationFunction.ValidateStudentName();
    int age = ValidationFunction.ValidateStudentAge();
    Console.Write("Enter a password for your account: ");
    string password = Console.ReadLine() ?? string.Empty;

    // Use CalculationFunction to collect module names and grades
    (string[] modules, double[] grades) = CalculationFunction.CalculateStudentGrades();

    // Initialize the new student with the collected data
    Student newStudent = new Student(name, age, studentNumber, password)
    {
        StudentModules = modules,
        StudentGrades = grades
    };

    newStudent.SaveToFile();
    Console.Clear();
    Console.WriteLine("New student profile created successfully.");
}


}
static void EditStudentInfo(Student student)
{  
    student.DisplayInfo(); 
    Console.WriteLine("What would you like to edit?");
    Console.WriteLine("1. Personal Information");
    Console.WriteLine("2. Password");
    Console.Write("Select an option (1/2) or type 'no' to exit: ");
    string editChoice = Console.ReadLine()?.ToLower() ?? string.Empty;

    if (editChoice == "1")
    {
        student.UpdateInfo();
        student.SaveToFile();
        Console.Clear();
        Console.WriteLine("Personal information updated successfully."); 
        EditStudentInfo(student);
    }
    else if (editChoice == "2")
    {
        Console.Write("Enter a new password: ");
        student.Password = Console.ReadLine() ?? string.Empty;
        student.SaveToFile();
        Console.Clear();
        Console.WriteLine("Student information updated successfully.");
        EditStudentInfo(student);
    }
    else if (editChoice != "no" && editChoice != "n")
    {
        Console.WriteLine("Invalid option. Please try again.");
    }
}

    }
}