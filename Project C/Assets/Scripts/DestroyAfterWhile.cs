using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterWhile : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyIt());
    }

    IEnumerator DestroyIt()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
