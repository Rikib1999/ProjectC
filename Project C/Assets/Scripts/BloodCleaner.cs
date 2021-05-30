using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCleaner : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyIt());
    }

    IEnumerator DestroyIt()
    {
        yield return new WaitForSeconds(7f);
        for (float f = 1f; transform.localScale.x >= 0f; f -= 0.01f)
        {
            transform.localScale = new Vector3(transform.localScale.x - f, transform.localScale.y - f, transform.localScale.z);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }
}
