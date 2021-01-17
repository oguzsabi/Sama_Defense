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
        var currentWaypointPosition = _waypoints[_currentWaypointIndex].position;

        var step = _moveSpeed * Time.deltaTime;
        var position = transform.position;
        var targetPosition = new Vector3(currentWaypointPosition.x, position.y, currentWaypointPosition.z);
        position = Vector3.MoveTowards(position, targetPosition, step);
        transform.position = position;

        if (Math.Abs(transform.position.x - targetPosition.x) < 0.5f &&
            Math.Abs(transform.position.z - targetPosition.z) < 0.5f)
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
