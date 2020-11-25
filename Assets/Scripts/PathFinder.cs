using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private GameObject path;

    private Transform[] waypoints;
    private int currentWaypointIndex;
    private int waypointsLength;
    private float moveSpeed;
    
    // Start is called before the first frame update
    private void Start()
    {
        currentWaypointIndex = 1;
        moveSpeed = gameObject.GetComponent<Enemy>().GetMoveSpeed();
        
        waypoints = path.GetComponentsInChildren<Transform>();
        waypointsLength = waypoints.Length;
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentWaypointIndex >= waypointsLength) return;
        
        GoToNextWaypoint();
    }

    private void GoToNextWaypoint()
    {
        var currentWaypoint = waypoints[currentWaypointIndex];
        
        var step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, step);
        
        if (Math.Abs(transform.position.x - currentWaypoint.position.x) < 0.1f &&
            Math.Abs(transform.position.z - currentWaypoint.position.z) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }
}
