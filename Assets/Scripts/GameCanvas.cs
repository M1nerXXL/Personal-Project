using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public PlayerCollision playerCollisionScript;
    public StageColor stageColorScript;

    public TextMeshProUGUI stageText;
    public TextMeshProUGUI scoreNumber;

    public float score = 0;
    public int scoreLimit = 9999999;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Change StageText text
        stageText.text = "Stage " + GameManager.gameRound;

        //Change color of StageText
        if (GameManager.gameRound > 1)
        {
            Color textColorTop = stageColorScript.newColor;
            Color textColorBottom = new Color(stageColorScript.newColor.r / 2, stageColorScript.newColor.g / 2, stageColorScript.newColor.b / 2);
            stageText.colorGradient = new VertexGradient(textColorTop, textColorTop, textColorBottom, textColorBottom);
        }

        //Calculate score
        if (GameManager.gameActive && score < scoreLimit)
        {
            int baseScore = 170;
            int layerBonus = 100;
            int levelBonus = 30;
            score += (baseScore + layerBonus * (3 - playerCollisionScript.layer) + levelBonus * (GameManager.gameRound)) * Time.deltaTime;
            if (score > scoreLimit)
            {
                score = scoreLimit;
            }
        }

        //Display score
        scoreNumber.text = score.ToString("#,###");
    }
}
