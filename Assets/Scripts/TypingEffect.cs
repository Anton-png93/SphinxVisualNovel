using System.Collections;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Поле для текста
    public string fullText;              // Полный текст
    public float typingSpeed = 0.03f;    // Скорость печати
    public GameObject continueButton;    // Кнопка "Продолжить"

    private Coroutine typingCoroutine;

    public void StartTyping(string newText)
    {
        fullText = newText;
        dialogueText.text = "";
        continueButton.SetActive(false);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true); // Показать кнопку после печати
    }
}