using UnityEngine;
using UnityEngine.UI;

public class DivineUIAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public float frameRate = 24f;

    private Image image;
    private int currentFrame;
    private float timer;

        void Awake()
        {
            frames = new Sprite[523];

            for (int i = 0; i < frames.Length; i++)
            {
                int frameNumber = 151 + i;
                string spriteName = $"pool1_{frameNumber:D5}"; // Pads to 5 digits (00151, 00152, etc.)
                frames[i] = Resources.Load<Sprite>(spriteName);

                if (frames[i] == null)
                    Debug.LogWarning($"Missing sprite: {spriteName}");
            }
        }

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