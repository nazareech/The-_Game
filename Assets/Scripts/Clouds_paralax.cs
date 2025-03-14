﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Clouds_paralax : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;
    

    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size); 
    }
}
