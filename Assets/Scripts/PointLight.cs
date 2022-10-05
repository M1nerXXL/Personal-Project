using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Move through tunnel
        if (!GameManager.gameOver)
        {
            transform.Translate(Vector3.back * Time.deltaTime * GameManager.gameSpeed);
        }
        //Move back to start when leaving tunnel
        if (transform.position.z < -50)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 30);
        }
    }
}
