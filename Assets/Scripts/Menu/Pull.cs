using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour
{
    public float deltaY;
    Vector3 init;
    public void OnEnable()
    {
        init = transform.position;
    }
    public void PullRob()
    {
        StartCoroutine(Trans(transform.position + new Vector3(0f, deltaY, 0f)));
        Invoke("RecoverRob", 1f);
    }
    public void RecoverRob()
    {
        StartCoroutine(Trans(init));
    }
    private IEnumerator Trans(Vector3 target)
    {
        Debug.Log("in");
        Vector3 i = transform.position;

        for(float f = 0;f<0.5f;f+=Time.deltaTime)
        {
            transform.position = Vector3.Lerp(i, target, f / 0.5f);
            yield return null;
        }
    }
}
