using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreChecker : MonoBehaviour
{
    public TMP_Text score;

    public void updateScore(int updatedScore)
    {
        score.text = updatedScore.ToString();
    }
}
