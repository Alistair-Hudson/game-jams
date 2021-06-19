using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayArea : MonoBehaviour
{
    [SerializeField] int pick_cost = 50;
    [SerializeField] int test_cost = 10;
    [SerializeField] Image stability_bar = null;
    [SerializeField] TMP_Text treasure_remaining = null;
    [SerializeField] TMP_Text AreaCollapseWarning = null;
    [SerializeField] MinableBrick[] brick_prefabs;
    [SerializeField] Treasure[] treasure_prefabs;
    [SerializeField] AudioClip collapseSound = null;

    int grid_size = 9;
    int number_of_bricks = 0;
    int number_of_treasures = 0;
    int collapse_number = 0;
    bool sound_playing = false;

    ToolBag toolBag = null;
    TreasureBag treasureBag = null;
    LevelController levelController = null;

    // Start is called before the first frame update
    private void Start()
    {
        float treasure_chance = 50 * Mathf.Exp(-FindObjectOfType<LevelController>().GetLevelNumber() / 10);
        grid_size = (int)(3 + Mathf.Log(FindObjectOfType<LevelController>().GetLevelNumber()));
        toolBag = FindObjectOfType<ToolBag>();
        treasureBag = FindObjectOfType<TreasureBag>();
        levelController = FindObjectOfType<LevelController>();
        AreaCollapseWarning.gameObject.SetActive(false);

        BuildPlayArea(treasure_chance);

        if (0 >= number_of_treasures)
        {
            Treasure newTreasure = Instantiate(treasure_prefabs[0],
                                    new Vector3(Random.Range(0, grid_size),
                                                Random.Range(0, grid_size),
                                                0),
                                    Quaternion.identity);
            newTreasure.transform.parent = transform;
            ++number_of_treasures;
        }

        collapse_number = number_of_bricks / 3;

        Camera.main.transform.position = new Vector3(grid_size / 2,
                                                     grid_size / 2,
                                                     Camera.main.transform.position.z);

    }

    private void BuildPlayArea(float treasure_chance)
    {
        for (int i = 0; i < grid_size; ++i)
        {
            for (int j = 0; j < grid_size; ++j)
            {
                MinableBrick newBrick = Instantiate(brick_prefabs[Random.Range(0, brick_prefabs.Length)],
                                                    new Vector3(i, j, 0),
                                                    Quaternion.identity);
                newBrick.transform.parent = transform;
                ++number_of_bricks;

                if (treasure_chance >= Random.Range(0, 100))
                {
                    Treasure newTreasure = Instantiate(treasure_prefabs[Random.Range(0, treasure_prefabs.Length)],
                                                        new Vector3(i, j, 0),
                                                        Quaternion.identity);
                    newTreasure.transform.parent = transform;
                    switch (newTreasure.GetTreasureType())
                    {
                        case TREASURE_TYPE.GLASS:
                            number_of_treasures += 0;
                            break;
                        case TREASURE_TYPE.FOOLS_GOLD:
                            number_of_treasures += 0;
                            break;
                        default:
                            ++number_of_treasures;
                            break;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        stability_bar.fillAmount = (float)collapse_number/number_of_bricks;
        treasure_remaining.text = "Treasure Remaining " + number_of_treasures.ToString();
        if (collapse_number >= number_of_bricks)
        {
            if (!sound_playing)
            {
                sound_playing = true;
                AudioSource.PlayClipAtPoint(collapseSound, Camera.main.transform.position);
            }
            FindObjectOfType<PlayerControls>().SetControlActive(false);
            AreaCollapseWarning.gameObject.SetActive(true);
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
        AreaCollapseWarning.gameObject.SetActive(false);
        levelController.NextLevel();
    }

    public void CallMainMenu()
    {
        FindObjectOfType<HSHandler>().AddScore("Player", levelController.GetLevelNumber());
        levelController.MainMenu();
        FindObjectOfType<TreasureBag>().EndGame();
        FindObjectOfType<ToolBag>().MainMenu();
    }
}
