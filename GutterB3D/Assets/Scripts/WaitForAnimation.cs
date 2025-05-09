using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitForAnimation : MonoBehaviour
{
    private Animator animator;
    private bool hasPlayed = false;
    public string nextSceneName;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("imageAnimDivine") && 
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            !hasPlayed)
        {
            hasPlayed = true;
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
