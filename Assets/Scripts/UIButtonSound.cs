using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    
   public void ButtonSound()
    {
        AudioManager.instance.PlayButtonSound();
    }
}
