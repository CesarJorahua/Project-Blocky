using System;
using System.Globalization;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

/// TODO: Separate the score and move management into different classes
/// TODO: Implement DI for better testability
/// <summary>
/// Manages game score and move count, tracking player progress and game state.
/// Handles game over conditions, UI updates, and game reset functionality using a pseudo-singleton pattern.
/// </summary>
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

    /// <summary>
    /// Initializes the score and move manager, validates references, and applies starting values.
    /// Sets the pseudo singleton instance and retrieves the InputManager reference.
    /// </summary>
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

    /// <summary>
    /// Decrements the move counter, adds 10 points to the score, and updates the UI.
    /// Triggers game over screen when moves reach zero or below.
    /// </summary>
    [Obsolete("Implemented for task 2 and removed after completion of task 3")]
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

    /// <summary>
    /// Resets the game state by restoring initial variables and UI elements.
    /// </summary>
    public void ResetGameplay()
    {
        currentMoves = startingMoves;
        score = 0;
        presentationScreen.GetComponent<GraphicRaycaster>().enabled = true;
        ApplyStartingValues();
        gameOverScreen.SetActive(false);
        inputManager.enabled = true;
    }

    /// <summary>
    /// Validates that a reference is not null and logs an error if it is missing.
    /// </summary>
    /// <typeparam name="T">The type of the reference to validate.</typeparam>
    /// <param name="reference">The reference to check.</param>
    /// <param name="objectName">The name of the object being validated (for error messages).</param>
    private void ValidateReference<T>(T reference, string objectName)
    {
        if (reference == null)
        {
            Debug.LogError($"[{GetType()}] {objectName} not found! Make sure it is attached to this component.",this);
        }
    }

    /// <summary>
    /// Applies the starting values to the UI text elements for moves and score.
    /// </summary>
    private void ApplyStartingValues()
    {
        movesText.text = startingMoves.ToString();
        scoreText.text = "0";
    }

    /// <summary>
    /// Adds points to the current score based on the number of blocks removed and updates the UI.
    /// </summary>
    /// <param name="blocksCollected">The number of blocks removed.</param>
    public void AddScore(int blocksCollected)
    {
        score += blocksCollected * Constants.POINT_MULTIPLIER;
        //Format show in the image (score with thousands separator)
        scoreText.text = score.ToString("N0", new CultureInfo("es-MX"));
    }

    /// <summary>
    /// Decrements the move counter and updates the UI.
    /// Triggers game over screen and disables player input when moves reach zero or below.
    /// </summary>
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
