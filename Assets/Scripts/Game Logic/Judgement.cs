using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Judgement : MonoBehaviour
{
    public JokeTeller Talker;
    public BarSystem Bar;
    public GameObject Success;
    public Text SuccessText;
    private int _thereshold = 500;

    private void OnEnable()
    {
        Success.SetActive(false);
        Talker.gameObject.GetComponent<Animator>().SetBool("die", false);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
            DiveToHell();
    }
    public void Judge()
    {
        Success.SetActive(true);
        SuccessText.text = "¦³½ì­È¡G" + Bar.GoodCurrentValue.ToString() + "\n¦aº»­È¡G" + Bar.BadCurrentValue.ToString();
    }
    public void DiveToHell()
    {
        Talker.gameObject.GetComponent<Animator>().SetBool("die", true);
    }
}
