using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject enemyShipPrefab;

    public List<GameObject> lane1s;
    public List<GameObject> lane2s;
    public List<GameObject> lane3s;

    public BrickSpawner brickSpawner;
    public float patternInterval = 10f; // time between pattern drops

    private List<int> availablePatterns = new List<int>();
    private int totalPatterns;

    public GameObject Ammo;
    private Vector3 spawnPos;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        totalPatterns = brickSpawner.GetPatternCount();
        ResetPatternPool();
        StartCoroutine(PatternDropLoop());
        StartCoroutine(SpawnAShip());
        StartCoroutine(SpawnAmmo());
    }

    private IEnumerator SpawnAShip()
    {

        GameObject instance = Instantiate(enemyShipPrefab);
        EnemyShip enemyShip = instance.GetComponent<EnemyShip>();
        int lane = enemyShip.lane;
        //Debug.Log("New ship spawned in lane " + lane);
        if (lane == 1)
        {
            lane1s.Add(instance);
            Debug.Log(lane1s);
        }
        else if(lane ==2)
        {
            lane2s.Add(instance);
        }
        else
        {
            lane3s.Add(instance);
        }
        yield return new WaitForSeconds(10);
        StartCoroutine(SpawnAShip());
    }

    public void ShiftLane1()
    {
        if (lane1s.Count > 0)
        {
            lane1s.RemoveAt(0);
        }
    }
    public void ShiftLane2()
    {
        if (lane2s.Count > 0)
        {
            lane2s.RemoveAt(0);
        }
    }
    public void ShiftLane3()
    {
        if (lane3s.Count > 0)
        {
            lane3s.RemoveAt(0);
        }
    }

    private IEnumerator PatternDropLoop()
    {
        while (true)
        {
            int chosenIndex = ChooseNextPattern();
            brickSpawner.SpawnPattern(chosenIndex);
            yield return new WaitForSeconds(patternInterval);
        }
    }

    private int ChooseNextPattern()
    {
        if (availablePatterns.Count == 0)
            ResetPatternPool();

        int randomIndex = Random.Range(0, availablePatterns.Count);
        int chosenPattern = availablePatterns[randomIndex];
        availablePatterns.RemoveAt(randomIndex);

        Debug.Log("Chosen pattern: " + chosenPattern);
        return chosenPattern;
    }

    private void ResetPatternPool()
    {
        availablePatterns.Clear();
        for (int i = 0; i < totalPatterns; i++)
        {
            availablePatterns.Add(i);
        }
        Debug.Log("Pattern pool reset");
    }

    private IEnumerator SpawnAmmo()
    {
        float xValue = Random.Range(-3f, 3f);
        float zValue = Random.Range(-3f, 0.12f);
        spawnPos = new Vector3(xValue, 0.74f, zValue);
        Instantiate(Ammo, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(10);
        StartCoroutine(SpawnAmmo());
    }

}
