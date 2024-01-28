using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public void Cutscenes() {

        SceneManager.LoadSceneAsync(2);
    }

}
