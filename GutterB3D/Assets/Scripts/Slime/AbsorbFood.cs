using UnityEngine;

public class AbsorbFood : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            transform.localScale *= 2;
            Destroy(other.gameObject);
        }
    }
}