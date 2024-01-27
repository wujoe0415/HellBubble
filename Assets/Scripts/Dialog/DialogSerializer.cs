using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option {
    public string content;
    public int devilAmount;
    public int satisfyAmount;
}
public class Joke{
    public string[] Dialogs;
    public Option[] Options;
}
public class DialogSerializer : MonoBehaviour
{
    public GameObject Bubble;
    private JokeManager _dialogImporter;
    public List<string> _dialogs;
    public Text DialogBox;
    private RectTransform _rect;

    [Range(0.1f, 3f)]
    public float TextSpeed = 1f;

    private void Start()
    {
        _dialogImporter = GetComponent<JokeManager>();
        _rect = GetComponent<RectTransform>();
        _dialogs = _dialogImporter.GetJokes();
        StartCoroutine(StartDialog());
    }

    public void StartJoke(Joke joke)
    {
        StartCoroutine(StartDialog(joke.Dialogs));
    }
    public IEnumerator StartDialog(string[] dialogs)
    {
        WaitForSeconds _finishWait = new WaitForSeconds(1f);
        WaitForSeconds _finishText = new WaitForSeconds(0.07f * TextSpeed);
        Vector2 initSize = _rect.sizeDelta;
        foreach(string dialog in _dialogs)
        {
            DialogBox.text = null;
            int layer = 0;
            _rect.sizeDelta = initSize;
            for (int i = 0; i < dialog.Length; i++)
            {
                DialogBox.text = dialog.Substring(0, i);
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
    }
    public void GenerateOptions(Option[] options)
    {
        Vector2[] pos = new Vector2[3];
        pos[0] = new Vector2(0f, 0f);
        pos[1] = new Vector2(100f, 100f);
        pos[2] = new Vector2(-100f, -100f);
        GameObject bubbleParent = new GameObject("BubbleParent");
        for (int i = 0; i < 3; i++) {
            GameObject bubble = Instantiate(Bubble, bubbleParent.transform);
            bubble.GetComponent<RectTransform>().anchoredPosition = pos[i];
            bubble.GetComponent<OptionSerializer>().SerializeOption(options[i]);
        } 
    }
}
