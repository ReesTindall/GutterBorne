using UnityEngine;
using UnityEngine.UI;

public class WaterUIAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public float frameRate = 10f;

    private Image image;
    private int currentFrame;
    private float timer;

    void Start()
    {
        image = GetComponent<Image>();
        currentFrame = 0;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            currentFrame = (currentFrame + 1) % frames.Length;
            image.sprite = frames[currentFrame];
            timer = 0f;
        }
    }
}