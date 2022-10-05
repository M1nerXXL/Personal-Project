using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleCanvas : MonoBehaviour
{
    public Renderer stageRenderer;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI PlayButtonText;

    public Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Change UI color
        Color textColorTop = stageRenderer.material.color;
        Color textColorBottom = new Color(stageRenderer.material.color.r / 2, stageRenderer.material.color.g / 2, stageRenderer.material.color.b / 2);
        titleText.colorGradient = new VertexGradient(textColorTop, textColorTop, textColorBottom, textColorBottom);
        PlayButtonText.color = stageRenderer.material.color;
        playButton.image.color = stageRenderer.material.color;

    }
}
