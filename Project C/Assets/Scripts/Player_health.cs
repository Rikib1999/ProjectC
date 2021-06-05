using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_health : MonoBehaviour
{
    public string newName = "";
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    private int timer = 0;
    public Material material;
    private Material mat;
    public Health_bar healthBar;
    public Animator animator;
    public GameObject others;
    public int p;
    public bool invicible;
    public int dead = 0;

    public GameObject text;
    public float scorePoints;

    public GameObject[] bloodPrefab;
    private GameObject blood;
    private Transform floor;

    public void Start()
    {
        invicible = true;

        if(gameObject == GameObject.FindGameObjectWithTag("Player 1"))
        {
            p = 1;
            newName = "Player 1";
            Physics.IgnoreLayerCollision(8, 9, true);
        }
        else
        {
            p = 2;
            newName = "Player 2";
            Physics.IgnoreLayerCollision(11, 9, true);
        }

        Physics.IgnoreLayerCollision(8, 11, true);
        StartCoroutine(Invicible(p));

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        text = GameObject.Find("Score");
    }

    private void Update()
    {
        timer++;
        if (timer > 100)
        {
            if (currentHealth < (maxHealth * 0.75) && currentHealth > 0)
            {
                currentHealth++;
                healthBar.SetHealth(currentHealth);
            }
            timer = 0;
        }
    }

    /*public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        BloodSplash();

        if (currentHealth <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Die());
        }
        else
        {
            StartCoroutine(CantFire());
        }
    }*/

    public void TakeDamage(float damage, bool onFire)
    {
        if (dead != 1)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

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
                StartCoroutine(CantFire());
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

    IEnumerator CantFire()
    {
        GetComponent<PlayerMove>().enabled = false;
        gameObject.transform.Find("Others").Find("WeaponHolder").gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        GetComponent<PlayerMove>().enabled = true;
        gameObject.transform.Find("Others").Find("WeaponHolder").gameObject.SetActive(true);
    }

    public void CantFirePause()
    {
        if (currentHealth > 0)
        {
            GetComponent<PlayerMove>().enabled = false;
            gameObject.transform.Find("Others").Find("WeaponHolder").gameObject.SetActive(false);
        }
    }

    public void CanFirePause()
    {
        if (currentHealth > 0)
        {
            GetComponent<PlayerMove>().enabled = true;
            gameObject.transform.Find("Others").Find("WeaponHolder").gameObject.SetActive(true);
        }
    }

    IEnumerator Die()
    {
        GameMaster.PlayerIsDead(p);

        Score score = text.GetComponent<Score>();
        score.AddScore(scorePoints);

        GetComponent<PlayerMove>().enabled = false;
        GetComponent<Rigidbody>().drag = 3f;
        GetComponent<Rigidbody>().angularDrag = 3f;
        GetComponent<Rigidbody>().mass = 50f;

        gameObject.layer = 13;
        int numOfChildren2 = gameObject.transform.childCount;
        for (int i = 0; i < numOfChildren2; i++)
        {
            gameObject.transform.GetChild(i).gameObject.layer = 13;
        }

        animator.SetFloat("Death", 1);
        Destroy(others);
        yield return new WaitForSeconds(5f);

        mat = Instantiate(material);
        int numOfChildren = transform.childCount;
        for (int i = 0; i < numOfChildren; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material = mat;
        }

        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = mat.GetColor("_BaseColor");
            c.a = f;
            mat.SetColor("_BaseColor", c)
;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);
        GameMaster.KillPlayer(this, p);
    }

    IEnumerator Invicible(int p)
    {
        yield return new WaitForSeconds(3f);
        if (p == 1)
        {
            Physics.IgnoreLayerCollision(8, 9, false);
        }
        else
        {
            Physics.IgnoreLayerCollision(11, 9, false);
        }
        Physics.IgnoreLayerCollision(8, 11, false);
        invicible = false;
    }
}