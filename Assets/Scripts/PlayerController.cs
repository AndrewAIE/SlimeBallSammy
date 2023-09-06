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

    [SerializeField]
    private float m_slingLimit;
    private float m_slingLength;

    public BoxCollider2D m_boxCollider;

    public string m_collisionTag;
    
        
    enum State
    {
        Stuck,
        Stretch,
        Flying,
        Falling,
        Dying
    }
    private State m_state = State.Stuck;


    private Rigidbody2D m_playerBody;

    private void Start()
    {
        m_sling = transform.GetChild(0).gameObject;
        m_playerBody = GetComponent<Rigidbody2D>();
        m_worldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        m_boxCollider = GetComponent<BoxCollider2D>();
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
            m_state = State.Dying;
        }
    }
    

    

    public void Attach(GameObject box)
    {
        transform.SetParent(box.transform, false);

    }

    
    public void Stuck()
    {
        if(Input.GetMouseButton(0))
        {
            m_state = State.Stretch;
        }
    }

    public void Stretch()
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
        //On mouse up, remove from parent, add force and change state and turn off sling indicator
        //if thre are any inconsistencies with physics, call physics in Fixed Update
        if(Input.GetMouseButtonUp(0) && m_slingPos.y <= m_playerPos.y)
        {
            m_playerBody.AddForce(m_slingVector * -1, ForceMode2D.Impulse);
            m_sling.SetActive(false);
            m_state = State.Flying;
        }
        if (Input.GetMouseButtonUp(0) && m_slingPos.y > m_playerPos.y)
        {
            m_playerBody.AddForce(m_slingVector * -1, ForceMode2D.Impulse);
            m_sling.SetActive(false);
            m_state = State.Falling;
        }
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
    }

    

    
    

}
