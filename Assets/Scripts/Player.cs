using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public bool alive;
    private SpriteRenderer playerSprite;

    public float waitTime; 
    void Start()
    {
        alive = true; 
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //if fallen off hole/get hit by enemy
        if (!alive)
        {
            Die();
        }
    }
    
    public IEnumerator RespawnPlayer() //StartCoroutine(RespawnPlayer());
    {
        yield return new WaitForSeconds(waitTime);
        playerSprite.color = new Color(255, 0, 0, 255);
    }

    public void Die()
    {
        
        //also disable button press? Probably will just have to check if other player near cannon
        playerSprite.color = new Color(255,0,0,80);//make player a ghost? 

    }
}
