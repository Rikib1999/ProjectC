using UnityEngine;
using System.Collections;

public class ParticleDestroyer : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating("DestroyParticles", 15f, 15f);
    }

    public void DestroyParticles()
    {
        GameObject[] particles = GameObject.FindGameObjectsWithTag("particles");

        foreach (GameObject particle in particles)
        {
            if (!particle.GetComponent<ParticleSystem>().isPlaying)
            {
                Destroy(particle);
            }
        }
    }
}