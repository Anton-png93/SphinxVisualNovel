using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroController : MonoBehaviour
{
    public TMP_Text dialogueText;      // DialogueText (TMP)
    public Button continueButton;      // ContinueButton (Button на твоём спрайте)
    [TextArea(2,4)] public string[] lines; // Сюда вставь фразы из п.1
    public float charDelay = 0.02f;    // скорость печати

    int index = 0;
    bool typing;
    Coroutine typingCo;

    void Start()
    {
        dialogueText.text = "";
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(OnContinue);
        ShowNext();
    }

    void OnContinue()
    {
        if (typing)
        {
            typing = false; // докрутить мгновенно
            dialogueText.maxVisibleCharacters = dialogueText.text.Length;
        }
        else
        {
            ShowNext();
        }
    }

    void ShowNext()
    {
        if (index >= lines.Length)
        {
            continueButton.gameObject.SetActive(false); // на последней фразе кнопку уберём
            return;
        }

        continueButton.gameObject.SetActive(false);
        if (typingCo != null) StopCoroutine(typingCo);
        typingCo = StartCoroutine(TypeLine(lines[index++]));
    }

    IEnumerator TypeLine(string line)
    {
        typing = true;
        dialogueText.text = line;
        dialogueText.ForceMeshUpdate();                   // корректный подсчёт символов
        dialogueText.maxVisibleCharacters = 0;
        int total = dialogueText.textInfo.characterCount;

        for (int i = 0; i <= total; i++)
        {
            if (!typing) break;
            dialogueText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(charDelay);
        }

        dialogueText.maxVisibleCharacters = total;
        typing = false;
        continueButton.gameObject.SetActive(true);        // показать «Продолжить»
    }
}