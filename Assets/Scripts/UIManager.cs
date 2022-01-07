using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // Singleton
    private static UIManager _instance;

    public static UIManager Instance 
    {
        get 
        {
            if (_instance == null) 
            {
                Debug.LogError("UIManager is null");
            }
            return _instance;
        }
    }

    // Variables
    [SerializeField]
    private Text _coinsDisplay;

    void Awake() 
    {
        if (_instance == null) 
        {
            _instance = this;
        }
    }

    void Start() 
    {
        _coinsDisplay.text = "Coins: 0";
    }

    public void UpdateCoinsDisplay(int value)
    {
        _coinsDisplay.text = "Coins: " + value.ToString();
    }
}
