using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class AttackPatterns : MonoBehaviour
{
    // Start is called before the first frame update
    public int patternNum;
    private void Awake()
    {
        patternNum = Random.Range(1, 5); // four attacks (to start)
    }
    void Start()
    {
        if (patternNum == 1)
        {
            pattern1();
        }
        else if (patternNum == 2)
        {
            pattern2();
        }
        else if (patternNum == 3)
        {
            pattern3();
        }
        else if (patternNum == 4)
        {
            pattern4();
        }
    }

    void pattern1()
    {
        
    }
    void pattern2()
    {

    }
    void pattern3()
    {

    }
    void pattern4()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
