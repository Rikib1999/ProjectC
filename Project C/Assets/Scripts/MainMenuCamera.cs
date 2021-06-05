using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public bool turningLeft = true;
    public float y;

    public void Update()
    {
        y = transform.eulerAngles.y;
        if (transform.eulerAngles.y >= 39f && transform.eulerAngles.y <= 41f && !turningLeft)
        {
            turningLeft = true;
        }
        else if (transform.eulerAngles.y >= 319f && transform.eulerAngles.y <= 321f && turningLeft)
        {
            turningLeft = false;
        }

        if (turningLeft)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 0.01f, transform.eulerAngles.z);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 0.01f, transform.eulerAngles.z);
        }
    }
}