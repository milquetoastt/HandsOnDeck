using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public Player player1;
    public Player player2;
    
    public GameObject arrow1;
    public GameObject arrow2;

    public GameObject shootArrow;

    public SimpleShip simpleShip;
    public GameObject tentacle; 
    public GameObject ship;
    public Cannon cannon;


    public TMP_Text tutorialText; 
    public enum TutorialStep
    {
        Start,
        Shoot,
        Move,
        End
    }

    public TutorialStep currentStep = TutorialStep.Start;
 
    void Start()
    {
        player1.alive = false;//to make sure they are dead
        player2.alive = false;

        player1.Die();//to change assets
        player2.Die();

        tentacle.SetActive(false);

        arrow1.SetActive(true);
        arrow2.SetActive(true);
        shootArrow.SetActive(false);

        //GameManager.Instance.StopSpawning();
    }
    void Update()
    {

        if (currentStep == TutorialStep.Start)
        {

            if (player1.alive == true && player2.alive == true)
            {
                NextStep();
                //Debug.Log("STEP ONE COMPLETE");
            }
        }

        if (currentStep == TutorialStep.Shoot && Input.GetKeyDown(KeyCode.Space) && cannon.canFire)//will have to fix for: && cannon.canFire == true
        {
            NextStep();
            //Debug.Log("STEP TWO COMPLETE");
        }

        if (currentStep == TutorialStep.Move && simpleShip == null)
        {
            NextStep();
            //Debug.Log("STEP THREE COMPLETE");
        }
    }

    public void NextStep()
    {
        currentStep = (TutorialStep)((int)currentStep + 1);

        switch (currentStep)
        {
            case TutorialStep.Start:
                tutorialText.text = "Step here to revive your pirate!";
                break;
            case TutorialStep.Shoot:
                tutorialText.text = "Press the button in the barrel to shoot. One player must be next to the cannon and the other must shoot.";
                arrow1.SetActive(false);
                arrow2.SetActive(false);

                tentacle.SetActive(true);
                shootArrow.SetActive(true);
                break;
            case TutorialStep.Move:
                tutorialText.text = "Move the cannon to shoot the ship!";
                shootArrow.SetActive(false);
                ship.SetActive(true);
                break;
            case TutorialStep.End:
                tutorialText.text = "Good Luck Pirates!";
                StartCoroutine(startGame());
                break;
        }
    }
    
    public IEnumerator startGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
        //load next scene
    }
}
