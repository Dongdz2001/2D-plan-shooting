using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Project
{


    public class GameOverPanel : MonoBehaviour
    {
        private GameManager m_GameManager;
        [SerializeField] private TextMeshProUGUI m_TxtResult;
        [SerializeField] private TextMeshProUGUI m_TxtNoti;
        [SerializeField] private TextMeshProUGUI m_TxtHighScore;
        // Start is called before the first frame update
        void Start()
        {
            m_GameManager = FindObjectOfType<GameManager>();
        }

        // Update is called once per frame
        public void BtnHome_Pressed(){
            m_GameManager.Home();
        }
        public void DisPlayHighScore(int score){
            m_TxtHighScore.text = "HIGHSCORE: " + score ; 
        }
        public void DisPlayResult(bool isWin){
            if (isWin)
            {
                m_TxtResult.text = "YOU WIN";
                m_TxtNoti.text = "WINER!!";

            }else
            {
                m_TxtResult.text = "YOU LOSE";
                m_TxtNoti.text = "GAME \n OVER";
            }
        }
    }
}
