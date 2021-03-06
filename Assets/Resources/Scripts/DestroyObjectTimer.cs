using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectTimer : MonoBehaviour
{
    public float TimeTillDestruction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimedDestroy(TimeTillDestruction));
    }

    IEnumerator TimedDestroy(float destroyAfter)
    {
        yield return new WaitForSeconds(destroyAfter);
        GameObject.Destroy(this.gameObject);
    }
}
