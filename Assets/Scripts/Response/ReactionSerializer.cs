using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ReactionSerializer : MonoBehaviour
{
    public Transform[] Audiences;
    public Sprite[] LeftBubble;
    public Sprite[] RightBubble;

    public GameObject Response;

    public AudioSource Clap;
    public AudioSource Boring;
    public AudioSource Boo;

    private Vector2[] _offsets = new Vector2[] {
        new Vector2(750f, 330f),
        new Vector2(850f, 300f),
        new Vector2(900f, 280f),
    };
    public void StartReaction(int interest, int insult)
    {
        // Interest
        if (interest == 10)
        {
            for (int i = 0; i < Random.Range(0, 2); i++)
                GenerateReaction(3);
            for (int i = 0; i < Random.Range(4, 6); i++)
                GenerateReaction(4);
        }
        else if(interest >= 8)
        {
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateReaction(0);
            for (int i = 0; i < Random.Range(1, 3); i++)
                GenerateReaction(3);
            for (int i = 0; i < Random.Range(2, 4); i++)
                GenerateReaction(4);
        }
        else if (interest >= 6)
        {
            for (int i = 0; i < Random.Range(1, 2); i++)
                GenerateReaction(0);
            for (int i = 0; i < Random.Range(2, 4); i++)
                GenerateReaction(3);
            for (int i = 0; i < Random.Range(0, 2); i++)
                GenerateReaction(4);
        }
        else if (interest >= 4)
        {
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateReaction(0);
            for (int i = 0; i < Random.Range(2, 4); i++)
                GenerateReaction(3);
            for (int i = 0; i < Random.Range(2, 4); i++)
                GenerateReaction(4);
        }
        else if (interest >= 2)
        {
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateReaction(0);
            for (int i = 0; i < Random.Range(4, 6); i++)
                GenerateReaction(3);
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateReaction(4);
        }
        else
        {
            for (int i = 0; i < Random.Range(6, 8); i++)
                GenerateReaction(0);
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateReaction(3);
        }

        // Insult
        if (insult == 10)
        {
            for (int i = 0; i < Random.Range(0, 2); i++)
                GenerateReaction(1);
            for (int i = 0; i < Random.Range(4, 6); i++)
                GenerateReaction(2);
        }
        else if (insult >= 8)
        {
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateReaction(0);
            for (int i = 0; i < Random.Range(1, 3); i++)
                GenerateReaction(1);
            for (int i = 0; i < Random.Range(2, 4); i++)
                GenerateReaction(2);
        }
        else if (insult >= 6)
        {
            for (int i = 0; i < Random.Range(1, 2); i++)
                GenerateReaction(0);
            for (int i = 0; i < Random.Range(2, 4); i++)
                GenerateReaction(1);
            for (int i = 0; i < Random.Range(0, 2); i++)
                GenerateReaction(2);
        }
        else if (insult >= 4)
        {
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateReaction(0);
            for (int i = 0; i < Random.Range(2, 4); i++)
                GenerateReaction(1);
            for (int i = 0; i < Random.Range(2, 4); i++)
                GenerateReaction(2);
        }
        else if (insult >= 2)
        {
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateReaction(0);
            for (int i = 0; i < Random.Range(4, 6); i++)
                GenerateReaction(1);
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateReaction(2);
        }
        else
        {
            for (int i = 0; i < Random.Range(6, 8); i++)
                GenerateReaction(0);
            for (int i = 0; i < Random.Range(0, 1); i++)
                GenerateReaction(1);
        }
        SoundReaction(interest, insult);
        GroupReaction(interest, insult);
        TellerReaction(interest, insult);
    }

    public void SoundReaction(int interest, int insult)
    {
        Clap.volume = interest / 10f;
        Clap.Play();

        // if boring is bgm
        if (interest < 5 && insult < 5)
            BoringBGM(0.8f);


        Boo.volume = insult / 10;
        Boo.Play();
    }
    public void GroupReaction(int interest, int insult)
    {

    }
    public void TellerReaction(int interest, int insult)
    {

    }
    public void GenerateReaction(int res)
    {
        int index = Random.Range(0, 3);
        GameObject response = Instantiate(Response, Audiences[index]);
        Random.InitState(Random.Range(200, 1000));
        response.transform.localPosition = new Vector3(Random.Range(-_offsets[index].x, _offsets[index].x), Random.Range(_offsets[index].y - 50, _offsets[index].y), 0f);
        response.GetComponent<Image>().sprite = Random.Range(0, 10) > 5 ? LeftBubble[res] : RightBubble[res];
    }

    private IEnumerator BoringBGM(float sec)
    {
        Boring.Stop();

        yield return new WaitForSeconds(sec);

        Boring.Play();
    }
}
