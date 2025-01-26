using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopMusic : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }
}
