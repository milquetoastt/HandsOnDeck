using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //List<GameObject> heart = new List<GameObject>();
    public GameObject[] heart = new GameObject[10];
    public PlayerShip deck;
    int numHearts;

    public Sprite emptyHeart;

    public GameObject loseScreen;
    // Start is called before the first frame update
    void Start()
    {
        //NEED TO ADD instanciate hearts
    }

    // Update is called once per frame
    void Update()
    {
        numHearts = deck.GetHealth() / 10;
        //if num hearts less than 10
            //THEN heart[numHearts] change sprite
        if (numHearts < 10) 
        {
            heart[numHearts].GetComponent<Image>().sprite = emptyHeart;
        }

        if (deck.GetHealth() == 0)
        {
            loseScreen.SetActive(true);
            Time.timeScale = 0;
        }

    }
}
