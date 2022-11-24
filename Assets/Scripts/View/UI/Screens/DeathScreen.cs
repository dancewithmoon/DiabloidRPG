using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View.UI.Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DeathScreen : MonoBehaviour
    {
        private CanvasGroup _screen;

        private void Awake()
        {
            _screen = GetComponent<CanvasGroup>();
            _screen.alpha = 0;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            StartCoroutine(ShowScreenCoroutine());
        }

        private IEnumerator ShowScreenCoroutine()
        {
            while(_screen.alpha != 1)
            {
                _screen.alpha += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(0);
        }

    }
}
