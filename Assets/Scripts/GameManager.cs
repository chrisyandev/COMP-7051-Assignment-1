using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Canvas m_MenuCanvas;
    [SerializeField]
    private Canvas m_ScoreCanvas;
    [SerializeField]
    private Canvas m_TimerCanvas;


    [Space( 20 )]
    [SerializeField]
    private InputActionAsset m_DefaultInputActionAsset;
    [SerializeField]
    private InputActionReference m_StartGameAction;

    [Space( 20 )]
    [SerializeField]
    private Player m_PlayerScript;
    [SerializeField]
    private AIPaddle m_AiPaddle;
    [SerializeField]
    private Ball m_Ball;


    [SerializeField] private TMP_Text Player1ScoreDisplay;
    [SerializeField] private TMP_Text Player2ScoreDisplay;
    [SerializeField] private TMP_Text RoundTimerDisplay;
    [SerializeField] private TMP_Text WinnerDisplay;

    [SerializeField] private int Player1Score;
    [SerializeField] private int Player2Score;

    private float DEFAULT_GAME_TIME_IN_SECONDS = 20.0f;
    private float timeLeft;
    private bool timerOn = false;

    private bool isInGame = false;


    private static GameManager _instance;

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManager();
        }

        return _instance;
    }



    private void Awake()
    {

        _instance = this;

        // Enable all action maps inside the default action asset.
        foreach ( var actionMap in m_DefaultInputActionAsset.actionMaps )
        {
            actionMap.Enable();
        }

        m_StartGameAction.action.started += OnStartGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = DEFAULT_GAME_TIME_IN_SECONDS;
        timerOn = true;

        Player1Score = 0;
        Player2Score = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if ( timerOn )
        {
            if ( timeLeft > 0 )
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer( timeLeft );
            }
            else
            {
                Debug.Log( "Time up!" );
                timeLeft = 0;
                timerOn = false;
                AnnounceWinner();
                //ResetGame();
            }
        }
    }

    void AnnounceWinner()
    {
        if ( Player1Score > Player2Score )
        {
            WinnerDisplay.text = "Player 1 Wins.";
        }
        else
        {
            WinnerDisplay.text = "Player 2 Wins.";

            // If AI enabled
            //WinnerDisplay.text = "You lost to AI noob.";
        }
    }

    private void ResetGame()
    {
        // To-do
        WinnerDisplay.text = "";
        Player1Score = 0;
        Player2Score = 0;
    }

    private void StartGame()
    {
        // To-do
    }


    public static void UpdateScore( int PlayerID )
    {
        if ( PlayerID == 1 )
        {
            _instance.Player1Score++;
            _instance.Player1ScoreDisplay.text = String.Format( "{0:00}", _instance.Player1Score );
            
        }
        else
        {
            _instance.Player2Score++;
            _instance.Player2ScoreDisplay.text = String.Format( "{0:00}", _instance.Player2Score );
        }
    }

    void UpdateTimer( float currentTime )
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt( currentTime / 60 );
        float seconds = Mathf.FloorToInt( currentTime % 60 );

        RoundTimerDisplay.text = string.Format( "{0:00} : {1:00}", minutes, seconds );

    }

    public void SetIsInGame( bool toSet )
    {
        isInGame = toSet;
    }

    public void OnStartGame( InputAction.CallbackContext context )
    {
        isInGame = true;
        Debug.Log( "Started Game" );
        this.m_MenuCanvas.enabled = false;
        m_StartGameAction.action.started -= OnStartGame;
        // Reset Game State
        // Start Pong Logic
    }

    public bool IsInGame()
    {
        return isInGame;
    }

    private void OnDestroy()
    {

    }
}
