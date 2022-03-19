using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Project
{


    public class GamePlayPanel : MonoBehaviour
    {
        private GameManager m_GameManager;
        [SerializeField] private TextMeshProUGUI m_TxtScore;
        // Start is called before the first frame update
        void Start()
        {
            m_GameManager = FindObjectOfType<GameManager>();
        }

        // Update is called once per frame
        public void btnPause_Pressed()
        {
            m_GameManager.Pause();
        }
        public void DisPlayScore(int score)
        {
            m_TxtScore.text = "SCORE: " + score;
        }
    }
}
