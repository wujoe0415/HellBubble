using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Judgement : MonoBehaviour
{
    public Animator DieTalkers;
    public BarSystem Bar;
    public GameObject Success;
    public Text SuccessText;
    
    public GameObject Fail;
    private int _thereshold = 500;

    private void OnEnable()
    {
        Success.SetActive(false);
        Fail.SetActive(false);
        DieTalkers.SetBool("die", false);
    }
    public void Judge()
    {
        Success.SetActive(true);
        SuccessText.text = "¦³½ì­È¡G" + Bar.GoodCurrentValue.ToString() + "\n¦aº»­È¡G" + Bar.BadCurrentValue.ToString();
    }
    public void DiveToHell()
    {
        Fail.SetActive(true);
        DieTalkers.SetBool("die", true);
    }
}
