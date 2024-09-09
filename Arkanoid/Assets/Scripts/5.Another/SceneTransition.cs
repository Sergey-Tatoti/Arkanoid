using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private GameObject _panelLoading;
    [SerializeField] private float _durationShowPanel;
    [SerializeField] private int _indexGameScene;

    public void HideLoadingPanel() => ShowPanel(false);

    public void LoadGameScene() => StartCoroutine(LoadScene(_indexGameScene));

    public void LoadMainScene() => StartCoroutine(LoadScene(0));

    private void ShowPanel(bool isShow)
    {
        ActivatePanel(true);

        if (isShow)
            _panelLoading.GetComponent<Image>().DOFade(0, _durationShowPanel).OnComplete(() => ActivatePanel(false));
        else
            _panelLoading.GetComponent<Image>().DOFade(1, _durationShowPanel);
    }

    private IEnumerator LoadScene(int index)
    {
        Time.timeScale = 1.0f;
        ShowPanel(false);

        yield return new WaitForSeconds(_durationShowPanel);
        SceneManager.LoadScene(index);
    }

    private void ActivatePanel(bool isActivate) => _panelLoading.SetActive(isActivate);
}
