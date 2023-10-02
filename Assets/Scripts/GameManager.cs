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


    public TMP_Text Player1Score;
    public TMP_Text Player2Score;
    public TMP_Text RoundTimer;


    //[SerializeField]
    //private TextMeshProUGUI m_RoundTimer;

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

    private float DEFAULT_GAME_TIME_IN_SECONDS = 60.0f;
    private float timeLeft;
    private bool timerOn = false;

    private bool isInGame = false;


    private static GameManager _instance;

    public static GameManager GetInstance()
    {
        if ( _instance == null )
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
        Player1Score.text = "12";
        Player2Score.text = "69";

        timerOn = true;
        //RoundTimer.text = "00:12";
    }

    // Update is called once per frame
    void Update()
    {
        if ( timerOn )
        {
            if ( timeLeft > 0 )
            {
                timeLeft -= Time.deltaTime;
                updateTimer( timeLeft );
            }
            else
            {
                Debug.Log( "Time up!" );
                timeLeft = 0;
                timerOn = false;
            }
        }
    }

    void updateTimer( float currentTime )
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt( currentTime / 60 );
        float seconds = Mathf.FloorToInt( currentTime % 60 );

        RoundTimer.text = string.Format( "{0:00} : {1:00}", minutes, seconds );

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
