using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public bool[] deadPlayers;
    public GameObject menuCanvas;
    public GameObject notificationPrefab;

    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }

        deadPlayers = new bool[3];
        Cursor.visible = false;
        //DataTransfer.singlePlayer = true;
        if (DataTransfer.singlePlayer) {
            SpawnPlayer(1);
            deadPlayers[1] = false;
            deadPlayers[2] = true;
        } else
        {
            SpawnPlayer(1);
            SpawnPlayer(2);
            deadPlayers[1] = false;
            deadPlayers[2] = false;
        }

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MultipleTargetsCamera>().enabled = true;
    }

    public Transform playerPrefab1;
    public Transform playerPrefab2;
    private string playerName = "";
    public Transform spawnPoint;
    public float spawnDelay = 3f;

    public void SpawnPlayer(int p)
    {
        if (p == 1)
        {
            Instantiate(playerPrefab1, spawnPoint.position, spawnPoint.rotation);
            GetWeapon(1, p, true);
        }
        else
        {
            Instantiate(playerPrefab2, spawnPoint.position, spawnPoint.rotation);
            GetWeapon(1, p, true);
        }
    }

    public IEnumerator RespawnPlayer(int p)
    {
        yield return new WaitForSeconds(spawnDelay);

        gm.StartCoroutine(gm.GameOver(p, false));

        if (p == 1)
        {
            spawnPoint = GameObject.FindGameObjectWithTag("Player 2").transform;
            Instantiate(playerPrefab1, spawnPoint.position, spawnPoint.rotation);
            GetWeapon(1, p, true);
        }
        else
        {
            spawnPoint = GameObject.FindGameObjectWithTag("Player 1").transform;
            Instantiate(playerPrefab2, spawnPoint.position, spawnPoint.rotation);
            GetWeapon(1, p, true);
        }
    }

    public static void KillPlayer (Player_health player, int p)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer(p));
    }

    public static void PlayerIsDead(int p)
    {
        gm.StartCoroutine(gm.GameOver(p, true));
    }

    public IEnumerator GameOver (int p, bool dead)
    {
        deadPlayers[p] = dead;

        if (deadPlayers[1]==true && deadPlayers[2] == true)
        {
            StopCoroutine(RespawnPlayer(1));
            StopCoroutine(RespawnPlayer(2));
            yield return new WaitForSeconds(1f);
            menuCanvas.GetComponent<PauseMenu>().GameOverMenu();
            Cursor.visible = true;
        }
    }

    public void GetWeapon(int x, int p, bool respawn)
    {
        if (respawn == true) { return; }
        GameObject weapon = null;
        if(p == 1)
        {
            weapon = GameObject.FindGameObjectWithTag("Weapon Holder1");
            if (weapon != null)
            {
                playerName = GameObject.FindGameObjectWithTag("Player 1").name;
            }
        }
        else
        {
            weapon = GameObject.FindGameObjectWithTag("Weapon Holder2");
            if (weapon != null)
            {
                playerName = GameObject.FindGameObjectWithTag("Player 2").name;
            }
        }
        if (weapon != null)
        {
            if (weapon.transform.GetChild(x - 1).GetComponent<IsLocked>().locked == true)
            {
                weapon.transform.GetChild(x - 1).GetComponent<IsLocked>().locked = false;
            }
            else
            {

                GameObject notification = Instantiate(notificationPrefab);
                notification.GetComponent<Notification>().message = playerName + " picked up: " + WeaponName(x - 1);
                notification.transform.SetParent(GameObject.FindGameObjectWithTag("Notifications").transform, false);
            }
            SetAmmo(x - 1, weapon.transform);
        }
    }

    void SetAmmo(int i, Transform weapon)
    {
        switch (i)
        {
            case 0:
                return;
            case 1:
                weapon.GetChild(i).GetComponent<Uzi>().currentAmmo = weapon.GetChild(i).GetComponent<Uzi>().maxAmmo;
                break;
            case 2:
                weapon.GetChild(i).GetComponent<Shotgun>().currentAmmo = weapon.GetChild(i).GetComponent<Shotgun>().maxAmmo;
                break;
            case 3:
                return;
            case 4:
                return;
            case 5:
                return;
            case 6:
                return;
            case 7:
                return;
            case 8:
                return;
            case 9:
                return;
        }
        return;
    }

    string WeaponName(int i)
    {
        switch (i)
        {
            case 0:
                return "pistol ammo";
            case 1:
                return "UZI ammo";
            case 2:
                return "shotgun ammo";
            case 3:
                return "grenades";
            case 4:
                return "flamethrower fuel";
            case 5:
                return "barricades";
            case 6:
                return "missiles";
            case 7:
                return "c";
            case 8:
                return "x";
            case 9:
                return "a";
        }
        return "nothing";
    }
}
