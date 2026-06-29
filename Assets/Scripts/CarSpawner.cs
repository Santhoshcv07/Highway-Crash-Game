using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] carsPrefab;

    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] UIManager uiManager;
    [SerializeField] EndlessCity[] cityArray;
    [SerializeField] TrafficManager trafficManager;
    [SerializeField] LaneMovement laneMovement;
    
    void Start()
    {
       SpawnCar();
    }
    void SpawnCar()
    {
        int currentCarIndex =   PlayerPrefs.GetInt("CarIndexValue", 0);
        GameObject newCar = Instantiate(carsPrefab[currentCarIndex], transform.position, transform.rotation);
        CarController carController = newCar.GetComponent<CarController>();

        carController.SetUiManager(uiManager);
        cameraMovement.SetTransform(carController.transform);
        uiManager.SetCarController(carController);
        cityArray[0].SetTransform(carController.transform);
        cityArray[1].SetTransform(carController.transform);
        trafficManager.SetCarController(carController);
        laneMovement.SetTransform(carController.transform);
        
    }
    

    
}
