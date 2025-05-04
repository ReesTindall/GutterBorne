using UnityEngine;

public class PawDamage : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<LoseSlime>()?.TakeHit();
    }
}