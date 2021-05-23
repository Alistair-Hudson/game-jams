using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    [SerializeField] int grid_size = 10;
    [SerializeField] MinableBrick[] brick_prefabs;
    [SerializeField] Treasure[] treasure_prefabs;

    int number_of_bricks = 0;
    int number_of_treasures = 0;

    // Start is called before the first frame update
    void Start()
    {
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
        if (0 >= number_of_bricks)
        {
            Debug.Log("Mine Collapse");
        }
        if (0 >= number_of_treasures)
        {
            Debug.Log("All treasures found");
        }
        
    }

    public void ReduceBricksLeft()
    {
        --number_of_bricks;
    }

    public void ReduceTreasureLeft()
    {
        ++number_of_treasures;
    }
}
