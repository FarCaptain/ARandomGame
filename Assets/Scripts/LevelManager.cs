using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Image prograssBar;
    private float target;

    #region Singleton
    public static LevelManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    public async void LoadScene(string sceneName)
    {
        target = 0f;
        prograssBar.fillAmount = 0f;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(500);
            target = scene.progress;
        } while (prograssBar.fillAmount < 0.9f);

        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
    }

    private void Update()
    {
        prograssBar.fillAmount = Mathf.MoveTowards(prograssBar.fillAmount, target, 3 * Time.deltaTime);
    }
}
