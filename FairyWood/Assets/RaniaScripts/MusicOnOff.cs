using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicOnOff : MonoBehaviour
{
    private Sprite SoundOnImage;
    private Sprite SoundOffImage;
    public Button button;
    public bool isOn = true;

    public AudioSource audioSource;
    void Start()
    {
        if (button != null && SoundOnImage != null)
        {
            button.image.sprite = SoundOnImage;
        }
        else
        {
            Debug.LogError("Assurez-vous d'assigner les sprites et le bouton dans l'éditeur !");
        }

        if (button != null)
        {
            button.onClick.AddListener(ButtonClicked);
        }
    }

    public void ButtonClicked()
    {
        isOn = !isOn;

        button.image.sprite = isOn ? SoundOnImage : SoundOffImage;

        if (audioSource != null)
        {
            if (isOn)
                audioSource.Play();
            else
                audioSource.Pause();
        }
    }
}
