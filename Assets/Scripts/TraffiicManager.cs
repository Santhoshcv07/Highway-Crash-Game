using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    [SerializeField] Transform[] lane;
    [SerializeField] GameObject[] trafficVehicle;
    [SerializeField] CarController carController;
    [SerializeField] float minSpawnTime = 30f;
     [SerializeField] float maxSpawnTime = 60f;
     private float dynamicTimer = 2f;
 
    void Start()
    {
        StartCoroutine(TrafficSpawner());
    }
    
    public void SetCarController(CarController controller)
    {
        carController = controller;
    }
   
   IEnumerator TrafficSpawner()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            
             if(carController.CarSpeed()>20f)
            {
                 dynamicTimer = Random.Range( minSpawnTime, maxSpawnTime) / carController.CarSpeed();
                SpawnTrafficVehicle();
            }
             yield return new WaitForSeconds(dynamicTimer);
        }
    }
    void SpawnTrafficVehicle()
    {
           int randomLaneIndex =  Random.Range(0,lane.Length);
            int randomTrafficVehicleIndex = Random.Range(0, trafficVehicle.Length);
            Instantiate(trafficVehicle[randomTrafficVehicleIndex], lane[randomLaneIndex].position,Quaternion.identity);
    }
}
