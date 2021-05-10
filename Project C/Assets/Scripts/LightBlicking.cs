using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlicking : MonoBehaviour
{
    public Light lightL;
    public float intens;

    void Start()
    {
        StartCoroutine(Light());
        intens = lightL.intensity;
    }

    IEnumerator Light()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 5f));
        if (UnityEngine.Random.Range(0, 10) == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 0)
                {
                    lightL.intensity = 0f;
                    yield return new WaitForSeconds(0.4f);
                }
                else
                {
                    lightL.intensity = intens;
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        yield return new WaitForSeconds(10f);
        StartCoroutine(Light());
    }
}
