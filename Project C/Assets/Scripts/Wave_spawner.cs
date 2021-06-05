using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wave_spawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemies;
        public int[] counts;
        public float rate;
    }

    public Wave[] waves;
    public int waveNumber = 0;
    private int nextWave = 0;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;
    public GameObject notificationPrefab;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if(state!= SpawnState.SPAWNING)
            {
                for (int i = 0; i < waves[0].counts.Length; i++)
                {
                    waves[0].counts[i] += waves[0].counts[i] / 5;
                }
                waves[0].rate += 1;
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave+1 > waves.Length - 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        waveNumber++;
        GameObject notification = Instantiate(notificationPrefab);
        notification.GetComponent<Notification>().message = "WAVE " + waveNumber.ToString();
        notification.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 45;
        notification.transform.SetParent(GameObject.FindGameObjectWithTag("Notifications").transform, false);

        for (int i = 0; i < _wave.enemies.Length; i++)
        {
            for (int j = 0; j < _wave.counts[i]; j++)
            {
                SpawnEnemy(_wave.enemies[i]);
                yield return new WaitForSeconds(1f / _wave.rate);
            }
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
