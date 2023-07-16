using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller_1 : MonoBehaviour
{
    public static int CIRCLES_COUNT = 100;
    public static float MOVE_TO_CIRCLE_MOVE_COEF = 0.1f;

    public GameObject circlePrefab;
    private List<GameObject> circles = new List<GameObject>();

    private Vector3 mousePosition;
    private Vector2 scrollPosition;

    public GameObject scoreDisplay;
    private float score = 0;

    public GameObject commentDisplay;

    private Dictionary<float, string> comments = new Dictionary<float, string>() {
        { 3650, "Pental Flower" },
        { 4450, "Net Flower" },
        { 5130, "Bastion" },
        { 5890, "Wormhole" },
        { 6000, "Darkness"},
        { 6700, "Contingency"},
        { 7170, "Flame of Purity"},
        { 7721, "Wrong Mark"},
        { 8450, "Supernova" },
        { 8920, "Butterfly"}
    };

    private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = FindAnyObjectByType<LineRenderer>();
        for (int i = -CIRCLES_COUNT; i < CIRCLES_COUNT; i++)
        {
            GameObject circle = Instantiate(circlePrefab, transform.position, Quaternion.identity);
            var order = i;
            circle.GetComponent<Circle>().setOrder(order);
            circles.Add(circle);
        }
        lineRenderer.positionCount = CIRCLES_COUNT * 2 - 1;
        lineRenderer.sortingLayerName = "Foreground";
    }

    private void Update()
    {
        //handleMouseMoving();
        //handleScroll();
        handleSwipe();
        redrawLines();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void handleSwipe()
    {
        if (Input.touchCount == 0)
        {
            return;
        }
        var bias = Input.touches[0].deltaPosition.x * MOVE_TO_CIRCLE_MOVE_COEF;
        for (int i = 0; i < circles.Count; i++)
        {
            circles[i].GetComponent<Circle>().shift(bias);
        }
        mousePosition = Input.mousePosition;
        updateScore(bias);
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
        for (int i = 0; i < CIRCLES_COUNT - 1; i++)
        {
            circles[i].GetComponent<Circle>().shift(bias);
        }
        mousePosition = Input.mousePosition;
    }


    private void redrawLines()
    {
        for (int i = 0; i < circles.Count - 1; i++)
        {
            lineRenderer.SetPosition(i, circles[i].transform.Find("Sprite").position);
        }
    }

    private void updateScore(float bias)
    {
        score += bias;
        string stringScore = ((int)score).ToString();
        scoreDisplay.GetComponent<TextMeshProUGUI>().text = stringScore;
        setComment();
    }

    private void setComment()
    {
        foreach (var i in comments.Keys)
        {
            if (Mathf.Abs(score) > i && Mathf.Abs(score) < i + 100f)
            {
                commentDisplay.GetComponent<TextMeshProUGUI>().text = comments[i];
                return;
            }
        }
        commentDisplay.GetComponent<TextMeshProUGUI>().text = "";
    }
}