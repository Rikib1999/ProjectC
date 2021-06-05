using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float currentScore;
    private bool[] firstTime;
    private GameObject gameMaster;
    public GameObject notificationPrefab;

    private void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM");
        currentScore = 0;
        scoreText.text = "SCORE: " + currentScore.ToString("0");
        firstTime = new bool[9];
        for (int i = 0; i < 9; i++)
        {
            firstTime[i] = true;
        }
    }

    public void AddScore(float scorePoints)
    {
        currentScore = currentScore + scorePoints;
        scoreText.text = "SCORE: " + currentScore.ToString("0");

        if (currentScore > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", currentScore);
        }

        if (currentScore >= 100000f)//70000
        {
            UnlockWeapon(10, "Pistol 10 UNLOCKED!");
        }
        else if (currentScore >= 90000f)//60000
        {
            UnlockWeapon(9, "Pistol 9 UNLOCKED!");
        }
        else if (currentScore >= 80000f)//50000
        {
            UnlockWeapon(8, "Pistol 8 UNLOCKED!");
        }
        else if (currentScore >= 70000f)//700
        {
            UnlockWeapon(7, "ROCKET LAUNCHER UNLOCKED!");
        }
        else if (currentScore >= 60000f)//600f
        {
            UnlockWeapon(6, "BARRICADES UNLOCKED!");
        }
        else if (currentScore >= 50000f)//500f
        {
            UnlockWeapon(5, "FLAMETHROWER UNLOCKED!");
        }
        else if (currentScore >= 40000f)//400f
        {
            UnlockWeapon(4, "GRENADES UNLOCKED!");
        }
        else if (currentScore >= 1500f)//6000
        {
            UnlockWeapon(3, "SHOTGUN UNLOCKED!");
        }
        else if (currentScore >= 500f)//2000
        {
            UnlockWeapon(2, "UZI UNLOCKED!");
        }
    }

    private void UnlockWeapon(int n, string text)
    {
        if (firstTime[n - 2] == true)
        {
            gameMaster.GetComponent<GameMaster>().GetWeapon(n, 1, false);
            if (!DataTransfer.singlePlayer)
            {
                gameMaster.GetComponent<GameMaster>().GetWeapon(n, 2, false);
            }
            GameObject notification = Instantiate(notificationPrefab);
            notification.GetComponent<Notification>().message = text;
            notification.transform.SetParent(GameObject.FindGameObjectWithTag("Notifications").transform, false);
            firstTime[n - 2] = false;
        }
    }
}