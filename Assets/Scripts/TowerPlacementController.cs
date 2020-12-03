using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementController : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private KeyCode addTowerHotkey = KeyCode.A;
    [SerializeField] private float rotationMultiplier = 10f;
    

    private GameObject newTower;
    private float mouseWheelDelta;
    private Camera mainCamera;
    
    // Start is called before the first frame update
    private void Start()
    {
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
        if (!Input.GetMouseButtonDown(0)) return;
        
        newTower.GetComponent<Tower>().MakeTowerReady();
        newTower.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
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

        if (Physics.Raycast(ray, out var raycastHit))
        {
            // newTower.transform.position = raycastHit.point;
            newTower.transform.position = new Vector3(raycastHit.point.x, newTower.transform.position.y, raycastHit.point.z);
            // This one can be used if we decide to use rough terrain
            // newTower.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void HandleAddTowerHotkey()
    {
        if (!Input.GetKeyDown(addTowerHotkey)) return;
        
        if (!newTower)
        {
            newTower = Instantiate(towerPrefab);
            var newTowerPosition = newTower.transform.position;
            newTower.transform.position = new Vector3(newTowerPosition.x, newTowerPosition.y + 7f, newTowerPosition.z);
        }
        else
        {
            Destroy(newTower);
        }
    }
}
