using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadFirstLevel(int id)
    {
        SceneManager.LoadScene(id, LoadSceneMode.Single);
    }
}
