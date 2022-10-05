using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    public GameManager gameManagerScript;

    public GameObject shieldLayer;
    public Image handleColor;
    public Renderer playerRenderer;
    public SphereCollider playerCollider;

    public Material[] materials;
    public ParticleSystem[] shardParticles;
    public ParticleSystem dustParticles;
    public ParticleSystem shieldParticles;

    public int layer = 0;
    public bool poweredUp = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Change dust particle speed
        var main = dustParticles.main;
        main.startSpeed = GameManager.gameSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            //Activate shield
            Destroy(other.gameObject);
            poweredUp = true;
            shieldLayer.SetActive(true);
        }
        else if (other.CompareTag("Obstacle"))
        {
            if (poweredUp)
            {
                //Deactivate shield
                poweredUp = false;
                shieldParticles.Play();
                shieldLayer.SetActive(false);
            }
            else
            {
                //Shatter layer
                shardParticles[layer].Play();
                //Next layer
                layer++;
                if (layer <= 2)
                {
                    playerRenderer.material = materials[layer];
                    handleColor.material = materials[layer];
                }
                else
                {
                    //Remove player
                    playerRenderer.enabled = false;
                    playerCollider.enabled = false;
                    dustParticles.Stop();
                    //Trigger Game Over
                    GameManager.gameActive = false;
                    GameManager.gameOver = true;
                    gameManagerScript.triggerGameOver = true;
                }
            }
        }
    }
}
