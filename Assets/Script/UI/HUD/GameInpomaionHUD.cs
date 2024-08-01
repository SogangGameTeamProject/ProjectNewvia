using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Newvia
{
    public class GameInpomaionHUD : MonoBehaviour
    {
        private GameManager _gameManager = null;
        public Text killCountTxt = null;
        private int killCount = 0;

        private void Start()
        {
            _gameManager = GameManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            killCount = _gameManager.killCount;
            if (killCountTxt != null)
                killCountTxt.text = killCount.ToString();
        }
    }
}
