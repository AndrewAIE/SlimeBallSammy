using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    GameObject m_sling;

    Vector2 m_playerPos;
    Vector2 m_slingPos;
    Vector2 m_slingVector;
    Vector2 m_slingDirection;

    bool m_dragging = false;

    [SerializeField]
    private float m_slingLimit;
    private float m_slingLength;
    private float m_deceleration;

    private void Start()
    {       
        m_sling = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        m_playerPos = transform.position;
    }

    public void OnMouseDown()
    {
        m_dragging = true;
    }

    public void OnMouseDrag()
    {
        if (m_dragging)
        {
            
            m_sling.SetActive(true);
            m_slingPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);            

            //Create vector between the Mouse Position and the Player, along with its magnitude and normalization
            m_slingVector = m_slingPos - m_playerPos;
            m_slingLength = m_slingVector.magnitude;
            m_slingDirection = m_slingVector.normalized;
            //Limit the length the sling can be
            if (m_slingLength > m_slingLimit)
            {
                m_slingLength = m_slingLimit;
            }                
            //Set sling "Handle" position
            m_sling.transform.position = m_playerPos + m_slingLength * m_slingDirection;
        }    

    }

    public void OnMouseUp()
    {
        m_dragging = false;
        m_sling.SetActive(false);
    }

    
}
