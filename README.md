# Student Grades Management System

## Description
This project is a C# console application for managing student grades. It allows admins and users (students) to access and modify student records, including personal information, module grades, and passwords. The system calculates average scores and determines whether a student has passed or failed based on their grades.

## Installation

### Prerequisites
- .NET Core SDK

### Setup
Clone the repository and navigate to the project directory.

```bash
git clone https://github.com/YourGithubUsername/Student-Grades-Management-System.git
cd Student-Grades-Management-System
```

### Usage
Run the application using the .NET CLI:

```bash
dotnet run
```

## Features
- Admin and user (student) login system.
- Admins can view and edit all student records.
- Students can view and edit their own information.
- Calculation of average grades and pass/fail status.
- Data persistence in JSON files.

## Display
Here’s an example of what you will see when running the application:

```plaintext
  _____ _             _            _                          _                    _            _       _            
  / ____| |           | |          | |                        | |                  | |          | |     | |            
 | (___ | |_ _   _  __| | ___ _ __ | |_   _ __ ___   __ _ _ __| | _____    ___ __ _| | ___ _   _| | __ _| |_ ___  _ __
  \___ \| __| | | |/ _` |/ _ \ '_ \| __| | '_ ` _ \ / _` | '__| |/ / __|  / __/ _` | |/ __| | | | |/ _` | __/ _ \| '__|
  ____) | |_| |_| | (_| |  __/ | | | |_  | | | | | | (_| | |  |   <\__ \ | (_| (_| | | (__| |_| | | (_| | || (_) | |  
 |_____/ \__|\__,_|\__,_|\___|_| |_|\__| |_| |_| |_|\__,_|_|  |_|\_\___/  \___\__,_|_|\___|\__,_|_|\__,_|\__\___/|_|        

Folder not found, creating 'Student Numbers' folder...

   _____  _               _               _     _          __                                _    _              
  / ____|| |             | |             | |   (_)        / _|                              | |  (_)              
 | (___  | |_  _   _   __| |  ___  _ __  | |_   _  _ __  | |_  ___   _ __  _ __ ___    __ _ | |_  _   ___   _ __  
  \___ \ | __|| | | | / _` | / _ \| '_ \ | __| | || '_ \ |  _|/ _ \ | '__|| '_ ` _ \  / _` || __|| | / _ \ | '_ \
  ____) || |_ | |_| || (_| ||  __/| | | || |_  | || | | || | | (_) || |   | | | | | || (_| || |_ | || (_) || | | |
 |_____/  \__| \__,_| \__,_| \___||_| |_| \__| |_||_| |_||_|  \___/ |_|   |_| |_| |_| \__,_| \__||_| \___/ |_| |_|                                                                                                                                                                                                                                                                

===== Main Menu =====
1. Admin
2. User
3. Register New Student
4. Quit
Select an option: 3

===== Register New Student =====
Enter student name: John Doe
Enter student age: 20
Enter student number: 12345
Enter password: ******
Enter module names separated by commas (e.g., Math, Science): Math, Science, History
Enter grades for Math: 85
Enter grades for Science: 90
Enter grades for History: 78
Student registered successfully.

===== Main Menu =====
1. Admin
2. User
3. Register New Student
4. Quit
Select an option: 2

Enter student number: 12345
Enter password: ******
--- Student Info ---
Name: John Doe
Age: 20
Modules:
- Math: 85
- Science: 90
- History: 78

===== Main Menu =====
1. Admin
2. User
3. Register New Student
4. Quit
Select an option: 4
Exiting the application...
```

## Knowledge Gained
Through this project, you can learn and apply:

### Data Types
- String, integer, and lists.

### Classes
- Creating and organizing classes for modular code.

### Conditional Statements
- Using `if-else` to handle user input and validate data.

### Loops
- Utilizing loops to navigate menus and process lists.

### File Handling
- Reading and writing JSON files for data persistence.

## Contribution
Contributions to the project are welcome! Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Commit your changes.
4. Push to your branch and submit a pull request.

## TODOs
- Adjust all functions to reside in their respective classes.
- Implement additional loops for better control flow.

---
Feel free to reach out if you have any questions or feedback!
