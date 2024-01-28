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
        Options[0] = new Option(data.punchline1, data.tag1, data.fun1, data.insult1);
        Options[1] = new Option(data.punchline2, data.tag2, data.fun2, data.insult2);
        Options[2] = new Option(data.punchline3, data.tag3, data.fun3, data.insult3);
        Options[3] = new Option(data.punchline4, data.tag4, data.fun4, data.insult4);
    }
}
public class Option {
    public Option(string content, string tag, int fun, int devil){
        Content = content;
        Tag = tag;
        SatisfyAmount = fun;
        DevilAmount = devil;
    }
    public string Content;
    public string Tag;
    public int SatisfyAmount;
    public int DevilAmount;
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
    private Vector2 initSize = Vector2.one;

    private Animator _an;

    private void Start()
    {
        _jokeManager = GetComponent<JokeManager>();
        initSize = DialogBox.sizeDelta;
        _an = GetComponent<Animator>();
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

        DialogText.text = null;
        int layer = 0;
        DialogBox.sizeDelta = initSize;

        Talk();
        _an.SetBool("handup", true);

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
        EndTalk();
        _an.SetBool("handup", false);

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
        DialogBox.sizeDelta = initSize;
        
        Talk();

        foreach (string dialog in CurrentJoke.Dialogs)
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

        int dlayer = -1;
        DialogText.text = null;
        DialogBox.sizeDelta = initSize;
        foreach (string dialog in CurrentJoke.Dialogs)
        {
            DialogText.text += dialog + "\n";
            dlayer += dialog.Length / 24 + 1;
            DialogBox.sizeDelta = initSize + new Vector2(0f, 0f + dlayer * 50f); 
        }
        DialogText.text = DialogText.text.Substring(0, DialogText.text.Length - 1);
        GameLogic.IsEndDialog = true;
        GenerateOptions(CurrentJoke.Options);

        EndTalk();
    }
    public void GenerateOptions(Option[] options)
    {
        PopData[] popDatas= new PopData[3];
        List<int> arr = new List<int>{ 0,1,2,3};
        arr.RemoveAt(UnityEngine.Random.Range(0, 4));
        for (int i = 0; i < 3; i++) {
            popDatas[i] = new PopData(options[arr[i]].Content, options[arr[i]].SatisfyAmount * Audience.InterestsWeight[(JokeManager.stringIndexMap[options[arr[i]].Tag])], options[i].DevilAmount * Audience.InsultsWeight[(JokeManager.stringIndexMap[options[i].Tag])], (PopTypeEnum)i + 1);
            //bubble.GetComponent<OptionSerializer>().OnClick += () => { StartJoke(); };
        }
        PopManager.Instance.Show(popDatas[0], popDatas[1], popDatas[2]);
    }

    private void EndTalk()
    {
        _an.SetBool("talk", false);
    }
    private void Talk()
    {

        _an.SetBool("talk", true);
    }
}
