using System.Collections.Generic;
using UnityEngine;

public class GamePlayMamager : MonoBehaviour
{
    [Header("Компоненты Меню")]
    [SerializeField] private GameMenuUI _menuUI;
    [SerializeField] private BlockManager _blockManager;
    [SerializeField] private Ball _ball;
    [SerializeField] private Player _player;
    [SerializeField] private GameSaves _gameSaves;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private SceneTransition _sceneTransition;
    [SerializeField] private List<Stage> _stages = new List<Stage>();
    [SerializeField] private int _countLife = 3;
    [SerializeField] private float _offsetPositionBallY;

    private int _numberStage;
    private int _countBlock;
    private Stage _stage;
    private bool _isPlay = true;

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
        _ball.TouchedGameDestroyer += OnTouchedBallGameDestroyer;
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
        _ball.TouchedGameDestroyer -= OnTouchedBallGameDestroyer;
        _stage.DestroyedBlock -= OnDestroyedBlock;
    }

    private void SetValuesAfterLoading()
    {
        SetStage();
        _countBlock = _stage.GetCountWorkBlocks();

        _soundManager.UseSound(SoundManager.Type.GameMusic);
        _menuUI.SetValue(_countBlock, _countLife);
        _player.SetValue();
        _stage.SetValue(_blockManager);
        _ball.SetPosition(_player, _offsetPositionBallY);
        _stage.gameObject.SetActive(true);

        _stage.DestroyedBlock += OnDestroyedBlock;
    }

    private void Update()
    {
        if (_isPlay)
        {
            _player.MovePlatform();

            if (Input.GetKey(KeyCode.Space))
                _ball.TryActivate();
        }
    }

    #region OnClicked

    private void OnClickedButtonMusic(bool isWork) { _soundManager.TurnOnListner(isWork); _gameSaves.SaveStatusSound(isWork); }

    private void OnClickedButton() => _soundManager.UseSound(SoundManager.Type.SoundClickedButton);

    private void OnClickedButtonRestart() { _isPlay = false; _sceneTransition.LoadGameScene(); }

    private void OnClickedButtonHome() { _isPlay = false; _sceneTransition.LoadMainScene(); }
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

    private void OnTouchedBallGameDestroyer()
    {
        _countLife--;
        _menuUI.SetValue(_countBlock, _countLife);

        if (_countLife == 0)
            EndGame(false, SoundManager.Type.SoundEndGame);
        else
            SpawnBall();
    }

    private void EndGame(bool isWin, SoundManager.Type typeSound)
    {
        _soundManager.UseSound(typeSound);
        _menuUI.ShowResultPanel(isWin);
    }

    private void SpawnBall()
    {
        _ball.gameObject.SetActive(false);
        _ball.gameObject.SetActive(true);
        _ball.SetPosition(_player, _offsetPositionBallY);
    }

    private void SetStage()
    {
        if (_stages.Count > _numberStage)
            _stage = _stages[_numberStage];
        else
            _stage = _stages[Random.Range(0, _stages.Count - 1)];
    }
}