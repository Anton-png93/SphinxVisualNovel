using System.Collections;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // Поле для текста
    public string fullText;               // Полный текст
    public float typingSpeed = 0.03f;     // Скорость печати символов
    public GameObject continueButton;     // Кнопка "Продолжить"

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
        }
    }
}