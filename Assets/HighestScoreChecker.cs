using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HighestScoreChecker : MonoBehaviour
{
    string highestScore;
    public string nameFile;
    public TMP_Text highscoretext;

    // Start is called before the first frame update
    void Start()
    {
        /*
        if (HandleText.ReadString(nameFile)==null)
        {
            HandleText.WriteString(nameFile, "0");
        }
        */
        HandleText.ReadOrCreateString(nameFile);

        highscoretext = GetComponent<TMP_Text>();
        highestScore = HandleText.ReadString(nameFile);
        highscoretext.text = "Highest Score: " + highestScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
