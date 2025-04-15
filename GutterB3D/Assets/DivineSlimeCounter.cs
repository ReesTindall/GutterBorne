using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivineSlimeCounter : MonoBehaviour
{
    [SerializeField] private Text counter;

    public float totalDivineSlime = 0f;
    public float currDivineSlime;
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

        currDivineSlime += slime;
        currDivineSlime = Mathf.Clamp(currDivineSlime, 0, totalDivineSlime); //will not go past range

        string newCount = currDivineSlime.ToString() + "/" + totalDivineSlime.ToString();
        setCounter(newCount);
    }

    void setCounter(string count) {
        counter.text = count;
    }

}

