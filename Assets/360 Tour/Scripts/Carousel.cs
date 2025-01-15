using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carousel : MonoBehaviour
{
    private Image[] showImages = new Image[3];
    public List<Image> images;
    private int count;
    public Button nextButton; // Botão para imagem seguinte
    public Button prevButton; // Botão para imagem anterior
    public float transitionSpeed = 1.0f; // Velocidade de transição entre imagens

    private int currentIndex = 0;
    private bool isTransitioning = false;

    void Start()
    {
        // Adicionar listeners aos botões
        count = 0;
        for(int i = 0; i < 3; i++){
            showImages[i] = images[i];
        }
        nextButton.onClick.AddListener(ShowNextImage);
        prevButton.onClick.AddListener(ShowPreviousImage);

        UpdateImagePositions();
    }

    void ShowNextImage()
    {
        count++;
        if(count >= images.Count) count = 0;
        if(count + 2 < images.Count){
            showImages[2] = images[count+2];
            showImages[1] = images[count+1];
        } else if(count+1 < images.Count){
            showImages[2] = images[0];
            showImages[1] = images[count+1];
        } else if(count < images.Count){
            showImages[2] = images[1];
            showImages[1] = images[0];
        }
        showImages[0] = images[count];
        UpdateImagePositions();
    }

    void ShowPreviousImage()
    {
        count--;
        if(count < 0) count = images.Count - 1;
        if(count - 2 >= 0){
            showImages[1] = images[count-1];
            showImages[0] = images[count-2];
        } else if(count - 1 >= 0){
            showImages[1] = images[count - 1];
            showImages[0] = images[images.Count - 1];
        } else if(count < images.Count){
            showImages[1] = images[images.Count - 1];
            showImages[0] = images[images.Count - 2];
        }
        showImages[2] = images[count];
        UpdateImagePositions();
    }

    void UpdateImagePositions()
    {
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false);
        }
        foreach (Image img in showImages)
        {
            img.gameObject.SetActive(true);
        }
        // Posicionar as imagens de acordo com os índices
        showImages[0].rectTransform.anchoredPosition = new Vector2(-510, showImages[0].rectTransform.anchoredPosition.y);
        showImages[1].rectTransform.anchoredPosition = new Vector2(0, showImages[1].rectTransform.anchoredPosition.y);
        showImages[2].rectTransform.anchoredPosition = new Vector2(510, showImages[2].rectTransform.anchoredPosition.y);
    }
}
