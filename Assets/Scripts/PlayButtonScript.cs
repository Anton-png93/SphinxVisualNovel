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
        continueButton.SetActive(false); // <- сначала скрываем
        typingEffect.StartTyping(
        "Ты шёл по раскалённой пустыне, провизия закончилась, а во фляге плескались последние капли воды...\n\n" +
        "Ноги подкашивались, надежда угасала. Но вдруг — вдали среди песчаных бурь показался пик древней пирамиды.\n\n" +
        "По легенде, она принадлежала последнему царю этих забытых земель. А внутри, как гласят сказания, " +
        "обитает жуткий СФИНКС — хранитель сокровищницы древнего мира.\n\n" +
        "Он не знает жалости, не знает сна… Но, если сможешь одолеть его хитростью — все богатства станут твоими."
        );
    }
}