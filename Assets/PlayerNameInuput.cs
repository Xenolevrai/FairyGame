using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInuput : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField nameinputfield = null;
    [SerializeField] private Button continuebutton = null;

    public static string DisplayName
    {
        get;
        private set;
    }


    private const string PlayerPrefsNameKey = "PlayerName";
    private void Start() => Setupinfield();
        
     private void Setupinfield()
    {
        if (! PlayerPrefs.HasKey(PlayerPrefsNameKey)) 
            { return; }
        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);
        nameinputfield.text = defaultName;
        SetPlayerName(defaultName);
    }


    public void SetPlayerName(string name)
    {
        continuebutton.interactable = !string.IsNullOrEmpty(name);
    }


    public void saveplayername()
    {
        DisplayName = nameinputfield.text;
        PlayerPrefs.SetString(PlayerPrefsNameKey,DisplayName);
    }
}
