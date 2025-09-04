using UnityEngine;

public class HeroController : MonoBehaviour
{
    public Animator anim; // теперь поле публичное

    public void PlayEntrance()
    {
        Debug.Log("PlayEntrance вызван");
        anim.SetTrigger("PlayHero");
    }
}