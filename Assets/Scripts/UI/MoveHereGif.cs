using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveHereGif : MonoBehaviour
{
    public Sprite[] frames;
    public Image explosion;
    public float frameRate = 0.1f;

    private int currentImage;
    // Use this for initialization
    void Start()
    {
        currentImage = 0;
        InvokeRepeating("ChangeImage", 0.1f, frameRate);
    }

    private void ChangeImage()
    {
        if (currentImage == frames.Length - 1)
        {
            currentImage = 0;
        }
        currentImage += 1;
        explosion.sprite = frames[currentImage];
    }
}