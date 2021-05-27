using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float scorePoints = 100;
    public int dropChance = 8;
    public int dead = 0;

    public GameObject zombieBody;
    public GameObject supplyCratePrefab;
    private Transform position1;
    private Transform position2;
    private float nextTimeToSearch = 0;

    public Material material;
    private Material mat;
    private GameObject text;
    public GameObject[] bloodPrefab;
    private GameObject blood;
    private Transform floor;

    public void Start()
    {
        transform.position = new Vector3(transform.position.x, 1.392f, transform.position.z);

        if (GameObject.FindGameObjectWithTag("Player 1") != null)
        {
            position1 = GameObject.FindGameObjectWithTag("Player 1").transform;
        }

        if (GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            position2 = GameObject.FindGameObjectWithTag("Player 2").transform;
        }
        GetComponent<NavMeshAgent>().SetDestination(position1.position);
        text = GameObject.Find("Score");
    }

    public void Update()
    {
        if (position1 != null && position2 != null)
        {
            FindPlayer(3);
            float distance1 = Vector3.Distance(position1.position, transform.position);
            float distance2 = Vector3.Distance(position2.position, transform.position);

            if (distance1 <= distance2)
            {
                GetComponent<NavMeshAgent>().SetDestination(position1.position);
            }
            else
            {
                GetComponent<NavMeshAgent>().SetDestination(position2.position);
            }
        }
        else if (position1 != null)
        {
            FindPlayer(2);
            GetComponent<NavMeshAgent>().SetDestination(position1.position);
        }
        else if (position2 != null)
        {
            FindPlayer(1);
            GetComponent<NavMeshAgent>().SetDestination(position2.position);
        }
        else
        {
            FindPlayer(3);
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
    }

    public void TakeDamage (float damage, bool onFire)
    {
        if (dead != 1)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                GetComponent<CapsuleCollider>().enabled = false;
                BloodSplash();
                dead = 1;
                StopAllCoroutines();
                StartCoroutine(Die());
            }
            else if (!onFire)
            {
                StartCoroutine(CantAttack());
                BloodSplash();
            }
            onFire = false;
        }
    }

    private void BloodSplash()
    {
        floor = GameObject.FindGameObjectWithTag("Ground").transform;
        int b = UnityEngine.Random.Range(0, 4);
        blood = Instantiate(bloodPrefab[b], new Vector3(transform.position.x, floor.position.y + 0.01f, transform.position.z), Quaternion.Euler(90f, 0f, UnityEngine.Random.Range(0, 360)));
        float r = UnityEngine.Random.Range(1.8f, 3f);
        blood.transform.localScale = new Vector3(r, r, 1f);
    }

    IEnumerator CantAttack()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        //GetComponent<ZombieCombat>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<NavMeshAgent>().enabled = true;
        //GetComponent<ZombieCombat>().enabled = true;
    }

    IEnumerator Die()
    {
        //Score score = text.GetComponent<Score>();
        //score.AddScore(scorePoints);

        int r = UnityEngine.Random.Range(1, dropChance + 1);
        if (r == 1)
        {
            GameObject supplyCrate = Instantiate(supplyCratePrefab, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.Euler(0f, 0f, 0f));
        }

        GetComponent<Rigidbody>().drag = 3f;
        GetComponent<NavMeshAgent>().enabled = false;
        //GetComponent<ZombieCombat>().enabled = false;
        gameObject.layer = 13;
        zombieBody.layer = 13;
        int numOfChildren = zombieBody.transform.childCount;
        for (int i = 0; i < numOfChildren; i++)
        {
            zombieBody.transform.GetChild(i).gameObject.layer = 13;
        }

        //GetComponent<Rigidbody>().detectCollisions = false;

        Animator animator = GetComponentInChildren<Animator>();
        animator.SetFloat("Death", 1);
        yield return new WaitForSeconds(5f);

        mat = Instantiate(material);
        for (int i = 0; i < numOfChildren; i++)
        {
            GameObject child = zombieBody.transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material = mat;
        }

        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = mat.GetColor("_BaseColor");
            c.a = f;
            mat.SetColor("_BaseColor", c);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void FindPlayer(int pp)
    {
        if (pp == 2)
        {
            if (nextTimeToSearch <= Time.time)
            {
                GameObject searchResult = GameObject.FindGameObjectWithTag("Player 2");
                if (searchResult != null)
                {
                    position2 = searchResult.transform;
                }
                nextTimeToSearch = Time.time + 0.5f;
            }
        }
        else if (pp == 1)
        {
            if (nextTimeToSearch <= Time.time)
            {
                GameObject searchResult = GameObject.FindGameObjectWithTag("Player 1");
                if (searchResult != null)
                {
                    position1 = searchResult.transform;
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
                    position1 = searchResult1.transform;
                }
                GameObject searchResult2 = GameObject.FindGameObjectWithTag("Player 2");
                if (searchResult2 != null)
                {
                    position2 = searchResult2.transform;
                }
                nextTimeToSearch = Time.time + 0.5f;
            }
        }
    }
}