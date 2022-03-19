using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project
{
    public class EnamiController : MonoBehaviour
    {
        [SerializeField] private float m_MoveSpeed;
        public Transform[] m_WayPoints;
        [SerializeField] private ProjectiltController m_Projectile; // Tham chieu den projecttile ngoai Editor
        [SerializeField] private Transform m_FiringPoint; // quy dinh vi tri ban dan m_FiringCooldown
        [SerializeField] private float m_Min_FiringCooldown; // quy dinh toc do ban (time among 2 turn fire)
        [SerializeField] private float m_Max_FiringCooldown; // quy dinh toc do ban (time among 2 turn fire)
        [SerializeField] private int m_Hp_Enemy;
        private  int m_CurrentHp;
        private float m_CheckCooldown;// kiem tra thoi gian giua 2 lan ban
        private int m_CurrentWayPointIndex = -1 ;
        // Start is called before the first frame update
        private bool m_Active ;
        private SpawnManager m_SpawManager ;
        private GameManager m_GameManager;
        private AudioManager m_AudioManager;
        void Start()
        {
            m_SpawManager = FindObjectOfType<SpawnManager>();
            m_GameManager = FindObjectOfType<GameManager>();
            m_AudioManager = FindObjectOfType<AudioManager>(); 
        }

        // Update is called once per frame
        void Update()
        {
            if (!m_Active || m_WayPoints.Length == 0) 
            {
                return;
            }
            int nextWayPoint = m_CurrentWayPointIndex + 1;
            if (nextWayPoint > m_WayPoints.Length -1)
            {   
                // Gán lại vị trí tiếp theo về điểm bắt đầu cho Enemi
                nextWayPoint = 0 ;
            //    Destroy(gameObject,0f);
                
            }
            // Di chuyển enami tới điểm đích,  
            //MoveTowards có 3 tham số : vị trí hiện tại enami, vị trí điểm đích , quãng đường sẽ di chuyển
             transform.position = Vector3.MoveTowards(transform.position,m_WayPoints[nextWayPoint].position,m_MoveSpeed*Time.deltaTime);
           
            //Kiểm tra enami tới đích chưa
            if (transform.position == m_WayPoints[nextWayPoint].position)
            {
                m_CurrentWayPointIndex = nextWayPoint ;
            }
            //Enami quay mat đung hướng
            Vector3 direction =  m_WayPoints[nextWayPoint].position - transform.position;
            // Góc của vector
            float edge = Mathf.Atan2(direction.y,direction.x)* Mathf.Rad2Deg; // chuyển sang hệ tọa độ 360
            // Enemi đi theo hướng Point
            transform.rotation = Quaternion.AngleAxis(edge + 270, Vector3.forward);

                if (m_CheckCooldown <= 0)
                {
                    Fire();
                    m_CheckCooldown = Random.Range(m_Min_FiringCooldown,m_Max_FiringCooldown);
                }
            

            m_CheckCooldown -= Time.deltaTime; 

        }
        public void Init(Transform[] wayPoints){
            // Chỉ định đường đi chung cho các Enami đc Spawn
            m_WayPoints = wayPoints;
            // Chỉ khi nào Enemi đc init Active mới bằng True
            m_Active = true;
            // Gán start position cho Enemi
            transform.position = wayPoints[0].position;
             m_CheckCooldown = Random.Range(m_Min_FiringCooldown,m_Max_FiringCooldown); 
             m_CurrentHp = m_Hp_Enemy;
        }
         private void Fire()
        {
             //  nhân bản đối tượng đạn
      
            ProjectiltController proj = m_SpawManager.SpawnEnemyProjectitle(m_FiringPoint.position); //Instantiate(m_Projectile, m_FiringPoint.position, Quaternion.identity, null);
            proj.Fire();
            m_AudioManager.PlayPlasmaSFXClip();
        }
        public void Hit(int damage){
            m_CurrentHp -= damage;
            if (m_CurrentHp <= 0)
            {
                // Destroy(gameObject);
                m_SpawManager.ReleaseEnemy(this);
                m_SpawManager.SpawnDestroyEnemy(gameObject.transform.position);
                m_GameManager.AddScore(1);
                m_AudioManager.PlayEnemyDestroyClip();
            }
            m_AudioManager.PlayHitSFXClip();
        }
        public bool CheckHp(){
            if (m_Hp_Enemy > 0)
            {
                return true;
            }
            return false;
        }
    }
} 