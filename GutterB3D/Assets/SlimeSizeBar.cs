using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSizeBar : MonoBehaviour
{
    public float slimeSize, maxSize;

    [SerializeField] private SizeBar sizeBar;

    void Start() {
          if (sizeBar != null) {
            sizeBar.SetMaxSize(maxSize);
            sizeBar.SetSize(slimeSize);
        } else {
            Debug.LogError("SizeBar reference missing");
        }
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food")){
            SetSize(1f);
        }  
        

        if (other.CompareTag("Enemy")) {
            SetSize(-1f);
        }
    }

    void SetSize(float sizeChange) {
        slimeSize += sizeChange;
        slimeSize = Mathf.Clamp(slimeSize, 0, maxSize); //will not go past range
        sizeBar.SetSize(slimeSize); //updates UI size bar
    }
}
