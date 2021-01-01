using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] public GameObject waterTower;
    [SerializeField] public GameObject fireTower;
    [SerializeField] public GameObject earthTower;
    [SerializeField] public GameObject woodTower;
    
    [SerializeField] public GameObject waterProjectile;
    [SerializeField] public GameObject fireProjectile;
    [SerializeField] public GameObject earthProjectile;
    [SerializeField] public GameObject woodProjectile;
   
    
    /*
    [SerializeField] public KeyCode RupgradeWater = KeyCode.U;
    [SerializeField] public KeyCode RupgradeFire = KeyCode.I;
    [SerializeField] public KeyCode RupgradeEarth = KeyCode.O;
    [SerializeField] public KeyCode RupgradeWood = KeyCode.P;
    */
    
    [SerializeField] public KeyCode AupgradeWater = KeyCode.U;
    [SerializeField] public KeyCode AupgradeFire = KeyCode.I;
    [SerializeField] public KeyCode AupgradeEarth = KeyCode.O;
    [SerializeField] public KeyCode AupgradeWood = KeyCode.P;
    
    
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
        //RangeUpgradeHandler();
        DamageUpgradeHandler();
        AccuracyUpgradeHandler();
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
       
        tower.GetComponent<Tower>().damage += 5;
        
        
    }
    
    public void IncreaseProjectileAccuracy(GameObject projectile)
    {
        
        print(projectile.name + "Accuracy before upgrade " + projectile.GetComponent<Projectile>().accuracy );
        
        projectile.GetComponent<Projectile>().accuracy += 10;
        
        print(projectile.name + "Accuracy after upgrade " + projectile.GetComponent<Projectile>().accuracy);
    }
    
    /*
    public void RangeUpgradeHandler()"
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
    */

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
    
    public void AccuracyUpgradeHandler()
    {
        if (_gameSession.AreThereEnoughDiamonds(0))
        {
            if (Input.GetKeyDown(AupgradeWater))
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseProjectileAccuracy(waterProjectile);
            }
            if (Input.GetKeyDown(AupgradeFire)) 
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseProjectileAccuracy(fireProjectile);
            } 
            if (Input.GetKeyDown(AupgradeEarth))
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseProjectileAccuracy(earthProjectile);
            } 
            if (Input.GetKeyDown(AupgradeWood))
            {
                _gameSession.ChangeDiamondAmountBy(-5);
                IncreaseProjectileAccuracy(woodProjectile); 
            }
        }
    }

}
