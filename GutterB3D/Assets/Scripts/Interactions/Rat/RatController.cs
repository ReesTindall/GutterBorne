using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RatController : MonoBehaviour
{
    /* ---------- Patrol ------------------------------- */
    [Header("Patrol")]
    public float patrolRadius = 4f;
    public float patrolSpeed  = 1f;

    /* ---------- Detection / Chase -------------------- */
    [Header("Detection / Chase")]
    public float chaseSpeed  = 2f;
    public float chaseRange  = 10f;
    public float attackRange = 1.5f;

    /* ---------- Attack -------------------------------- */
    [Header("Attack")]
    public float attackCooldown = 3f;

    /* ---------- Sniff Telegraph ----------------------- */
    [Header("Sniff Telegraph")]
    public float sniffDuration = 0.8f;

    /* ---------- References ---------------------------- */
    public Collider pawCollider;            // assign hitbox
    Animator  anim;
    Rigidbody rb;
    Transform player;

    /* ---------- Internal state ------------------------ */
    Vector3  patrolCenter;
    Vector3  patrolPoint;
    bool     patrolPointSet;
    bool     sniffDone;
    bool     sniffingNow;
    float    lastAttackTime;

    /* ================================================== */
    void Start()
    {
        anim   = GetComponentInChildren<Animator>();
        rb     = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player")?.transform;

        /* lock Y position & tipping */
        rb.constraints = RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;

        patrolCenter = transform.position;
        ChoosePatrolPoint();

        if (pawCollider) pawCollider.enabled = false;
    }

    /* ================================================== */
    void FixedUpdate()
    {
        if (!player) return;

        float dist = Vector3.Distance(rb.position, player.position);

        /* ---- Sniff phase -------------------------------- */
        if (!sniffDone)
        {
            if (dist <= chaseRange) StartCoroutine(SniffThenChase());
            else                    Patrol();
            return;
        }
        if (sniffingNow) { anim.SetFloat("Speed", 0f); return; }

        /* ---- Chase or Bite ------------------------------ */
        if (dist > attackRange) Chase();
        else                    Bite();
    }

    /* ================================================== */
    /* ---------------- Movement helpers ---------------- */
    void Patrol()
    {
        if (!patrolPointSet ||
            Vector3.Distance(rb.position, patrolPoint) < 0.2f)
            ChoosePatrolPoint();

        MoveTowards(patrolPoint, patrolSpeed);
        anim.SetFloat("Speed", patrolSpeed);
    }

    void Chase()
    {
        MoveTowards(player.position, chaseSpeed);
        anim.SetFloat("Speed", chaseSpeed);
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 dir = target - rb.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f) return;

        dir.Normalize();

        /* rotate */
        Quaternion look = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Slerp(
            transform.rotation, look, Time.fixedDeltaTime * 10f);

        /* physics move */
        Vector3 nextPos = rb.position + dir * speed * Time.fixedDeltaTime;
        rb.MovePosition(nextPos);
    }

    void ChoosePatrolPoint()
    {
        float x = Random.Range(-patrolRadius, patrolRadius);
        float z = Random.Range(-patrolRadius, patrolRadius);
        patrolPoint = patrolCenter + new Vector3(x, 0f, z);
        patrolPointSet = true;
    }

    /* ================================================== */
    /* ---------------- Attack logic -------------------- */
    void Bite()
    {
        anim.SetFloat("Speed", 0f);

        if (Time.time - lastAttackTime < attackCooldown) return;
        lastAttackTime = Time.time;

        anim.SetBool("Attack", true);
        StartCoroutine(ResetBoolNextFrame("Attack"));
    }

    /* Animation Events called from Attack clip */
    public void HitStart()
    {
        if (!pawCollider) return;
        pawCollider.enabled = true;
        var pd = pawCollider.GetComponent<PawDamage>();
        if (pd) pd.alreadyHit = false;
    }
    public void HitEnd()
    {
        if (pawCollider) pawCollider.enabled = false;
    }

    /* ================================================== */
    /* ---------------- Sniff helper -------------------- */
    IEnumerator SniffThenChase()
    {
        sniffDone   = true;
        sniffingNow = true;
        anim.SetBool("Sniff", true);
        anim.SetFloat("Speed", 0f);
        yield return new WaitForSeconds(sniffDuration);
        anim.SetBool("Sniff", false);
        sniffingNow = false;
    }

    IEnumerator ResetBoolNextFrame(string p)
    {
        yield return null;
        anim.SetBool(p, false);
    }

    /* Editor gizmo */
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}