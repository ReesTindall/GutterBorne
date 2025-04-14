using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeBar : MonoBehaviour
{
   public float slimeSize, maxSize, width, height;

   [SerializeField] private Gradient gradient;
   [SerializeField] private Image image;
   [SerializeField] private Text text;

   public void SetMaxSize(float max) {
      maxSize = max;
   }

   public void SetSize(float size) {
      slimeSize = size;

      //change percent of image shown (to mimic slider)
      if(maxSize > 0) {
         float newFill= slimeSize / maxSize;
         image.fillAmount = newFill;
         // Debug.Log("new fill: " + newFill);

         //change color gradient
         Color newColor = gradient.Evaluate(newFill);
         image.color = newColor;
         text.color = newColor;
         // Debug.Log("new color: " + newFill);
      }
   }
}

