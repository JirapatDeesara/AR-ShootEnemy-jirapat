using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Rendering;


public class UImanager : MonoBehaviour
{
    [Header("Button Setup")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button shootButton;

    [Header("UI Setupp")]
    [SerializeField] private TMP_Text greetingText;
    //Week11
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image crosshair;


    public static event Action OnUIStartButtonPressed;
    public static event Action OnUIRestartButtonPressed;
    public static event Action OnUIShootButtonPressed;

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonPressed);
        restartButton.onClick.AddListener(OnRestartButtonPressed);
        shootButton.onClick.AddListener(OnShootButtonPressed);

        restartButton.gameObject.SetActive(false);
        shootButton.gameObject.SetActive(false);

        scoreText.gameObject.SetActive(false);
        crosshair.gameObject.SetActive(false);
    }// End start

    private void OnStartButtonPressed()
    {
        OnUIStartButtonPressed?.Invoke();
        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        shootButton.gameObject.SetActive(true);
        greetingText.gameObject.SetActive(false);   

        scoreText.gameObject.SetActive(true);
        crosshair.gameObject.SetActive(true);
    }// End Start method

    private void OnRestartButtonPressed()
    {
        // Restart button make start and greeting text button appear but can't see restart
        OnUIRestartButtonPressed?.Invoke();
        startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        shootButton.gameObject.SetActive(false);
        greetingText.gameObject.SetActive(true);
        // fasle mean close, True mean no
        scoreText.gameObject.SetActive(false);
        crosshair.gameObject.SetActive(false);
    }//End Restart method

    private void OnShootButtonPressed()
    {
        OnUIShootButtonPressed?.Invoke();
        
    }//End Shoot method

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }// End UpdateScore

}// End UI managert
