using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Newvia
{
    public class GameManager : Singleton<GameManager>
    {
        private MonsterSpawnManager _monsterSpawnManager = null;
        public int killCount { get; set; }//킬 카운트
        
        public GameFlowType flowType { get; set; }

        public WavesInfo wavesInfo = null;//웨이브들의 정보
        private int nowWave = -1;//현제 웨이브

        public GameObject gameOverPopup = null;
        public GameObject gmaeClearPopup = null;

        private void Start()
        {
            Debug.Log("게임 시작");
            _monsterSpawnManager = MonsterSpawnManager.Instance;
            GameFlowEventBus.Publish(GameFlowType.GameStart);
        }

        private void OnEnable()
        {
            GameFlowEventBus.Subscribe(GameFlowType.GameStart, GameStart);
            GameFlowEventBus.Subscribe(GameFlowType.Proceeding, Proceeding);
            GameFlowEventBus.Subscribe(GameFlowType.GameClear, GameClear);
            GameFlowEventBus.Subscribe(GameFlowType.GameOver, GameOver);
            GameFlowEventBus.Subscribe(GameFlowType.Pause, Pause);
            GameFlowEventBus.Subscribe(GameFlowType.NextWave, NextWave);
        }

        

        private void OnDisable()
        {
            GameFlowEventBus.Unsubscribe(GameFlowType.GameStart, GameStart);
            GameFlowEventBus.Unsubscribe(GameFlowType.Proceeding, Proceeding);
            GameFlowEventBus.Unsubscribe(GameFlowType.GameClear, GameClear);
            GameFlowEventBus.Unsubscribe(GameFlowType.GameOver, GameOver);
            GameFlowEventBus.Unsubscribe(GameFlowType.Pause, Pause);
            GameFlowEventBus.Unsubscribe(GameFlowType.NextWave, NextWave);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }

        //이벤트 별 처리 함수
        private void GameStart()
        {
            flowType = GameFlowType.GameStart;
            GameFlowEventBus.Publish(GameFlowType.NextWave);
        }
        private void Proceeding()
        {
            flowType = GameFlowType.Proceeding;
        }

        private void GameClear()
        {
            flowType = GameFlowType.GameClear;
            
            if (gmaeClearPopup)
                gmaeClearPopup.SetActive(true);
        }

        private void GameOver()
        {
            flowType = GameFlowType.GameOver;
            if (gameOverPopup)
                gameOverPopup.SetActive(true);
        }

        private void Pause()
        {
            flowType = GameFlowType.Pause;
        }

        //웨이브 전환
        private void NextWave()
        {
            flowType = GameFlowType.NextWave;
            nowWave++;

            if (wavesInfo.wave.Count <= nowWave)
            {
                GameFlowEventBus.Publish(GameFlowType.GameClear);
                return;
            }

            if(wavesInfo != null)
            {
                WaveLevel wave = wavesInfo.wave[nowWave];
                _monsterSpawnManager.OnSpawner(wave.isBoss, wave.monsterList, wave.maxSpawnNum, wave.spawnMonsterNum,
                                               wave.maxFieldMonsterNum, wave.minSpawnTime, wave.maxSpawnTime);
            }

            GameFlowEventBus.Publish(GameFlowType.Proceeding);
        }
        
    }

}
