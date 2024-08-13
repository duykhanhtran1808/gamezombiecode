using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    PauseGame pauseGame;

    private void Start()
    {
        pauseGame = FindObjectOfType<PauseGame>();
        gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    { 
        gameOverCanvas.enabled = true;
        Time.timeScale = 0f;
        
        if(FindObjectOfType<WeaponSwitcher>() != null)
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
        pauseGame.EndGame();
    }
}
