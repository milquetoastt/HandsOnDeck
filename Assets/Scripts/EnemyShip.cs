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
    public MultiRendererOutline warningOutline;
    public AudioSource audioSource;
    public float elapsed = 0f;

    public AudioClip deathSound;
    public AudioClip sinkSound;

    public GameObject explosionShipPrefab;

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
        //StartCoroutine(EnemyFireCoroutine());  <- OLD METHOD. UNCOMMENT TO USE OLD CANNONBALL SYSTEM
        StartCoroutine(StartLightCoroutine());
        if (lane == 3)
        {
            transform.rotation = Quaternion.Euler(0f, 128f, 0f);
        }
        if(lane == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 52f, 0f);
        }
    }

    private IEnumerator StartLightCoroutine()
    {
        
        yield return new WaitForSeconds(13f);
        warningOutline.ToggleOutline();
        StartCoroutine(FlashyCoroutine());
    }

    private IEnumerator FlashyCoroutine()
    {
        Debug.Log("turnin on da lights here");
        yield return new WaitForSeconds(1f);
        warningOutline.ToggleOutline();
        StartCoroutine(FlashyCoroutine());
    }

    //if health is 0, destroy ship
    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        elapsed += Time.deltaTime;
        //Debug.Log(elapsed);
    }

    void FixedUpdate(   )
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
            FindFirstObjectByType<ScreenShake>().Shake(0.25f, 0.4f, 30f);
            Instantiate(explosionShipPrefab, transform.position, transform.rotation);
            RankChange();
            //audioSource.PlayOneShot(deathSound);
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            Destroy(this.gameObject);
        }

        PlayerCannonBall cannonBall = other.GetComponent<PlayerCannonBall>();
        if (cannonBall != null)
        {
            //audioSource.PlayOneShot(deathSound);
            //audioSource.PlayOneShot(sinkSound);
            Instantiate(explosionShipPrefab, transform.position, transform.rotation);
            GameManager.Instance.UpCounter();
            if (elapsed < 10)
            {
                GameManager.Instance.AddPoints(100);
            }
            else if (elapsed > 10 && elapsed < 15)
            {
                GameManager.Instance.AddPoints(50);
            }
            else
            {
                GameManager.Instance.AddPoints(20);
            }
            if (deathSound != null)
            {
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
            }
            if (sinkSound != null)
            {
                AudioSource.PlayClipAtPoint(sinkSound, transform.position);
            }
            Destroy(other.gameObject);
            RankChange();

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
