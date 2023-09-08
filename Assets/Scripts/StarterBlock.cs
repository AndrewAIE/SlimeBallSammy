using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterBlock : MonoBehaviour
{
    public float m_fallSpeed;
    bool m_isMoving = false;
    float m_timer = 0;
    

    // Update is called once per frame
    void Update()
    {
        if(m_isMoving)
        {
            transform.Translate(Vector3.down * m_fallSpeed * Time.deltaTime);            
        }
        else
        {
            m_timer += Time.deltaTime;
            if (m_timer >= 2.5f)
            {
                m_isMoving = true;
            }            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KillZone")
        {
            if (GetComponentInChildren<Transform>() != null)
            {
                GetComponentInChildren<PlayerController>().Detach();
            }

            Destroy(gameObject);
        }
    }
}
