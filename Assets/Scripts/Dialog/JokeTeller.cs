using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Canvas OptionCanvas;
    public GameObject Option;
    private JokeManager _jokeManager;
    public Joke CurrentJoke;
    public Text DialogBox;
    private RectTransform _rect;

    [Range(0.1f, 3f)]
    public float TextSpeed = 1f;

    private void Start()
    {
        _jokeManager = GetComponent<JokeManager>();
        _rect = GetComponent<RectTransform>();
        // Animation
        StartJoke();
    }
    
    public void StartJoke()
    {
        CurrentJoke = _jokeManager.GetJoke();
        StartCoroutine(StartDialog(CurrentJoke.Dialogs));
    }
    public IEnumerator StartDialog(string[] dialogs)
    {
        WaitForSeconds _finishWait = new WaitForSeconds(1f);
        WaitForSeconds _finishText = new WaitForSeconds(0.07f * TextSpeed);
        Vector2 initSize = _rect.sizeDelta;
        foreach(string dialog in CurrentJoke.Dialogs)
        {
            DialogBox.text = null;
            int layer = 0;
            _rect.sizeDelta = initSize;
            for (int i = 0; i < dialog.Length; i++)
            {
                DialogBox.text = dialog.Substring(0, i+1);
                yield return _finishText;
                if(i/24 != layer)
                {
                    layer++;
                    _rect.sizeDelta = initSize + new Vector2(0f, 0f + layer * 50f);
                }
            }
            yield return _finishWait;
        }
        yield return null;
        GenerateOptions(CurrentJoke.Options);
    }
    public void GenerateOptions(Option[] options)
    {
        Vector2[] pos = new Vector2[3];
        pos[0] = new Vector2(0f, -200f);
        pos[1] = new Vector2(200f, 100f);
        pos[2] = new Vector2(-200f, 100f);
        //Vector2 anchor = GetComponent<RectTransform>().anchoredPosition;
        GameObject bubbleParent = new GameObject("BubbleParent");
        bubbleParent.transform.parent = transform;
        bubbleParent.transform.localPosition = Vector3.zero;
        for (int i = 0; i < 3; i++) {
            GameObject bubble = Instantiate(Option, bubbleParent.transform);
            bubble.GetComponent<RectTransform>().anchoredPosition = /*anchor +*/ pos[i];
            bubble.GetComponent<OptionSerializer>().SerializeOption(options[i]);
            //bubble.GetComponent<OptionSerializer>().OnClick += () => { StartJoke(); };
        } 
    }
}
