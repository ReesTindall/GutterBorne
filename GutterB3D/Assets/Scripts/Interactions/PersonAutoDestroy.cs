using UnityEngine;

public class PersonAutoDestroy : MonoBehaviour
{
    public float destroyZ = -10f; // Z position where the person is destroyed

    void Update()
    {
        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }
}
