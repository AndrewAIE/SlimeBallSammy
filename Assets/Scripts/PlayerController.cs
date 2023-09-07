using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

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
    [SerializeField]
    private float m_slingPower;
    private float m_slingLength;
    private string m_collisionTag;
    
    private Animator m_animation;
    SphereCollider m_collider;
    
    enum State
    {
        Stuck,
        Stretch,
        Flying,
        Falling,
        Dying
    }
    private State m_state = State.Stuck;


    private Rigidbody m_playerBody;

    private void Start()
    {
        m_sling = transform.GetChild(0).gameObject;
        m_playerBody = GetComponent<Rigidbody>();
        m_worldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        m_collider = GetComponent<SphereCollider>();
        m_animation = GetComponent<Animator>();
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

        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("State = " + m_state);
        }
    }
    
    public void Stuck()
    {
        m_animation.Play("StickDown");
        if(Input.GetMouseButtonDown(0))
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
        //if there are any inconsistencies with physics, call physics in Fixed Update
        if(Input.GetMouseButtonUp(0))
        {
               
            m_playerBody.AddForce(m_slingVector * -m_slingPower, ForceMode.Impulse);
            m_sling.SetActive(false);            
            m_state = State.Flying;
        }        
    }

    public void Flying()
    {        
        m_animation.Play("FlyAnimation");
        transform.up = m_playerBody.velocity.normalized;
        transform.localScale = Vector3.one * 0.8f;
    }

    public void Falling()
    {
        
        m_animation.Play("FallAnimation");
    }

    public void Dying()
    {
        m_animation.Play("DeathAnimation");        
        m_collider.enabled = false;
        m_playerBody.useGravity = false;
        m_playerBody.velocity = Vector2.zero;
        StartCoroutine(ResetPosition());
    }

    public void OnCollisionEnter(Collision other)
    {
        //check what type of block the player is colliding with
        m_collisionTag = other.gameObject.tag;
        
        switch(m_collisionTag)
        {
            case "Block":
                m_state = State.Stuck;
                m_playerBody.useGravity = false;
                m_playerBody.velocity = Vector3.zero;
                transform.up = other.contacts[0].normal;
                transform.SetParent(other.transform, true);
                break;
           case "Ice":
                m_state = State.Stuck;
                transform.SetParent(other.transform, true);
                transform.up = other.contacts[0].normal;
                break;
           case "Break":
                m_state = State.Stuck;
                m_playerBody.useGravity = false;
                m_playerBody.velocity = Vector3.zero;
                transform.up = other.contacts[0].normal;
                transform.SetParent(other.transform, true);
                StartCoroutine(DestroyBlock(other.gameObject));
                break;
           case "Death":
                m_state = State.Dying;
                break;
        }
    }
   
   
   
    public void OnCollisionExit(Collision other)
    {
        m_playerBody.useGravity = true;
        transform.SetParent(null);
    }


    IEnumerator DestroyBlock(GameObject block)
    {        
        //wait half a second, and then destroy block
        yield return new WaitForSecondsRealtime(0.5f);
        //if player is still attached to block, detach
        if(block == transform.parent.gameObject)
        {
            Detach();
        }
        Destroy(block);
    }
    IEnumerator ResetPosition()
    {
        yield return new WaitForSecondsRealtime(2f);
        m_collider.enabled = true;
        m_playerBody.useGravity = true;
        transform.position = new Vector3(0, -2, 0);
        m_playerBody.velocity = Vector2.zero;
        m_state = State.Falling;
    }

    public void Detach()
    {
        //remove block as parent, and enter falling state
        transform.SetParent(null);
        m_playerBody.useGravity = true;
        m_state = State.Falling;
        if (m_sling.activeInHierarchy== true)
        {
            m_sling.SetActive(false);
        }
    }


}
