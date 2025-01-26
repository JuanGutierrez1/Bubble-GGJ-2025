using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelIndicator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelIndicator;
    private void Start()
    {
        levelIndicator.text = $"Nivel {SceneManager.GetActiveScene().buildIndex}";
    }
}
