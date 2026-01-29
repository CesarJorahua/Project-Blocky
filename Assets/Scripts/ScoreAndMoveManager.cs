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

    private void Awake()
    {
        currentMoves = startingMoves;
        if(gameOverScreen==null)
            Debug.LogError($"[{GetType()}] GameOver screen not found! Make sure there is attached to this component.", this);
        if(presentationScreen==null)
            Debug.LogError($"[{GetType()}] Presentation screen not found! Make sure there is attached to this component.", this);

    }

    public void MakeMove()
    {
        currentMoves--;
        score+=10;
        Debug.Log($"[{GetType()}] Current moves:  " + currentMoves);
        Debug.Log($"[{GetType()}] Score: " + score);
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
        gameOverScreen.SetActive(false);
    }

}
