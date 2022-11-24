using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public GameObject moneyTextObject;
    public int money = 2;
    TextMeshProUGUI moneyText;
    
    // Start is called before the first frame update
    void Start()
    {
        moneyText = moneyTextObject.GetComponent<TextMeshProUGUI>();

        moneyText.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addMoney(int addition = 1)
    {
        money += addition;
        moneyText.text = money.ToString();
    }
}
