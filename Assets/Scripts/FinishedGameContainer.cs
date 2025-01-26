using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class FinishedGameContainer : MonoBehaviour
{
    [SerializeField]
    private Launcher launcher;

    [SerializeField]
    private GameObject[] toiletPapers;

    [SerializeField]
    private RectTransform winContainer;

    [SerializeField]
    private int nextLevel;

    [SerializeField]
    private GameObject continueButton, restartButton;

    public void ShowContainer(bool hasWon)
    {
        winContainer.DOLocalMoveY( 0, 0.5f).SetEase(Ease.OutCirc);

        if (hasWon)
        {
            toiletPapers[0].SetActive(launcher.Projectiles >= launcher.oneStar);
            toiletPapers[1].SetActive(launcher.Projectiles >= launcher.twoStars);
            toiletPapers[2].SetActive(launcher.Projectiles >= launcher.threeStars);
            continueButton.SetActive(true);
            restartButton.SetActive(false);
        }
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
