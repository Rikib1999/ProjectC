using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponAmmoText : MonoBehaviour
{
    public GameObject weaponHolder;
    public TextMeshProUGUI weaponText;
    public string[] weapon;

    void Start()
    {
        weapon = new string[10];
        weapon[0] = "Pistol";
        weapon[1] = "UZI: ";
        weapon[2] = "Shotgun: ";
        weapon[3] = "Grenades: ";
        weapon[4] = "Flamethrower: ";
        weapon[5] = "Barricades: ";
        weapon[6] = "Rocket Launcher: ";
        weaponText.text = weapon[0];
    }

    private void Update()
    {
        if (weaponHolder.GetComponent<WeaponSwitching>().selectedWeapon == 0)
        {
            weaponText.text = weapon[0];
        }
        else
        {
            weaponText.text = weapon[weaponHolder.GetComponent<WeaponSwitching>().selectedWeapon] + weaponHolder.GetComponent<WeaponSwitching>().ammo;
        }
    }
}
