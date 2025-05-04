using System.Collections;
using UnityEngine;

public class RatController : MonoBehaviour
{
    [Header("Detection / Movement")]
    public float chaseSpeed   = 4f;
    public float chaseRange   = 10f;
    public float attackRange  = 1.5f;

    [Header("Attack")]
    public float attackCooldown = 1f;   // seconds between bites

    [Header("Sniff Telegraph")]
    public float sniffDuration  = 0.8f; // length of sniff clip

    public Collider pawCollider; 

    Transform player;
    Animator  anim;
    float     lastAttackTime;
    bool      sniffDone;
    bool      sniffingNow;
    float     fixedY;

    void Start ()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        anim   = GetComponentInChildren<Animator>();
        fixedY = transform.position.y;        // lock Y once

        if (pawCollider != null) pawCollider.enabled = false;
    }


    void Update ()
    {
        if (player == null) return;

        /* keep rat glued to start height */
        Vector3 pos = transform.position;
        pos.y = fixedY;
        transform.position = pos;

        float dist = Vector3.Distance(transform.position, player.position);


        if (!sniffDone)
        {
            if (dist <= chaseRange)
                StartCoroutine(SniffThenChase());
            else
                anim.SetFloat("Speed", 0f);
            return;
        }

        if (sniffingNow)
        {
            anim.SetFloat("Speed", 0f);
            return;
        }

        if (dist > attackRange)    Chase();
        else                       Bite();
    }


    void Chase()
    {
        /* direction in XZ plane */
        Vector3 dir = (player.position - transform.position);
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f) return;   // already on top

        dir.Normalize();

        /* rotate toward slime */
        Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation   = Quaternion.Slerp(transform.rotation,
                                                targetRot,
                                                Time.deltaTime * 10f);

        /* move */
        transform.position += dir * chaseSpeed * Time.deltaTime;

        anim.SetFloat("Speed", chaseSpeed);   // drive Walk
    }

    void Bite()
    {
        anim.SetFloat("Speed", 0f);

        if (Time.time - lastAttackTime < attackCooldown) return;
        lastAttackTime = Time.time;

        anim.SetBool("Attack", true);
        StartCoroutine(ResetBoolNextFrame("Attack"));

    }
    public void HitStart()
    {
        if (pawCollider == null) return;
        pawCollider.enabled = true;

        PawDamage pd = pawCollider.GetComponent<PawDamage>();
        if (pd) pd.alreadyHit = false;      
    }

    public void HitEnd()
    {
        if (pawCollider) pawCollider.enabled = false;
    }

    IEnumerator SniffThenChase()
    {
        sniffDone   = true;
        sniffingNow = true;

        anim.SetBool ("Sniff", true);
        anim.SetFloat("Speed", 0f);

        yield return new WaitForSeconds(sniffDuration);

        anim.SetBool("Sniff", false);
        sniffingNow = false;
    }

    IEnumerator ResetBoolNextFrame(string param)
    {
        yield return null;                 // one frame
        anim.SetBool(param, false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    
}