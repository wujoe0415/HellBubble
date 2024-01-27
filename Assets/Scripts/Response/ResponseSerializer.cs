using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ResponseSerializer : MonoBehaviour
{
    public Transform[] Audiences;
    public Sprite[] LeftBubble;
    public Sprite[] RightBubble;

    public GameObject Response;

    private Vector2[] _offsets = new Vector2[] {
        new Vector2(70f, 5f),
        new Vector2(90f, 10f),
        new Vector2(110f, 20f),
    };

    public void StartResponse(int interest, int insult)
    {
        // Interest
        if(interest == 10)
        {
            for (int i = 0; i < Random.Range(0, 2); i++)
                GenerateResponse(3);
            for (int i = 0; i < Random.Range(4, 6); i++)
                GenerateResponse(4);
        }
        else if(interest >= 8)
        {
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateResponse(1);
            for (int i = 0; i < Random.Range(1, 3); i++)
                GenerateResponse(3);
            for (int i = 0; i < Random.Range(2, 4); i++)
                GenerateResponse(4);
        }
        else if (interest >= 6)
        {
            for (int i = 0; i < Random.Range(1, 2); i++)
                GenerateResponse(1);
            for (int i = 0; i < Random.Range(2, 4); i++)
                GenerateResponse(3);
            for (int i = 0; i < Random.Range(0, 2); i++)
                GenerateResponse(4);
        }
        else if (interest >= 4)
        {
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateResponse(1);
            for (int i = 0; i < Random.Range(2, 4); i++)
                GenerateResponse(3);
            for (int i = 0; i < Random.Range(2, 4); i++)
                GenerateResponse(4);
        }
        else if (interest >= 2)
        {
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateResponse(1);
            for (int i = 0; i < Random.Range(4, 6); i++)
                GenerateResponse(3);
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateResponse(4);
        }
        else
        {
            for (int i = 0; i < Random.Range(6, 8); i++)
                GenerateResponse(1);
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateResponse(3);
        }
        // Insult

    }
    public void GenerateResponse(int res)
    {
        int index = Random.Range(0, 2);
        GameObject response = Instantiate(Response, Audiences[index]);
        response.transform.localPosition = _offsets[index];
        response.GetComponent<Image>().sprite = Random.Range(0, 10) > 5 ? LeftBubble[res] : RightBubble[res];
    }
}
