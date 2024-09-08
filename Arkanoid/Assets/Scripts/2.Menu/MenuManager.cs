using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Компоненты Меню")]
    [SerializeField] private MenuUI _menuUI;
    [SerializeField] private MenuBackGround _menuBackGround;
    [SerializeField] private PlayerMenu _player;
    [SerializeField] private GameSaves _gameSaves;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private SceneTransition _sceneTransition;
    [Header("Список еды и время появления")]
    [SerializeField] private List<Apple> _apples;
    [SerializeField] private int _timeReloadApple;
    [Header("Cкорость заднего фона")]
    [SerializeField] private float _speedBackGround;
    [Header("Продолжительность вращения игрока")]
    [SerializeField] private float _offsetRotationZ;
    [SerializeField] private float _durationRotatePlayer;

    private void OnEnable()
    {
        _gameSaves.LoadedStage += OnLoadedStage;
        _gameSaves.LoadedStatusSound += OnLoadedStatusSound;
        _menuUI.ClickedButtonPlay += OnClickedButtonPlay;
        _menuUI.ClickedButton += OnClickedButton;
        _menuUI.ChangedEnabledMusic += OnClickedButtonMusic;

        for (int i = 0; i < _apples.Count; i++)
        {
            _apples[i].ClickedApple += OnClickedApple;
        }
    }

    private void OnDisable()
    {
        _gameSaves.LoadedStage -= OnLoadedStage;
        _gameSaves.LoadedStatusSound -= OnLoadedStatusSound;
        _menuUI.ClickedButtonPlay -= OnClickedButtonPlay;
        _menuUI.ClickedButton -= OnClickedButton;
        _menuUI.ChangedEnabledMusic -= OnClickedButtonMusic;

        for (int i = 0; i < _apples.Count; i++)
        {
            _apples[i].ClickedApple -= OnClickedApple;
        }
    }

    private void SetValuesAfterLoading()
    {
        _menuBackGround.SetValue(_speedBackGround);
        _player.StartRotate(_durationRotatePlayer, _offsetRotationZ);

        for (int i = 0; i < _apples.Count; i++) { _apples[i].SetValue(_timeReloadApple); }
    }

    private void OnClickedButtonMusic(bool isWork) { _soundManager.TurnOnListner(isWork); _gameSaves.SaveStatusSound(isWork); }

    private void OnClickedButton() => _soundManager.UseSound(SoundManager.Type.SoundClickedButton);

    private void OnClickedApple() => _soundManager.UseSound(SoundManager.Type.SoundEatingApple);

    private void OnClickedButtonPlay() => _sceneTransition.LoadGameScene();

    private void OnLoadedStage(int stage) { _menuUI.SetStage(stage); SetValuesAfterLoading(); }

    private void OnLoadedStatusSound(bool isWork) { _soundManager.TurnOnListner(isWork); _menuUI.ChangeButtonSound(isWork);}
}