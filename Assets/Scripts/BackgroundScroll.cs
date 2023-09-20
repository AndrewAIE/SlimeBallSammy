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

    // Start is called before the first frame update
    void Start()
    {
        height = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {        
        _image.uvRect = new Rect(_image.uvRect.position  + new Vector2(_x, _y) * Time.deltaTime, _image.uvRect.size);
    }
}
