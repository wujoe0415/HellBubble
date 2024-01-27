using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSerializer : MonoBehaviour
{
    private DialogImporter _dialogImporter;
    public List<string> _dialogs;
    public Text DialogBox;
    private RectTransform _rect;

    [Range(0.1f, 3f)]
    public float TextSpeed = 1f;

    private void Start()
    {
        _dialogImporter = GetComponent<DialogImporter>();
        _rect = GetComponent<RectTransform>();
        _dialogs = _dialogImporter.GetDiaLog();
        StartCoroutine(StartDialog());
    }
    public IEnumerator StartDialog()
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
}
