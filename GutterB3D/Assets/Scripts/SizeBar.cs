using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeBar : MonoBehaviour
{
   public float slimeSize, maxSize, width, height;

   [SerializeField] private RectTransform sizeBar;

   public void SetMaxSize(float max) {
      maxSize = max;
   }

   public void SetSize(float size) {
      Debug.Log("slime size: ");
      Debug.Log(slimeSize);
      slimeSize = size;

        if (maxSize > 0) {
            float newWidth = (slimeSize / maxSize) * width;
            sizeBar.sizeDelta = new Vector2(newWidth, height);
            Debug.Log("new width: " + newWidth);
        } else {
            Debug.LogWarning("max size is 0 or negative");
        }
   }
}

