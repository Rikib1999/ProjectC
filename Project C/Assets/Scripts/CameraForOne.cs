using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForOne : MonoBehaviour
{
    Transform player;
    public Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player 1").transform;
    }

    void Update()
    {
        transform.position = player.position + offset;
    }
}
