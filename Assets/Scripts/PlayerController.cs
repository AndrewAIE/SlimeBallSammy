using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    GameObject m_sling;

    Vector2 m_playerPos;
    Vector2 m_slingPos;
    Vector2 m_slingVector;
    Vector2 m_slingDirection;
    Vector3 m_worldSize;

    bool m_dragging = false;

    [SerializeField]
    private float m_slingLimit;
    private float m_slingLength;

    
        
    enum State
    {
        Stuck,
        Stretch,
        Flying,
        Falling,
        Dying
    }
    private State m_state = State.Falling;


    private Rigidbody2D m_playerBody;

    private void Start()
    {
        m_sling = transform.GetChild(0).gameObject;
        m_playerBody = GetComponent<Rigidbody2D>();
        m_worldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    private void Update()
    {
        //update player position every frame
        m_playerPos = transform.position;
        switch (m_state)
        {
            case State.Stuck:
                Stuck();
                break;
            case State.Stretch:
                Stretch();
                break;
            case State.Flying:
                Flying();
                break;
            case State.Falling:
                Falling();
                break;
            case State.Dying:
                Dying();
                break;
        }
        
        //if player leaves the side edges of the screen, they will appear on the other side
        if (m_playerPos.x > m_worldSize.x + transform.localScale.x)
        {
            gameObject.transform.position = new Vector3(-m_worldSize.x, m_playerPos.y, 0);
        }
        if (m_playerPos.x < -m_worldSize.x - transform.localScale.x)
        {
            gameObject.transform.position = new Vector3(m_worldSize.x, m_playerPos.y, 0);
        }
        if (m_playerPos.y < -m_worldSize.y)
        {
            Dying();
        }
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
            //Set Vector to new length and set sling "Handle" position
            m_slingVector = m_slingLength * m_slingDirection;
            m_sling.transform.position = m_playerPos + m_slingVector;
        }
    }

    public void OnMouseUp()
    {
        m_playerBody.AddForce(m_slingVector * -1, ForceMode2D.Impulse);
        m_dragging = false;

        m_sling.SetActive(false);
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject box = hit.gameObject;
        Attach(box);
    }

    public void Attach(GameObject box)
    {
        transform.SetParent(box.transform, false);

    }

    
    public void Stuck()
    {

    }

    public void Stretch()
    {

    }

    public void Flying()
    {

    }

    public void Falling()
    {

    }

    public void Dying()
    {
        transform.position = new Vector3(0, 0, 0);
        m_playerBody.velocity = Vector2.zero;
        //m_state = 
    }



}
