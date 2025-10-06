using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroesController1 : MonoBehaviour
{
    [Header("UI refs")]
    [SerializeField] private Image heroImage;          // объект героя (UI Image)
    [SerializeField] private Image sphinxImage;        // объект сфинкса (UI Image)
    [SerializeField] private RectTransform dialoguePanel;
    [SerializeField] private TMP_Text nameText;        // имя над рамкой
    [SerializeField] private TMP_Text dialogueText;    // основной текст
    [SerializeField] private Button continueButton;

    [Header("Sequence")]
    [SerializeField] private string heroName = "Фарид";
    [SerializeField] private Color heroColor = new Color(1f, 0.85f, 0.2f);   // тёпло-жёлтый
    [SerializeField] private string sphinxName = "СФИНКС";
    [SerializeField] private Color sphinxColor = new Color(0.9f, 0.25f, 0.2f); // красный

    [SerializeField] private float startDelay = 0.25f;
    [SerializeField] private float slideOffsetX = 650f;   // насколько уезжаем за край
    [SerializeField] private float slideDuration = 0.6f;  // время въезда/выезда

    [Header("Typing")]
    [SerializeField] private float charDelay = 0.02f;     // задержка между символами
    [SerializeField] private float afterLineDelay = 0.2f; // пауза после строки перед кнопкой

    [Header("Replica sets")]
    [TextArea(2,6)] [SerializeField] private string[] heroLines;
    [TextArea(2,6)] [SerializeField] private string[] sphinxLines;

    // внутреннее
    private Vector2 heroHome;
    private Vector2 sphinxHome;
    private bool waitClick;

    private void Awake()
    {
        if (continueButton) continueButton.gameObject.SetActive(false);

        // Запоминаем «домашние» позиции (куда должны приехать)
        if (heroImage)   heroHome   = heroImage.rectTransform.anchoredPosition;
        if (sphinxImage) sphinxHome = sphinxImage.rectTransform.anchoredPosition;

        // Уводим за экран
        if (heroImage)   heroImage.rectTransform.anchoredPosition   = heroHome   + new Vector2(-slideOffsetX, 0f);
        if (sphinxImage) sphinxImage.rectTransform.anchoredPosition = sphinxHome + new Vector2(+slideOffsetX, 0f);
    }

    private void OnEnable()
    {
        if (continueButton) continueButton.onClick.AddListener(OnContinue);
        StartCoroutine(RunSequence());
    }

    private void OnDisable()
    {
        if (continueButton) continueButton.onClick.RemoveListener(OnContinue);
    }

    private IEnumerator RunSequence()
    {
        yield return new WaitForSeconds(startDelay);

        // ——— Въезд героя
        if (heroImage) yield return SlideX(heroImage.rectTransform, heroImage.rectTransform.anchoredPosition, heroHome, slideDuration);

        // Имя/цвет героя
        if (nameText)    { nameText.text = heroName; nameText.color = heroColor; }
        if (dialogueText){ dialogueText.color = heroColor; }

        // Реплики героя
        yield return TypeAll(heroLines);

        // Убираем героя влево
        if (heroImage) yield return SlideX(heroImage.rectTransform, heroHome, heroHome + new Vector2(-slideOffsetX, 0f), 0.45f);

        // ——— Въезд сфинкса справа
        if (sphinxImage) yield return SlideX(sphinxImage.rectTransform, sphinxImage.rectTransform.anchoredPosition, sphinxHome, slideDuration);

        // Имя/цвет сфинкса
        if (nameText)    { nameText.text = sphinxName; nameText.color = sphinxColor; }
        if (dialogueText){ dialogueText.color = sphinxColor; }

        // Реплики сфинкса (до загадки; ввод подключим следующим шагом)
        yield return TypeAll(sphinxLines);

        // Здесь можешь вызвать следующую логику (загадка/инпут) или показать кнопку «Продолжить»
        if (continueButton) continueButton.gameObject.SetActive(true);
    }

    // Печать всех строк с ожиданием клика после каждой
    private IEnumerator TypeAll(string[] lines)
    {
        if (lines == null || lines.Length == 0) yield break;

        for (int i = 0; i < lines.Length; i++)
        {
            yield return TypeLine(lines[i]);
            yield return new WaitForSeconds(afterLineDelay);

            // Кнопка «Продолжить»
            if (continueButton) continueButton.gameObject.SetActive(true);
            waitClick = true;
            yield return new WaitUntil(() => waitClick == false);
            if (continueButton) continueButton.gameObject.SetActive(false);
        }
    }

    // Печать одной строки
    private IEnumerator TypeLine(string line)
    {
        if (!dialogueText) yield break;

        dialogueText.text = line ?? "";
        dialogueText.ForceMeshUpdate();
        dialogueText.maxVisibleCharacters = 0;

        // идём по символам
        int total = dialogueText.textInfo.characterCount;
        for (int i = 0; i < total; i++)
        {
            dialogueText.maxVisibleCharacters = i + 1;
            yield return new WaitForSeconds(charDelay);
        }
    }

    // Въезд/выезд по X
    private IEnumerator SlideX(RectTransform rt, Vector2 from, Vector2 to, float duration)
    {
        if (!rt || duration <= 0f) { if (rt) rt.anchoredPosition = to; yield break; }

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / duration);
            rt.anchoredPosition = Vector2.LerpUnclamped(from, to, Smooth(k));
            yield return null;
        }
        rt.anchoredPosition = to;
    }

    // чуть мягче, чем линейно
    private float Smooth(float x) => x * x * (3f - 2f * x);

    private void OnContinue() => waitClick = false;
}