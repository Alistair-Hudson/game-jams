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
        FindObjectOfType<PlayArea>().ReduceTreasureLeft();
        Destroy(gameObject);
    }

}
