using UnityEngine;

public class RatHealth : MonoBehaviour
{
    public int maxHP = 3;
    public bool invulnerable;
    public float destroyDelay = 2f;   // length of RatDeath clip
    int hp;
    Animator anim;
    Collider[] colliders;             // so we can disable hits

    void Awake()
    {
        hp = maxHP;
        anim = GetComponentInChildren<Animator>();
        colliders = GetComponentsInChildren<Collider>();
    }

    public void TakeDamage(int dmg)
    {
        if (invulnerable) return;
        if (hp <= 0) return;
        hp -= dmg;

        if (hp <= 0) StartCoroutine(Die());
    }

    System.Collections.IEnumerator Die()
    {

        foreach (var c in colliders) c.enabled = false;
        RatController rc = GetComponent<RatController>();
        if (rc) rc.enabled = false;

        if (anim) anim.SetBool("Death", true);
        yield return null;                 // wait one frame
        anim.SetBool("Death", false);

        
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}