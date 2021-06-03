using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TREASURE_TYPE
{
    GOLD = 0,
    RUBY,
    SAPHIRE,
    EMERALD,
    DIAMOND,
    FOOLS_GOLD,
    GLASS,
}

public class Treasure : MonoBehaviour
{
    [SerializeField] TREASURE_TYPE type = TREASURE_TYPE.GOLD;
    [SerializeField] int value = 0;
    [SerializeField] AudioClip takeSound = null;


    public TREASURE_TYPE GetTreasureType()
    {
        return type;
    }

    public int GetTreasureValue()
    {
        return value;
    }

    public void TakeTreaure()
    {
        AudioSource.PlayClipAtPoint(takeSound, Camera.main.transform.position);

        switch (type)
        {
            case TREASURE_TYPE.FOOLS_GOLD:
                break;
            case TREASURE_TYPE.GLASS:
                break;
            default:
                FindObjectOfType<PlayArea>().ReduceTreasureLeft();
                break;
        }
        Destroy(gameObject);
    }

}
