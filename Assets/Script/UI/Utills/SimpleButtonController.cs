using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleButtonController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ShowPopup(GameObject popupPrefab)
    {
        Instantiate(popupPrefab, transform);
    }

}
