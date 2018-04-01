using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    // Game state
    int level;
    enum Screen { MainMenu, Password, GameOver };
    Screen currentScreen;
    int passwordAttemptsRemaining;
    const int PasswordAttemptsStart = 5;

    // Passwords
    List<string> level1passwords = new List<string>();
    List<string> level2passwords = new List<string>();
    List<string> level3passwords = new List<string>();
    string password;

    // Levels
    List<string> levels = new List<string>();

    // Use this for initialization
    void Start ()
    {
        SetGameContent();
        ShowMainMenu("start");
    }

    void SetGameContent()
    {
        //Populate level password lists
        level1passwords.AddRange(new string[] { "history", "science", "fiction", "reference", "dewey" });
        level2passwords.AddRange(new string[] { "investigate", "jurisdiction", "detective", "criminal", "identify" });
        level3passwords.AddRange(new string[] { "intelligence", "espionage", "communications", "terrorism", "conspiracy" });

        // Create level list
        levels.AddRange(new string[] { "University", "Police Station", "ASIO" });        
    }

    void ShowMainMenu(string status)
    {
        string mainMenuStartText = "Infiltrator 3000\n\n" +
            "Scanning for targets...\n\n" + 
            "Available targets:\n\n" +
            "1) " + levels[0] + "\n" +
            "2) " + levels[1] + "\n" +
            "3) " + levels[2] + "\n\n" +
            "Select Target:";

        string mainMenuInvalidSelectText = "Infiltrator 3000\n\n" +
            "Available targets:\n\n" +
            "1) " + levels[0] + "\n" +
            "2) " + levels[1] + "\n" +
            "3) " + levels[2] + "\n\n" +
            "Invalid target selected. Try again.\n\n" +
            "Select Target:";

        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        if(status == "start")
        {
            Terminal.WriteLine(mainMenuStartText);
        }
        else if( status == "invalid")
        {
            Terminal.WriteLine(mainMenuInvalidSelectText);
        }
        else
        {
            Terminal.WriteLine("Invalid Status");
        }
    }

    void OnUserInput(string input)
    {
        if (input == null)
        {
            ShowMainMenu("invalid");
        }
        else if (input.ToLower() == "exit")
        {
            ShowMainMenu("start");
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            RunPasswordScreen(CheckPassword(input));
        }
        else if (currentScreen == Screen.GameOver)
        {
            RunGameOverScreen();
        }

    }

    void RunMainMenu(string input)
    {
        if(input == "1" || input == "2" || input == "3")
        {
            StartGame(int.Parse(input));
        }
        else
        {
            ShowMainMenu("invalid");
        }
    }

    void StartGame(int input)
    {
        currentScreen = Screen.Password;
        level = input;
        passwordAttemptsRemaining = PasswordAttemptsStart;
        SetRandomPassword(input);
        ShowPasswordScreen();
    }

    void SetRandomPassword(int input)
    {
        switch (input)
        {
            case 1:
                password = level1passwords[Random.Range(0, level1passwords.Count)];
                break;
            case 2:
                password = level2passwords[Random.Range(0, level2passwords.Count)];
                break;
            case 3:
                password = level3passwords[Random.Range(0, level3passwords.Count)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
    }

    void ShowPasswordScreen()
    {
        Terminal.ClearScreen();

        Terminal.WriteLine("Target: " + levels[level - 1] + "\n");

        if (passwordAttemptsRemaining < PasswordAttemptsStart)
        {
            Terminal.WriteLine("Password incorrect. Try again.\n");
        }
        else
        {
            Terminal.WriteLine("Type guess and press Enter.\n");
        }

        Terminal.WriteLine("Tries Remaining: " +
            passwordAttemptsRemaining +
            "\n\nType exit back out." +
            "\n\nPassword Hash: ##" + password.Anagram() + "##" +
            "\n\nEnter Password:");
    }

    void RunPasswordScreen(bool input)
    {
        if(input == false)
        {
            passwordAttemptsRemaining--;
            if(passwordAttemptsRemaining <= 0)
            {
                currentScreen = Screen.GameOver;
                ShowGameOverScreen(false);
            }
            else
            {
                ShowPasswordScreen();
            }
        }
        else
        {
            currentScreen = Screen.GameOver;
            ShowGameOverScreen(true);
        }
    }

    bool CheckPassword(string input)
    {
        return input.ToLower() == password || input == "007";
    }

    void ShowGameOverScreen(bool result)
    {
        Terminal.ClearScreen();
        if (result == true)
        {
            Terminal.WriteLine(levels[level - 1] + " infiltrated.");
        }
        else
        {
            Terminal.WriteLine(levels[level - 1] + " infiltration failed.");
        }
        ShowGameOverArt(result);
        Terminal.WriteLine("\n\nPress any key to return to main menu.");
    }

    void ShowGameOverArt(bool result)
    {
        string level1Win = @"
 _    _ _____ _   _ _ 
| |  | |_   _| \ | | | You now
| |  | | | | |  \| | | have a 
| |/\| | | | | . ` | | 4.0 GPA.
\  /\  /_| |_| |\  |_| \(ˆ˚ˆ)/
 \/  \/ \___/\_| \_(_)";

        string level1Fail = @"
______ ___  _____ _     
|  ___/ _ \|_   _| |     Expelled
| |_ / /_\ \ | | | |     from
|  _||  _  | | | | |     university.
| |  | | | |_| |_| |____ (-_-;)
\_|  \_| |_/\___/\_____/";

        string level2Win = @"
 _    _ _____ _   _ _ 
| |  | |_   _| \ | | | Bye bye
| |  | | | | |  \| | | parking and
| |/\| | | | | . ` | | speeding
\  /\  /_| |_| |\  |_| fines.
 \/  \/ \___/\_| \_(_) \(^-^)/";

        string level2Fail = @"
______ ___  _____ _     
|  ___/ _ \|_   _| |     The police
| |_ / /_\ \ | | | |     have tracked
|  _||  _  | | | | |     your
| |  | | | |_| |_| |____ location.
\_|  \_| |_/\___/\_____/ (╯°□°)╯";

        string level3Win = @"
 _    _ _____ _   _ _ 
| |  | |_   _| \ | | | You find
| |  | | | | |  \| | | footage of
| |/\| | | | | . ` | | aliens at
\  /\  /_| |_| |\  |_| Pine Gap.
 \/  \/ \___/\_| \_(_) (<>..<>)";

        string level3Fail = @"
______ ___  _____ _      ASIO
|  ___/ _ \|_   _| |     officially
| |_ / /_\ \ | | | |     deny any
|  _||  _  | | | | |     knowledge
| |  | | | |_| |_| |____ of you.
\_|  \_| |_/\___/\_____/ (⌐■_■)--︻╦╤─";


        if (result == true)
        {
            switch (level)
            {
                case 1:
                    Terminal.WriteLine(level1Win);
                    break;
                case 2:
                    Terminal.WriteLine(level2Win);
                    break;
                case 3:
                    Terminal.WriteLine(level3Win);
                    break;
                default:
                    Debug.LogError("Invalid level number");
                    break;
            }
        }
        else
        {
            switch (level)
            {
                case 1:
                    Terminal.WriteLine(level1Fail);
                    break;
                case 2:
                    Terminal.WriteLine(level2Fail);
                    break;
                case 3:
                    Terminal.WriteLine(level3Fail);
                    break;
                default:
                    Debug.LogError("Invalid level number");
                    break;
            }
        }
    }

    void RunGameOverScreen()
    {
        if(Input.anyKey)
        {
            currentScreen = Screen.MainMenu;
            ShowMainMenu("start");
        }
        
    }
}
