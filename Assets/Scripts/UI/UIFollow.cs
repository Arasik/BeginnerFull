using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour {
    public Vector2 offset;
    public RectTransform rectTransform; // 跟随UI

    void Update() {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        rectTransform.position = screenPos + new Vector2(offset.x, offset.y);

        if (screenPos.x > Screen.width || screenPos.x < 0 || screenPos.y > Screen.height || screenPos.y < 0) 
            rectTransform.gameObject.SetActive(false);
        else 
            rectTransform.gameObject.SetActive(true);
    }

}
