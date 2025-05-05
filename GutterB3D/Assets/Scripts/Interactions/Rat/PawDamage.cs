using UnityEngine;

public class PawDamage : MonoBehaviour
{
    public int damage = 1;
    [HideInInspector] public bool alreadyHit;

    public string damageType = "Rat"; 


    void OnEnable() => alreadyHit = false; // Reset for each swing

    void OnTriggerEnter(Collider other)
    {
        if (alreadyHit) return;
        if (other.CompareTag("Player"))
        {
            Vector3 dir = other.transform.position - transform.root.position;

            LoseSlime ls = other.GetComponent<LoseSlime>();
            if (ls != null)
            {
                ls.TakeHit(dir.normalized, 6f, damageType); 
                alreadyHit = true;
            }
        }
    }
}





// using UnityEngine;

// public class PawDamage : MonoBehaviour
// {
//     public int damage = 1;
//     [HideInInspector] public bool alreadyHit;

//     void OnEnable()  => alreadyHit = false;   // reset each swing
//     void OnTriggerEnter(Collider other)
//     {
//         if (alreadyHit) return;
//         if (other.CompareTag("Player"))
        
//         {
//             Vector3 dir = other.transform.position - transform.root.position;
//             // LoseSlime ls = other.GetComponent<LoseSlime>();
//             // ls?.TakeHit();                               // shrink

//             //BlobMovementRelative mover = other.GetComponent<BlobMovementRelative>();
//             other.GetComponent<LoseSlime>()?.TakeHit(dir, 6f);
//             alreadyHit = true;               // only once per swing
//         }
//     }
// }