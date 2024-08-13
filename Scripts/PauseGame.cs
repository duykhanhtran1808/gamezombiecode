using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] Canvas pauseMenuCanvas;
    FPSController playerController;
    //WeaponSwitcher weaponSwitcher;
    Weapon weapon;
    bool gamePaused = false;
    bool disableUnpause = false;

    private void Start()
    {
        playerController = FindObjectOfType<FPSController>();
        weapon = FindObjectOfType<Weapon>();
        //weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
        pauseMenuCanvas.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) && !disableUnpause)
        {
            if(!gamePaused)
            {
                ProcessPause();
            }
            else
            {
                UnPause();
            }
        }
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseMenuCanvas.enabled = false;
        if (FindObjectOfType<WeaponSwitcher>() != null)
        {
            FindObjectOfType<WeaponSwitcher>().enabled = true;
        }
        if (FindObjectOfType<WeaponZoom>() != null)
        {
            FindObjectOfType<WeaponZoom>().enabled = true;
        }
        if (FindObjectOfType<Weapon>() != null)
        {
            FindObjectOfType<Weapon>().enabled = true;
        }
        gamePaused = false;
        playerController.LockCursor();
        weapon.UnlockShoot();
    }

    public void ProcessPause()
    {
        Time.timeScale = 0;
        pauseMenuCanvas.enabled = true;
        if (FindObjectOfType<WeaponSwitcher>() != null)
        {
            FindObjectOfType<WeaponSwitcher>().enabled = false;
        }
        if (FindObjectOfType<WeaponZoom>() != null)
        {
            FindObjectOfType<WeaponZoom>().enabled = false;
        }
        if (FindObjectOfType<Weapon>() != null)
        {
            FindObjectOfType<Weapon>().enabled = false;
        }
        gamePaused = true;
        playerController.UnlockCursor();
        weapon.LockShoot();
    }

    public void EndGame()
    {
        ProcessPause();
        disableUnpause = true;
    }
}
