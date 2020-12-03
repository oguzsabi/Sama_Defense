﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathFinder : MonoBehaviour
{
    //[SerializeField] private GameObject[] paths;

    private Transform[] waypoints;
    private int currentWaypointIndex;
    private int waypointsLength;
    private float moveSpeed;
    private GameObject selectedPath;

    // Start is called before the first frame update
    private void Start()
    {
        /*
        selectedPath = paths[Random.Range(0, paths.Length - 1)];
        currentWaypointIndex = 1;
        moveSpeed = gameObject.GetComponent<Enemy>().GetMoveSpeed();
        
        waypoints = selectedPath.GetComponentsInChildren<Transform>();
        waypointsLength = waypoints.Length;
        */
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentWaypointIndex >= waypointsLength || !selectedPath) return;
        
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
        selectedPath = enemyPaths[Random.Range(0, enemyPaths.Length - 1)];
        currentWaypointIndex = 1;
        moveSpeed = gameObject.GetComponent<Enemy>().GetMoveSpeed();
        
        waypoints = selectedPath.GetComponentsInChildren<Transform>();
        waypointsLength = waypoints.Length;
        
   }
}
