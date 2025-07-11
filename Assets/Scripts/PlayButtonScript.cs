using UnityEngine;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour
{
    public GameObject title;            // СФИНКС
    public GameObject playButton;       // Кнопка ИГРАТЬ
    public GameObject soundButton;      // Кнопка звука
    public GameObject camelBackground;  // Новый фон
    public GameObject dialoguePanel;    // Панель диалога

    public void StartGame()
    {
        title.SetActive(false);
        playButton.SetActive(false);
        soundButton.SetActive(false);

        camelBackground.SetActive(true);
        dialoguePanel.SetActive(true);
    }
}