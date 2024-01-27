using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    public string[] stand_up_comedy;
}

public class JokeManager : MonoBehaviour
{
    public TextAsset jsonFile;
    private string filePath = "dialog"; // Replace with the actual path to your JSON file

    public List<string> GetJokes()
    {
        List<string> dialogLines = new List<string>();
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(filePath);
        if (jsonTextAsset == null)
        {
            Debug.LogError("JSON file not found. Make sure it's in the Resources folder.");
            return null;
        }

        string jsonText = jsonTextAsset.text;
        DialogueData comedyData = JsonUtility.FromJson<DialogueData>(jsonText);

        if (comedyData != null && comedyData.stand_up_comedy != null)
        {
            // Print or use the result as needed
            foreach (string joke in comedyData.stand_up_comedy)
            {
                dialogLines.Add(joke);
            }
        }
        else
            Debug.LogError("Failed to parse JSON data.");

        return dialogLines;
    }
}
