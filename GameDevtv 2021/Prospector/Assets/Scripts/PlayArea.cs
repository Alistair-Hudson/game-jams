using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayArea : MonoBehaviour
{
    [SerializeField] int grid_size = 10;
    [SerializeField] int pick_cost = 50;
    [SerializeField] int test_cost = 10;
    [SerializeField] Image stability_bar = null;
    [SerializeField] TMP_Text treasure_remaining = null;
    [SerializeField] GameObject all_treasure_found_screen = null;
    [SerializeField] GameObject area_collapse_screen = null;
    [SerializeField] MinableBrick[] brick_prefabs;
    [SerializeField] Treasure[] treasure_prefabs;

    int number_of_bricks = 0;
    int number_of_treasures = 0;

    ToolBag toolBag = null;
    TreasureBag treasureBag = null;
    LevelController levelController = null;

    // Start is called before the first frame update
    void Start()
    {
        toolBag = FindObjectOfType<ToolBag>();
        treasureBag = FindObjectOfType<TreasureBag>();
        levelController = FindObjectOfType<LevelController>();
        all_treasure_found_screen.SetActive(false);
        area_collapse_screen.SetActive(false);

        for (int i = 0; i < grid_size; ++i)
        {
            for (int j= 0; j < grid_size; ++j)
            {
                MinableBrick newBrick = Instantiate(brick_prefabs[Random.Range(0, brick_prefabs.Length)], 
                                                    new Vector3(i, j, 0), 
                                                    Quaternion.identity);
                newBrick.transform.parent = transform;
                ++number_of_bricks;

                if (50 >= Random.Range(0, 100))
                {
                    Treasure newTreasure = Instantiate(treasure_prefabs[Random.Range(0, treasure_prefabs.Length)],
                                                        new Vector3(i, j, 0),
                                                        Quaternion.identity);
                    newTreasure.transform.parent = transform;
                    switch (newTreasure.GetTreasureType())
                    {
                        case TREASURE_TYPE.GLASS:
                            break;
                        case TREASURE_TYPE.FOOLS_GOLD:
                            break;
                        default:
                            ++number_of_treasures;
                            break;
                    }
                }
            }
        }

        Camera.main.transform.position = new Vector3(grid_size / 2, 
                                                     grid_size / 2, 
                                                     Camera.main.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        stability_bar.fillAmount = 1;
        treasure_remaining.text = "Treasure Remaining " + number_of_treasures.ToString();
        if (0 >= number_of_bricks)
        {
            area_collapse_screen.SetActive(true);
            Debug.Log("Mine Collapse");
        }
        if (0 >= number_of_treasures)
        {
            all_treasure_found_screen.SetActive(true);
            Debug.Log("All treasures found");
        }
        
    }

    public void ReduceBricksLeft()
    {
        --number_of_bricks;
    }

    public void ReduceTreasureLeft()
    {
        --number_of_treasures;
    }

    public void BuyPick()
    {
        if (treasureBag.SpendMoney(pick_cost))
        {
            toolBag.AddPick();
        }
    }

    public void BuyTest()
    {
        if (treasureBag.SpendMoney(test_cost))
        {
            toolBag.AddTest();
        }
    }

    public void CallNextLevel()
    {
        levelController.NextLevel();
    }

    public void CallMainMenu()
    {
        levelController.MainMenu();
    }
}
