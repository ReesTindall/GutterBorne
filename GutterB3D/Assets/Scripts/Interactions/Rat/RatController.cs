using System.Collections;
using UnityEngine;

public class RatController : MonoBehaviour
{
    [Header("Ground Snap")]
    public Transform groundCheck;
    public float groundOffset = 0.1f;

    [Header("Chase & Attack")]
    public float chaseSpeed      = 4f;
    public float chaseRange      = 10f;
    public float attackRange     = 1.5f;
    public float attackCooldown  = 1f;

    [Header("Sniff Behaviour")]
    public float sniffDuration   = 0.8f;

    private Transform player;
    private Animator anim;
    private float lastAttackTime;
    private bool hasSniffed;
    private bool isSniffing;

    void Start()
    {
        var ply = GameObject.FindGameObjectWithTag("Player");
        if (ply != null) player = ply.transform;
        anim   = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, 1f))
        {
            var pos = transform.position;
            pos.y = hit.point.y + groundOffset;
            transform.position = pos;
        }

        if (player == null) return;
        float dist = Vector3.Distance(transform.position, player.position);

        // 2) Sniff once when player enters range
        if (!hasSniffed)
        {
            if (dist <= chaseRange)
                StartCoroutine(DoSniffThenChase());
            else
                anim.SetFloat("Speed", 0f);
            return;
        }

        // 3) If currently sniffing, freeze
        if (isSniffing)
        {
            anim.SetFloat("Speed", 0f);
            return;
        }

        // 4) Chase vs Attack
        if (dist > attackRange)
        {
            // chase
            Vector3 dir = (player.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                Time.deltaTime * chaseSpeed
            );
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                chaseSpeed * Time.deltaTime
            );
            anim.SetFloat("Speed", chaseSpeed);
        }
        else
        {
            // attack
            anim.SetFloat("Speed", 0f);
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                DoAttack();
                lastAttackTime = Time.time;
            }
        }
    }

    IEnumerator DoSniffThenChase()
    {
        hasSniffed = true;
        isSniffing = true;

        anim.SetFloat("Speed", 0f);
        anim.SetBool("Sniff", true);   
        Debug.Log("This fucker should be sniffing");       // start sniff
        yield return new WaitForSeconds(sniffDuration);
        anim.SetBool("Sniff", false);         // reset sniff
        isSniffing = false;
    }

    void DoAttack()
    {
        anim.SetBool("Attack", true);         // start attack
        Debug.Log("Rat attacks slime!");
        StartCoroutine(ResetAttackFlag());
        // TODO: player.GetComponent<SlimeHealth>()?.TakeDamage(1);
    }

    IEnumerator ResetAttackFlag()
    {
        // allow one frame (or tweak delay to match your clip length)
        yield return null;
        anim.SetBool("Attack", false);        // reset attack
    }
}