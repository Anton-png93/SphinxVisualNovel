using UnityEngine;
using TMPro;

public class PlayButtonScript : MonoBehaviour
{
    public GameObject title;              // СФИНКС
    public GameObject playButton;         // Кнопка ИГРАТЬ
    public GameObject soundButton;        // Кнопка звука
    public GameObject camelBackground;    // Фон с верблюдом
    public GameObject dialoguePanel;      // Панель с текстом
    public GameObject richFrame;          // Рамка
    public GameObject continueButton;     // Кнопка Продолжить
    public GameObject goldBackground;
    public TypingEffect typingEffect; // Эффект печати текста

    public void StartGame()
    {
        title.SetActive(false);
        playButton.SetActive(false);
        soundButton.SetActive(false);

        camelBackground.SetActive(true);
        dialoguePanel.SetActive(true);
        richFrame.SetActive(true);
        goldBackground.SetActive(true);
        continueButton.SetActive(true);
        typingEffect.StartTyping("Ты осмелился войти в гробницу древнего царя...");
    }
}