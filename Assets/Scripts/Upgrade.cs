using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] public GameObject waterTower;
    [SerializeField] public GameObject fireTower;
    [SerializeField] public GameObject earthTower;
    [SerializeField] public GameObject woodTower;
    
    [SerializeField] public KeyCode RupgradeWater = KeyCode.U;
    [SerializeField] public KeyCode RupgradeFire = KeyCode.I;
    [SerializeField] public KeyCode RupgradeEarth = KeyCode.O;
    [SerializeField] public KeyCode RupgradeWood = KeyCode.P;
    
    [SerializeField] public KeyCode DupgradeWater = KeyCode.Z;
    [SerializeField] public KeyCode DupgradeFire = KeyCode.X;
    [SerializeField] public KeyCode DupgradeEarth = KeyCode.C;
    [SerializeField] public KeyCode DupgradeWood = KeyCode.V;

    private GameSession _gameSession;
    
    private Transform _waterTColliderTransform;
    private Transform _fireTColliderTransform;
    private Transform _earthTColliderTransform;
    private Transform _woodTColliderTransform;
    
    void Start()
    {
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
        
        _waterTColliderTransform = waterTower.transform;
        _fireTColliderTransform = fireTower.transform;
        _earthTColliderTransform = earthTower.transform;
        _woodTColliderTransform = woodTower.transform;

    }
    
    void Update()
    {
        RangeUpgradeHandler();
        DamageUpgradeHandler();
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

    public void IncreaseDamage(GameObject tower)
    {
        print("In increase damage");
        print("Damage before increase " + tower.GetComponent<Tower>().damage);
        tower.GetComponent<Tower>().damage += 5;
        print("Damage after increase " + tower.GetComponent<Tower>().damage);
        
    }
    
    public void RangeUpgradeHandler()
    {
        if (_gameSession.AreThereEnoughDiamonds(5))
        {
            
            if (Input.GetKeyDown(RupgradeWater))
            { 
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseRadius(GetChildCollider(_waterTColliderTransform)); 
            }
            if (Input.GetKeyDown(RupgradeFire))
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseRadius(GetChildCollider(_fireTColliderTransform)); 
            }

            if (Input.GetKeyDown(RupgradeEarth))
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseRadius(GetChildCollider(_earthTColliderTransform)); 
            }

            if (Input.GetKeyDown(RupgradeWood))
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseRadius(GetChildCollider(_woodTColliderTransform)); 
            }
        }
        
    }

    public void DamageUpgradeHandler()
    {
        if (_gameSession.AreThereEnoughDiamonds(5))
        {
            if (Input.GetKeyDown(DupgradeWater))
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseDamage(waterTower);
            }
            if (Input.GetKeyDown(DupgradeFire)) 
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseDamage(fireTower);
            } 
            if (Input.GetKeyDown(DupgradeEarth))
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseDamage(earthTower);
            } 
            if (Input.GetKeyDown(DupgradeWood))
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseDamage(woodTower); 
            }
        }
    }

    
    
}
