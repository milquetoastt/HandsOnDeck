using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyShip : MonoBehaviour
{
    public int health = 1; //1 health to start
    public float speed = 1f;
    public int lane;
    public List<Vector3> laneStartCoords = new List<Vector3>();
    public List<Transform> laneTargets = new List<Transform>();
    public float fireSpeed = 10f;
    //public Transform target;

    private Coroutine enemyFireCoroutine;

    private void Awake()
    {
        lane = Random.Range(1, 4);
        transform.position = laneStartCoords[lane-1];
    }
    void Start()
    {
        Debug.Log("This ship will spawn in lane " + lane);
        StartCoroutine(EnemyFireCoroutine());
    }

    //if health is 0, destroy ship
    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this);
        }
        
    }

    void FixedUpdate()
    {
        //ship moves down a lane
        transform.position = Vector3.MoveTowards(
        transform.position,
        laneTargets[lane-1].position,
        speed * Time.deltaTime
    );
    }

    private IEnumerator EnemyFireCoroutine()
    {
        //this will repeat forever
        Debug.Log("waitin");
        yield return new WaitForSeconds(fireSpeed);
        Debug.Log("EnemyShip fired in lane " + lane);
        StartCoroutine(EnemyFireCoroutine());
    }
}
