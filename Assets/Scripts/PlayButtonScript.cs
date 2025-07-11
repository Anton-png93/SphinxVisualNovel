using UnityEngine;

public class PlayButtonScript : MonoBehaviour
{
    public GameObject title;              // СФИНКС
    public GameObject playButton;         // Кнопка ИГРАТЬ
    public GameObject soundButton;        // Кнопка звука
    public GameObject camelBackground;    // Фон с верблюдом
    public GameObject dialoguePanel;      // Панель с текстом
    public GameObject richFrame;          // Рамка
    public GameObject continueButton;     // Кнопка Продолжить

    public void StartGame()
    {
        title.SetActive(false);
        playButton.SetActive(false);
        soundButton.SetActive(false);

        camelBackground.SetActive(true);
        dialoguePanel.SetActive(true);
        richFrame.SetActive(true);
        continueButton.SetActive(true);
    }
}