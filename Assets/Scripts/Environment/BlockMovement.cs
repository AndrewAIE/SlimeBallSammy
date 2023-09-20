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

    private Renderer blockRenderer;
    private Color newColour;
    private Material newMaterial;

    //private SpawnBlocks m_sb;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        //m_spawnBlocks = GetComponentInParent<GameObject>();
        m_sb = GetComponentInParent<SpawnBlocks>();

        blockRenderer = gameObject.GetComponent<Renderer>();
        newMaterial = new Material(blockRenderer.material);
        

        if(tag == "Fake")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.6f);
        }

        //var width = Camera.main.orthographicSize * 2.0 * Screen.width / Screen.height;
        //Debug.Log("Width = " + width);
        //transform.localScale = new Vector3(((float)width) / 9.0f, ((float)width) / 9.0f, ((float)width) / 9.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //m_rb.velocity = Vector3.down * m_fallSpeed;
        transform.Translate(Vector3.down * m_fallSpeed * Time.deltaTime);
        m_fallSpeed = m_sb.blockFallSpeed;
    }

    /// <summary>
    /// OnTriggerEnter:
    /// Does two functions.
    /// Firstly it checks if blocks are entering the killzone and then if they are it willdestroy the block
    /// 
    /// Otherwise it will check to see if the player has touched a Fake block and then run the "FadeBlock" coroutine.
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "KillZone")
        {            
            Destroy(gameObject);
            if (GetComponentInChildren<Transform>() != null)
            {
                GetComponentInChildren<PlayerController>().Detach();
            }            
        }   

        if (gameObject.tag == "Fake")
        {
            StartCoroutine(FadeBlock(gameObject));
        }
    }

    /// <summary>
    /// FadeBlock:
    /// 
    /// This function runs a loop for the fake blocks. Where every 0.05 seconds the block will change its opacity by 10% until it is invisible.
    /// The block will then be destroyed to prevent this code running on an invisible block and causing a visual bug.
    /// </summary>
    /// <param name="block"></param>
    /// <returns></returns>
    IEnumerator FadeBlock(GameObject block)
    {
        //Material mat = block.GetComponent<Material>();
        float fade = 1;

        for (int i = 0; i < 11; i++)
        {
            Debug.Log("Womp " + newColour);

            newColour = new Color(0.745f, 0.745f, 0.745f, fade);
            newMaterial.SetColor("_Color", newColour);
            blockRenderer.material = newMaterial;
            fade -= 0.1f;
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(block);
    }

    /// <summary>
    /// OnCollisionEnter:
    /// 
    /// When the player collides with a block, the block will change colour to take on a green tint. This is to easily show which block Sammy is on.
    /// This will help people on mobile as their screens cut off some of the gameplay.
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        newColour = new Color(0.75f, 1, 0.75f, 1);
        newMaterial.SetColor("_Color", newColour);
        Debug.Log("Bolcok change colluuruj NOW");
        blockRenderer.material = newMaterial;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionExit(Collision collision)
    {
        newColour = new Color(1, 1, 1, 1);
        newMaterial.SetColor("_Color", newColour);
        Debug.Log("Back to normal");
        blockRenderer.material = newMaterial;
    }

}
