using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector director;
    public List<PlayableAsset> cutscenes;

    private int lastGameState = -1;

    private void Update()
    {
        if (GameManager.Instance.gameState != lastGameState)
        {
            lastGameState = GameManager.Instance.gameState;
            HandleGameStateChange(GameManager.Instance.gameState);
        }
    }

    private void HandleGameStateChange(int state)
    {
        switch (state)
        {
            case 0:
                PlayCutscene(0);
                break;
            case 2:
                PlayCutscene(1);
                break;
            case 4:
                PlayCutscene(2);
                break;
            case 7:
                PlayCutscene(3);
                break;
            case 9:
                PlayCutscene(4);
                break;
            case 11:
                PlayCutscene(5);
                break;
            default:
                break;
        }
    }

    private void PlayCutscene(int index)
    {
        if (index >= 0 && index < cutscenes.Count && cutscenes[index] != null)
        {
            director.playableAsset = cutscenes[index];
            director.Play();
        }
        else
        {
            Debug.LogWarning($"Cutscene no índice {index} não encontrada.");
        }
    }
}
