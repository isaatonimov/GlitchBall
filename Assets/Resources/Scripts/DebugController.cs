using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebugController : MonoBehaviour
{

    public KeyCode DebugKeyOne;
    public KeyCode DebugKeyTwo;

    public UnityEvent DebugKeyOnePressed;
    public UnityEvent DebugKeyTwoPressed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(DebugKeyOne))
            DebugKeyOnePressed.Invoke();

        if (Input.GetKeyDown(DebugKeyTwo))
            DebugKeyTwoPressed.Invoke();
    }
}
