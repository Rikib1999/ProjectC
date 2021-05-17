using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;
    Rigidbody rb;
    private Vector3 movement;
    Animator animator;
    private float i = 0;
    public string Horizontal;
    public string Vertical;
    public Camera cam;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw(Horizontal);
        movement.z = Input.GetAxisRaw(Vertical);

        if (movement.x != 0 || movement.z != 0)
        {
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if (movement.x < 0)
        {
            if(movement.z < 0)
            {
                transform.rotation = Quaternion.Euler(0, 225, 0);
                i = 225;
                StartCoroutine(SW());
            }
            else if (movement.z == 0)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
                i = 270;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 315, 0);
                i = 315;
                StartCoroutine(NW());
            }
        }else if (movement.x == 0)
        {
            if(movement.z < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                i = 180;
            }
            else if (movement.z == 0)
            {
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                i = 0;
            }
        }
        else
        {
            if(movement.x > 0)
            {
                if (movement.z < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 135, 0);
                    i = 135;
                    StartCoroutine(SE());
                }
                else if (movement.z == 0)
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    i = 90;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 45, 0);
                    i = 45;
                    StartCoroutine(NE());
                }
            }
        }

        transform.rotation = Quaternion.Euler(0, i, 0);

        float height = 2f * cam.orthographicSize + 13f;
        float width = cam.orthographicSize * cam.aspect;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -width + cam.transform.position.x, width + cam.transform.position.x), transform.position.y, Mathf.Clamp(transform.position.z, -height + cam.transform.position.z + 40f, height + cam.transform.position.z - 3f));
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    IEnumerator SW()
    {
        yield return new WaitForSeconds(0.05f);
        i = 225;
    }
    IEnumerator NW()
    {
        yield return new WaitForSeconds(0.05f);
        i = 315;
    }
    IEnumerator SE()
    {
        yield return new WaitForSeconds(0.05f);
        i = 135;
    }
    IEnumerator NE()
    {
        yield return new WaitForSeconds(0.05f);
        i = 45;
    }
}
