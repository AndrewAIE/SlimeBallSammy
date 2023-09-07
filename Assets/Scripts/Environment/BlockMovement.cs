using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{

    private Vector3 m_position;

    private Rigidbody m_rb;

    public float m_fallSpeed;

    //public GameObject m_spawnBlocks;
    private SpawnBlocks m_sb;

    //private SpawnBlocks m_sb;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        //m_spawnBlocks = GetComponentInParent<GameObject>();
        m_sb = GetComponentInParent<SpawnBlocks>();
    }

    // Update is called once per frame
    void Update()
    {
        //m_rb.velocity = Vector3.down * m_fallSpeed;
        transform.Translate(Vector3.down * m_fallSpeed * Time.deltaTime);
        m_fallSpeed = m_sb.blockFallSpeed;
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
