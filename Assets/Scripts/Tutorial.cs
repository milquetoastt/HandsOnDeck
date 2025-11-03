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
    public GameObject ship;


    public TMP_Text tutorialText; 
    public enum TutorialStep
    {
        Start,
        Shoot,
        Move,
        End
    }

    public TutorialStep currentStep = TutorialStep.Start;
    // Start is called before the first frame update
    void Start()
    {
        player1.alive = false;
        player2.alive = false;

        arrow1.SetActive(true);
        arrow2.SetActive(true);
        shootArrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (currentStep == TutorialStep.Start)
        {

            if (player1.alive == true && player2.alive == true)
            {
                NextStep();
                Debug.Log("STEP ONE COMPLETE");
            }
        }

        if (currentStep == TutorialStep.Shoot && Input.GetKeyDown(KeyCode.Space))
        {
            NextStep();
            Debug.Log("STEP TWO COMPLETE");
        }

        if (currentStep == TutorialStep.Move && simpleShip == null)
        {
            NextStep();
            Debug.Log("STEP THREE COMPLETE");
        }
    }

    public void NextStep()
    {
        currentStep = (TutorialStep)((int)currentStep + 1);

        switch (currentStep)
        {
            case TutorialStep.Start:
                tutorialText.text = "Step here to revive your pirate!";
                //setactive arrows
                break;
            case TutorialStep.Shoot:
                tutorialText.text = "Press the button in the barrel to shoot.";
                arrow1.SetActive(false);
                arrow2.SetActive(false);

                shootArrow.SetActive(true);
                //disable arrows
                break;
            case TutorialStep.Move:
                tutorialText.text = "Move the cannon to shoot the ship!";
                shootArrow.SetActive(false);
                ship.SetActive(true);
                ///spawn a ship cannt attack you
                //WHEN YOU KILL THE SHIP IT MOVES
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
