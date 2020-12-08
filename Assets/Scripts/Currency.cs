using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Currency : MonoBehaviour
{
    private GameObject coinUI;

    public int coin; 
    
    // Start is called before the first frame update
    void Start()
    {
        coinUI = GameObject.Find("Coin");
    }

    // Update is called once per frame
    void Update()
    {
        coinUI.GetComponent<Text>().text = coin.ToString();
        if (coin == 0)
        {
            coinUI.GetComponent<Text>().text = "asdasdasdas";
            
        }
    }

    public bool DecrementCoin(int amount)
    {
        return coin - amount >= 0;
    }
    
}

