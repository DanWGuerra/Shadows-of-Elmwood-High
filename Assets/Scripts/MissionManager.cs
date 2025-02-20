using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;


[System.Serializable]
public class Mission
{
    public int id;
    public string description;
    public bool completed;
}

[System.Serializable]
public class MissionList
{
    public List<Mission> missions;
}


public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;
    private MissionList missionList;
    private string filePath;

    public event Action OnMissionUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        filePath = Path.Combine(Application.streamingAssetsPath, "missions.json");
        LoadMissions();
        ResetMissions(); // Reinicia misiones al iniciar el juego
    }

    private void LoadMissions()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            missionList = JsonUtility.FromJson<MissionList>(json);
        }
        else
        {
            Debug.LogError("No se encontró el archivo de misiones.");
        }
    }

    private void ResetMissions()
    {
        foreach (var mission in missionList.missions)
        {
            mission.completed = false;
        }
        SaveMissions();
        OnMissionUpdated?.Invoke();
    }

    public void CompleteMission(int missionId)
    {
        Mission mission = missionList.missions.Find(m => m.id == missionId);
        if (mission != null && !mission.completed)
        {
            mission.completed = true;
            SaveMissions();
            OnMissionUpdated?.Invoke();
        }
    }

    private void SaveMissions()
    {
        string json = JsonUtility.ToJson(missionList, true);
        File.WriteAllText(filePath, json);
    }

    public Mission GetCurrentMission()
    {
        return missionList.missions.Find(m => !m.completed);
    }
}

