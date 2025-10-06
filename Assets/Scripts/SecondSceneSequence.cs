using System.Collections;
using UnityEngine;

public class SecondSceneSequence : MonoBehaviour
{
    public GameObject backgroundRuins;
    public GameObject heroImage;
    public GameObject goldBackground;
    public GameObject richFrame;
    public GameObject heroNameText;

    public void StartSequence()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        backgroundRuins.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        heroImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        goldBackground.SetActive(true);
        richFrame.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        heroNameText.SetActive(true);
    }
}