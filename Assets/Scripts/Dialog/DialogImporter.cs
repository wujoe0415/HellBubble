using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
    public List<string> stand_up_comedy;
}

[System.Serializable]
public class DialogueData
{
    public List<DialogueEntry> entries;
}


public class DialogImporter : MonoBehaviour
{
    public TextAsset jsonFile;
    private string filePath = "Assets/Resources/dialog.json"; // Replace with the actual path to your JSON file

    public List<string> GetDiaLog()
    {
        DialogueData dialogData = JsonUtility.FromJson<DialogueData>(jsonFile.text);

        List<string> dialogLines = SplitDialogByLines(dialogData);

        return dialogLines;
    }

    List<string> SplitDialogByLines(DialogueData dialogData)
    {
        List<string> lines = new List<string>();

        foreach (DialogueEntry entry in dialogData.entries)
        {
            lines.AddRange(entry.stand_up_comedy);
        }

        return lines;
    }
}
