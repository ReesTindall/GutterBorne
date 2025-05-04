using UnityEngine;

public class PawDamage : MonoBehaviour
{
    public int damage = 1;
    bool alreadyHit;

    void OnEnable()  => alreadyHit = false;   // reset each swing
    void OnTriggerEnter(Collider other)
    {
        if (alreadyHit) return;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<LoseSlime>()?.TakeHit();
            alreadyHit = true;               // only once per swing
        }
    }
}