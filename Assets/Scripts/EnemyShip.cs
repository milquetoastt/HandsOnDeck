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
    public List<Vector3> laneTargets = new List<Vector3>();
    public float fireSpeed = 10f;

    public AudioSource audioSource;

    public AudioClip deathSound;
    public AudioClip sinkSound;

    public GameObject CannonFire;
    //public Transform target;

    private Coroutine enemyFireCoroutine;

    private void Awake()
    {
        lane = Random.Range(1, 4);
        transform.position = laneStartCoords[lane-1];
    }
    void Start()
    {
        //Debug.Log("This ship will spawn in lane " + lane);
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
        laneTargets[lane-1],
        speed * Time.deltaTime
    );
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENemey ship hit something");
        PlayerShip playership = other.GetComponent<PlayerShip>();
        if (playership != null)
        {
            RankChange();
            audioSource.PlayOneShot(deathSound);
            Destroy(this.gameObject);
        }

        EnemyCannonBall cannonBall = other.GetComponent<EnemyCannonBall>();
        if(cannonBall != null)
        {
            Destroy(other.gameObject);
            RankChange();
            audioSource.PlayOneShot(deathSound);
            audioSource.PlayOneShot(sinkSound);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator EnemyFireCoroutine()
    {
        //this will repeat forever
        //Debug.Log("waitin");
        yield return new WaitForSeconds(fireSpeed);
        Instantiate(CannonFire);
        //Debug.Log("EnemyShip fired in lane " + lane);
        StartCoroutine(EnemyFireCoroutine());
    }
    
    private void RankChange()
    {
        if (lane == 1) { GameManager.Instance.ShiftLane1(); }
        if (lane == 2) { GameManager.Instance.ShiftLane2(); }
        if (lane == 3) { GameManager.Instance.ShiftLane3(); }
    }
}
