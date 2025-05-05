using UnityEngine;

public class SlimeHurtTrigger : MonoBehaviour
{
    public float knockbackForce = 5f;
    public string damageType = "Person";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoseSlime slime = other.GetComponent<LoseSlime>();
            if (slime != null)
            {
                Vector3 hitDir = (other.transform.position - transform.position).normalized;
                slime.TakeHit(hitDir, knockbackForce, damageType);

                Slime_Scale scaler = other.GetComponent<Slime_Scale>();
                if (scaler != null)
                    scaler.isFlattenedFromCollision = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Slime_Scale scaler = other.GetComponent<Slime_Scale>();
            if (scaler != null)
                scaler.isFlattenedFromCollision = false;
        }
    }
}
