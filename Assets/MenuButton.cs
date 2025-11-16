using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuButton : MonoBehaviour
{
    private Coroutine confirmTimeRoutine;
    public float waitTime;
    public bool comfirmPlayer;

    private Animator buttonAnimator;
    private string playerNum;

    // Start is called before the first frame update
    void Start()
    {
        confirmTimeRoutine = null;
        comfirmPlayer = false;
        buttonAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && confirmTimeRoutine == null)
        {
            playerNum = collision.name;
            confirmTimeRoutine = StartCoroutine(comfirmTime());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if (confirmTimeRoutine != null)
            {
                //Debug.Log("Player exited spawn. Stopping coroutine.");
                StopCoroutine(confirmTimeRoutine);
                
                //stop animation
                confirmTimeRoutine = null;
            }
            comfirmPlayer = false;

            buttonAnimator.SetBool("comfirm", false);
        }
    }

    public IEnumerator comfirmTime()
    {
        //play animation time closing circle
        buttonAnimator.SetBool("comfirm", true);

        yield return new WaitForSeconds(waitTime);
        //play full circle
        comfirmPlayer = true;
        if (playerNum == "Player")
        {
            buttonAnimator.SetBool("blue", true);
        }else if ((playerNum == "Player2"))
        {
            buttonAnimator.SetBool("pink", true);
        }
        


        confirmTimeRoutine = null;


    }
}
