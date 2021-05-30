using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int destroy = 0;
    private bool played = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!played && other.tag == "Ground")
        {
            gameObject.GetComponent<AudioSource>().Play();
            played = true;
        }
    }

    void Update()
    {
        if (destroy == 5000)
        {
            Destroy(gameObject);
        }
        else
        {
            destroy++;
        }
    }
}
