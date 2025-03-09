using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private int gameSceneIndex = 1;

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }

}
