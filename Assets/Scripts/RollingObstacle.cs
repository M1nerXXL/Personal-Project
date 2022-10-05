using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingObstacle : MonoBehaviour
{
    public PlayerController playerControllerScript;

    private float[] rollingSpeed = new float[4];
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        //Different rolling speeds
        rollingSpeed[0] = -0.30f * playerControllerScript.turningSpeed;
        rollingSpeed[1] = -0.15f * playerControllerScript.turningSpeed;
        rollingSpeed[2] = 0.15f * playerControllerScript.turningSpeed;
        rollingSpeed[3] = 0.30f * playerControllerScript.turningSpeed;
        //Random rolling speed from list
        index = Random.Range(0, rollingSpeed.Length);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate obstacle
        transform.Rotate(0, 0, rollingSpeed[index]);
    }
}
