using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public KeyCode next;
    public KeyCode previous;
    public int ammo;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(next))
        {
            WhatWeapon("next", selectedWeapon);
        }
        if (Input.GetKeyDown(previous))
        {
            WhatWeapon("previous", selectedWeapon);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && transform.GetChild(1).GetComponent<IsLocked>().locked == false)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3 && transform.GetChild(2).GetComponent<IsLocked>().locked == false)
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4 && transform.GetChild(3).GetComponent<IsLocked>().locked == false)
        {
            selectedWeapon = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5 && transform.GetChild(4).GetComponent<IsLocked>().locked == false)
        {
            selectedWeapon = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && transform.childCount >= 6 && transform.GetChild(5).GetComponent<IsLocked>().locked == false)
        {
            selectedWeapon = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && transform.childCount >= 7 && transform.GetChild(6).GetComponent<IsLocked>().locked == false)
        {
            selectedWeapon = 6;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

        ammo = GetAmmo(selectedWeapon, transform.GetChild(selectedWeapon));
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    void WhatWeapon(string dir, int sw)
    {
        if(dir == "next")
        {
            if(sw == transform.childCount - 1)
            {
                if(transform.GetChild(0).GetComponent<IsLocked>().locked == false)
                {
                    selectedWeapon = 0;
                }
                else
                {
                    WhatWeapon("next", 0);
                }
            }
            else
            {
                if (transform.GetChild(sw + 1).GetComponent<IsLocked>().locked == false)
                {
                    selectedWeapon = sw + 1;
                }
                else
                {
                    WhatWeapon("next", sw + 1);
                }
            }
        }
        else
        {
            if (sw == 0)
            {
                if (transform.GetChild(transform.childCount - 1).GetComponent<IsLocked>().locked == false)
                {
                    selectedWeapon = transform.childCount - 1;
                }
                else
                {
                    WhatWeapon("previous", transform.childCount - 1);
                }
            }
            else
            {
                if (transform.GetChild(sw - 1).GetComponent<IsLocked>().locked == false)
                {
                    selectedWeapon = sw - 1;
                }
                else
                {
                    WhatWeapon("previous", sw - 1);
                }
            }
        }
    }

    int GetAmmo(int i, Transform weapon)
    {
        switch (i)
        {
            case 0:
                return -1;
            case 1:
                return -1; //return weapon.GetComponent<Uzi>().currentAmmo;
            case 2:
                return -1;
            case 3:
                return -1;
            case 4:
                return -1;
            case 5:
                return -1;
            case 6:
                return -1;
            case 7:
                return -1;
            case 8:
                return -1;
            case 9:
                return -1;
        }
        return -1;
    }
}
