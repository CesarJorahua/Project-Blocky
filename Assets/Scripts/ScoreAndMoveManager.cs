using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndMoveManager : MonoBehaviour
{
    [SerializeField] private int startingMoves = 5;

    [Space(10)]
    [Header("Canvases")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject presentationScreen;

    [Space(10)]
    [Header("TMP Objects")]
    [SerializeField] private TMPro.TextMeshProUGUI movesText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    private int currentMoves;
    private int score = 0;
    private InputManager inputManager;

    //TODO: Replace this pseudo singleton with DI implementation
    public static ScoreAndMoveManager Instance { get; private set; }

    private void Awake()
    {
        currentMoves = startingMoves;
        Instance = this;
        inputManager = FindFirstObjectByType<InputManager>();
        ValidateReference(gameOverScreen,"Game over");
        ValidateReference(presentationScreen,"Presentation screen");
        ValidateReference(movesText,"Moves TMP object");
        ValidateReference(scoreText,"Score TMP object");

        ApplyStartingValues();
    }

    public void MakeMove()
    {
        currentMoves--;
        score+=10;
        Debug.Log($"[{GetType()}] Current moves:  " + currentMoves);
        Debug.Log($"[{GetType()}] Score: " + score);
        scoreText.text = score.ToString("N0", new CultureInfo("es-MX"));
        movesText.text = currentMoves.ToString();
        if (currentMoves <= 0)
        {
            gameOverScreen.SetActive(true);
            //Disable interaction with the presentation canvas
            presentationScreen.GetComponent<GraphicRaycaster>().enabled = false;
            Debug.Log($"[{GetType()}] Game over restart");
        }
    }

    public void ResetGameplay()
    {
        currentMoves = startingMoves;
        score = 0;
        presentationScreen.GetComponent<GraphicRaycaster>().enabled = true;
        ApplyStartingValues();
        gameOverScreen.SetActive(false);
        inputManager.enabled = true;
    }

    private void ValidateReference<T>(T reference, string objectName)
    {
        if (reference == null)
        {
            Debug.LogError($"[{GetType()}] {objectName} not found! Make sure it is attached to this component.",this);
        }
    }

    private void ApplyStartingValues()
    {
        movesText.text = startingMoves.ToString();
        scoreText.text = "0";
    }

    public void AddScore(int addition)
    {
        score += addition * 10;
        scoreText.text = score.ToString("N0", new CultureInfo("es-MX"));
    }

    public void UseMove()
    {
        currentMoves--;
        movesText.text = currentMoves.ToString();
        if (currentMoves <= 0)
        {
            gameOverScreen.SetActive(true);
            //Disable interaction with the presentation canvas
            presentationScreen.GetComponent<GraphicRaycaster>().enabled = false;
            inputManager.enabled = false;
            Debug.Log($"[{GetType()}] Game over restart");
        }
    }
}
