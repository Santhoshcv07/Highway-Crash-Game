using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI distanceText;
      [SerializeField] TextMeshProUGUI scoreText;


    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI totalDistanceText;
    [SerializeField] TextMeshProUGUI MaximumSpeedText;

    [SerializeField] CarController CarController;
   
    [SerializeField] GameObject gameOverPanel;

    [SerializeField] GameObject speedIcon;
     [SerializeField] GameObject distanceIcon;
      [SerializeField] GameObject scoreIcon;

    private float speed = 0f;
    private float distance = 0f;
     private float score = 0f;
     private float maximumSpeed = 0f;
    
    void Start()
    {
        gameOverPanel.SetActive(false);
        speedIcon.SetActive(true);
        distanceIcon.SetActive(true);
        scoreIcon.SetActive(true);
        Time.timeScale = 1f;
    }
    public void SetCarController(CarController controller)
    {
        CarController = controller;
    }
    void Update()
    {
        DistanceUI();
        SpeedUI();
        ScoreUI();
        MaximumSpeed();
        
    }
    void DistanceUI()
    {
        distance = CarController.transform.position.z / 1000;
        distanceText.text = distance.ToString("0.00" + "Km");
    }
    void SpeedUI()
    {
        speed = CarController.CarSpeed();
        speedText.text = speed.ToString("0"+"Km/h");
    }
    void ScoreUI()
    {
        score = CarController.transform.position.z * 6;
        scoreText.text = score.ToString("0");
    }
    public void GameOver()
    {
          Time.timeScale = 0f;
          gameOverPanel.SetActive(true);
          speedIcon.SetActive(false);
        distanceIcon.SetActive(false);
        scoreIcon.SetActive(false);
          totalScoreText.text = score.ToString("0");
          totalDistanceText.text = distance.ToString("0.00" + "Km");
    }
    void MaximumSpeed()
    {
        float currentSpeed = CarController.CarSpeed();
        if (currentSpeed > maximumSpeed)
        {
            maximumSpeed = currentSpeed;
        }
        MaximumSpeedText.text = maximumSpeed.ToString("0"+"km/h");
    }
    public void TryAgain()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public void GarageButton()
    {
        SceneManager.LoadScene("Garage");
    }
    }
    

