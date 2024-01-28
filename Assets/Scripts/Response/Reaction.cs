using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reaction : MonoBehaviour
{
    private Image _image;

    private void OnEnable()
    {
        float scaler = Random.Range(0.8f, 1f);
        transform.localScale = Vector3.one * scaler;
        _image = GetComponent<Image>();
        _image.enabled = false;
        StartCoroutine(StartLife());
    }

    private IEnumerator StartLife()
    {
        yield return new WaitForSeconds(Random.Range(0.4f, 0.8f));
        _image.enabled = true;
        Vector3 init = transform.position;
        Vector3 targetPosition = init + new Vector3(0f, 10f, 0f);
        
        Color initColor = _image.color;
        initColor.a = 0f;
        Color targetColor = initColor;
        targetColor.a = 255f;
        float time = Random.Range(0.5f, 1f);
        for (float f = 0f; f < time; f+= Time.deltaTime)
        {
            transform.position = Vector3.Lerp(init, targetPosition, f / time);
            _image.color = Color.Lerp(initColor, targetColor, f / 0.5f);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        initColor = _image.color;
        initColor.a = 255f;
        targetColor = initColor;
        targetColor.a = 0f;
        time = Random.Range(0.5f, 0.8f);
        for (float f = 0f; f < time; f += Time.deltaTime)
        {
            _image.color = Color.Lerp(initColor, targetColor, f / time);
            yield return null;
        }
        Destroy(gameObject);
    }
}
