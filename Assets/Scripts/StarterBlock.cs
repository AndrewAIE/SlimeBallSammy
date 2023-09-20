using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterBlock : MonoBehaviour
{
    public float m_fallSpeed;
    bool m_isMoving = false;
    float m_timer = 0;
    [SerializeField]
    float m_startTimer;

    Color newColour;

    Material newMaterial;

    Renderer blockRenderer;

    private void Start()
    {
        blockRenderer = gameObject.GetComponent<Renderer>();
        newMaterial = new Material(blockRenderer.material);
    }

    // Update is called once per frame
    void Update()
    {
        //Count up timer, once it has reached the desired number, start moving blocks
        if(m_isMoving)
        {
            transform.Translate(Vector3.down * m_fallSpeed * Time.deltaTime);            
        }
        else
        {
            m_timer += Time.deltaTime;
            if (m_timer >= m_startTimer)
            {
                m_isMoving = true;
            }            
        }
    }

    /// <summary>
    /// If it's in the Kill Zone, it destroys itself
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "KillZone")
        {
            Destroy(gameObject);
            if (GetComponentInChildren<Transform>() != null)
            {
                GetComponentInChildren<PlayerController>().Detach();
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        newColour = new Color(0.75f, 1, 0.75f, 1);
        newMaterial.SetColor("_Color", newColour);
        blockRenderer.material = newMaterial;
    }
    public void OnCollisionExit(Collision collision)
    {
        newColour = new Color(1, 1, 1, 1);
        newMaterial.SetColor("_Color", newColour);
        blockRenderer.material = newMaterial;
    }

}
