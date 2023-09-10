using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset m_DefaultInputActionAsset;

    [SerializeField]
    private InputActionReference m_StartGameAction;

    private bool isInGame;

    private void Awake()
    {
        // Enable all action maps inside the default action asset.
        foreach (var actionMap in m_DefaultInputActionAsset.actionMaps)
        {
            actionMap.Enable();
        }

        m_StartGameAction.action.started += OnStartGame;
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

    public void OnStartGame(InputAction.CallbackContext context)
    {
        isInGame = true;
        Debug.Log("Started Game");
        // Clear Menu UI
        // Reset Game State
        // Start Pong Logic
    }

    private void OnDestroy()
    {
    }
}
