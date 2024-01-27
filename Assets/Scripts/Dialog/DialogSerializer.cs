using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSerializer : MonoBehaviour
{
    private DialogImporter _dialogImporter;
    private List<string> _dialogs;
    public Text DialogBox;

    private void Start()
    {
        _dialogImporter = GetComponent<DialogImporter>();
        _dialogs = _dialogImporter.GetDiaLog();
        StartCoroutine(StartDialog());
    }
    public IEnumerator StartDialog()
    {
        WaitForSeconds _finishWait = new WaitForSeconds(1f);
        foreach(string dialog in _dialogs)
        {
            DialogBox.text = null;
            Debug.Log(dialog);
            for (int i = 0; i < dialog.Length; i++)
            {
                DialogBox.text = dialog.Substring(0, i);
                yield return null;
            }
            yield return _finishWait;
        }
        yield return null;
    }
}
