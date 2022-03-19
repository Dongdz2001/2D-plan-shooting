using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project
{

    public class PlayerControler : MonoBehaviour
    {
        
        [SerializeField] private float m_Speed;
        [SerializeField] private ProjectiltController m_Projectile; // Tham chieu den projecttile ngoai Editor
        [SerializeField] private Transform m_FiringPoint; // quy dinh vi tri ban dan
        [SerializeField] private float m_FiringCooldown; // quy dinh toc do ban (time among 2 turn fire)
        // Start is called before the first frame update
        [SerializeField] private int m_HP_Controller;
        private int m_CurrentHp ;
        private float m_CheckCooldown;// kiem tra thoi gian giua 2 lan ban
        private SpawnManager m_SpawManager;
        private GameManager m_GameManager;
        private AudioManager m_AudioManager;
        void Start()
        {
            m_CurrentHp = m_HP_Controller ;
            m_SpawManager = FindObjectOfType<SpawnManager>();
            // Lấy ra GameManager Object
            m_GameManager = FindObjectOfType<GameManager>();
            m_AudioManager = FindObjectOfType<AudioManager>(); 
        }

        // Update is called once per frame
        void Update()
        {
            if (!m_GameManager.isActive())
            {
                return;
            }
            float horizontal = Input.GetAxis("Horizontal"); // kiem tra an xuong mui ten trai phai va a d
            float vertical = Input.GetAxis("Vertical"); // Kiem tra INput nap vao mui ten len xuong va ws
            Vector2 direction = new Vector2(horizontal, vertical);
            if (transform.position[0] >= -2.8f && transform.position[0] <= 2.8f && transform.position[1] >= -4.5f)
            {
                // Debug.Log("x= " + transform.position);
                transform.Translate(direction * Time.deltaTime * m_Speed);
            }
            else 
            {
                if (transform.position[0] < -2.8f ) 
                {
                    transform.position =   new Vector2(transform.position[0]+0.1f, transform.position[1]);
                }
                else if (transform.position[0] > 2.8f  ) 
                {
                    transform.position = new Vector2(transform.position[0]-0.1f, transform.position[1]);
                }
                else if (transform.position[1] < -4.5f  )
                {
                     transform.position = new Vector2(transform.position[0]+0.1f, -4.5f );
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (m_CheckCooldown <= 0)
                {
                    Fire();
                    m_CheckCooldown = m_FiringCooldown;
                }
            }

            m_CheckCooldown -= Time.deltaTime;

        }

        private void Fire()
        {
             //  nhân bản đối tượng đạn
            ProjectiltController proj =  m_SpawManager.SpawnPlayerProjectitle(m_FiringPoint.position) ; //Instantiate(m_Projectile, m_FiringPoint.position, Quaternion.identity, null);
            proj.Fire();
            m_SpawManager.SpawnShootingFX(m_FiringPoint.position);
            m_AudioManager.PlayLazerSFX();
        }
        public void Hit(int damage){
            m_CurrentHp -= damage;
            if (m_CurrentHp <= 0)
            {
                Destroy(gameObject);
                m_GameManager.GameOver(false);
                m_SpawManager.SpawnDestroyPlayer(gameObject.transform.position);
                m_AudioManager.PlayPlayerDestroyClip();
            }
            m_AudioManager.PlayPlasmaPlayerSFXClip();
        }
         private void OnTriggerEnter2D(Collider2D collider2D){
             if (collider2D.gameObject.CompareTag("Enemi")){
                 Destroy(gameObject);
                m_GameManager.GameOver(false);
                m_SpawManager.SpawnDestroyPlayer(gameObject.transform.position);
                m_AudioManager.PlayPlayerDestroyClip();
             }
         }
    }

}
