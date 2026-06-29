using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    
    void Start()
    {
        
    }
 
 private void OnTriggerEnter(Collider other)
    {
        Destroy(other.transform.parent.gameObject);
    }
    
}
