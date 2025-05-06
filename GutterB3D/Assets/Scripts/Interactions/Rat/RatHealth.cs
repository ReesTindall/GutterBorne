using UnityEngine;
using System.Collections;

public class RatHealth : MonoBehaviour
{
    public int maxHP = 3;
    public bool invulnerable;
    public float destroyDelay = 2f;   // length of RatDeath clip
    int hp;

    Animator anim;
    Collider[] colliders;

    [Header("Feedback Effects")]
    //public GameObject poofEffectPrefab;
    public AudioClip squeakClip;
    public float flashDuration = 0.2f;
    private AudioSource audioSource;

    void Start() { }

    void Awake()
    {
        hp = maxHP;
        anim = GetComponentInChildren<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        audioSource = GetComponent<AudioSource>();
        if (!audioSource) audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void TakeDamage(int dmg)
    {
        if (invulnerable || hp <= 0) return;

        hp -= dmg;

        // 🔴 Visual/audio feedback even if not dead
        StartCoroutine(FlashRed());
        //PlayPoof();
        PlaySqueak();

        if (hp <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator FlashRed()
    {
        SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if (renderer == null) yield break;

        Material[] mats = renderer.materials;
        Color[] originalColors = new Color[mats.Length];

        for (int i = 0; i < mats.Length; i++)
        {
            originalColors[i] = mats[i].color;
            mats[i].color = Color.Lerp(mats[i].color, Color.red, 0.5f); // Tint toward red
        }

        yield return new WaitForSeconds(flashDuration);

        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].color = originalColors[i];
        }
    }

    // void PlayPoof()
    // {
    //     if (poofEffectPrefab)
    //     {
    //         Vector3 spawnPos = transform.position + Vector3.up * 0.5f;
    //         Instantiate(poofEffectPrefab, spawnPos, Quaternion.identity);
    //     }
    // }

    void PlaySqueak()
    {
        if (squeakClip && audioSource)
        {
            audioSource.PlayOneShot(squeakClip);
        }
    }

    IEnumerator Die()
    {
        foreach (var c in colliders) c.enabled = false;
        RatController rc = GetComponent<RatController>();
        if (rc) rc.enabled = false;

        if (anim) anim.SetBool("Death", true);
        yield return null;
        anim.SetBool("Death", false);

        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}





// using UnityEngine;

// public class RatHealth : MonoBehaviour
// {
//     public int maxHP = 3;
//     public bool invulnerable;
//     public float destroyDelay = 2f;   // length of RatDeath clip
//     int hp;
//     Animator anim;
//     Collider[] colliders;             // so we can disable hits

//     void Start(){
        
//     }

//     void Awake()
//     {
//         hp = maxHP;
//         anim = GetComponentInChildren<Animator>();
//         colliders = GetComponentsInChildren<Collider>();
//     }

//     public void TakeDamage(int dmg)
//     {
//         if (invulnerable) return;
//         if (hp <= 0) return;
//         hp -= dmg;

//         if (hp <= 0) StartCoroutine(Die());
//     }

//     System.Collections.IEnumerator Die()
//     {

//         foreach (var c in colliders) c.enabled = false;
//         RatController rc = GetComponent<RatController>();
//         if (rc) rc.enabled = false;

//         if (anim) anim.SetBool("Death", true);
//         yield return null;                 // wait one frame
//         anim.SetBool("Death", false);

        
//         yield return new WaitForSeconds(destroyDelay);
//         Destroy(gameObject);
//     }
// }