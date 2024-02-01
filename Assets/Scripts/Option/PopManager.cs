using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopManager : MonoBehaviour
{
    public static PopManager Instance = null;
    public JokeTeller Comedian;

    public PopControler LeftPopControler = null;
    public PopControler RightPopControler = null;
    public PopControler ButtonPopConroler = null;
    private PopData LeftPopData = null;
    private PopData RightPopData = null;
    private PopData ButtonPopData = null;
    public ReactionSerializer Reaction;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    public void Init()
    {
        LeftPopControler.Hide();
        RightPopControler.Hide();
        ButtonPopConroler.Hide();
    }

    public void Show(PopData leftPop, PopData rightPop, PopData b, Action onClose = null)
    {
        LeftPopData = leftPop;
        RightPopData = rightPop;
        ButtonPopData = b;
        LeftPopControler.Show(leftPop.Text_value, leftPop.PopType);
        RightPopControler.Show(rightPop.Text_value, rightPop.PopType);
        ButtonPopConroler.Show(b.Text_value, b.PopType);
    }

    public void Close()
    {
        OnClose();
    }

    public void OnClose(PopTypeEnum popType = PopTypeEnum.None)
    {
        switch (popType)
        {
            case PopTypeEnum.Left:
                Comedian.Punchline(LeftPopData.Text_value);
                BarSystem.Instance.AddValue(LeftPopData.GoodValue, LeftPopData.BadValue);
                Reaction.StartReaction(LeftPopData.GoodValue, LeftPopData.BadValue);
                break;
            case PopTypeEnum.Right:
                Comedian.Punchline(RightPopData.Text_value);
                BarSystem.Instance.AddValue(RightPopData.GoodValue, RightPopData.BadValue);
                Reaction.StartReaction(RightPopData.GoodValue, RightPopData.BadValue);
                break;
            case PopTypeEnum.Button:
                Comedian.Punchline(ButtonPopData.Text_value);
                BarSystem.Instance.AddValue(ButtonPopData.GoodValue, ButtonPopData.BadValue);
                Reaction.StartReaction(ButtonPopData.GoodValue, ButtonPopData.BadValue);
                break;
        }

        LeftPopControler.Hide();
        RightPopControler.Hide();
        ButtonPopConroler.Hide();
    }

    public class PopData
    {
        public string Text_value;
        public int GoodValue = 0;
        public int BadValue = 0;
        public PopTypeEnum PopType = PopTypeEnum.None;

        public PopData(string set_text, int set_good_value, int set_bad_value, PopTypeEnum set_popType)
        {
            Text_value = set_text;
            GoodValue = set_good_value;
            BadValue = set_bad_value;
            PopType = set_popType;
        }
        public PopData(string set_text, float set_good_value, float set_bad_value, PopTypeEnum set_popType)
        {
            Text_value = set_text;
            GoodValue = Convert.ToInt32(set_good_value);
            BadValue = Convert.ToInt32(set_bad_value);
            PopType = set_popType;
        }
    }

    public enum PopTypeEnum
    {
        None = 0,
        Left = 1,
        Right = 2,
        Button = 3
    }
}
