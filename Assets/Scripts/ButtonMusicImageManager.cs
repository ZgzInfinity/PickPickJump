
/*
 * ----------------------------------------
 * -- Project: Pick-Pick Jump -------------
 * -- Author: Rubén Rodríguez Estebban ----
 * -- Date: 11/11/2021 --------------------
 * ----------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;

/**
 * Script that controls the chaging of a button image when it's pressed
 */

public class ButtonMusicImageManager : MonoBehaviour
{

    // Reference to the button of the sprite to be shown when it's not pressed
    public Sprite OffSprite;

    // Reference to the button of the sprite to be shown when it's pressed
    public Sprite OnSprite;

    // Reference to the button image
    public Image image;

    // Start is called before the first frame update
    private void Start()
    {
        // Set the button with the image of not pressed
        image.sprite = OffSprite;
    }

    // Change the sprite of the button image
    public void ChangeMusicButtonImage()
    {
        // If the button was pressed 
        if (image.sprite == OnSprite)
        {
            // Change the sprite to not pressed
            image.sprite = OffSprite;
        }
        else
        {
            // Change the sprite to pressed
            image.sprite = OnSprite;
        }
    }
}
