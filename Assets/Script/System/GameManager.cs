using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Newvia
{
    public class GameManager : Singleton<GameManager>
    {
        public int killCount { get; set; }//킬 카운트
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("TestScene");
            }
        }
    }

}
