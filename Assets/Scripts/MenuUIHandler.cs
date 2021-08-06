using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        MainManager.mainManager.teamColor = color;
    }

    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;

        // will pre-select the saved color in the MainManager
        ColorPicker.SelectColor(MainManager.mainManager.teamColor);

        // MainManager.mainManager = null; // we are getting an error because this feild is read only
    }

    //SceneManager is the class that handles everything related to loading and unloading scenes
    public void StartNewScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        // Saves the color when the user quits the game
        MainManager.mainManager.SaveColor();

        // Checking if we are playing the game in a build or the editor
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        // only works in the built application, not when you’re testing in the Editor
        Application.Quit();
#endif
    }



    // Testing with buttons
    public void SavePickedColor()
    {
        MainManager.mainManager.SaveColor();
    }

    public void LoadPickedColor()
    {
        MainManager.mainManager.LoadColor();
        ColorPicker.SelectColor(MainManager.mainManager.teamColor);
    }
}
