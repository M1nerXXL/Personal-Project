using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageColor : MonoBehaviour
{
    public Renderer stageRenderer;

    public Color[] colors;
    public Color oldColor;
    public Color newColor;

    private int previousRoundColor = -1;
    public int newRoundColor = 0;
    private int obstacleColor;
    private float startTime;
    public float time;
    private float colorChangeSpeed = 0.5f;
    public bool changeOnSpawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (changeOnSpawn) //Obstacle
        {
            if(GameManager.gameRound > 0)
            {
                obstacleColor = (GameManager.gameRound - 1) % colors.Length;
                stageRenderer.material.color = colors[obstacleColor];
                stageRenderer.material.SetColor("_EmissionColor", colors[obstacleColor]);
            }
        }
        else //Stage
        {
            //In next round, change indexes to the right colors
            if (GameManager.gameRound > newRoundColor + 1)
            {
                newRoundColor = (newRoundColor + 1);
                previousRoundColor = (newRoundColor - 1);
                startTime = Time.time;
            }
            //Change color from previous color to new one
            if (GameManager.gameRound > 1)
            {
                oldColor = colors[previousRoundColor % colors.Length];
                newColor = colors[newRoundColor % colors.Length];
                time = (Time.time - startTime) * colorChangeSpeed;
                stageRenderer.material.color = Color.Lerp(oldColor, newColor, time);
            }
        }
    }
}
