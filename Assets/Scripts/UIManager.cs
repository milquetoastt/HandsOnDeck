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
    public PlayerShip deck;
    public Cannon cannon; 
    int numHearts;

    public Sprite emptyHeart;

    public GameObject loseScreen;

    public TMP_Text maxAmmoText;
    public TMP_Text currentAmmoText;

    //cannon aimer
    public GameObject crossHairUI;
    public Sprite lockCrossHair;
    public Sprite looseCrossHair;
    public float moveSpeed;
    public float timeToNewPos;

    //RectTransform of crosshair
    private RectTransform crossHairRectTransform;
    public Vector2 manualScale = new Vector2(1f, 1f);
    public Vector2 manualOffset = new Vector2(0f, 0f);

    private RectTransform canvasRectTransform;
    private Vector2 targetPosition;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        cannon = GameObject.FindWithTag("Cannon").GetComponent<Cannon>();
        maxAmmoText.text = cannon.GetMaxAmmo().ToString();
        crossHairRectTransform = crossHairUI.GetComponent<Image>().rectTransform;
        canvasRectTransform = GetComponent<RectTransform>();
        CalculateBounds();
        StartCoroutine(Wander());
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(0);
        }
        numHearts = deck.GetHealth() / 10;
        if (numHearts < 10) 
        {
            heart[numHearts].GetComponent<Image>().sprite = emptyHeart;
        }

        if (deck.GetHealth() == 0)
        {
            loseScreen.SetActive(true);
            Time.timeScale = 0;
        }

        currentAmmoText.text = cannon.GetCurrentAmmo().ToString();
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

    IEnumerator Wander()
    {
        while (true)
        {
            int randomEdge = Random.Range(0, 4);
            if (cannon.GetComponent<CannonAimer>().GetNumberLane() != 0)
            {
                if (cannon.GetComponent<CannonAimer>().target == null)
                {
                    randomScreenPositon(randomEdge);
                }
                else
                {
                    crossHairUI.GetComponent<Image>().sprite = lockCrossHair;

                    Vector3 laneposition = cannon.GetComponent<CannonAimer>().target.position;
                    Vector3 screenPosition = mainCamera.WorldToScreenPoint(laneposition);

                    Vector2 localPos;

                    Camera eventCamera = (GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceOverlay) ? null : mainCamera;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        canvasRectTransform,
                        screenPosition,
                        eventCamera,
                        out localPos
                    );


                    localPos.x = (localPos.x * manualScale.x) + manualOffset.x;
                    localPos.y = (localPos.y * manualScale.y) + manualOffset.y;

                    targetPosition = localPos;
                }

            }
            else 
            {
                randomScreenPositon(randomEdge);
            }

            while (Vector2.Distance(crossHairRectTransform.anchoredPosition, targetPosition) > 1f)
            {
                crossHairRectTransform.anchoredPosition = Vector2.MoveTowards(
                    crossHairRectTransform.anchoredPosition,
                    targetPosition,
                    moveSpeed * Time.deltaTime
                );

                yield return null;
            }

            yield return new WaitForSeconds(timeToNewPos);
        }
    }

    void randomScreenPositon(int randomEdge)
    {
        crossHairUI.GetComponent<Image>().sprite = looseCrossHair;

        switch (randomEdge)
        {
            case 0: // Top Edge
                targetPosition = new Vector2(Random.Range(minX, maxX), maxY);
                break;
            case 1: // Bottom Edge
                targetPosition = new Vector2(Random.Range(minX, maxX), minY);
                break;
            case 2: // Left Edge
                targetPosition = new Vector2(minX, Random.Range(minY, maxY));
                break;
            case 3: // Right Edge
                targetPosition = new Vector2(maxX, Random.Range(minY, maxY));
                break;
        }
    }
}

