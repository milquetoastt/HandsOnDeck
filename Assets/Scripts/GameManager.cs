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

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(SpawnAShip());
    }

    private IEnumerator SpawnAShip()
    {

        GameObject instance = Instantiate(enemyShipPrefab);
        EnemyShip enemyShip = instance.GetComponent<EnemyShip>();
        int lane = enemyShip.lane;
        Debug.Log("New ship spawned in lane " + lane);
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

    public void StopSpawning()
    {
        StopAllCoroutines();
    }
}
