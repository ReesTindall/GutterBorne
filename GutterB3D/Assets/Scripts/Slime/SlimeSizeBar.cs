using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSizeBar : MonoBehaviour
{
    [SerializeField] private SizeBar sizeBar;

    void Start() {
        float slimeSize = GameHandler.handler.slimeSize;
        float maxSize = GameHandler.handler.maxSize;

          if (sizeBar != null) {
            float initialSize = GameHandler.handler.slimeSize;
            sizeBar.SetSize(initialSize / GameHandler.handler.maxSize);
        } else {
            Debug.LogError("SizeBar reference missing");
        }
    }

     void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Food")){
            SetSize(1f);
        }  

        if(other.CompareTag("Enemy")){
            SetSize(-1f);
        }
    }

    void SetSize(float sizeChange) {
        GameHandler.handler.slimeSize += sizeChange;
        GameHandler.handler.slimeSize = Mathf.Clamp(GameHandler.handler.slimeSize, 0, GameHandler.handler.maxSize); //will not go past range
        
        float newRatio = GameHandler.handler.slimeSize / GameHandler.handler.maxSize;
        sizeBar.SetSize(newRatio); //updates UI size bar
    }
}
