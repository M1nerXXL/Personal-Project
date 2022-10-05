using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public StageColor stageColorScript;
    public SpawnManager spawnManagerScript;
    public ProgressMeter progressMeterScript;
    public PlayerCollision playerCollisionScript;
    public GameCanvas gameCanvasScript;
    public Renderer playerRenderer;
    public Renderer stageRenderer;
    public SphereCollider playerCollider;
    public GameObject titleScreen;
    public GameObject gameScreen;
    public GameObject gameOverScreen;
    public CanvasGroup titleScreenGroup;
    public CanvasGroup gameScreenGroup;
    public CanvasGroup gameOverScreenGroup;
    public TextMeshProUGUI stageText;
    public ParticleSystem dustParticles;
    public Material playerLayerOne;
    public Image handle;
    public Image meterBackground;
    public Camera gameCamera;
    public Color stageOneRed;

    static public int gameRound = 0;
    static public float gameSpeed = 10;
    static public bool gameActive = false;
    static public bool gameOver = false;

    public float fadeSpeed;
    public float cameraSpeed;
    private float titleOpacity = 1;
    private float gameOpacity = 0;
    private float gameOverOpacity = 0;
    private float cameraLerpTime = 0;
    private bool playButtonPressed = false;
    public bool triggerGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        gameScreenGroup.alpha = 0;
        gameScreen.SetActive(false);
        gameOverScreenGroup.alpha = 0;
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playButtonPressed)
        {
            gameActive = true;
            gameScreen.SetActive(true);

            //Fade out title UI
            if (titleOpacity > 0)
            {
                titleScreenGroup.alpha = titleOpacity;
                titleOpacity -= fadeSpeed * Time.deltaTime;
            }
            //Move camera
            else if (cameraLerpTime < 1)
            {
                gameCamera.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(20, 0, 0), cameraLerpTime);
                gameCamera.transform.localPosition = Vector3.Lerp(new Vector3(0, -0.3f, -45), new Vector3(0, 0.3f, -1.5f), cameraLerpTime);
                cameraLerpTime += cameraSpeed * Time.deltaTime;
            }
            //Fade in game UI
            else if (gameOpacity < 1)
            {
                gameScreenGroup.alpha = gameOpacity;
                gameOpacity += fadeSpeed * Time.deltaTime;
            }
            else
            {
                titleScreen.SetActive(false);
                cameraLerpTime = 0;
                titleOpacity = 1;
                playButtonPressed = false;
            }
        }

        if (triggerGameOver)
        {
            gameOverScreen.SetActive(true);
            //Fade in game over UI
            if (gameOverOpacity < 1)
            {
                gameOverScreenGroup.alpha = gameOverOpacity;
                gameOverOpacity += fadeSpeed * Time.deltaTime;
            }
            else
            {
                triggerGameOver = false;
            }
        }

        handle.color = new Color(handle.color.r, handle.color.g, handle.color.b, gameScreenGroup.alpha);
    }

    public void PlayButton()
    {
        playButtonPressed = true;
    }

    public void RetryButton()
    {
        resetGame();
        //Restart game
        gameActive = true;
    }

    public void ReturnToTitleButton()
    {
        resetGame();
        gameSpeed = 10;
        //Disable Game UI
        gameScreen.SetActive(false);
        gameOpacity = 0;
        //Reset camera for Title screen
        gameCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
        gameCamera.transform.localPosition = new Vector3(0, -0.3f, -45);
        //Enable TitleScreen UI
        titleScreen.SetActive(true);
        titleScreenGroup.alpha = 1;
    }

    public void resetGame()
    {
        //Disable Game Over UI
        gameOver = false;
        triggerGameOver = false;
        gameOverScreen.SetActive(false);
        gameOverOpacity = 0;
        //Reset level
        gameRound = 0;
        spawnManagerScript.stopRound = true;
        //Reset stage
        stageColorScript.newRoundColor = 0;
        stageRenderer.material.color = stageOneRed;
        //Reset UI
        Color UIColorTop = stageOneRed;
        Color UIColorBottom = new Color(128, 0, 0);
        stageText.colorGradient = new VertexGradient(UIColorTop, UIColorTop, UIColorBottom, UIColorBottom);
        meterBackground.color = stageOneRed;
        progressMeterScript.timeInRound = 0;
        gameCanvasScript.score = 0;
        //Destroy obstacles and powerups
        GameObject[] obstaclesToDestroy = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject[] powerupsToDestroy = GameObject.FindGameObjectsWithTag("Powerup");
        for (int i = 0; i < obstaclesToDestroy.Length; i++)
        {
            Destroy(obstaclesToDestroy[i]);
        }
        for (int i = 0; i < powerupsToDestroy.Length; i++)
        {
            Destroy(powerupsToDestroy[i]);
        }

        //Reset player
        playerRenderer.enabled = true;
        playerCollider.enabled = true;
        playerCollisionScript.layer = 0;
        playerRenderer.material = playerLayerOne;
        handle.material = playerLayerOne;
        dustParticles.Play();
    }
}