using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    public GameObject[] spawnBlocks;

    [SerializeField]
    private GameObject[] m_spawnPoints;

    private IEnumerator spawn;

    private bool m_canSpawn = true;

    public float blockFallSpeed;

    //To count frames before adjusting speed (This should be edited later to account for height)
    private float m_speedCounter;

    private bool m_startGame = true;

    private bool m_changeSpeed = true;

    private int m_spwn1;
    private int m_spwn2;

    private float m_shortestSpawnTime = 4.0f;
    private float m_longestSpawnTime = 5.5f;

    // Start is called before the first frame update
    void Start()
    {
        blockFallSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        spawn = SpawnBlock(m_shortestSpawnTime); //spawnBlocks[Random.Range(0, 7)], 
        if (m_canSpawn)
        {
            StartCoroutine(spawn);
        }

        if (m_startGame)
        {
            StartGame();
        }

        if (m_changeSpeed)
        {
            StartCoroutine(DifficultyChange());
        }
    }

    IEnumerator SpawnBlock(float time)
    {
        m_canSpawn = false;
        yield return new WaitForSeconds(time);
        //for (int i = 0; i < 2; i++)
        //{
        //    
        //}
        m_spwn1 = Random.Range(0, 5);
        m_spwn2 = Random.Range(0, 5);
        while (m_spwn2 == m_spwn1)
        {
            m_spwn2 = Random.Range(0, 5);
        }
        Instantiate(spawnBlocks[0], m_spawnPoints[m_spwn1].transform.position, Quaternion.identity, transform);
        Instantiate(spawnBlocks[Random.Range(0, 7)], m_spawnPoints[m_spwn2].transform.position, Quaternion.identity, transform);
        //Instantiate(block, transform.position, Quaternion.identity, transform);
        m_canSpawn = true;
        
    }

    IEnumerator DifficultyChange()
    {
        m_changeSpeed = false;
        Debug.Log("Script called");
        yield return new WaitForSeconds(20f);
        Debug.Log("Speed has changed");
        blockFallSpeed += 0.1f;
        m_shortestSpawnTime -= 0.1f;
        m_longestSpawnTime -= 0.1f;
        m_changeSpeed = true;
    }

    public void StartGame()
    {
        blockFallSpeed = 0.5f;
        m_startGame = false;
        m_changeSpeed = true;
    }

}
