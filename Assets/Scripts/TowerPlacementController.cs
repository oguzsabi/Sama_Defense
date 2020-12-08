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
    [SerializeField] private int towerWorth;
    [SerializeField] private int towerLimit;
    
    private GameObject newTower;
    private float mouseWheelDelta;
    private Camera mainCamera;
    public Currency currencyScript;
    private GameObject towerCountUI;

    public int towerCount = 0;
    
    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = Camera.main;
        currencyScript = GameObject.FindWithTag("GameController").GetComponent<Currency>();

        towerCountUI = GameObject.Find("TowerCount");
    }

    // Update is called once per frame
    private void Update()
    {
        HandleAddTowerHotkey();

        if (!newTower) return;
        
        MoveNewTowerToMousePosition();
        RotateNewTowerWithMouseWheel();
        PlaceNewTowerIfClicked();
        
        towerCountUI.GetComponent<Text>().text = towerCount.ToString();
        
    }

    private bool towerLimitChecker()
    {
        if (towerLimit >= towerCount)
        {
            return true;
        }
        return false;
    }
    
    private void PlaceNewTowerIfClicked()
    {
        if (!Input.GetMouseButtonDown(0) || !newTower.GetComponent<Tower>().isPlaceable) return;
        
        SetupTower();
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
        print("DecrementCoin: " + currencyScript.DecrementCoin(towerWorth) + "Tower Limit Checker: " + towerLimitChecker());
        
        if (Input.GetKeyDown(addFireTowerHotkey) && currencyScript.DecrementCoin(towerWorth) && towerLimitChecker())
        {
            CreateNewTower(towerPrefabs[0]);
            currencyScript.DecrementCoin(towerWorth);
            towerCount++;
        }
        if (Input.GetKeyDown(addWaterTowerHotkey) && currencyScript.DecrementCoin(towerWorth) && towerLimitChecker())
        {
            CreateNewTower(towerPrefabs[1]);
            currencyScript.DecrementCoin(towerWorth);
            towerCount++;
        }
        if (Input.GetKeyDown(addEarthTowerHotkey) && currencyScript.DecrementCoin(towerWorth) && towerLimitChecker())
        {
            CreateNewTower(towerPrefabs[2]);
            currencyScript.DecrementCoin(towerWorth);
            towerCount++;
        }
        if (Input.GetKeyDown(addWoodTowerHotkey) && currencyScript.DecrementCoin(towerWorth) && towerLimitChecker())
        {
            CreateNewTower(towerPrefabs[3]);
            currencyScript.DecrementCoin(towerWorth);
            towerCount++;
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
