using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{

    private Vector3 m_position;

    private Rigidbody m_rb;

    public float m_fallSpeed;

    //private SpawnBlocks m_sb;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        //m_sb.GetComponent<SpawnBlocks>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rb.velocity = Vector3.down * m_fallSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KillZone")
        {
            if(GetComponentInChildren<Transform>() != null)
            {
                GetComponentInChildren<PlayerController>().Detach();
            }

            Destroy(gameObject);
        }   
    }
}
