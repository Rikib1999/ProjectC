using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetsCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    private Vector3 velocity;
    private float smoothTime = 0.5f;
    private float nextTimeToSearch = 0f;

    public float minx, maxx, minz, maxz;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player 1") != null && GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            targets[0] = GameObject.FindGameObjectWithTag("Player 1").transform;
            targets[1] = GameObject.FindGameObjectWithTag("Player 2").transform;
}
        else
        {
            GetComponent<CameraForOne>().enabled = true;
            GetComponent<MultipleTargetsCamera>().enabled = false;
        }
    }
    private void FixedUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }

        if (targets[0] != null && targets[1] != null)
        {
            smoothTime = 0.1f;
            Vector3 centerPoint = GetCenterPoint();
            Vector3 newPosition = centerPoint + offset;
            transform.position = Vector3.SmoothDamp(transform.position, /*newPosition*/new Vector3(Mathf.Clamp(newPosition.x, minx, maxx), newPosition.y, Mathf.Clamp(newPosition.z, minz, maxz)), ref velocity, smoothTime);
        }
        else if (targets[0] != null && targets[1] == null)
        {
            smoothTime = 0.5f;
            transform.position = Vector3.SmoothDamp(transform.position, /*targets[0].position + offset*/new Vector3(Mathf.Clamp(targets[0].position.x, minx, maxx), targets[0].position.y + offset.y, Mathf.Clamp(targets[0].position.z + offset.z, minz, maxz)) /*+ offset*/, ref velocity, smoothTime);
            StartCoroutine(Wait());
            FindPlayer(2);
        }
        else if (targets[1] != null)
        {
            smoothTime = 0.5f;
            transform.position = Vector3.SmoothDamp(transform.position, /*targets[1].position + offset*/new Vector3(Mathf.Clamp(targets[1].position.x, minx, maxx), targets[1].position.y + offset.y, Mathf.Clamp(targets[1].position.z + offset.z, minz, maxz)) /*+ offset*/, ref velocity, smoothTime);
            StartCoroutine(Wait());
            FindPlayer(1);
        }
        else
        {
            GetComponent<MultipleTargetsCamera>().enabled = false;
        }
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        smoothTime = 0f;
    }

    void FindPlayer(int p)
    {
        if (p == 2)
        {
            if (nextTimeToSearch <= Time.time)
            {
                GameObject searchResult = GameObject.FindGameObjectWithTag("Player 2");
                if (searchResult != null)
                {
                    targets[1] = searchResult.transform;
                }
                nextTimeToSearch = Time.time + 0.5f;
            }
        }
        else
        {
            if (nextTimeToSearch <= Time.time)
            {
                GameObject searchResult = GameObject.FindGameObjectWithTag("Player 1");
                if (searchResult != null)
                {
                    targets[0] = searchResult.transform;
                }
                nextTimeToSearch = Time.time + 0.5f;
            }
        }
    }
}
