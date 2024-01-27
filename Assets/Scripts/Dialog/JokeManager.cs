using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class JokeData {
    public float id;
    public string type;
    public string dialog;
    public string punchline1;
    public float tag1;
    public string punchline2;
    public float tag2;
    public string punchline3;
    public float tag3;
    public string punchline4;
    public int tag4;
}

[System.Serializable]
public class ComedyData
{
    public JokeData[] stand_up_comedy;
    public ComedyData(JokeData[] jokes)
    {
        stand_up_comedy = jokes;
    }
}

public class JokeManager : MonoBehaviour
{
    private string _filePath = "jokes";
    public List<int> ToldJokes = new List<int>();

    private ComedyData _comedyDataBase;

    public void Start()
    {
        InitJokes();
        ToldJokes = new List<int>();
    }
    public void InitJokes()
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(_filePath);
        if (jsonTextAsset == null)
        {
            Debug.LogError("JSON file not found. Make sure it's in the Resources folder.");
            return;
        }

        string jsonText = jsonTextAsset.text;
        _comedyDataBase = JsonUtility.FromJson<ComedyData>("{\"stand_up_comedy\":" + jsonTextAsset.text + "}");

        if (_comedyDataBase != null && _comedyDataBase.stand_up_comedy != null)
        {
            // Print or use the result as needed
            foreach (JokeData joke in _comedyDataBase.stand_up_comedy)
                Debug.Log(joke.dialog);
        }
        else
            Debug.LogError("Failed to parse JSON data.");

    }
    public Joke GetJoke()
    {
        int index = UnityEngine.Random.Range(0, 2);
        while(ToldJokes.Contains(index))
        {
            index = UnityEngine.Random.Range(0, 2);
            continue;
        }
        ToldJokes.Add(index);
        Joke j = new Joke(_comedyDataBase.stand_up_comedy[index]);
        return j;
    }
}
