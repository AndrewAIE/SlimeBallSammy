using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    public GameObject[] testSpawn;

    private IEnumerator spawn;

    private bool m_canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawn = SpawnBlock(testSpawn[Random.Range(0, 4)], Random.Range(2.0f, 7.5f));
        if (m_canSpawn)
        {
            StartCoroutine(spawn);
        }
        
    }

    IEnumerator SpawnBlock(GameObject block, float time)
    {
        m_canSpawn = false;
        yield return new WaitForSeconds(time);
        Instantiate(block, transform.position, Quaternion.identity);
        m_canSpawn = true;
        
    }

}
