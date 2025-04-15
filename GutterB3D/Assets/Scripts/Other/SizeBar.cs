using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeBar : MonoBehaviour
{
   // public float width, height;

   [SerializeField] private Gradient gradient;
   [SerializeField] private Image image;
   [SerializeField] private Text text;

   public void SetSize(float ratio) {
         //change percent of image shown (to mimic slider)
         float fill = Mathf.Clamp01(ratio);
         image.fillAmount = fill;
         Debug.Log("new fill: " + fill);

         //change color gradient
         Color newColor = gradient.Evaluate(fill);
         image.color = newColor;
         text.color = newColor;
         Debug.Log("new color: " + fill);
   }
}

