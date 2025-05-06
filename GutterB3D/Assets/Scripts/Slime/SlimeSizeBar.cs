using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSizeBar : MonoBehaviour
{
    [SerializeField] private SizeBar sizeBar;

	private GameHandler handler;

	void Awake (){
		handler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
	}

    void Start() {
        float slimeSize = GameHandler.slimeSize;
        float maxSize = handler.maxSize;

          if (sizeBar != null) {
            float initialSize = GameHandler.slimeSize;
            sizeBar.SetSize(initialSize / handler.maxSize);
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
            SetSize(-2.5f);
        }

        if(other.CompareTag("Human"))
            SetSize(-5f);
    }

    public void SetSize(float sizeChange) {
        GameHandler.slimeSize += sizeChange;
        GameHandler.slimeSize = Mathf.Clamp(GameHandler.slimeSize, 0, handler.maxSize); //will not go past range
        
        float newRatio = GameHandler.slimeSize / handler.maxSize;
        sizeBar.SetSize(newRatio); //updates UI size bar
    }
}
