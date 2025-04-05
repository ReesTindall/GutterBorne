using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeBar : MonoBehaviour
{
   public Slider slider;
   public Gradient gradient;
   
   public void SetSize(int size) {
      slider.value = size;
   }
}

