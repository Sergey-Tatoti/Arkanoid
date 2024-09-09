using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [Header("Тексты")]
    [SerializeField] private TMP_Text _blockText;
    [SerializeField] private TMP_Text _lifeText;
    [SerializeField] private string _block = "Block";
    [Header("Кнопки")]
    [SerializeField] private List<Button> _buttonsRestart;
    [SerializeField] private List<Button> _buttonsHome;
    [SerializeField] private Button _buttonPause;
    [SerializeField] private Button _buttonBackPause;
    [SerializeField] private Button _ButtonNextStage;
    [SerializeField] private Button _buttonOnMusic;
    [SerializeField] private Button _buttonOffMusic;
    [Header("Панели")]
    [SerializeField] private GameObject _panelPauseGame;
    [SerializeField] private GameObject _panelLoseGame;
    [SerializeField] private GameObject _panelWinGame;

    public event UnityAction ClickedButtonNextStage;
    public event UnityAction ClickedButtonHome;
    public event UnityAction ClickedButtonRestart;
    public event UnityAction<bool> ChangedEnabledMusic;
    public event UnityAction ClickedButton;


    private void OnEnable()
    {
        _ButtonNextStage.onClick.AddListener(() => ClickedButtonNextStage?.Invoke());
        _buttonOnMusic.onClick.AddListener(() => ChangeButtonSound(false));
        _buttonOffMusic.onClick.AddListener(() => ChangeButtonSound(true));
        _buttonPause.onClick.AddListener(() => ClickedButtonPause(true));
        _buttonBackPause.onClick.AddListener(() => ClickedButtonPause(false));

        for (int i = 0; i < _buttonsRestart.Count; i++) { _buttonsRestart[i].onClick.AddListener(() => ClickedButtonRestart?.Invoke()); }

        for (int i = 0; i < _buttonsHome.Count; i++) { _buttonsHome[i].onClick.AddListener(() => ClickedButtonHome?.Invoke()); }
    }

    private void OnDisable()
    {
        _ButtonNextStage.onClick.RemoveListener(() => ClickedButtonNextStage?.Invoke());
        _buttonOnMusic.onClick.RemoveListener(() => ChangeButtonSound(false));
        _buttonOffMusic.onClick.RemoveListener(() => ChangeButtonSound(true));
        _buttonPause.onClick.RemoveListener(() => ClickedButtonPause(true));
        _buttonBackPause.onClick.RemoveListener(() => ClickedButtonPause(false));

        for (int i = 0; i < _buttonsRestart.Count; i++) { _buttonsRestart[i].onClick.RemoveListener(() => ClickedButtonRestart?.Invoke()); }

        for (int i = 0; i < _buttonsHome.Count; i++) { _buttonsHome[i].onClick.RemoveListener(() => ClickedButtonHome?.Invoke()); }
    }

    public void SetValue(int stage, int life)
    {
        _blockText.text = _block + " " + stage.ToString();
        _lifeText.text = life.ToString();
    }

    public void ShowResultPanel(bool isWin) 
    { 
        _panelWinGame.gameObject.SetActive(isWin); 
        _panelLoseGame.gameObject.SetActive(!isWin);
        Time.timeScale = 0;
    }

    public void ChangeButtonSound(bool isOnMusic)
    {
        _buttonOnMusic.gameObject.SetActive(isOnMusic);
        _buttonOffMusic.gameObject.SetActive(!isOnMusic);

        ClickedButton?.Invoke();
        StartCoroutine(ChangeEnabledMusic(isOnMusic));
    }

    private IEnumerator ChangeEnabledMusic(bool isOnMusic)
    {
        yield return new WaitForSeconds(0.1f);
        ChangedEnabledMusic?.Invoke(isOnMusic);
    }

    private void ClickedButtonPause(bool isPause)
    {
        _panelPauseGame.gameObject.SetActive(isPause);

        if (isPause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}