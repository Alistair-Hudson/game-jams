using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolBag : MonoBehaviour
{
    [SerializeField] int number_of_picks = 5;
    [SerializeField] int number_of_tests = 3;
    [SerializeField] int pick_durablity = 3;

    [SerializeField] TMP_Text picks_text = null;
    [SerializeField] TMP_Text tests_text = null;

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
        picks_text.text = "x" + number_of_picks.ToString();
        tests_text.text = "x" + number_of_tests.ToString();
    }

    public void UsePick()
    {
        --current_pick_durabilty;
        if (0 >= current_pick_durabilty)
        {
            --number_of_picks;
            picks_text.text = "x" + number_of_picks.ToString();
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
        tests_text.text = "x" + number_of_tests.ToString();
        return true;
    }

    public void AddPick()
    {
        ++number_of_picks;
        picks_text.text = "x" + number_of_picks.ToString();
    }

    public void AddTest()
    {
        ++number_of_tests;
        tests_text.text = "x" + number_of_tests.ToString();
    }
}
