using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{

    private void OnEnable()
    {
        ActionHelper.GoHome += LoadLevel;
    }
    private void OnDisable()
    {
        ActionHelper.GoHome -= LoadLevel;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
