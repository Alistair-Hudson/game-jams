using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinableBrick : MonoBehaviour
{
    [SerializeField] int toughness = 1;


    public void DecreaseToughness()
    {
        --toughness;
        if (0 >= toughness)
        {
            FindObjectOfType<PlayArea>().ReduceBricksLeft();
            Destroy(gameObject);
        }
    }
}
