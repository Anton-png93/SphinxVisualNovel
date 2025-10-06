using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroController : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text dialogueText;
    public Button continueButton;

    [Header("Text")]
    [TextArea(2, 4)] public string[] lines;
    public float charDelay = 0.02f;

    [Header("SFX")]
    public AudioSource sfxSource;     // перетащи сюда твой AudioSource (SFX)
    public AudioClip typeSfx;         // короткий "блип"
    public AudioClip clickSfx;        // (необязательно) звук кнопки
    [Range(0f, 0.2f)] public float sfxInterval = 0.04f; // минимум между блепами
    public bool randomizePitch = true;
    [Range(0f, 0.5f)] public float pitchJitter = 0.06f; // +/- к питчу

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
            typing = false;
            dialogueText.maxVisibleCharacters = dialogueText.text.Length;
            if (sfxSource) sfxSource.Stop();
        }
        else
        {
            if (clickSfx && sfxSource) sfxSource.PlayOneShot(clickSfx);
            ShowNext();
        }
    }

    void ShowNext()
    {
        if (index >= lines.Length)
        {
            continueButton.gameObject.SetActive(false);
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
        dialogueText.ForceMeshUpdate();
        dialogueText.maxVisibleCharacters = 0;

        int total = dialogueText.textInfo.characterCount;
        float nextSfxTime = 0f;

        for (int i = 0; i <= total; i++)
        {
            if (!typing) break;

            dialogueText.maxVisibleCharacters = i;

            // Проигрываем "блип" не чаще sfxInterval и только на печатаемые символы
            if (typeSfx && sfxSource && Time.time >= nextSfxTime && i > 0)
            {
                char ch = (i - 1 < line.Length) ? line[i - 1] : ' ';
                if (!char.IsWhiteSpace(ch) && !".,!?;:—…-".Contains(ch))
                {
                    if (randomizePitch) sfxSource.pitch = 1f + Random.Range(-pitchJitter, pitchJitter);
                    sfxSource.PlayOneShot(typeSfx);
                    nextSfxTime = Time.time + sfxInterval;
                }
            }

            yield return new WaitForSeconds(charDelay);
        }

        dialogueText.maxVisibleCharacters = total;
        typing = false;
        continueButton.gameObject.SetActive(true);
    }
}