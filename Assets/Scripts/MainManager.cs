using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    
    public void SaveColor()
    {
        //created a new instance of the save data and filled its team color class member with the TeamColor variable saved in the MainManager
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        //transformed that instance to JSON with JsonUtility.ToJson
        string json = JsonUtility.ToJson(data);

        //special method File.WriteAllText to write a string to a file
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        //reversal of the SaveColor method
        string path = Application.persistentDataPath + "/savefile.json";

        //uses the method File.Exists to check if a .json file exists, 
        if (File.Exists(path))
        {
            //the method will read its content with File.ReadAllText
            string json = File.ReadAllText(path);

            //It will then give the resulting text to JsonUtility.FromJson to transform it back into a SaveData instance
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            //will set the TeamColor to the color saved in that SaveData
            TeamColor = data.TeamColor;
        }
    }
    public Color TeamColor; // new variable declared

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }
}
