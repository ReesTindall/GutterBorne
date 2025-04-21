using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCounter : MonoBehaviour
{
   [SerializeField] private Text counterBanana;
   [SerializeField] private Text counterGum;
   [SerializeField] private Text counterCup;
   [SerializeField] private Text counterPaper;

   [SerializeField] private Image iconBanana;
   [SerializeField] private Image iconGum;
   [SerializeField] private Image iconCup;
   [SerializeField] private Image iconPaper;


    public float totalBanana = 0f;
    public float currBanana;
    public float totalPaper = 0f;
    public float currPaper;
    public float totalGum = 0f;
    public float currGum;
    public float totalCup = 0f;
    public float currCup;

    private float addedTrash = 1f;
    

    void OnAwake() {
        //reset variables
        currBanana = 0f;
        currCup = 0f;
        currGum = 0f;
        currPaper = 0f;
    }

    void Start() {
        if(counterBanana != null) { //there is trash to count
            if(totalBanana != 0) {
                setBananaCounter(currBanana + "/" + totalBanana);
            } else {
                currBanana = 1f; //"complete" ratio for enterGrate 
                totalBanana = 1f;
                setBananaCounter(""); //no count
                iconBanana.enabled = false;
            }
        } else {
            Debug.Log("No banana counter.");
        }

        if(counterGum != null) { //there is trash to count
            if(totalGum != 0) {
                setGumCounter(currGum + "/" + totalGum);
            } else {
                currGum = 1f; //"complete" ratio for enterGrate 
                totalGum = 1f;
                setGumCounter(""); //no count
                iconGum.enabled = false;
            }
        } else {
            Debug.Log("No Gum counter.");
        }

        if(counterCup != null) { //there is trash to count
            if(totalCup != 0) {
                setCupCounter(currCup + "/" + totalCup);
            } else {
                currCup = 1f; //"complete" ratio for enterGrate 
                totalCup = 1f;
                setCupCounter("");
                iconCup.enabled = false;
            }
        } else {
            Debug.Log("No Cup counter.");
        }

        if(counterPaper != null) { //there is trash to count
            if(totalPaper != 0) {
                setPaperCounter(currPaper + "/" + totalPaper);
            } else {
                currPaper = 1f; //"complete" ratio for enterGrate 
                totalPaper = 1f;
                setPaperCounter("");
                iconPaper.enabled = false;
            }
        } else {
            Debug.Log("No Paper counter.");
        }
    }

    public void setBanana() {

        currBanana += addedTrash;
        currBanana = Mathf.Clamp(currBanana, 0, totalBanana); //will not go past range

        string newCount = currBanana.ToString() + "/" + totalBanana.ToString();
        setBananaCounter(newCount);
    }

    public void setGum() {

        currGum += addedTrash;
        currGum = Mathf.Clamp(currGum, 0, totalGum); //will not go past range

        string newCount = currGum.ToString() + "/" + totalGum.ToString();
        setGumCounter(newCount);
    }

    public void setCup() {

        currCup += addedTrash;
        currCup = Mathf.Clamp(currCup, 0, totalCup); //will not go past range

        string newCount = currCup.ToString() + "/" + totalCup.ToString();
        setCupCounter(newCount);
    }

    public void setPaper() {

        currPaper += addedTrash;
        currPaper = Mathf.Clamp(currPaper, 0, totalPaper); //will not go past range

        string newCount = currPaper.ToString() + "/" + totalPaper.ToString();
        setPaperCounter(newCount);
    }

    void setBananaCounter(string count) {
        counterBanana.text = count;
    }

     void setGumCounter(string count) {
        counterGum.text = count;
    }

     void setCupCounter(string count) {
        counterCup.text = count;
    }

     void setPaperCounter(string count) {
        counterPaper.text = count;
    }
}
