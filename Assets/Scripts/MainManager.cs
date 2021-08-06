using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO; // for input and output

public class MainManager : MonoBehaviour
{
    // With this code, you can now set the property’s value from within the class, but only get it from outside the class
    public static MainManager mainManager { get; private set; } // create static instance of this class
    public Color teamColor; //pass the color that the user selects to the units

    private void Awake()
    {
        if (mainManager != null)
        {
            Destroy(gameObject);
            return;
        }

        mainManager = this;
        DontDestroyOnLoad(gameObject); // GameObject attached to this script not to be destroyed when the scene changes.

        LoadColor(); // Loads the last saved color if the file exist
    }


    // Help us save the color into a json file
    [Serializable]
    public class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData saveData = new SaveData(); //  created a new instance of the save data
        saveData.TeamColor = teamColor; // filled team color class member with the TeamColor variable saved in the MainManager

        string json = JsonUtility.ToJson(saveData); // transform the instance to JSON

        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json); // writing the text into a text file
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/saveFile.json"; // check if a .json file exists
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path); // reads the json that is in the persistent data folder
            SaveData saveData = JsonUtility.FromJson<SaveData>(json); // the resulting text to JsonUtility.FromJson to transform it back into a SaveData instance
            teamColor = saveData.TeamColor; // set the TeamColor to the color saved in that SaveData
        }
    }
}
