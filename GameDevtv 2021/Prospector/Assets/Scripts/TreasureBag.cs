using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBag : MonoBehaviour
{
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

    public int GetMoney()
    {
        return money;
    }

    public void AddMoney(int value)
    {
        money += value;
    }

    public bool SpendMoney(int value)
    {
        if (money < value)
        {
            return false;
        }
        money -= value;
        return true;
    }
}
