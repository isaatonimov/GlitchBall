using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTargetController : MonoBehaviour
{
    public List<Transform> Waypoints;
    public float MovementSpeed = 2f;

    private Transform currentWaypointTarget;
    // Start is called before the first frame update
    void Start()
    {
        if (Waypoints == null)
            Waypoints = new List<Transform>();

        currentWaypointTarget = Waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        float step = MovementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentWaypointTarget.transform.position, step);

        //when waypoint is reached, go to next waypoint
        if (Vector3.Distance(transform.position, currentWaypointTarget.transform.position) < 0.001f)
        {
            int currentIndex = Waypoints.IndexOf(currentWaypointTarget);
            currentWaypointTarget = Waypoints[Random.Range(0, Waypoints.Count)];
        }
    }
}
