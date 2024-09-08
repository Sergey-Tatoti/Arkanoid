using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _stageText;
    [SerializeField] private Button _buttonPlay;
    [SerializeField] private Button _buttonOnMusic;
    [SerializeField] private Button _buttonOffMusic;
    [SerializeField] private string _stage = "Stage";

    public event UnityAction ClickedButtonPlay;
    public event UnityAction ClickedButton;
    public event UnityAction<bool> ChangedEnabledMusic;

    private void OnEnable()
    {
        _buttonPlay.onClick.AddListener(ClickedButtonPlayGame);
        _buttonOnMusic.onClick.AddListener(() => ChangeButtonSound(false));
        _buttonOffMusic.onClick.AddListener(() => ChangeButtonSound(true));
    }

    private void OnDisable()
    {
        _buttonPlay.onClick.RemoveListener(ClickedButtonPlayGame);
        _buttonOnMusic.onClick.RemoveListener(() => ChangeButtonSound(false));
        _buttonOffMusic.onClick.RemoveListener(() => ChangeButtonSound(true));
    }

    public void SetStage(int score) => _stageText.text = _stage + " " + score.ToString();

    public void ChangeButtonSound(bool isOnMusic)
    {
        _buttonOnMusic.gameObject.SetActive(isOnMusic);
        _buttonOffMusic.gameObject.SetActive(!isOnMusic);

        ClickedButton?.Invoke();
        StartCoroutine(ChangeEnabledMusic(isOnMusic));
    }

    private void ClickedButtonPlayGame()
    {
        ClickedButton?.Invoke();
        ClickedButtonPlay?.Invoke();
    }

    private IEnumerator ChangeEnabledMusic(bool isOnMusic)
    {
        yield return new WaitForSeconds(0.1f);
        ChangedEnabledMusic?.Invoke(isOnMusic);
    }
}