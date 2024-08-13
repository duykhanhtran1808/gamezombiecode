using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneUIControl : MonoBehaviour
{
    [SerializeField] Canvas phoneMenu;
    PauseGame pauseMenu;
    bool openPhone;
    // Start is called before the first frame update
    void Start()
    {
        openPhone = true;
        phoneMenu.enabled = true;
        pauseMenu = FindObjectOfType<PauseGame>();
        pauseMenu.ProcessPause();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            openPhone = !openPhone;
            if (openPhone)
            {
                pauseMenu.ProcessPause();
                phoneMenu.enabled = true;
            }
            else
            {
                pauseMenu.UnPause();
                phoneMenu.enabled = false;
            }
        }

        
    }
}
