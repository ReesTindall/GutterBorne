using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivineSlimeCounter : MonoBehaviour
{
    [SerializeField] private Text counter;

    public float totalDivineSlime = 0f;
    private float currDivineSlime;
    private float slime = 1f;
    

    void OnAwake() {
        //reset variables
        currDivineSlime = 0f;
    }

    void Start() {
        if(counter != null) {
            setCounter(currDivineSlime + "/" + totalDivineSlime);
        } else {
            Debug.Log("Counter missing reference!");
        }
    }

    public void setSlime() {
        Debug.Log("curr slime:" + currDivineSlime);

        currDivineSlime += slime;
        currDivineSlime = Mathf.Clamp(currDivineSlime, 0, totalDivineSlime); //will not go past range
        Debug.Log("new slime:" + currDivineSlime);

        string newCount = currDivineSlime.ToString() + "/" + totalDivineSlime.ToString();
        Debug.Log("new count: " + newCount);
        setCounter(newCount);
    }

    void setCounter(string count) {
        counter.text = count;
        Debug.Log("counter text: " + counter.text);
    }

}

