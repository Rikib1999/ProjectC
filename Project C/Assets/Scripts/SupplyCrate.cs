using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyCrate : MonoBehaviour
{
    private GameObject gameMaster;
    public GameObject notificationPrefab;
    private float score;
    public float delay = 15f;
    private float countdown;
    private int max;
    private string playerName = "";
    private bool taken = false;

    void Start()
    {
        countdown = delay;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && taken == false)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player 1"))
        {
            Pickup(other, 1);
        }
        if (other.CompareTag("Player 2"))
        {
            Pickup(other, 2);
        }
    }

    private void Pickup(Collider player, int p)
    {
        //TODO:
        //sound
        //effect

        taken = true;
        playerName = player.GetComponent<Player_health>().newName;
        score = GameObject.Find("Score").GetComponent<Score>().currentScore;

        if (score >= 70000f)
        {
            max = 10;
        }
        else if (score >= 60000f)
        {
            max = 9;
        }
        else if (score >= 50000f)
        {
            max = 8;
        }
        else if (score >= 40000f)
        {
            max = 7;
        }
        else if (score >= 30000f)
        {
            max = 6;
        }
        else if (score >= 25000f)
        {
            max = 5;
        }
        else if (score >= 17500f)
        {
            max = 4;
        }
        else if (score >= 6000f)
        {
            max = 3;
        }
        else if (score >= 2000f)
        {
            max = 2;
        }
        else if (score < 2000f)
        {
            max = 1;
        }

        if (player.GetComponent<Player_health>().currentHealth < 50f)
        {
            player.GetComponent<Player_health>().currentHealth = player.GetComponent<Player_health>().maxHealth;
            player.GetComponent<Player_health>().healthBar.SetHealth(player.GetComponent<Player_health>().currentHealth);
            GameObject notification = Instantiate(notificationPrefab);
            notification.GetComponent<Notification>().message = playerName + " picked up: health";
            notification.transform.SetParent(GameObject.FindGameObjectWithTag("Notifications").transform, false);
        }
        else if (player.GetComponent<Player_health>().currentHealth < 100f)
        {
            if (UnityEngine.Random.Range(0, 2) == 0 || max == 1)
            {
                player.GetComponent<Player_health>().currentHealth = player.GetComponent<Player_health>().maxHealth;
                player.GetComponent<Player_health>().healthBar.SetHealth(player.GetComponent<Player_health>().currentHealth);
                GameObject notification = Instantiate(notificationPrefab);
                notification.GetComponent<Notification>().message = playerName + " picked up: health";
                notification.transform.SetParent(GameObject.FindGameObjectWithTag("Notifications").transform, false);
            }
            else
            {
                gameMaster = GameObject.FindGameObjectWithTag("GM");
                gameMaster.GetComponent<GameMaster>().GetWeapon(UnityEngine.Random.Range(2, max + 1), p, false);
            }
        }
        else
        {
            if (max != 1)
            {
                gameMaster = GameObject.FindGameObjectWithTag("GM");
                gameMaster.GetComponent<GameMaster>().GetWeapon(UnityEngine.Random.Range(2, max + 1), p, false);
            }
            else
            {
                GameObject notification = Instantiate(notificationPrefab);
                notification.GetComponent<Notification>().message = playerName + " picked up: pistol ammo";
                notification.transform.SetParent(GameObject.FindGameObjectWithTag("Notifications").transform, false);
            }
        }

        Destroy(gameObject);
    }
}
