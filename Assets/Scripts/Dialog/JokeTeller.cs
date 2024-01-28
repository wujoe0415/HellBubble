using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PopManager;

public class Joke
{
    public int id;
    public string type;
    public string[] Dialogs;
    public Option[] Options;
    public Joke(JokeData data)
    {
        Dialogs = data.dialog.Split(new string[] { "|||" }, StringSplitOptions.None);
        Options = new Option[4];
        Options[0] = new Option(data.punchline1, Convert.ToInt32(data.tag1));
        Options[1] = new Option(data.punchline2, Convert.ToInt32(data.tag2));
        Options[2] = new Option(data.punchline3, Convert.ToInt32(data.tag3));
        Options[3] = new Option(data.punchline4, Convert.ToInt32(data.tag4));
    }
}
public class Option {
    public Option(string content, int tag){
        Content = content;
        Tag = tag;
        DevilAmount = 0;
        SatisfyAmount = 1;
    }
    public string Content;
    public int Tag;
    public int DevilAmount;
    public int SatisfyAmount;
}
public class JokeTeller : MonoBehaviour
{
    public GameObject Option;
    private JokeManager _jokeManager;
    public Joke CurrentJoke;
    public Text DialogText;
    public RectTransform DialogBox;

    [Range(0.1f, 3f)]
    public float TextSpeed = 1f;

    private void Start()
    {
        _jokeManager = GetComponent<JokeManager>();
        // Animation
        //StartJoke();
    }
    
    public void StartJoke()
    {
        DialogText.text = "";
        CurrentJoke = _jokeManager.GetJoke();
        StartCoroutine(StartDialog(CurrentJoke.Dialogs));
    }
    public void Punchline(string punchline)
    {
        DialogText.text = "";
        StartCoroutine(StartPunchline(punchline));
    }
    public IEnumerator StartPunchline(string punchline)
    {
        WaitForSeconds _finishWait = new WaitForSeconds(1f);
        WaitForSeconds _finishText = new WaitForSeconds(0.07f * TextSpeed);
        Vector2 initSize = DialogBox.sizeDelta;

        DialogText.text = null;
        int layer = 0;
        DialogBox.sizeDelta = initSize;
        for (int i = 0; i < punchline.Length; i++)
        {
            DialogText.text = punchline.Substring(0, i + 1);
            yield return _finishText;
            if (i / 24 != layer)
            {
                layer++;
                DialogBox.sizeDelta = initSize + new Vector2(0f, 0f + layer * 50f);
            }
        }
        yield return _finishWait;
        yield return new WaitForSeconds(0.5f);
        DialogText.text = "";
        DialogBox.sizeDelta = initSize;
        GameLogic.IsEndChoice = true;
    }
    public IEnumerator StartDialog(string[] dialogs)
    {
        WaitForSeconds _finishWait = new WaitForSeconds(1f);
        WaitForSeconds _finishText = new WaitForSeconds(0.07f * TextSpeed);
        Vector2 initSize = DialogBox.sizeDelta;
        foreach(string dialog in CurrentJoke.Dialogs)
        {
            DialogText.text = null;
            int layer = 0;
            DialogBox.sizeDelta = initSize;
            for (int i = 0; i < dialog.Length; i++)
            {
                DialogText.text = dialog.Substring(0, i+1);
                yield return _finishText;
                if(i/24 != layer)
                {
                    layer++;
                    DialogBox.sizeDelta = initSize + new Vector2(0f, 0f + layer * 50f);
                }
            }
            yield return _finishWait;
        }
        yield return new WaitForSeconds(1f);
        DialogText.text = "...";
        DialogBox.sizeDelta = initSize;
        GameLogic.IsEndDialog = true;
        GenerateOptions(CurrentJoke.Options);
    }
    public void GenerateOptions(Option[] options)
    {
        PopData[] popDatas= new PopData[3];
        for (int i = 0; i < 3; i++) {
            popDatas[i] = new PopData(options[i].Content, options[i].SatisfyAmount, options[i].DevilAmount, (PopTypeEnum)i + 1);
            //bubble.GetComponent<OptionSerializer>().OnClick += () => { StartJoke(); };
        }
        PopManager.Instance.Show(popDatas[0], popDatas[1], popDatas[2]);
    }
}
