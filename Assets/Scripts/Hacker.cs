using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    // Game state
    int level;
    string levelName;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;

	// Use this for initialization
	void Start ()
    {
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Infiltrator 3000 ready...\n" +
            "Targets acquired...\n" +
            "\n" +
            "1) Local University\n" +
            "2) Local Police Station\n" +
            "3) ASIO\n" +
            "\n" +
            "Select target:");
    }

    void OnUserInput(string input)
    {
        if (input == "menu" || input == "Menu")
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }

    }

    void RunMainMenu(string input)
    {
        switch (input)
        {
            case "1":
                level = 1;
                levelName = "Local University";

                StartGame(1);
                break;
            case "2":
                level = 2;
                levelName = "Local Police Station";
                StartGame(2);
                break;
            case "3":
                level = 3;
                levelName = "ASIO";
                StartGame(3);
                break;
            default:
                ShowMainMenu();
                break;
        }
    }

    void StartGame(int level)
    {
        currentScreen = Screen.Password;
        Terminal.WriteLine("\nTargeting " + levelName + "...");
        Terminal.WriteLine("Enter Password: ");
    }
}
