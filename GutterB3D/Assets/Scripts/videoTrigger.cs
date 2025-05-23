using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AnimationTrigger : MonoBehaviour
{
    [Tooltip("GameObject that holds your Canvas or animated object.")]
    public GameObject animatedObject;

    [Tooltip("Animator component to trigger.")]
    public Animator animator;

    [Tooltip("Name of the trigger parameter in the Animator.")]
    public string animationTriggerName = "Play";

    private Collider triggerCollider;

    void Reset()
    {
        triggerCollider = GetComponent<Collider>();
        triggerCollider.isTrigger = true;
    }

    void Start()
    {
        triggerCollider = triggerCollider ?? GetComponent<Collider>();

        // Optionally hide the object until triggered
        if (animatedObject != null)
            animatedObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (animatedObject != null)
                animatedObject.SetActive(true);

            if (animator != null && !string.IsNullOrEmpty(animationTriggerName))
                animator.SetTrigger(animationTriggerName);

            triggerCollider.enabled = false; // Prevent retriggering
        }
    }
}


// using UnityEngine;
// using UnityEngine.Video;

// [RequireComponent(typeof(Collider))]
// public class VideoTrigger : MonoBehaviour
// {
//     [Tooltip("Drag the GameObject that holds your Canvas + VideoPlayer here.")]
//     public GameObject videoCanvas;

//     [Tooltip("Drag the VideoPlayer component here.")]
//     public VideoPlayer videoPlayer;

//     private Collider triggerCollider;

//     void Reset()
//     {
//         // Make sure this Collider is set as a trigger by default
//         triggerCollider = GetComponent<Collider>();
//         triggerCollider.isTrigger = true;
//     }

//     void Start()
//     {
//         triggerCollider = triggerCollider ?? GetComponent<Collider>();

//         // Hide the video canvas until triggered
//         if (videoCanvas != null)
//             videoCanvas.SetActive(false);

//         // Optional: subscribe to end event if you want to hide canvas when done
//         if (videoPlayer != null)
//             videoPlayer.loopPointReached += OnVideoEnd;
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             // Show canvas and start playback
//             if (videoCanvas != null)
//                 videoCanvas.SetActive(true);

//             if (videoPlayer != null)
//                 videoPlayer.Play();

//             // Prevent retriggering
//             triggerCollider.enabled = false;
//         }
//     }

//     // Optional callback: hides canvas when the video finishes
//     void OnVideoEnd(VideoPlayer vp)
//     {
//         if (videoCanvas != null)
//             videoCanvas.SetActive(false);
//     }
// }
