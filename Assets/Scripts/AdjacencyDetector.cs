using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjacencyDetector : MonoBehaviour
{
    private Tower _parentTowerComponent;
    private GameObject _parentRangeIndicator;
    
    // Start is called before the first frame update
    private void Start()
    {
        _parentTowerComponent = transform.parent.GetComponent<Tower>();
        _parentRangeIndicator = _parentTowerComponent.rangeIndicator;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 8 && other.gameObject.layer != 12) return;

        if (_parentRangeIndicator.activeSelf)
        {
            _parentRangeIndicator.SetActive(false);
        }
        _parentTowerComponent.isPlaceable = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 8 && other.gameObject.layer != 12) return;
        _parentTowerComponent.isPlaceable = true;
        _parentRangeIndicator.SetActive(true);
    }
}
