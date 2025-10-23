using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("players lose!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player ship hit something");
        EnemyShip enemyShip = other.GetComponent<EnemyShip>();
        if (enemyShip != null)
        {
            health -= 1;
            Debug.Log("EnemyShip hit player ship! Minus one health for a total of :" + health + " health");
            
        }

        
    }
}
