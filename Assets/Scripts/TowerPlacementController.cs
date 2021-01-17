using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlacementController : MonoBehaviour
{
    [SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private KeyCode addFireTowerHotkey;
    [SerializeField] private KeyCode addWaterTowerHotkey;
    [SerializeField] private KeyCode addEarthTowerHotkey;
    [SerializeField] private KeyCode addWoodTowerHotkey;
    [SerializeField] private float rotationMultiplier = 10f;
    [SerializeField] private LayerMask mask;

    private GameObject _newTower;
    private float _mouseWheelDelta;
    private Camera _mainCamera;
    private GameSession _gameSession;
    private static List<GameObject> _currentTowers = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleAddTowerHotkey();

        if (!_newTower) return;
        
        MoveNewTowerToMousePosition();
        RotateNewTowerWithMouseWheel();
        TowerPlacer();
    }
    
    /// <summary>
    /// Prepares Tower to be placed
    /// Increments number of the towers that is placed
    /// Decrements coin amount by tower cost
    /// </summary>
    private void TowerPlacer()
    {
        if (!Input.GetMouseButtonDown(0) || !_newTower.GetComponent<Tower>().isPlaceable) return;
        
        PrepareTower();
        _gameSession.IncrementTowerCount();
        _gameSession.ChangeCoinAmountBy(-20);
    }
    
    /// <summary>
    /// Sets new tower's attributes
    /// </summary>
    private void PrepareTower()
    {
        _currentTowers.Add(_newTower);
        
        _newTower.GetComponent<Tower>().MakeTowerReady();
        _newTower.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        _newTower.GetComponentInChildren<Tower>().adjacencyDetector.SetActive(false);
        _newTower.layer = 8;
        _newTower = null;
    }
    /// <summary>
    /// Rotates the tower with mousewheel
    /// </summary>
    private void RotateNewTowerWithMouseWheel()
    {
        _mouseWheelDelta += Input.mouseScrollDelta.y;
        _newTower.transform.Rotate(Vector3.up, _mouseWheelDelta * rotationMultiplier);
    }
    
    /// <summary>
    /// Sets the towers location to mouse cursors position
    /// </summary>
    private void MoveNewTowerToMousePosition()
    {
        if (_mainCamera is null) return;
        
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var raycastHit, Mathf.Infinity, ~mask))
        {
            _newTower.transform.position = new Vector3(raycastHit.point.x, _newTower.transform.position.y, raycastHit.point.z);
            // This one can be used if we decide to use rough terrain
            // newTower.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }
    
    /// <summary>
    /// Calls create new tower if there is enough coins and tower placement limit is not reached.
    /// if 'Escape' pressed tower placement operation will be canceled
    /// </summary>
    private void HandleAddTowerHotkey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_newTower)
            {
                Destroy(_newTower);
            }
            return;
        }
        
        if (_gameSession.AreThereEnoughCoins(20) && !_gameSession.IsTowerLimitReached())
        {
            if (Input.GetKeyDown(addFireTowerHotkey))
            {
                CreateNewTower(towerPrefabs[0]);
            }

            if (Input.GetKeyDown(addWaterTowerHotkey))
            {
                CreateNewTower(towerPrefabs[1]);
            }

            if (Input.GetKeyDown(addEarthTowerHotkey))
            {
                CreateNewTower(towerPrefabs[2]);
            }

            if (Input.GetKeyDown(addWoodTowerHotkey))
            {
                CreateNewTower(towerPrefabs[3]);
            }
        }
    }
    
    /// <summary>
    /// Creates a new tower from tower prefab
    /// </summary>
    /// <param name="towerPrefab"></param>
    private void CreateNewTower(GameObject towerPrefab) 
    {
        if (!_newTower)
        {
            _newTower = Instantiate(towerPrefab);
            var newTowerPosition = _newTower.transform.position;
            _newTower.transform.position =
                new Vector3(newTowerPosition.x, newTowerPosition.y, newTowerPosition.z);
        }
        else
        {
            Destroy(_newTower);
        }
    }

    public static List<GameObject> CurrentTowers => _currentTowers;
}
