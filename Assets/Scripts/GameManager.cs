using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Canvas m_MenuCanvas;
    [SerializeField]
    private TextMeshProUGUI m_RoundTimer;

    [Space(20)]
    [SerializeField]
    private InputActionAsset m_DefaultInputActionAsset;
    [SerializeField]
    private InputActionReference m_StartGameAction;

    [Space(20)]
    [SerializeField]
    private Player m_PlayerScript;
    [SerializeField]
    private AIPaddle m_AiPaddle;
    [SerializeField]
    private Ball m_Ball;

    private float DEFAULT_GAME_TIME_IN_SECONDS = 60.0f;
    private bool isInGame = false;    
    
    public bool isPvp;

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
        foreach (var actionMap in m_DefaultInputActionAsset.actionMaps)
        {
            actionMap.Enable();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIsInGame(bool toSet)
    {
        isInGame = toSet;
    }

    public bool IsInGame()
    {
        return isInGame;
    }

    private void OnDestroy()
    {
    }

    //On click function for Player vs Player button
    public void OnPlayerVsPlayer()
    {
        isPvp = true;
        isInGame = true;

        Debug.Log("Started PvP Game");
        this.m_MenuCanvas.enabled = false;        
    }

    //On click function for Player vs AI button
    public void OnPlayerVsAI()
    {
        isPvp = false;
        isInGame = true;

        Debug.Log("Started PvAI Game");
        this.m_MenuCanvas.enabled = false;        
    }
}
