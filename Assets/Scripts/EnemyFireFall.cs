using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireFall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Cannonball;
    private Vector3 spawnPos;
    void Start()
    {
        float xValue = Random.Range(-3.5f, 3.5f);
        float zValue = Random.Range(-1f, 1.6f);
        spawnPos = new Vector3(xValue, 10, zValue);
        Instantiate(Cannonball, spawnPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject);
    }
}
