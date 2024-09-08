using UnityEngine;
using UnityEngine.Events;

public class GameSaves : MonoBehaviour
{
    private const string SaveKey = "GameSaves";

    private SaveData.GameData _data;

    public event UnityAction<int> LoadedStage;
    public event UnityAction<bool> LoadedStatusSound;

    private void Start()
    {
        _data = SaveManager.Load<SaveData.GameData>(SaveKey);

        LoadedStage?.Invoke(_data.Stage);
        LoadedStatusSound?.Invoke(_data.IsWorkSound);
    }

    #region SaveOptions
    public void SaveStatusSound(bool isWork) => SaveManager.Save(SaveKey, GetSaveStatusSoundSnapshot(isWork));

    public void SaveStage(int stage) => SaveManager.Save(SaveKey, GetSaveStageSnapshotis(stage));

    SaveData.GameData GetSaveStatusSoundSnapshot(bool isWork) {_data.IsWorkSound = isWork; return _data; }

    SaveData.GameData GetSaveStageSnapshotis(int stage) { _data.Stage = stage; return _data; }
    #endregion
}