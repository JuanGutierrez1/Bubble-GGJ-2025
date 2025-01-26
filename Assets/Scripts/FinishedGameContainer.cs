using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishedGameContainer : MonoBehaviour
{
    [SerializeField]
    private RectTransform winContainer;

    [SerializeField]
    private int nextLevel;

    public void ShowContainer()
    {
        winContainer.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutCirc);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
