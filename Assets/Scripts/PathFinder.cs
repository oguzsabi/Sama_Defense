using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathFinder : MonoBehaviour
{
    private Transform[] waypoints;
    private int currentWaypointIndex;
    private int waypointsLength;
    private float moveSpeed;
    private GameObject selectedPath;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentWaypointIndex >= waypointsLength || !selectedPath) return;
        
        CheckMoveSpeed();
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

    public void SetEnemyPaths(GameObject[] enemyPaths)
    {
        selectedPath = enemyPaths[Random.Range(0, enemyPaths.Length)];
        currentWaypointIndex = 1;
        moveSpeed = gameObject.GetComponent<Enemy>().GetMoveSpeed();
        
        waypoints = selectedPath.GetComponentsInChildren<Transform>();
        waypointsLength = waypoints.Length;
    }

    public void CheckMoveSpeed()
    {
        moveSpeed = gameObject.GetComponent<Enemy>().GetMoveSpeed();
        
    }
}
