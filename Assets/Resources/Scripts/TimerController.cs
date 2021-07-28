using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    private int counter = 3;
    private TextMeshProUGUI counterText;
    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<TextMeshProUGUI>();
    }

    public void CounterTick()
    {
        if (counter == 0)
        {
            counterText.SetText("GO");
            counter = 3;
        }
        else
        {
            counterText.SetText(counter.ToString());
            counter--;
        }
    }
}
