using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //List<GameObject> heart = new List<GameObject>();
    private Camera mainCamera;
    public GameObject[] heart = new GameObject[10];
    public GameObject[] ammoUI = new GameObject[5];

    public PlayerShip deck;
    public Cannon cannon;
    int numHearts;

    public Sprite emptyHeart;

    public GameObject loseScreen;

    //old ammo counter
    public GameObject AmmoCounter;
    public TMP_Text maxAmmoText;
    public TMP_Text currentAmmoText;

    //player icons
    public Sprite player1;
    public Sprite player2;
    private Sprite setSprite;
    public GameObject[] playerIcon = new GameObject[2];

    //cannon aimer
    public GameObject crossHairUI;
    public Sprite lockCrossHair;
    public Sprite looseCrossHair;
    public float moveSpeed;

    //RectTransform of crosshair
    private RectTransform crossHairRectTransform;

    [Header("Rotation Aiming Control")]
    public float maxAngleY = 45f;//left and right
    public float maxAngleZ = 30f;//upndown
    public Vector2 manualScale = new Vector2(1f, 1f);
    public Vector2 manualOffset = new Vector2(0f, 0f);

    private RectTransform canvasRectTransform;
    private Vector2 targetPosition;
    private float minX, maxX, minY, maxY;

    public LevelLoader levelLoader;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        cannon = GameObject.FindWithTag("Cannon").GetComponent<Cannon>();
        maxAmmoText.text = cannon.GetMaxAmmo().ToString();//old ammo counter

        crossHairRectTransform = crossHairUI.GetComponent<Image>().rectTransform;
        canvasRectTransform = GetComponent<RectTransform>();
        levelLoader = GameObject.Find("SceneLoader").GetComponent<LevelLoader>();
        playerIcon[0].SetActive(false);
        playerIcon[1].SetActive(false);

        CalculateBounds();
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            levelLoader.LoadSceneLevel(0);
            
        }
        numHearts = deck.GetHealth() / 10;
        if (numHearts < 10 && numHearts >= 0)
        {
            heart[numHearts].GetComponent<Image>().sprite = emptyHeart;
        }

        if (deck.GetHealth() <= 0 && loseScreen.activeInHierarchy == false)
        {
            loseScreen.SetActive(true);
            AmmoCounter.SetActive(false);
            Time.timeScale = 0;

            GameObject.Find("Ship Health").SetActive(false);

           
        }else if (Input.GetKeyDown(KeyCode.Space) && loseScreen.activeInHierarchy == true)
        {
                Time.timeScale = 1;
                LoadLevelAgain();
        }

        //currentAmmoText.text = cannon.GetCurrentAmmo().ToString();//part of old ammo counter, use for testing
    }


    void LateUpdate()
    {
        if (cannon == null) return;

        if (cannon.canFire)
        {
            crossHairUI.SetActive(true);
            crossHairUI.GetComponent<Image>().sprite = lockCrossHair;

            Vector3 localEuler = cannon.transform.localEulerAngles;
            float rotY = localEuler.y > 180 ? localEuler.y - 360 : localEuler.y;
            float rotZ = localEuler.z > 180 ? localEuler.z - 360 : localEuler.z;
            float xPercent = Mathf.Clamp(rotY, -maxAngleY, maxAngleY) / maxAngleY;
            float yPercent = Mathf.Clamp(rotZ, -maxAngleZ, maxAngleZ) / maxAngleZ;
            float targetX = xPercent * maxX;
            float targetY = yPercent * maxY;

            targetPosition.x = (targetX * manualScale.x) + manualOffset.x;
            targetPosition.y = (targetY * manualScale.y) + manualOffset.y;

            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

            crossHairRectTransform.anchoredPosition = Vector2.Lerp(
                crossHairRectTransform.anchoredPosition,
                targetPosition,
                moveSpeed * Time.deltaTime
            
            );
        }
        else if (!cannon.canFire)
        {
            crossHairUI.SetActive(false);
        }
    }
    void CalculateBounds()
    {
        //float canvasWidth = canvasRectTransform.rect.width;
        //float canvasHeight = canvasRectTransform.rect.height;

        float canvasWidth = GetComponent<CanvasScaler>().referenceResolution.x;
        float canvasHeight = GetComponent<CanvasScaler>().referenceResolution.y;

        float imageWidth = crossHairRectTransform.rect.width;
        float imageHeight = crossHairRectTransform.rect.height;

        minX = -(canvasWidth / 2) + (imageWidth / 2);
        maxX = (canvasWidth / 2) - (imageWidth / 2);
        minY = -(canvasHeight / 2) + (imageHeight / 2);
        maxY = (canvasHeight / 2) - (imageHeight / 2);
    }

    public void UpdateAmmoUI(int ammo, int maxAmmo)
    {
        if (ammo == maxAmmo)
        {
            for (int x = 0; x < ammo; x++)
            {
                ammoUI[x].SetActive(true);
            }
        }
        else if (ammo < maxAmmo)
        {
            for (int x = 0; x < ammo; x++)
            {
                ammoUI[x].SetActive(true);
            }

            for (int x = ammo; x < maxAmmo; x++)
            {
                ammoUI[x].SetActive(false);
            }
        }

    }

    public void playerIconStatus(string playerName, bool display, int type)
    {
        //replace the cannon and barrel icos in the image
        //
        if (display)
        {
            if (playerName == "Player2")
            {
                setSprite = player2;
            }
            else if (playerName == "Player")
            {
                setSprite = player1;
            }
            playerIcon[type].GetComponent<Image>().sprite = setSprite;
            playerIcon[type].SetActive(true);
        }
        else if(!display)
        {
            playerIcon[type].SetActive(false);
        }
    }

    private void LoadLevelAgain()
    {
        StartCoroutine(levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
}

