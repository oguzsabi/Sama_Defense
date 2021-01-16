using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathFinder : MonoBehaviour
{
    private Transform[] _waypoints;
    private int _currentWaypointIndex;
    private int _waypointsLength;
    private float _moveSpeed;
    private GameObject _selectedPath;
    
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (_currentWaypointIndex >= _waypointsLength || !_selectedPath) return;
        
        CheckMoveSpeed();
        GoToNextWaypoint();
    }
    
    /// <summary>
    /// Directs the enemy unit to next waypoint in _waypoints array
    /// </summary>
    private void GoToNextWaypoint()
    {
        var currentWaypoint = _waypoints[_currentWaypointIndex];
        
        var step = _moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, step);
        
        if (Math.Abs(transform.position.x - currentWaypoint.position.x) < 0.1f &&
            Math.Abs(transform.position.z - currentWaypoint.position.z) < 0.1f)
        {
            _currentWaypointIndex++;
        }
    }
    
    /// <summary>
    /// The path that enemy unit will follow is being assigned
    /// </summary>
    /// <param name="enemyPaths"></param>
    public void SetEnemyPaths(GameObject[] enemyPaths)
    {
        _selectedPath = enemyPaths[Random.Range(0, enemyPaths.Length)];
        _currentWaypointIndex = 1;
        _moveSpeed = gameObject.GetComponent<Enemy>().GetMoveSpeed();
        
        _waypoints = _selectedPath.GetComponentsInChildren<Transform>();
        _waypointsLength = _waypoints.Length;
    }
    
    /// <summary>
    /// Checks movement speed for any slow or stun effect
    /// </summary>
    private void CheckMoveSpeed()
    {
        _moveSpeed = gameObject.GetComponent<Enemy>().GetMoveSpeed();
    }
}
