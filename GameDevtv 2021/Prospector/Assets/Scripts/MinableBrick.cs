using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinableBrick : MonoBehaviour
{
    [SerializeField] int toughness = 1;
    [SerializeField] AudioClip diggingSound = null;
    [SerializeField] Image crackImage = null;
    [SerializeField] Sprite[] cracking_sprites;

    int remaingToughness = 0;

    private void Start()
    {
        remaingToughness = toughness;
        crackImage.fillAmount = 0;
    }

    public void DecreaseToughness()
    {
        --remaingToughness;
        AudioSource.PlayClipAtPoint(diggingSound, Camera.main.transform.position);
        crackImage.fillAmount = (float)(toughness - remaingToughness) / (toughness - 1);
        if (0 >= remaingToughness)
        {
            FindObjectOfType<PlayArea>().ReduceBricksLeft();
            Destroy(gameObject);
        }
    }
}
