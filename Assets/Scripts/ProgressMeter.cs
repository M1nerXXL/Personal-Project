using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMeter : MonoBehaviour
{
    public SpawnManager spawnManagerScript;
    public StageColor stageColorScript;

    public Image backgroundImage;
    public Slider meter;

    private float roundDuration;
    private float meterProgress;
    public float timeInRound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Change Meter color
        if (GameManager.gameActive && GameManager.gameRound > 1)
        {
            backgroundImage.color = stageColorScript.newColor;
        }

        //Calculate round duration
        roundDuration = spawnManagerScript.waitBeforeRound + spawnManagerScript.obstacleQuantity * spawnManagerScript.obstacleSpawnInterval + spawnManagerScript.waitAfterRound;

        //Show progress on meter
        if (GameManager.gameActive)
        {
            timeInRound += Time.deltaTime;
        }
        meterProgress = timeInRound / roundDuration;
        meter.value = meterProgress;
    }
}