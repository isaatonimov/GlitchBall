using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using UnityEngine.Events;

public class PauseController : MonoBehaviour
{
    public KeyCode DebugButtonOne;

    public SoundFXRef PauseSound;
    public UnityEvent PauseGamePress;

    public void PauseGame()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (DebugButtonOne != KeyCode.None)
        {
            if (Input.GetKeyDown(DebugButtonOne))
            {
                PauseGamePress.Invoke();
                PauseSound.PlaySound();
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            PauseGamePress.Invoke();
            PauseSound.PlaySound();
        }

    }
}
