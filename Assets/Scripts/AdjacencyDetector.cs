using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjacencyDetector : MonoBehaviour
{
    private Tower _parentTower;
    private GameObject _parentRangeIndicator;
    
    // Start is called before the first frame update
    private void Start()
    {
        _parentTower = transform.parent.GetComponent<Tower>();
        _parentRangeIndicator = _parentTower.rangeIndicator;
    }

    private void OnTriggerStay(Collider other)
    {
        // If tower is not touching to a Path or another Tower
        if (other.gameObject.layer != 8 && other.gameObject.layer != 12) return;

        if (_parentRangeIndicator.activeSelf)
        {
            _parentRangeIndicator.SetActive(false);
        }
        _parentTower.isPlaceable = false;
    }

    private void OnTriggerExit(Collider other)
    {
        // If tower is not touching to a Path or another Tower
        if (other.gameObject.layer != 8 && other.gameObject.layer != 12) return;
        _parentTower.isPlaceable = true;
        _parentRangeIndicator.SetActive(true);
    }
}
