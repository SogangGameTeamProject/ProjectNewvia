using Newvia;
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

    public void OpenPopup(GameObject popupPrefab)
    {
        popupPrefab.SetActive(true);
    }

    public void ColsePopup(GameObject popupPrefab) {
        popupPrefab.SetActive(false);
    }

    public void PlayBtnSound(AudioClip clip)
    {
        SoundManger.Instance.PlaySFX(clip);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
