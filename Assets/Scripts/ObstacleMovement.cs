using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Get the right speed
        speed = GameManager.gameSpeed;

        //Move through tunnel
        if (GameManager.gameActive)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        //Destroy when leaving tunnel
        if (transform.position.z < -50)
        {
            Destroy(gameObject);
        }
    }
}
