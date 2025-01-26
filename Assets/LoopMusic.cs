using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopMusic : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);   
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }
}
