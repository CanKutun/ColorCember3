using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyunDurumu : MonoBehaviour
{
    
    void Start()
    {
        Time.timeScale = 0; // oyunu baslang��ta durdurur
    }

   
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Time.timeScale = 1; // oyun t�klan�nca ba�lar
            this.enabled = false;
        } 

    }
}
