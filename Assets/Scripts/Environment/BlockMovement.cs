using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{

    private Vector3 m_position;

    private Rigidbody m_rb;

    [SerializeField]
    private float m_fallSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
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
            Destroy(gameObject);
        }   
    }
}
