﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlacementController : MonoBehaviour
{
    [SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private KeyCode addFireTowerHotkey = KeyCode.T;
    [SerializeField] private KeyCode addWaterTowerHotkey = KeyCode.Y;
    [SerializeField] private KeyCode addEarthTowerHotkey = KeyCode.U;
    [SerializeField] private KeyCode addWoodTowerHotkey = KeyCode.I;
    [SerializeField] private float rotationMultiplier = 10f;
    [SerializeField] private LayerMask mask;

    private GameObject newTower;
    private float mouseWheelDelta;
    private Camera mainCamera;
    private GameSession _gameSession;

    // Start is called before the first frame update
    private void Start()
    {
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleAddTowerHotkey();

        if (!newTower) return;
        
        MoveNewTowerToMousePosition();
        RotateNewTowerWithMouseWheel();
        PlaceNewTowerIfClicked();
    }

    private void PlaceNewTowerIfClicked()
    {
        if (!Input.GetMouseButtonDown(0) || !newTower.GetComponent<Tower>().isPlaceable) return;
        
        SetupTower();
        _gameSession.IncrementTowerCount();
        _gameSession.ChangeCoinAmountBy(-20);
    }

    private void SetupTower()
    {
        newTower.GetComponent<Tower>().MakeTowerReady();
        newTower.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        newTower.GetComponentInChildren<Tower>().adjacencyDetector.SetActive(false);
        newTower.layer = 8;
        newTower = null;
    }

    private void RotateNewTowerWithMouseWheel()
    {
        mouseWheelDelta += Input.mouseScrollDelta.y;
        newTower.transform.Rotate(Vector3.up, mouseWheelDelta * rotationMultiplier);
    }

    private void MoveNewTowerToMousePosition()
    {
        if (mainCamera is null) return;
        
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var raycastHit, Mathf.Infinity, ~mask))
        {
            newTower.transform.position = new Vector3(raycastHit.point.x, newTower.transform.position.y, raycastHit.point.z);
            // This one can be used if we decide to use rough terrain
            // newTower.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void HandleAddTowerHotkey()
    {
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

    private void CreateNewTower(GameObject towerPrefab) 
    {
        if (!newTower)
        {
            newTower = Instantiate(towerPrefab);
            var newTowerPosition = newTower.transform.position;
            newTower.transform.position =
                new Vector3(newTowerPosition.x, newTowerPosition.y + 6.8f, newTowerPosition.z);
        }
        else
        {
            Destroy(newTower);
        }
    }
}
