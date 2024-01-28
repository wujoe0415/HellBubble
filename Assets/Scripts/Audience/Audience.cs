using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Audience : MonoBehaviour
{
    public static List<WeightScore> s_Weights = new List<WeightScore>();

    float[] interests = new float[] {
        1, 1, 1.5f, 1, 1, -1, 2, 1, 1, 1, 1, 1,
        1, -1, 1, 1.5f, 1.5f, 1.5f, -1.5f, 1, -1, 2, 1, 2,
        1, 1, 1.5f, -1, 1, 2, 1.5f, 2, 1.5f, 1.5f, 1, 1,
        -1, -1, -1, 2, 2, 1, 1, 1.5f, -1.5f, 2, 1, 2,
        2, 2, 1.7f, 2, 1.2f, 2, 1.2f, -1.5f, 2, 1.5f, 1, -1,
        1.5f, 1.5f, 2, 1, -1, 1, 2, 1, -1, 1, 1, 1,
        1.5f, 1.5f, 1.5f, 1, 1, 1, 1.5f, 2, 2, 1.5f, 1, 1
    };
    float[] insults = new float[] {
        1, 1, 1, 1.5f, 1, 1, 2, 1, 1, 2, 1, 2 ,
        1, 1.5f, 1, 1.5f, 1.5f, 1.5f, 1, 1, 1, 1, 1, 1 ,
        1, 1, 2, 2, 1, 1.5f, 1, 1, 1, 1, 2, 1 ,
        1, 1, 1.5f, 1, 1.5f, 1, 1.5f, 1, 1, 1, 1.5f, 1 ,
        1, 1, 1, 1, 1, 1.5f, 1.5f, 1.5f, 1, 1.5f, 1.5f, 1 ,
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,    
        1, 1.5f, 1, 1.5f, 1.5f, 1.5f, 1, 1, 1, 1, 1, 1
    };
    public enum AudienceTag
    {
        drunk,
        feminism,
        racism,
        disabilities,
        comedian,
        man,
        taiwanese
    }
    public class WeightScore
    {
        public float[] InterestingScores;
        public float[] InsultingScores;
        public WeightScore(float[] interests, float[] insults)
        {
            InterestingScores = interests;
            InsultingScores = insults;
        }
    }

    public static int CurrentAudienceTag;
    private void Awake()
    {
        s_Weights.Clear();
        for (int i = 0; i < 7; i++)
        {
            s_Weights.Add(new WeightScore(interests.Skip(12 * i).Take(12 * (i + 1) - 1).ToArray(), insults.Skip(12 * i).Take(12 * (i + 1) - 1).ToArray()));
        }
        CurrentAudienceTag = Random.Range(0, 7);
    }
    public static float[] InterestsWeight
    {
        get => s_Weights[CurrentAudienceTag].InterestingScores;
    }
    public static float[] InsultsWeight
    {
        get => s_Weights[CurrentAudienceTag].InsultingScores;
    }
}
