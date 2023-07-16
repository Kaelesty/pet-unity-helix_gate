using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public static int CIRCLES_COUNT = 100;
    public static float MOVE_TO_CIRCLE_MOVE_COEF = 0.02f;

    public GameObject circlePrefab;
    private List<GameObject> circles = new List<GameObject>();

    private Vector3 mousePosition;
    private Vector2 scrollPosition;

    public GameObject scoreDisplay;
    private float score = 0;

    private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = FindAnyObjectByType<LineRenderer>();
        for (int i = 1; i < CIRCLES_COUNT; i++)
        {
            GameObject circle = Instantiate(circlePrefab, transform.position, Quaternion.identity);
            circle.GetComponent<Circle>().setOrder(i);
            circles.Add(circle);
        }
        lineRenderer.positionCount = CIRCLES_COUNT - 1;
        lineRenderer.sortingLayerName = "Foreground";
    }

    private void Update()
    {
        //handleMouseMoving();
        //handleScroll();
        handleSwipe();
        redrawLines();
    }

    private void handleSwipe()
    {
        if (Input.touchCount == 0)
        {
            return;
        }
        var bias = Input.touches[0].deltaPosition.x * MOVE_TO_CIRCLE_MOVE_COEF;
        for (int i = 0; i < CIRCLES_COUNT - 1; i++)
        {
            circles[i].GetComponent<Circle>().shift(bias);
        }
        mousePosition = Input.mousePosition;
        score += bias;
        setScore(score);
    }

    private void handleScroll()
    {
        var bias = Input.mouseScrollDelta.x * MOVE_TO_CIRCLE_MOVE_COEF;
        for (int i = 0; i < CIRCLES_COUNT - 1; i++)
        {
            circles[i].GetComponent<Circle>().shift(bias);
        }
        mousePosition = Input.mousePosition;
    }

    private void handleMouseMoving()
    {
        if (mousePosition == null)
        {
            mousePosition = Input.mousePosition;
            return;
        }
        if (mousePosition == Input.mousePosition)
        {
            return;
        }
        var bias = (mousePosition.y - Input.mousePosition.y) * MOVE_TO_CIRCLE_MOVE_COEF;
        for (int i = 0; i < CIRCLES_COUNT - 1;i++)
        {
            circles[i].GetComponent<Circle>().shift(bias);
        }
        mousePosition = Input.mousePosition;
    }


    private void redrawLines()
    {
        for (int i = 0; i < CIRCLES_COUNT - 1; i++)
        {
            lineRenderer.SetPosition(i, circles[i].transform.Find("Sprite").position);
        }
    }

    private void setScore(float newScore)
    {
        string stringScore = ((int)newScore).ToString();
        scoreDisplay.GetComponent<TextMeshProUGUI>().text = stringScore;
    }
}
