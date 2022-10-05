using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turningSpeed;
    public float currentTurningSpeed;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Rotating stage
        if (GameManager.gameActive)
        {
            //Read input and change turning speed accordingly
            float turningInput = Input.GetAxis("Horizontal");
            currentTurningSpeed = turningInput * turningSpeed;
            //Rotate according to turning speed
            transform.Rotate(0, 0, currentTurningSpeed);
        }
    }
}
