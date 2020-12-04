using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjacencyDetector : MonoBehaviour
{
    private Tower _parentTowerComponent;
    // Start is called before the first frame update
    private void Start()
    {
        _parentTowerComponent = transform.parent.GetComponent<Tower>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 12)
        {
            print("can't place");
            _parentTowerComponent.isPlaceable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 12)
        {
            print("can place");
            _parentTowerComponent.isPlaceable = true;
        }
    }
}
