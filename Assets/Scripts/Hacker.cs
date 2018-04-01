using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    // Game state
    int level;
    enum Screen { MainMenu, Password, GameOver };
    Screen currentScreen;
    int passwordAttemptsRemaining;
    const int PasswordAttemptsStart = 3;

    // Passwords
    List<string> level1passwords = new List<string>();
    List<string> level2passwords = new List<string>();
    List<string> level3passwords = new List<string>();
    string password;

    // Levels
    List<string> levels = new List<string>();

    // Texts
    string mainMenuStartText;
    string mainMenuInvalidSelectText;

    // Use this for initialization
    void Start ()
    {
        SetGameContent();
        ShowMainMenu(mainMenuStartText);
    }

    void SetGameContent()
    {
        //Populate level password lists
        level1passwords.AddRange(new string[] { "history", "science", "fiction", "reference", "dewey" });
        level2passwords.AddRange(new string[] { "investigation", "jurisdiction", "criminology", "harassment", "identification" });
        level3passwords.AddRange(new string[] { "luminosity", "aeronautical", "spectrograph", "continuum", "astrometric" });

        // Create level list
        levels.AddRange(new string[] { "University", "Police Station", "ASIO" });

        // Main Menu Text
        mainMenuStartText = "Infiltrator 3000\n" +
            "\n" +
            "Available targets:\n" +
            "\n" +
            "1) " + levels[0] + "\n" +
            "2) " + levels[1] + "\n" +
            "3) " + levels[2] + "\n" +
            "\n" +
            "Select Target:";

        mainMenuInvalidSelectText = "Infiltrator 3000\n" +
            "\n" +
            "Available targets:\n" +
            "\n" +
            "1) " + levels[0] + "\n" +
            "2) " + levels[1] + "\n" +
            "3) " + levels[2] + "\n" +
            "\n" +
            "Invalid target selected. Try again.\n" +
            "\n" +
            "Select Target:";
    }

    void OnUserInput(string input)
    {
        if (input.ToLower() == "exit")
        {
            ShowMainMenu(mainMenuStartText);
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

    void ShowMainMenu(string input)
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine(input);
    }

    void RunMainMenu(string input)
    {
        if(input == "1" || input == "2" || input == "3")
        {
            StartGame(int.Parse(input));
        }
        else
        {
            ShowMainMenu(mainMenuInvalidSelectText);
        }
    }

    void StartGame(int input)
    {
        currentScreen = Screen.Password;
        level = input;
        passwordAttemptsRemaining = PasswordAttemptsStart;

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

        ShowPasswordScreen();
    }

    void ShowPasswordScreen()
    {
        Terminal.ClearScreen();

        Terminal.WriteLine("Target: " + levels[level - 1] + "\n");

        if (passwordAttemptsRemaining < 3)
        {
            Terminal.WriteLine("Password incorrect. Try again.\n");
        }
        Terminal.WriteLine("Tries Remaining: " + passwordAttemptsRemaining + "\n\nEnter Password");
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
        return input == password;
    }

    void ShowGameOverScreen(bool input)
    {
        if(input == true)
        {
            Terminal.ClearScreen();
            Terminal.WriteLine(levels[level - 1] + " successfully infiltrated.\n" +
                "\n" +
                "Press any key to return to main menu.");
        }
        else
        {
            Terminal.ClearScreen();
            Terminal.WriteLine(levels[level - 1] + " infiltration failed.\n" +
                "\n" +
                "Press any key to return to main menu.");
        }
    }

    void RunGameOverScreen()
    {
        if(Input.anyKey)
        {
            currentScreen = Screen.MainMenu;
            ShowMainMenu(mainMenuStartText);
        }
        
    }
}
