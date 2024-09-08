using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayMamager : MonoBehaviour
{
    [Header("Компоненты Меню")]
    [SerializeField] private GameMenuUI _menuUI;
    [SerializeField] private Ball _ball;
    [SerializeField] private GameSaves _gameSaves;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private SceneTransition _sceneTransition;

    private int _numberStage;
    private int _countShoot;
    private int _countBlock;

    private void OnEnable()
    {
        _gameSaves.LoadedStage += OnLoadedStage;
        _gameSaves.LoadedStatusSound += OnLoadedStatusSound;
        _menuUI.ClickedButton += OnClickedButton;
        _menuUI.ChangedEnabledMusic += OnClickedButtonMusic;
        _menuUI.ClickedButtonNextStage += OnClickedButtonRestart;
        _menuUI.ClickedButtonRestart += OnClickedButtonRestart;
        _menuUI.ClickedButtonHome += OnClickedButtonHome;
        _ball.TouchedObject += OnTouchedObject;
        _ball.DestroyedBlock += OnDestroyedBlock;
        _ball.TouchedPlatform += OnTouchedPlatform;
    }

    private void OnDisable()
    {
        _gameSaves.LoadedStage -= OnLoadedStage;
        _gameSaves.LoadedStatusSound -= OnLoadedStatusSound;
        _menuUI.ClickedButton -= OnClickedButton;
        _menuUI.ChangedEnabledMusic -= OnClickedButtonMusic;
        _menuUI.ClickedButtonRestart -= OnClickedButtonRestart;
        _menuUI.ClickedButtonHome -= OnClickedButtonHome;
        _ball.TouchedObject -= OnTouchedObject;
        _ball.DestroyedBlock -= OnDestroyedBlock;
        _ball.TouchedPlatform -= OnTouchedPlatform;
    }

    private void SetValuesAfterLoading()
    {
        _menuUI.SetValue(_numberStage, _countShoot);
    }

    #region OnClicked

    private void OnClickedButtonMusic(bool isWork) { _soundManager.TurnOnListner(isWork); _gameSaves.SaveStatusSound(isWork); }

    private void OnClickedButton() => _soundManager.UseSound(SoundManager.Type.SoundClickedButton);

    private void OnClickedButtonRestart() => _sceneTransition.LoadGameScene();

    private void OnClickedButtonHome() => _sceneTransition.LoadMainScene();
    #endregion
    #region Loaded
    private void OnLoadedStage(int stage) { _numberStage = stage; SetValuesAfterLoading(); }

    private void OnLoadedStatusSound(bool isWork) { _soundManager.TurnOnListner(isWork); _menuUI.ChangeButtonSound(isWork); }
    #endregion

    private void OnTouchedObject() => _soundManager.UseSound(SoundManager.Type.SoundHitBall);

    private void OnDestroyedBlock()
    {
        _soundManager.UseSound(SoundManager.Type.SoundDestroyBlock);
        _countBlock--;

        if (_countBlock == 0)
        {
            _numberStage++;
            _gameSaves.SaveStage(_numberStage);
            EndGame(true, SoundManager.Type.SoundWinGame);
        }
    }

    private void OnTouchedPlatform()
    {
        _countShoot--;

        if (_countShoot == 0)
            EndGame(false, SoundManager.Type.SoundEndGame);
    }

    private void EndGame(bool isWin, SoundManager.Type typeSound)
    {
        _menuUI.ShowResultPanel(isWin);
        _soundManager.UseSound(typeSound);
        Time.timeScale = 0;
    }
}