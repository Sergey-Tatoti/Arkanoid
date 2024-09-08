using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Type { MainMusic, GameMusic, SoundEatingApple, SoundClickedButton, SoundHitBall, SoundDestroyBlock, 
                       SoundEndGame, SoundWinGame }

    [SerializeField] private AudioListener _audioListener;
    [Header("Меню")]
    [SerializeField] private AudioSource _audioSoundEatingFood;
    [SerializeField] private AudioSource _audioSoundClickedButton;
    [SerializeField] private AudioSource _audioMusicMenu;
    [Header("Игра")]
    [SerializeField] private AudioSource _audioMusicGame;
    [SerializeField] private AudioSource _audioSoundHitBall;
    [SerializeField] private AudioSource _audioSoundDestroyBlock;
    [SerializeField] private AudioSource _audioSoundEndGame;
    [SerializeField] private AudioSource _audioSoundWinGame;

    public void TurnOnListner(bool isWork) => _audioListener.enabled = isWork;

    public void UseSound(SoundManager.Type typeSound)
    {
        switch (typeSound)
        {
            case SoundManager.Type.MainMusic:
                _audioMusicMenu.Play();
                break;
            case SoundManager.Type.GameMusic:
                _audioMusicGame.Play();
                break;
            case SoundManager.Type.SoundEatingApple:
                _audioSoundEatingFood.Play();
                break;
            case SoundManager.Type.SoundClickedButton:
                _audioSoundClickedButton.Play();
                break;
            case SoundManager.Type.SoundHitBall:
                _audioSoundHitBall.Play();
                break;
            case SoundManager.Type.SoundDestroyBlock:
                _audioSoundDestroyBlock.Play();
                break;
            case SoundManager.Type.SoundEndGame:
                _audioMusicGame.Stop();
                _audioSoundEndGame.Play();
                break;
            case SoundManager.Type.SoundWinGame:
                _audioMusicGame.Stop();
                _audioSoundWinGame.Play();
                break;
        }
    }
}