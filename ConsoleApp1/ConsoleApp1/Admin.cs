using System;
using System.IO;
using Newtonsoft.Json;

namespace StudentsGrades
{
    public static class Admin
    {
        public static void Login()
        {
            Console.Write("Enter admin password: ");
            string password = Console.ReadLine() ?? string.Empty;

            // Hash the input password before comparison
            string hashedPassword = SecurityHelper.ComputeSha256Hash(password);

            // Assuming the admin password is also stored as a hash
            string adminHashedPassword = SecurityHelper.ComputeSha256Hash("adminpassword");

            if (hashedPassword == adminHashedPassword)
            {
                Console.Clear();
                string[] files = Directory.GetFiles(Program.FolderPath, "*.json");

                if (files.Length == 0)
                {
                    Console.WriteLine("No student records found.");
                    return;
                }

                while (true)
                {
                    Console.WriteLine("\n===== Admin Panel =====");
                    Console.WriteLine("Available student records:");
                    foreach (string file in files)
                    {
                        Console.WriteLine(Path.GetFileNameWithoutExtension(file));
                    }

                    Console.WriteLine("\nEnter the student number to view or edit, or type 'exit' to go back:");
                    string studentNumber = Console.ReadLine() ?? string.Empty;
                    if (studentNumber.ToLower() == "exit")
                    {
                        Console.Clear();
                        break;
                    }

                    string filePath = Path.Combine(Program.FolderPath, $"{studentNumber}.json");
                    if (File.Exists(filePath))
                    {
                        string data = File.ReadAllText(filePath);
                        Student student = JsonConvert.DeserializeObject<Student>(data);

                        if (student != null)
                        {
                            EditStudent(student);
                            // Refresh the list of files after potential edits
                            files = Directory.GetFiles(Program.FolderPath, "*.json");
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
                Console.WriteLine("Incorrect password.");
            }
        }

        private static void EditStudent(Student student)
        {
            while (true)
            {
                student.DisplayInfoForAdmin();
                Console.WriteLine("1. Edit Personal Information");
                Console.WriteLine("2. Edit Password");
                Console.WriteLine("3. Edit Module Grades");
                Console.WriteLine("4. Go Back");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        student.UpdateInfo();
                        break;
                    case "2":
                        Console.Write("Enter new password: ");
                        string newPassword = Console.ReadLine() ?? string.Empty;
                        if (!string.IsNullOrEmpty(newPassword))
                        {
                            student.Password = SecurityHelper.ComputeSha256Hash(newPassword);
                            Console.WriteLine("Password updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Password cannot be empty.");
                        }
                        break;
                    case "3":
                        student.UpdateGrades();
                        break;
                    case "4":
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        continue; // Skip saving and clearing if invalid
                }

                student.SaveToFile();
                Console.Clear();
                Console.WriteLine("Changes saved successfully.\n");
            }
        }
    }
}
