using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroesController : MonoBehaviour
{
    [Header("UI refs")]
    public Image heroImage;                // твой Herolmage (UI Image)
    public GameObject dialoguePanel;       // контейнер DialoguePanel (внутри рамка/фон/кнопка/текст)
    public TMP_Text nameText;              // NameText
    public TMP_Text dialogueText;          // DialogueText (TMP)
    public Button continueButton;          // ContinueButton

    [Header("Sequence")]
    public string heroName = "Фарид";
    public Color heroColor = new Color(1f, 0.84f, 0.2f); // мягко-жёлтый
    public float startDelay = 0.25f;       // пауза перед выездом
    public float slideOffsetX = 650f;      // насколько «за кадром» прячем героя слева
    public float slideDuration = 0.6f;     // длительность выезда

    [Header("Typing")]
    [TextArea] public string[] heroLines;  // реплики героя по предложениям
    public float charDelay = 0.02f;        // скорость печати
    public float afterLineDelay = 0.20f;   // пауза после строки перед показом кнопки

    RectTransform heroRT;
    Vector2 heroTargetPos;
    bool typing = false;
    int lineIndex = 0;
    int currentTotal = 0;

    void Awake()
    {
        heroRT = heroImage.rectTransform;

        // запоминаем целевую позицию и уводим героя влево за кадр
        heroTargetPos = heroRT.anchoredPosition;
        heroRT.anchoredPosition = heroTargetPos + new Vector2(-slideOffsetX, 0f);

        // выключаем панель диалога и кнопку, готовим тексты
        dialoguePanel.SetActive(false);
        continueButton.gameObject.SetActive(false);
        nameText.gameObject.SetActive(false);

        nameText.text = heroName;
        nameText.color = heroColor;
        dialogueText.text = "";
        dialogueText.color = heroColor;
    }

    void Start()
    {
        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        yield return new WaitForSeconds(startDelay);

        // выезд героя слева
        yield return StartCoroutine(Slide(heroRT, heroRT.anchoredPosition, heroTargetPos, slideDuration));

        yield return new WaitForSeconds(0.15f);
        nameText.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.15f);
        dialoguePanel.SetActive(true);

        // первая реплика
        lineIndex = 0;
        yield return StartCoroutine(TypeCurrentLine());
    }

    IEnumerator Slide(RectTransform rt, Vector2 from, Vector2 to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float k = Mathf.SmoothStep(0f, 1f, t / duration);
            rt.anchoredPosition = Vector2.LerpUnclamped(from, to, k);
            yield return null;
        }
        rt.anchoredPosition = to;
    }

    IEnumerator TypeCurrentLine()
    {
        typing = true;
        continueButton.gameObject.SetActive(false);

        dialogueText.text = heroLines[lineIndex];
        dialogueText.ForceMeshUpdate();
        currentTotal = dialogueText.textInfo.characterCount;
        dialogueText.maxVisibleCharacters = 0;

        int shown = 0;
        while (shown < currentTotal)
        {
            shown++;
            dialogueText.maxVisibleCharacters = shown;
            yield return new WaitForSeconds(charDelay);
        }

        typing = false;
        yield return new WaitForSeconds(afterLineDelay);
        continueButton.gameObject.SetActive(true);
    }

    // повесь на OnClick у ContinueButton
    public void OnContinue()
    {
        if (typing)
        {
            // добить строку мгновенно
            dialogueText.maxVisibleCharacters = currentTotal;
            typing = false;
            return;
        }

        continueButton.gameObject.SetActive(false);
        lineIndex++;

        if (lineIndex < heroLines.Length)
        {
            StartCoroutine(TypeCurrentLine());
        }
        else
        {
            // здесь позже запустим появление Сфинкса и диалог с ним
            // пока просто скрываем кнопку
            continueButton.gameObject.SetActive(false);
        }
    }
}