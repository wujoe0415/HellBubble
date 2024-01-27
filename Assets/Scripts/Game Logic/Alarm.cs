using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alarm : MonoBehaviour
{
    public Image Fill;
    public Gradient AlarmGradient;

    private float maxValue = 0f;

    public void SetMaxValue(float value)
    {
        maxValue = value;
    }
    public void SetAlarmValue(float value)
    {
        Fill.fillAmount= value/ maxValue;
        Fill.color = AlarmGradient.Evaluate(value / maxValue);
    }
}
