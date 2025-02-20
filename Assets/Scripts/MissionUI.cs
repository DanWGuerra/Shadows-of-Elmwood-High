using UnityEngine;
using TMPro;
using System.Collections;

public class MissionUI : MonoBehaviour
{
    public TextMeshProUGUI missionText;
    private bool isMissionCompleted = false; // Nuevo flag para controlar la visualización

    private void Start()
    {
        if (MissionManager.Instance != null)
        {
            MissionManager.Instance.OnMissionUpdated += UpdateMissionText;
        }
        UpdateMissionText(); // Muestra la misión inicial correctamente
    }

    private void OnDestroy()
    {
        if (MissionManager.Instance != null)
        {
            MissionManager.Instance.OnMissionUpdated -= UpdateMissionText;
        }
    }

    public void UpdateMissionText()
    {
        Mission currentMission = MissionManager.Instance.GetCurrentMission();

        if (isMissionCompleted)
        {
            StartCoroutine(ShowMissionCompletedAndUpdate(currentMission));
        }
        else
        {
            missionText.text = currentMission != null ? "Misión: " + currentMission.description : "¡Todas las misiones completadas!";
        }
    }

    private IEnumerator ShowMissionCompletedAndUpdate(Mission nextMission)
    {
        missionText.text = "¡Misión completada!";
        yield return new WaitForSeconds(2f);

        if (nextMission != null)
        {
            missionText.text = "Misión: " + nextMission.description;
        }
        else
        {
            missionText.text = "¡Todas las misiones completadas!";
        }

        isMissionCompleted = false; // Reseteamos el flag después de mostrar la misión
    }
}


