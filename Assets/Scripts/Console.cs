using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Console : MonoBehaviour
{

    private Dictionary<string, Action<string>> cmdList;

    [SerializeField]
    private TMP_InputField m_cmdLine;

    [SerializeField]
    private TMP_Text m_cmdHistory;

    [SerializeField]
    private GameObject m_background; 

    private void Awake() {
        m_cmdLine.onEndEdit.AddListener(processCmd);
        cmdList = new Dictionary<string, Action<string>>()
        {
            ["colour"] = changeBGColour
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void processCmd(string input) {
        //TODO: use stringbuilder equivalent.
        m_cmdHistory.SetText(m_cmdHistory.text + input +"\n"); 
        m_cmdLine.SetTextWithoutNotify("");

        input = input.Trim();
        string cmd = input[..input.IndexOf(' ')];
        string args = input[(input.IndexOf(' ') + 1)..];

        if (cmd.Length == 0)  {
            printError("DID NOT ENTER A COMMAND");
            return;
        }
        
        if (!cmdList.ContainsKey(cmd)) {
            printError("NOT A VALID COMMAND");
            return;
        }

        cmdList[cmd](args);
    }

    private void printError(string errorMsg) {
        m_cmdHistory.SetText(m_cmdHistory.text + "ERROR: " + errorMsg + "\n");
    }

    private void changeBGColour(string hexCode) {
        Color newColour;
        if (ColorUtility.TryParseHtmlString(hexCode, out newColour))
            m_background.GetComponent<UnityEngine.UI.Image>().color = newColour;
        else
            printError("INVALID HEX CODE");
    }

    private void setBallSpeed(string speed) {
        // parse here and set the ball's speed.
    }

}
