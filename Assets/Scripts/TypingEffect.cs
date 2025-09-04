using System.Collections;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // Поле для текста
    public string fullText;               // Полный текст
    public float typingSpeed = 0.03f;     // Скорость печати символов
    public GameObject continueButton;     // Кнопка "Продолжить"
    public GameObject heroImage;         // Спрайт героя
    public TextMeshProUGUI heroNameText; // Имя героя
    public string heroName = "ФАРИД";    // Имя, которое будет отображаться
    public GameObject desertBackground; // фон пустыни
    public GameObject ruinsBackground;  // фон руин

    private Coroutine typingCoroutine;
    private string[] sentences;
    private int sentenceIndex = 0;

    public void StartTyping(string newText)
    {
        fullText = newText;
        sentences = fullText.Split(new[] { '.' }, System.StringSplitOptions.RemoveEmptyEntries);
        sentenceIndex = 0;
        dialogueText.text = "";
        continueButton.SetActive(false);


        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        string sentence = sentences[sentenceIndex].Trim() + ".";
        dialogueText.text = "";

        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
    }

    public void NextSentence()
    {
        Debug.Log("Кнопка нажата — метод NextSentence вызван");

        continueButton.SetActive(false);
        sentenceIndex++;
        // Если это первая фраза — меняем фон и показываем героя


        if (sentenceIndex < sentences.Length)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(TypeSentence());
        }
        else
        {
            // Когда фразы закончились — кнопка больше не нужна
            continueButton.SetActive(false);
            // Переход к сцене героя
            StartCoroutine(ShowHeroWithDelay());
        }

        IEnumerator ShowHeroWithDelay()
        {
            // Меняем фон сразу
            desertBackground.SetActive(false);
            ruinsBackground.SetActive(true);

            // Ждём 1 секунду, затем показываем героя с анимацией
            yield return new WaitForSeconds(1f);
            heroImage.SetActive(true);
            heroImage.GetComponent<Animator>().SetTrigger("PlayHero");

            // Ждём ещё 1 секунду, затем показываем имя и запускаем фразу
            yield return new WaitForSeconds(1f);
            heroNameText.text = heroName;
            heroNameText.color = Color.yellow;
            heroNameText.gameObject.SetActive(true);

            StartTyping("Наконец-то я добрался... Это было нелегко, но я справился.");
        }
    }
}