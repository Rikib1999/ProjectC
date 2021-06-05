using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour
{
    public string message;
    public float speed = 5;
    public RectTransform t;
    public TextMeshProUGUI text;
    public float y;

    void Start()
    {

        GameObject m = GameObject.FindGameObjectWithTag("Notifications");
        Transform mt = m.transform;
        int x = mt.childCount;
        Notification[] n = new Notification[x];

        text.text = message;
        t.localPosition = new Vector3(0f, -325f, 0f);
        y = t.localPosition.y;

        for (int j = 0; j < x; j++)
        {
            n[j] = m.transform.GetChild(j).GetComponent<Notification>();
            n[j].speed *= 8;
        }

        speed = 5;
    }

    void Update()
    {
        if(speed > 100f)
        {
            speed = 100f;
        }

        if(y <= -300f)
        {
            y += Time.deltaTime * speed * 8;
            t.localPosition = new Vector3(0f, y, 0f);
        }
        else if(y > -300f && y < -270f)
        {
            y += Time.deltaTime * speed;
            t.localPosition = new Vector3(0f, y, 0f);
        }
        else
        {
            if(text.alpha <= 0f)
            {
                Destroy(this.gameObject);
            }
            text.alpha -= Time.deltaTime * 5;
            y += Time.deltaTime * speed * 5;
            t.localPosition = new Vector3(0f, y, 0f);
        }

    }
}
