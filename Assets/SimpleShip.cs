using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShip : MonoBehaviour
{
    public int health = 1; //1 health to start
    public float speed = 1f;

    public GameObject firstLane;
    // Start is called before the first frame update
    void Start()
    {

    }

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
        firstLane.transform.localPosition, 
        speed * Time.deltaTime
    );
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCannonBall cannonBall = other.GetComponent<PlayerCannonBall>();
        if (cannonBall != null)
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

}
