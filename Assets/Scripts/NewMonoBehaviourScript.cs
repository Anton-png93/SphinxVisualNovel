using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public Sprite soundOnIcon;      // Обычный значок
    public Sprite soundOffIcon;     // Зачёркнутый значок
    private Image buttonImage;      // Компонент Image на кнопке

    private bool isSoundOn = true;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        UpdateIcon();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;

        AudioListener.volume = isSoundOn ? 1f : 0f;

        UpdateIcon();
    }

    private void UpdateIcon()
    {
        buttonImage.sprite = isSoundOn ? soundOnIcon : soundOffIcon;
    }
}