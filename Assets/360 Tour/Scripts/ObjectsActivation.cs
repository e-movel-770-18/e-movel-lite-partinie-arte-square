using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectActivationController : MonoBehaviour
{
    public bool activeCameraMove;
    public bool isParent;
    public bool icon;
    public bool isDifferentCameraTarget;
    public GameObject[] cameraTargets;
    public GameObject[] imgGlobby; // Array para armazenar os objetos IMGGlobby
    public Button[] buttons; // Array para armazenar os botões
    private CameraController cameraController;
    public Color activeColor;
    public Color inactiveColor;

    private void Start()
    {
        Debug.Log("Start called");
        // Encontra a instância do CameraController na cena
        cameraController = FindObjectOfType<CameraController>();

        // Adiciona a função OnButtonClick como listener para cada botão
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Captura o valor atual de i para o listener
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }

        if (!isDifferentCameraTarget)
        {
            cameraTargets = new GameObject[buttons.Length];
            for (int i = 0; i < buttons.Length; i++)
            {
                cameraTargets[i] = buttons[i].gameObject;
            }
        }
    }

    void Update()
    {
        if(isParent)
            for (int i = 0; i < imgGlobby.Length; i++)
            {
                if (imgGlobby[i].activeSelf)
                {
                    SetButtonColor(buttons[i], activeColor); // rgba(255,130,0,112)
                    if(!icon)
                        SetChildImagesActive(buttons[i], true);
                }
                else
                {
                    SetButtonColor(buttons[i], inactiveColor); // Transparent
                    if(!icon)
                        SetChildImagesActive(buttons[i], false);
                }
            }
    }

    void OnButtonClick(int buttonIndex)
    {
        Debug.Log("Button clicked: " + buttonIndex);
        // Desativa todos os objetos IMGGlobby
        foreach (GameObject img in imgGlobby)
        {
            img.SetActive(false);
        }

        // Ativa apenas o objeto IMGGlobby correspondente ao botão clicado
        imgGlobby[buttonIndex].SetActive(true);

        if (activeCameraMove && cameraController != null && cameraController.moveAble)
        {
            Debug.Log("Calling cameraMove");
            cameraController.CameraMove(cameraTargets[buttonIndex]);
        }
    }

    private void SetButtonColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.pressedColor = color;
        cb.selectedColor = color;
        button.colors = cb;
    }
    private void SetChildImagesActive(Button button, bool active)
    {
        foreach (Transform child in button.transform)
        {
            Image childImage = child.GetComponent<Image>();
            if (childImage != null)
            {
                childImage.gameObject.SetActive(active);
            }
        }
    }

    public void enterColor(){
        activeColor = new Color(255f / 255f, 130 / 255f, 0 / 255f, 188 / 255f);
        inactiveColor = new Color(77 / 255f, 93 / 255f, 85 / 255f, 188 / 255f);
    }

    public void returnColor(){
        activeColor = new Color(255f / 255f, 130 / 255f, 0 / 255f, 255 / 255f);
        inactiveColor = new Color(20 / 255f, 73 / 255f, 108 / 255f, 255 / 255f);
    }
}
