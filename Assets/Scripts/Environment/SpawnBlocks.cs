using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    //List of blocks that can be spawned in, multiple of the regular blocks were added to make them more likely to spawn
    public GameObject[] spawnBlocks;

    //List of empty gameobjects for locations for the blocks to spawn
    [SerializeField]
    private GameObject[] m_spawnPoints;

    private IEnumerator spawn;

    private bool m_canSpawn = true;

    public float blockFallSpeed;

    private bool m_startGame = true;

    private bool m_changeSpeed = true;

    //Ints for the locations of the spawn points
    private int m_spwn1;
    private int m_spwn2;
    private int m_spwn3;

    //private float m_shortestSpawnTime = 4.0f;
    //private float m_longestSpawnTime = 5.5f;

    private float m_spawnTime = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        blockFallSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        spawn = SpawnBlock(m_spawnTime); //spawnBlocks[Random.Range(0, 7)], 
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

    /// <summary>
    /// SpawnBlock(float time):
    /// 
    /// This function will spawn the blocks for a layer in the game. It will wait for the spawn wait time before creating the blocks.
    /// After the timer is done it will pick the loactions of the three spawn points (A while loop is run to ensure the same spots are not picked twice).
    /// The function will then spawn in a regular block at point 1, the a random block at point 2. A random 1/5 chance is then rolled to see if a third block will spawn.
    /// If yes it will spawn a random block at point 3. The bool m_canSpawn is used to make sure spawning does not clog up the screen with blocks.
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator SpawnBlock(float time)
    {
        m_canSpawn = false;
        yield return new WaitForSeconds(time);

        m_spwn1 = Random.Range(0, 5);
        m_spwn2 = Random.Range(0, 5);
        m_spwn3 = Random.Range(0, 5);
        while (m_spwn2 == m_spwn1 || m_spwn2 == m_spwn3 || m_spwn3 == m_spwn1)
        {
            m_spwn2 = Random.Range(0, 5);
            m_spwn3 = Random.Range(0, 5);
        }
        Instantiate(spawnBlocks[0], m_spawnPoints[m_spwn1].transform.position, Quaternion.identity, transform);
        Instantiate(spawnBlocks[Random.Range(0, 7)], m_spawnPoints[m_spwn2].transform.position, Quaternion.identity, transform);
        int thrdBlckChance = Random.Range(0, 26);
        if (thrdBlckChance <= 5)
        {
            Instantiate(spawnBlocks[Random.Range(0, 7)], m_spawnPoints[m_spwn3].transform.position, Quaternion.identity, transform);
        }
        m_canSpawn = true;
        
    }

    /// <summary>
    /// DifficultyChange():
    /// 
    /// Every 20 seconds the block fall speed and the interval between layers spawning is changed to make blocks fall and spawn faster, increasing difficulty.
    /// </summary>
    /// <returns></returns>
    IEnumerator DifficultyChange()
    {
        m_changeSpeed = false;
        Debug.Log("Script called");
        yield return new WaitForSeconds(20f);
        Debug.Log("Speed has changed");
        blockFallSpeed += 0.1f;
        if (m_spawnTime >= 0.02f)
        {
            m_spawnTime -= 0.1f;
        }    
        m_changeSpeed = true;
    }

    /// <summary>
    /// StartGame():
    /// 
    /// This sets the fall speed and the spawn timer to its base levels.
    /// </summary>
    public void StartGame()
    {
        blockFallSpeed = 0.5f;
        m_spawnTime = 4.0f;
        m_startGame = false;
        m_changeSpeed = true;
    }

}
