using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    public GameObject[] spawnBlocks;

    private IEnumerator spawn;

    private bool m_canSpawn = true;

    public float blockFallSpeed;

    //private BlockMovement m_bm;

    //To count frames before adjusting speed (This should be edited later to account for height)
    private float m_speedCounter;

    // Start is called before the first frame update
    void Start()
    {
        blockFallSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        spawn = SpawnBlock(spawnBlocks[Random.Range(0, 7)], Random.Range(4.0f, 7.5f));
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
