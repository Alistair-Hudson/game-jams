using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinableBrick : MonoBehaviour
{
    [SerializeField] int toughness = 1;
    [SerializeField] AudioClip diggingSound = null;
    [SerializeField] Sprite[] cracking_sprites;

    
    public void DecreaseToughness()
    {
        --toughness;
        AudioSource.PlayClipAtPoint(diggingSound, Camera.main.transform.position);
        if (0 >= toughness)
        {
            FindObjectOfType<PlayArea>().ReduceBricksLeft();
            Destroy(gameObject);
        }
    }
}
