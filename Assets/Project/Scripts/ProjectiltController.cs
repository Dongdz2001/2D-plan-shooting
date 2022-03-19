using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project
{
    public class ProjectiltController : MonoBehaviour
    {
        [SerializeField] private float m_MoveSpped;
        [SerializeField] private Vector2 m_Direction;
        [SerializeField] private int m_Damage;
        private bool m_FromPlayer;
        private SpawnManager m_SpawManager;
        private float m_lifeTime;

        // Start is called before the first frame update
        void Start()
        {
            m_SpawManager = FindObjectOfType<SpawnManager>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(m_Direction * Time.deltaTime * m_MoveSpped);
            if (m_lifeTime <= 0)
            {
                Release();
            }
            m_lifeTime -= Time.deltaTime;
        }
        public void Fire()
        {
            // Tự động hủy sau 10s
            // Destroy(gameObject, 5f);
            m_lifeTime = 10f;
        }
        private void Release()
        {
            if (m_FromPlayer)
            {
                m_SpawManager.ReleaseProjectiltPlayerController(this);
            }
            else
            {
                m_SpawManager.ReleaseProjectiltEnemyController(this);
            }
        }
        public void SetFromPlayer(bool value)
        {
            m_FromPlayer = value;
        }
        // Check Đạn chạm vào đạn của Enemi hoặc Enemi có kiểm tra vật lý
        // private void OnCollisionEnter2D(Collision2D Collision){
        //     Debug.Log("Touch = "+Collision.gameObject.name);
        // }
        // Check Đạn chạm vào đạn của Enemi hoặc Enemi không kiểm tra vật lý
        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            // Debug.Log("Touch Triger= "+collider2D.gameObject.name);
            if (collider2D.gameObject.CompareTag("Enemi"))
            {
                Release();
                // Destroy(gameObject);
                Vector3 hitPos = collider2D.ClosestPoint(transform.position);
                m_SpawManager.SpawnHitFX(hitPos);
                EnamiController enemy;
                //TryGetComponent trả về EnemyController nếu nó tìm thấy EnemyScript mà nó va trạm
                collider2D.gameObject.TryGetComponent(out enemy);
                enemy.Hit(m_Damage);
            }
            else if (collider2D.gameObject.CompareTag("Player"))
            {
                Release();
                Vector3 hitPos = collider2D.ClosestPoint(transform.position);
                m_SpawManager.SpawnHitFX(hitPos);
                // Destroy(gameObject);
                PlayerControler player;
                //TryGetComponent trả về EnemyController nếu nó tìm thấy EnemyScript mà nó va trạm
                collider2D.gameObject.TryGetComponent(out player);
                player.Hit(m_Damage);
            }

        }
    }
}

