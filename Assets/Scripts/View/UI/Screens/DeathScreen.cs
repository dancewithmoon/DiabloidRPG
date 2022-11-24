using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class DeathScreen : MonoBehaviour
{
    private CanvasGroup screen;

    private void Awake()
    {
        screen = GetComponent<CanvasGroup>();
        screen.alpha = 0;
    }

    public void Show()
    {
        StartCoroutine(ShowScreenCoroutine());
    }

    private IEnumerator ShowScreenCoroutine()
    {
        while(screen.alpha != 1)
        {
            screen.alpha += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

}
