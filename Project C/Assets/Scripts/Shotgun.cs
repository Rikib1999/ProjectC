using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Transform firePoint;
    public Transform rayPoint;
    public Transform ammoExit;
    public float damage = 45f;
    public GameObject shotEffect;
    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;
    public LineRenderer lineRenderer3;
    public LineRenderer lineRenderer4;
    public LineRenderer lineRenderer5;
    public int maxAmmo = 50;
    public int currentAmmo;
    public float range = 10f;
    public float impactForce = 100f;
    public KeyCode shoot;
    public GameObject[] impactEffect;
    public GameObject bulletPrefab;
    public GameObject shotSound;

    private void Start()
    {
        Reload();
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }

    public void OnDisable()
    {
        shotEffect.SetActive(false);
        lineRenderer1.enabled = false;
        lineRenderer2.enabled = false;
        lineRenderer3.enabled = false;
        lineRenderer4.enabled = false;
        lineRenderer5.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(shoot) && currentAmmo > 0)
        {
            StartCoroutine(Shoot());
        }
    }

    RaycastHit hit;
    IEnumerator Shoot()
    {
        currentAmmo--;

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

            lineRenderer1.SetPosition(0, rayPoint.position);
            lineRenderer1.SetPosition(1, hit.point);
            print(hit.point);
        }
        else
        {
            lineRenderer1.SetPosition(0, rayPoint.position);
            lineRenderer1.SetPosition(1, rayPoint.position + rayPoint.forward * range);
        }

        if (Physics.Raycast(firePoint.position, Quaternion.AngleAxis(-10, new Vector3(0, 1, 0)) * firePoint.forward, out hit, range))
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

            lineRenderer2.SetPosition(0, rayPoint.position);
            lineRenderer2.SetPosition(1, hit.point);
            print(hit.point);
        }
        else
        {
            lineRenderer2.SetPosition(0, rayPoint.position);
            lineRenderer2.SetPosition(1, rayPoint.position + Quaternion.AngleAxis(-10, new Vector3(0, 1, 0)) * rayPoint.forward * range);
        }

        if (Physics.Raycast(firePoint.position, Quaternion.AngleAxis(-20, new Vector3(0, 1, 0)) * firePoint.forward, out hit, range))
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

            lineRenderer3.SetPosition(0, rayPoint.position);
            lineRenderer3.SetPosition(1, hit.point);
            print(hit.point);
        }
        else
        {
            lineRenderer3.SetPosition(0, rayPoint.position);
            lineRenderer3.SetPosition(1, rayPoint.position + Quaternion.AngleAxis(-20, new Vector3(0, 1, 0)) * rayPoint.forward * range);
        }

        if (Physics.Raycast(firePoint.position, Quaternion.AngleAxis(10, new Vector3(0, 1, 0)) * firePoint.forward, out hit, range))
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

            lineRenderer4.SetPosition(0, rayPoint.position);
            lineRenderer4.SetPosition(1, hit.point);
            print(hit.point);
        }
        else
        {
            lineRenderer4.SetPosition(0, rayPoint.position);
            lineRenderer4.SetPosition(1, rayPoint.position + Quaternion.AngleAxis(10, new Vector3(0, 1, 0)) * rayPoint.forward * range);
        }

        if (Physics.Raycast(firePoint.position, Quaternion.AngleAxis(20, new Vector3(0, 1, 0)) * firePoint.forward, out hit, range))
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

            lineRenderer5.SetPosition(0, rayPoint.position);
            lineRenderer5.SetPosition(1, hit.point);
            print(hit.point);
        }
        else
        {
            lineRenderer5.SetPosition(0, rayPoint.position);
            lineRenderer5.SetPosition(1, rayPoint.position + Quaternion.AngleAxis(20, new Vector3(0, 1, 0)) * rayPoint.forward * range);
        }

        lineRenderer1.enabled = true;
        lineRenderer2.enabled = true;
        lineRenderer3.enabled = true;
        lineRenderer4.enabled = true;
        lineRenderer5.enabled = true;
        shotEffect.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        shotEffect.SetActive(false);
        lineRenderer1.enabled = false;
        lineRenderer2.enabled = false;
        lineRenderer3.enabled = false;
        lineRenderer4.enabled = false;
        lineRenderer5.enabled = false;

        float r = UnityEngine.Random.Range(-3f, 3f);
        float s = UnityEngine.Random.Range(-3f, 3f);
        GameObject bullet = Instantiate(bulletPrefab, ammoExit.position, Quaternion.Euler(r * 30, s * 30, s * 30));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(((ammoExit.up * (2)) + (ammoExit.right * 1.5f) + (ammoExit.forward * (-1.5f)) + new Vector3(r * 0.1f, 0f, s * 0.1f)) * 1f, ForceMode.VelocityChange);
    }
}
