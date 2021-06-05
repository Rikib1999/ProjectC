using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCombat : MonoBehaviour
{
    private Transform player;
    private Transform player1;
    private Transform player2;
    private Animator animator;
    private Transform zombiePosition;
    private int attackFinished = 0;
    public float damage = 25f;
    private int p;
    float nextTimeToSearch = 0f;
    bool invicible;
    bool invicible1;
    bool invicible2;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

        if (GameObject.FindGameObjectWithTag("Player 1") != null && GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            player1 = GameObject.FindGameObjectWithTag("Player 1").transform;
            player2 = GameObject.FindGameObjectWithTag("Player 2").transform;
        }
        else if (GameObject.FindGameObjectWithTag("Player 1") != null)
        {
            player1 = GameObject.FindGameObjectWithTag("Player 1").transform;
        }
        else if (GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            player2 = GameObject.FindGameObjectWithTag("Player 2").transform;
        }

        zombiePosition = GetComponent<Transform>();
    }

    void Update()
    {
        float distance = 0;
        float distance1 = 1000;
        float distance2 = 1000;
        p = 0;

        if (player1 != null && player2 != null)
        {
            distance1 = Vector3.Distance(player1.position, zombiePosition.position);
            distance2 = Vector3.Distance(player2.position, zombiePosition.position);
            FindPlayer(3);
            invicible1 = player1.GetComponent<Player_health>().invicible;
            invicible2 = player2.GetComponent<Player_health>().invicible;
        } else if (player1 != null && player2 == null)
        {
            distance1 = Vector3.Distance(player1.position, zombiePosition.position);
            FindPlayer(2);
            invicible1 = player1.GetComponent<Player_health>().invicible;
        } else if (player2 != null)
        {
            distance2 = Vector3.Distance(player2.position, zombiePosition.position);
            FindPlayer(1);
            invicible2 = player2.GetComponent<Player_health>().invicible;
        }

        if (distance1 < distance2)
        {
            player = player1;
            distance = distance1;
            p = 1;
            invicible = invicible1;
        }
        else if (distance1 > distance2)
        {
            player = player2;
            distance = distance2;
            p = 2;
            invicible = invicible2;
        }
        else
        {
            invicible = true;
        }

        if (distance < 2.4f && attackFinished == 0 && invicible == false && Vector3.Dot(player.position - zombiePosition.position, zombiePosition.forward) > 0.6f)
        {
            attackFinished = 1;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        GetComponent<NavMeshAgent>().enabled = false;

        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.6f);

        if (player != null && Vector3.Distance(player.position, zombiePosition.position) < 2.4f && Vector3.Dot(player.position - zombiePosition.position, zombiePosition.forward) > 0.6f)
        {
            if(p == 1)
            {
                player = GameObject.FindGameObjectWithTag("Player 1").transform;
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player 2").transform;
            }
            Player_health player_Health = player.GetComponent<Player_health>();
            player_Health.TakeDamage(damage, false);
            player.GetComponent<Rigidbody>().AddForce(400*(player.position - zombiePosition.position).normalized);
        }

        yield return new WaitForSeconds(0.6f);

        if (GetComponent<Enemy>().dead == 0)
        {
            GetComponent<NavMeshAgent>().enabled = true;
            attackFinished = 0;
        }
    }

    void FindPlayer(int pp)
    {
        if (pp == 2)
        {
            if (nextTimeToSearch <= Time.time)
            {
                GameObject searchResult1 = GameObject.FindGameObjectWithTag("Player 1");
                if (searchResult1 != null)
                {
                    player1 = searchResult1.transform;
                }
                GameObject searchResult2 = GameObject.FindGameObjectWithTag("Player 2");
                if (searchResult2 != null)
                {
                    player2 = searchResult2.transform;
                }
                nextTimeToSearch = Time.time + 0.5f;
            }
        }
        else if (pp == 1)
        {
            if (nextTimeToSearch <= Time.time)
            {
                GameObject searchResult1 = GameObject.FindGameObjectWithTag("Player 1");
                if (searchResult1 != null)
                {
                    player1 = searchResult1.transform;
                }
                GameObject searchResult2 = GameObject.FindGameObjectWithTag("Player 2");
                if (searchResult2 != null)
                {
                    player2 = searchResult2.transform;
                }
                nextTimeToSearch = Time.time + 0.5f;
            }
        }
        else
        {
            if (nextTimeToSearch <= Time.time)
            {
                GameObject searchResult1 = GameObject.FindGameObjectWithTag("Player 1");
                if (searchResult1 != null)
                {
                    player1 = searchResult1.transform;
                }
                GameObject searchResult2 = GameObject.FindGameObjectWithTag("Player 2");
                if (searchResult2 != null)
                {
                    player2 = searchResult2.transform;
                }
                nextTimeToSearch = Time.time + 0.5f;
            }
        }
    }
}
