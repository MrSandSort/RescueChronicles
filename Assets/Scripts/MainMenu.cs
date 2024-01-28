using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        int cutscenesIndex = currentBuildIndex + 1;
        int level1Index = currentBuildIndex + 2;

        Debug.Log($"Cutscenes Index: {cutscenesIndex}");
        Debug.Log($"Level-1 Index: {level1Index}");

        SceneManager.LoadScene(cutscenesIndex);  // Load cutscenes scene
        SceneManager.LoadScene(level1Index, LoadSceneMode.Additive);  // Load level-1 scene additively
    }
}
