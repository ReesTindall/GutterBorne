using System.Collections;
using UnityEngine;

public class RatController : MonoBehaviour
{
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
    private float fixedY;

    void Start()
    {
        var ply = GameObject.FindWithTag("Player");
        if (ply != null) player = ply.transform;

        anim = GetComponentInChildren<Animator>();

        // store the starting Y so we never change it
        fixedY = transform.position.y;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // 1) Sniff once on first detection
        if (!hasSniffed)
        {
            if (dist <= chaseRange)
                StartCoroutine(DoSniffThenChase());
            else
                anim.SetFloat("Speed", 0f);
            return;
        }

        // 2) If sniffing, hold still
        if (isSniffing)
        {
            anim.SetFloat("Speed", 0f);
            return;
        }

        // 3) Chase
        if (dist > attackRange)
        {
            // a) Compute horizontal-only direction (X axis)
            float deltaX = player.position.x - transform.position.x;
            Vector3 dir = new Vector3(deltaX, 0f, 0f).normalized;

            // b) Rotate to face the blob along X
            if (dir.x != 0f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.Euler(0f, targetRot.eulerAngles.y, 0f),
                    Time.deltaTime * chaseSpeed
                );
            }

            // c) Slide along X
            float newX = Mathf.MoveTowards(
                transform.position.x,
                player.position.x,
                chaseSpeed * Time.deltaTime
            );
            transform.position = new Vector3(newX, fixedY, transform.position.z);

            anim.SetFloat("Speed", chaseSpeed);
        }
        // 4) Attack
        else
        {
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
        yield return new WaitForSeconds(sniffDuration);
        anim.SetBool("Sniff", false);

        isSniffing = false;
    }

    void DoAttack()
    {
        anim.SetBool("Attack", true);
        StartCoroutine(ResetAttackFlag());
        Debug.Log("Rat attacks slime!");
        // TODO: apply damage to player here
    }

    IEnumerator ResetAttackFlag()
    {
        yield return null;                // let Animator see the true value for one frame
        anim.SetBool("Attack", false);
    }
}