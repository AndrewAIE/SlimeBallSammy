using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField]
    private RawImage _image;

    public float _x;
    public float _y;
    public float height;


    /// <summary>
    /// Collects screen height
    /// </summary>
    void Start()
    {
        height = Screen.height;
    }

    /// <summary>
    /// Finds the size of the image and reacts rectangle then loops it when the image is no longer in rectangle
    /// </summary>
    void Update()
    {
        
        _image.uvRect = new Rect(_image.uvRect.position  + new Vector2(_x, _y) * Time.deltaTime, _image.uvRect.size);
    }
}
