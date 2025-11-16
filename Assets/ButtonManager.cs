using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonManager : MonoBehaviour
{
    public MenuButton button1;
    public MenuButton button2;

    public LevelLoader levelLoader;
    public int LoadSceneNum;
    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.Find("SceneLoader").GetComponent<LevelLoader>();

    }

    // Update is called once per frame
    void Update()
    {
        if (button1.comfirmPlayer == true && button2.comfirmPlayer)
        {
            levelLoader.LoadSceneLevel(LoadSceneNum);
        }
    }
}
