using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    Slider healthS;

    // Start is called before the first frame update
    void Start()
    {
        healthS = GetComponent<Slider>();    
    }

    public void SetMaxHealth(int health)
    {
        healthS.maxValue = health;
        healthS.value = health;
    }
    public void SetHealth(int health) => healthS.value = health;
}
