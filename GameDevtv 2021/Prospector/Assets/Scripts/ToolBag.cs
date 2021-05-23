using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBag : MonoBehaviour
{
    [SerializeField] int number_of_picks = 5;
    [SerializeField] int number_of_tests = 3;
    [SerializeField] int pick_durablity = 3;

    int current_pick_durabilty = 0;

    private void Awake()
    {
        ToolBag[] bags = FindObjectsOfType<ToolBag>();
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
        current_pick_durabilty = pick_durablity;   
    }

    public void UsePick()
    {
        --current_pick_durabilty;
        if (0 >= current_pick_durabilty)
        {
            --number_of_picks;
            if (0 >= number_of_picks)
            {
                Debug.Log("Game over");
            }
            current_pick_durabilty = pick_durablity;
        }
    }

    public bool UseTest()
    {
        if (0 >= number_of_tests)
        {
            return false;
        }
        --number_of_tests;
        return true;
    }

    public void AddPick()
    {
        ++number_of_picks;
    }

    public void AddTest()
    {
        ++number_of_tests;
    }
}
