using UnityEngine;

public class AdjacencyDetector : MonoBehaviour
{
    private Tower _parentTower;
    private GameObject _parentRangeIndicator;
    
    private void Start()
    {
        _parentTower = transform.parent.GetComponent<Tower>();
        _parentRangeIndicator = _parentTower.rangeIndicator;
    }
    
    /// <summary>
    /// Tower can not be placed if it is on enemy walking path or near another tower
    /// Sets range indicator to inactive
    /// </summary>
    /// <param name="other"></param>
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
    
    /// <summary>
    /// Tower can be placed if it is not on enemy walking path or far enough from another tower
    /// Sets range indicator to active
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        // If tower is not touching to a Path or another Tower
        if (other.gameObject.layer != 8 && other.gameObject.layer != 12) return;
        _parentTower.isPlaceable = true;
        _parentRangeIndicator.SetActive(true);
    }
}
