using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolBag : MonoBehaviour
{
    [SerializeField] int starting_picks = 5;
    [SerializeField] int staring_tests = 3;
    [SerializeField] int pick_durablity = 3;

    [SerializeField] TMP_Text picks_text = null;
    [SerializeField] TMP_Text tests_text = null;

    [SerializeField] GameObject out_of_picks = null;

    int number_of_picks = 5;
    int number_of_tests = 3;
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
        number_of_picks = starting_picks;
        number_of_tests = staring_tests;
        current_pick_durabilty = pick_durablity;
        picks_text.text = "x" + number_of_picks.ToString();
        tests_text.text = "x" + number_of_tests.ToString();
        out_of_picks.SetActive(false);
    }

    public bool UsePick()
    {
        if (0 >= number_of_picks)
        {
            out_of_picks.SetActive(true);
            return false;
        }
        --current_pick_durabilty;
        if (0 >= current_pick_durabilty)
        {
            --number_of_picks;
            picks_text.text = "x" + number_of_picks.ToString();
            current_pick_durabilty = pick_durablity;
        }
        return true;
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
        out_of_picks.SetActive(false);
    }

    public void AddTest()
    {
        ++number_of_tests;
        tests_text.text = "x" + number_of_tests.ToString();
    }

    public void NewGame()
    {
        number_of_picks = starting_picks;
        number_of_tests = staring_tests;
        current_pick_durabilty = pick_durablity;
        picks_text.text = "x" + number_of_picks.ToString();
        tests_text.text = "x" + number_of_tests.ToString();
        out_of_picks.SetActive(false);
        FindObjectOfType<TreasureBag>().ResetGame();
    }

    public void MainMenu()
    {
        Destroy(gameObject);
    }
}
