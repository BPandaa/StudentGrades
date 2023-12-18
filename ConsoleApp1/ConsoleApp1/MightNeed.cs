// Displaying Function
 public static class DisplayFunctions
    {
        public static string title = @"
   _____ _             _            _                          _                    _            _       _            
  / ____| |           | |          | |                        | |                  | |          | |     | |            
 | (___ | |_ _   _  __| | ___ _ __ | |_   _ __ ___   __ _ _ __| | _____    ___ __ _| | ___ _   _| | __ _| |_ ___  _ __
  \___ \| __| | | |/ _` |/ _ \ '_ \| __| | '_ ` _ \ / _` | '__| |/ / __|  / __/ _` | |/ __| | | | |/ _` | __/ _ \| '__|
  ____) | |_| |_| | (_| |  __/ | | | |_  | | | | | | (_| | |  |   <\__ \ | (_| (_| | | (__| |_| | | (_| | || (_) | |  
 |_____/ \__|\__,_|\__,_|\___|_| |_|\__| |_| |_| |_|\__,_|_|  |_|\_\___/  \___\__,_|_|\___|\__,_|_|\__,_|\__\___/|_|                                                                                        
";


           public static string title1 = @"
   _____  _               _               _     _          __                                _    _              
  / ____|| |             | |             | |   (_)        / _|                              | |  (_)              
 | (___  | |_  _   _   __| |  ___  _ __  | |_   _  _ __  | |_  ___   _ __  _ __ ___    __ _ | |_  _   ___   _ __  
  \___ \ | __|| | | | / _` | / _ \| '_ \ | __| | || '_ \ |  _|/ _ \ | '__|| '_ ` _ \  / _` || __|| | / _ \ | '_ \
  ____) || |_ | |_| || (_| ||  __/| | | || |_  | || | | || | | (_) || |   | | | | | || (_| || |_ | || (_) || | | |
 |_____/  \__| \__,_| \__,_| \___||_| |_| \__| |_||_| |_||_|  \___/ |_|   |_| |_| |_| \__,_| \__||_| \___/ |_| |_|                                                                                                                                                                                                                                                                
";
        public static string assignment = "CET133 assignment 1";

        public static void DisplayCenteredTitle(string title)
        {
            int leftPosition = (Console.WindowWidth - title.Length) / 2;
            if (leftPosition < 0) leftPosition = 0;
            Console.SetCursorPosition(leftPosition, Console.CursorTop);
            Console.WriteLine(title);
            Console.WriteLine();
        }
    }