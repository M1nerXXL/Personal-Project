using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public PlayerController playerControllerScript;
    public PlayerCollision playerCollisionScript;
    public ProgressMeter progressMeterScript;
    public ObstacleMovement obstacleMovementScript;

    public GameObject[] obstacles;
    public GameObject powerup;

    public Coroutine currentCoroutine;
    public int obstacleQuantity;
    public int powerupRarity1000;
    public float obstacleSpawnInterval;
    public float waitBeforeRound = 3;
    public float waitAfterRound = 4;
    private bool newRound = true;
    public bool shortRounds = false;
    private bool powerupSpawned = false;
    public bool stopRound = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (stopRound)
        {
            stopRound = false;
            StopCoroutine(currentCoroutine);
            newRound = true;
            powerupSpawned = false;
        }
        if (GameManager.gameActive && newRound)
        {
            newRound = false;
            //Start the round
            currentCoroutine = StartCoroutine(SpawnObjects());
        }
    }

    IEnumerator SpawnObjects()
    {
        if (GameManager.gameActive)
        {
            //Get the right variables
            GameManager.gameRound++;
            progressMeterScript.timeInRound = 0;
            GameManager.gameSpeed = 8 + GameManager.gameRound * 2;
            playerControllerScript.turningSpeed = 3 - 1.5f / GameManager.gameRound;
            obstacleSpawnInterval = 0.5f + 1.5f / GameManager.gameRound;
            obstacleQuantity = 12 + GameManager.gameRound * 3;

            if (shortRounds)
            {
                obstacleQuantity = 2;
            }

            //Wait a few seconds
            yield return new WaitForSeconds(waitBeforeRound);

            //Display round information in console
            Debug.Log("Round: " + GameManager.gameRound + ", Game Speed: " + GameManager.gameSpeed + ", Turning Speed: " + playerControllerScript.turningSpeed + ", Spawn Interval: " + obstacleSpawnInterval + "s, Obstacle Quantity: " + obstacleQuantity);

            //Obstacle spawning
            for (int i = 0; i < obstacleQuantity; i++)
            {
                if (GameManager.gameActive)
                {
                    int index;
                    bool spawnPowerup = Random.Range(1, 1000) <= powerupRarity1000;

                    if (spawnPowerup && !powerupSpawned && !playerCollisionScript.poweredUp)
                    {
                        //Spawn Shield
                        Instantiate(powerup, new Vector3(0, 0, 50), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                        powerupSpawned = true;
                    }
                    else
                    {
                        //If round number lower than number of obstacles, use only certain obstacles
                        if (GameManager.gameRound < obstacles.Length)
                        {
                            index = Random.Range(0, GameManager.gameRound);
                        }
                        else
                        {
                            index = Random.Range(0, obstacles.Length);
                        }

                        //Spawn random object
                        GameObject temp =  Instantiate(obstacles[index], new Vector3(0, 0, 50), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                        temp.AddComponent<ObstacleMovement>();
                        if (index == 3)
                        {
                            temp.GetComponent<RollingObstacle>().playerControllerScript = playerControllerScript;
                        }
                    }
                    //Time inbetween spawns
                    yield return new WaitForSeconds(obstacleSpawnInterval);
                }
            }
            //Wait a few seconds
            yield return new WaitForSeconds(waitAfterRound);

            //Enable new round to start
            newRound = true;
            powerupSpawned = false;
        }
    }
}
