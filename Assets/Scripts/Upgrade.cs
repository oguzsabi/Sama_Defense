using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] public Transform waterTColliderTransform;
    [SerializeField] public Transform fireTColliderTransform;
    [SerializeField] public Transform earthTColliderTransform;
    [SerializeField] public Transform woodTColliderTransform;
    
    [SerializeField] public KeyCode upgradeWater = KeyCode.U;
    [SerializeField] public KeyCode upgradeFire = KeyCode.I;
    [SerializeField] public KeyCode upgradeEarth = KeyCode.O;
    [SerializeField] public KeyCode upgradeWood = KeyCode.P;

    private GameSession _gameSession;

    void Start()
    {
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();

    }
    
    void Update()
    {
        upgradeHandler();
    }
    
    
    public SphereCollider GetChildCollider(Transform Collidertransform)
    {
        var sphereCollider = Collidertransform.GetChild(0).GetComponent<SphereCollider>();
        return sphereCollider;
    }

    public void IncreaseRadius(SphereCollider sphereCollider)
    {
       sphereCollider.radius += 1f;
       
    }
    
    // Start is called before the first frame update
   

    public void upgradeHandler()
    {
        if (_gameSession.AreThereEnoughDiamonds(5))
        {
            if (Input.GetKeyDown(upgradeWater))
            { 
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseRadius(GetChildCollider(waterTColliderTransform)); 
            }

            if (Input.GetKeyDown(upgradeFire))
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseRadius(GetChildCollider(fireTColliderTransform)); 
            }

            if (Input.GetKeyDown(upgradeEarth))
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseRadius(GetChildCollider(earthTColliderTransform)); 
            }

            if (Input.GetKeyDown(upgradeWood))
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseRadius(GetChildCollider(woodTColliderTransform)); 
            }
        }
        
    }

    // Update is called once per frame
    
}
