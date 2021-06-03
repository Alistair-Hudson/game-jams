using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreasureBag : MonoBehaviour
{
    [SerializeField] TMP_Text money_text = null;

    int money = 0;

    private void Awake()
    {
        TreasureBag[] bags = FindObjectsOfType<TreasureBag>();
        if (1 < bags.Length)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        money_text.text = "$" + money.ToString();
    }

    public int GetMoney()
    {
        return money;
    }

    public void AddMoney(int value)
    {
        money += value;
        money_text.text = "$" + money.ToString();
    }

    public bool SpendMoney(int value)
    {
        if (money < value)
        {
            return false;
        }
        money -= value;
        money_text.text = "$" + money.ToString();
        return true;
    }

    public void ResetGame()
    {
        money = 0;
        money_text.text = "$" + money.ToString();
    }

    public void EndGame()
    {
        Destroy(gameObject);
    }
}
