using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Transform firePoint;
    public Transform ammoExit;
    public float damage = 25f;
    public GameObject shotEffect;
    public LineRenderer lineRenderer;
    public float range = 50f;
    public float impactForce = 50f;
    public KeyCode shoot;
    public GameObject[] impactEffect;
    public GameObject bulletPrefab;
    public GameObject shotSound;

    void Update()
    {
        if (Input.GetKeyDown(shoot))
        {
            StartCoroutine(Shoot());
        }
    }

    public void OnDisable()
    {
        shotEffect.SetActive(false);
        lineRenderer.enabled = false;
    }

    RaycastHit hit;
    IEnumerator Shoot()
    {
        Instantiate(shotSound, this.transform.position, this.transform.rotation);

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            Player_health ph = hit.transform.GetComponent<Player_health>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage, false);
                Instantiate(impactEffect[0], hit.point, hit.transform.rotation);
            }

            if (ph != null)
            {
                ph.TakeDamage(damage, false);
                Instantiate(impactEffect[0], hit.point, hit.transform.rotation);
            }

            if (hit.transform.gameObject.layer == 13)
            {
                Instantiate(impactEffect[0], hit.point, hit.transform.rotation);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            else if (hit.transform.gameObject.layer != 12)
            {
                Instantiate(impactEffect[1], hit.point, hit.transform.rotation);
            }

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hit.point);
            print(hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * range);
        }

        lineRenderer.enabled = true;
        shotEffect.SetActive(true);

        float r = UnityEngine.Random.Range(-3f, 3f);
        float s = UnityEngine.Random.Range(-3f, 3f);
        GameObject bullet = Instantiate(bulletPrefab, ammoExit.position, Quaternion.Euler(r*30, s*30, s*30));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(((ammoExit.up * (-2)) + (ammoExit.right * 1.5f) + (ammoExit.forward * (-1.5f)) + new Vector3(r*0.1f, 0f, s*0.1f)) * 1f, ForceMode.VelocityChange);

        yield return new WaitForSeconds(0.02f);
        shotEffect.SetActive(false);
        lineRenderer.enabled = false;
    }
}
