using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector director;
    public PlayableAsset cutscene1;
    public PlayableAsset cutscene2;

    private int lastGameState = -1; // guarda o último estado para detectar mudanças

    private void Update()
    {
        if (GameManager.Instance.gameState != lastGameState)
        {
            lastGameState = GameManager.Instance.gameState;

            switch (GameManager.Instance.gameState)
            {
                case 0:
                    director.playableAsset = cutscene1;
                    director.Play();
                    break;
                case 2:
                    director.playableAsset = cutscene2;
                    director.Play();
                    break;
                default:
                    break;
            }
        }
    }
}
