using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project
{
    public enum GameState
    {
        Home,
        GamePlay,
        Pause,
        GameOver
    }
    public class GameManager : MonoBehaviour
    {
        private GameState m_gameState;
        [SerializeField] private HomePanel m_HomePanel;
        [SerializeField] private GamePlayPanel m_GamePlayPanel;
        [SerializeField] private PausePanel m_PausePanel;
        [SerializeField] private GameOverPanel m_GameOverPanel;
        private SpawnManager m_SpawnManager;
        private AudioManager m_AudioManager;
        private bool m_CheckWin;
        private int m_Score;


        // Start is called before the first frame update
        void Start()
        {
            m_SpawnManager = FindObjectOfType<SpawnManager>();
            m_AudioManager = FindObjectOfType<AudioManager>();
            m_HomePanel.gameObject.SetActive(false);
            m_GamePlayPanel.gameObject.SetActive(false);
            m_PausePanel.gameObject.SetActive(false);
            m_GameOverPanel.gameObject.SetActive(false);
            SetState(GameState.Home);
            m_Score = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void SetState(GameState state)
        {
            m_gameState = state;
            m_HomePanel.gameObject.SetActive(m_gameState == GameState.Home);
            m_GamePlayPanel.gameObject.SetActive(m_gameState == GameState.GamePlay);
            m_PausePanel.gameObject.SetActive(m_gameState == GameState.Pause);
            m_GameOverPanel.gameObject.SetActive(m_gameState == GameState.GameOver);

            if (m_gameState == GameState.Pause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            if (m_gameState == GameState.Home)
            {
                // PlayHome music
                m_AudioManager.PlayHomeMusic();
            }
            else
            {
                m_AudioManager.PlayBattleMusic();
            }
        }
        public bool isActive()
        {
            return m_gameState == GameState.GamePlay;
        }
        public void Play()
        {
            SetState(GameState.GamePlay);
            m_Score = 0;
            m_GamePlayPanel.DisPlayScore(m_Score);
            m_SpawnManager.StartBattle();
        }
        public void Pause()
        {
            SetState(GameState.Pause);
        }
        public void Home()
        {
            SetState(GameState.Home);
            m_SpawnManager.Clear();
        }
        public void Continue()
        {
            SetState(GameState.GamePlay);
        }
        public void GameOver(bool win)
        {
            m_CheckWin = win;
            m_GameOverPanel.DisPlayResult(m_CheckWin);
            m_GameOverPanel.DisPlayHighScore(m_Score);
            Invoke("ClearObj",0.5f);
            
        }
        private void ClearObj(){
            m_SpawnManager.Clear();
             SetState(GameState.GameOver);
        }
        public void AddScore(int value)
        {
            m_Score += value;
            m_GamePlayPanel.DisPlayScore(m_Score);
            if (m_SpawnManager.IsClear())
            {
                GameOver(true);
            }
        }
    }
}
