using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyShip enemyShip = other.GetComponent<EnemyShip>();
        if (enemyShip != null)
        {
            health -= 10;
            Debug.Log("EnemyShip hit player ship! Minus one health for a total of :" + health + " health");
            
        }

        
    }

    public void DamageShip()
    {
        health -= 1;
    }

    public int GetHealth()
    {
        return health;
    }
}
