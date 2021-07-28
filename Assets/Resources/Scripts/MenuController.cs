using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuController : MonoBehaviour
{
    public UnityEvent OnButtonAPress;
    public UnityEvent OnButtonBPress;

    public bool ButtonAActive;
    public bool ButtonBActive;

    public KeyCode DebugButton1;
    public KeyCode DebugButton2;

    // Update is called once per frame
    void Update()
    {
        if (DebugButton1 != KeyCode.None)
        {
            if (Input.GetKeyDown(DebugButton1))
            {
                OnButtonAPress.Invoke();
            }
        }

        if (DebugButton2 != KeyCode.None)
        {
            if (Input.GetKeyDown(DebugButton2))
            {
                OnButtonBPress.Invoke();
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.One) && ButtonAActive)
            OnButtonAPress.Invoke();

        if (OVRInput.GetDown(OVRInput.Button.Two) && ButtonBActive)
            OnButtonBPress.Invoke();
    }
}
