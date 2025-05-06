using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    Text txt;                  
    public RatKingController king;          

    void Awake() => txt = GetComponent<Text>();

    void Update()
    {
        if (!king) return;

        txt.text = $"Wave {king.CurrentWave}/{king.TotalWaves}\n" +
               $"Rats left: {king.ActiveRats}";
    }
}