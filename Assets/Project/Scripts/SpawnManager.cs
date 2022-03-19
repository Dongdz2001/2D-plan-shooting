
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
namespace Project
{
    [System.Serializable]
    public class EnemiPool{ 
        public EnamiController prefab;
        // Save Enemi chưa hoạt động hoặc đã chết
        public List<EnamiController> inativeObject;
        // Save Enemi đang hoạt động
        public List<EnamiController> acLiveObject;

        // Tạo và trả về 1 Enemi
        public EnamiController Spawn(Vector3 position, Transform parent)
        {
            // Tìm xem Enemi nào đang rảnh không thì phải tạo mới
            if (inativeObject.Count == 0)
            {
                EnamiController newObj = GameObject.Instantiate(prefab,parent);
                newObj.transform.position = position ;
                acLiveObject.Add(newObj);
                return newObj;
            }else
            {   
                // Nếu tồn tại dunng  luôn Enemi
                EnamiController oldObj = inativeObject[0];
                oldObj.gameObject.SetActive(true);
                oldObj.transform.SetParent(parent);
                oldObj.transform.position = position ;
                acLiveObject.Add(oldObj);
                inativeObject.RemoveAt(0);
                return oldObj;
            }
        }
        public void Release(EnamiController obj){
            //Nếu Enemi tồn tại trong List Active sẽ xóa vào thêm  và List Inative và Disable trên Seacen
            if (acLiveObject.Contains(obj))
            {
                acLiveObject.Remove(obj);
                inativeObject.Add(obj);
                obj.gameObject.SetActive(false);
            }
        }
        public void clear(){
            while (acLiveObject.Count > 0)
            {
                 EnamiController obj = acLiveObject[0];
                 obj.gameObject.SetActive(false);
                 acLiveObject.RemoveAt(0);
                 inativeObject.Add(obj);
            }
        }
    }

    [System.Serializable]
    public class ParticleFXPool{ 
        public ParticleFX prefab;
        // Save Enemi chưa hoạt động hoặc đã chết
        public List<ParticleFX> inativeObject;
        // Save Enemi đang hoạt động
        public List<ParticleFX> acLiveObject;

        // Tạo và trả về 1 Enemi
        public ParticleFX Spawn(Vector3 position, Transform parent)
        {
            // Tìm xem Enemi nào đang rảnh không thì phải tạo mới
            if (inativeObject.Count == 0)
            {
                ParticleFX newObj = GameObject.Instantiate(prefab,parent);
                newObj.transform.position = position ;
                acLiveObject.Add(newObj);
                return newObj;
            }else
            {   
                // Nếu tồn tại dunng  luôn Enemi
                ParticleFX oldObj = inativeObject[0];
                oldObj.gameObject.SetActive(true);
                oldObj.transform.SetParent(parent);
                oldObj.transform.position = position ;
                acLiveObject.Add(oldObj);
                inativeObject.RemoveAt(0);
                return oldObj;
            }
        }
        public void Release(ParticleFX obj){
            //Nếu Enemi tồn tại trong List Active sẽ xóa vào thêm  vào List Inative và Disable trên Seacen
            if (acLiveObject.Contains(obj))
            {
                acLiveObject.Remove(obj);
                inativeObject.Add(obj);
                obj.gameObject.SetActive(false);
            }
        }
        public void clear(){
            while (acLiveObject.Count > 0)
            {
                 ParticleFX obj = acLiveObject[0];
                 obj.gameObject.SetActive(false);
                 acLiveObject.RemoveAt(0);
                 inativeObject.Add(obj);
            }
        }
    }

    [System.Serializable]
    public class ProjectiltControllerPool{ 
        public ProjectiltController prefab;
        // Save ProjectiltController chưa hoạt động hoặc đã chết
        public List<ProjectiltController> inativeObject;
        // Save ProjectiltController đang hoạt động
        public List<ProjectiltController> acLiveObject;

        // Tạo và trả về 1 ProjectiltController
        public ProjectiltController Spawn(Vector3 position, Transform parent)
        {
            // Tìm xem ProjectiltController nào đang rảnh không thì phải tạo mới
            if (inativeObject.Count == 0)
            {
                ProjectiltController newObj = GameObject.Instantiate(prefab,parent);
                newObj.transform.position = position ;
                acLiveObject.Add(newObj);
                return newObj;
            }else
            {   
                // Nếu tồn tại dunng  luôn ProjectiltController
                ProjectiltController oldObj = inativeObject[0];
                oldObj.gameObject.SetActive(true);
                oldObj.transform.SetParent(parent);
                oldObj.transform.position = position ;
                acLiveObject.Add(oldObj);
                inativeObject.RemoveAt(0);
                return oldObj;
            }
        }
        public void Release(ProjectiltController obj){
            //Nếu ProjectiltController tồn tại trong List Active sẽ xóa vào thêm  và List Inative và Disable trên Seacen
            if (acLiveObject.Contains(obj))
            {
                acLiveObject.Remove(obj);
                inativeObject.Add(obj);
                obj.gameObject.SetActive(false);
            }
        }
        public void clear(){
            while (acLiveObject.Count > 0)
            {
                 ProjectiltController obj = acLiveObject[0];
                 obj.gameObject.SetActive(false);
                 acLiveObject.RemoveAt(0);
                 inativeObject.Add(obj);
            } 
        }
    }
    public class SpawnManager : MonoBehaviour
    {
        // [SerializeField]private bool m_Active; // khi Active = True Coroutine mới chạy]
        // [SerializeField] private EnamiController m_EnamiPrefab;
        [SerializeField] private EnemiPool m_EnemiPool;
        [SerializeField] private ProjectiltControllerPool m_ProjectiltControllerPool_Player;
         [SerializeField] private ProjectiltControllerPool m_ProjectiltControllerPool_Enemy;
        [SerializeField] private int m_Min_TotalEnemi;
        [SerializeField] private int m_Max_TotalEnemi;
        [SerializeField] private float m_EnemySpawnInterval;
        [SerializeField] private EnamiPath[] m_Paths;
        [SerializeField] private int m_totalGroup;
        [SerializeField] private ParticleFXPool m_HitFXPool;
        [SerializeField]private ParticleFXPool m_ShootingFXPool;
        [SerializeField]private ParticleFXPool m_EnemyDesTroyPool;
        [SerializeField]private ParticleFXPool m_PlayDesTroyPool;
        [SerializeField]private PlayerControler m_PlayerControlerPrefab; 
        private bool m_SpawningEnemy;
        private PlayerControler m_Player;
        
        // Start is called before the first frame update
        // void Start()
        // {
            // Gọi Coroutine thông qua StartCoroutine
            // Coroutine là 1 thread cho phép quản lý Thread đang chạy 
            // Nó cho phép tạm dừng hoặc stop thread bất kỳ truyền vào 
            // StartCoroutine(IETestCoroutine());
            // StartCoroutine(IESpawnGroups(m_totalGroup));
        // }
        public void StartBattle(){
            if (m_Player == null)
            {
               m_Player = Instantiate(m_PlayerControlerPrefab); 
            }
            m_Player.transform.position = new Vector3(0,-3.5f,0);
            StartCoroutine(IESpawnGroups(m_totalGroup));
        }
        private IEnumerator IESpawnGroups(int pGroup){
            m_SpawningEnemy = true ;
            for (int i = 0; i < pGroup; i++)
            {
                // Truyền vào giá trị Min khi khởi tạo số lượng Enemi
                 int totalEnemi = Random.Range(m_Min_TotalEnemi,m_Max_TotalEnemi);
                 // Gán 1 path bất kỳ cho 1 Group Spawn Enemi 
                 EnamiPath path = m_Paths[Random.Range(0,m_Paths.Length)];
                 // Run thread "IESpawnEnemies" bằng Coroutine 
                 yield return StartCoroutine(IESpawnEnemies(totalEnemi,path));
                 if (i < pGroup -1)
                 {
                     yield return new WaitForSeconds(3f);
                 }
                 
            }
            m_SpawningEnemy = false ;
        }
        private IEnumerator IESpawnEnemies(int totalEnemis, EnamiPath path)
        {
           
            for (int i = 0; i < totalEnemis ; i++) 
            {
                //Truyền vào 1 delegate
                // yield return new WaitUntil(() => m_Active);
                //Delay khoảng thời gian mỗi lần Spawn Enemi
                yield return new WaitForSeconds(m_EnemySpawnInterval) ;
               // Spwan(Đẻ ra Enemi)
               // sửa null trong Instantiate(m_EnamiPrefab,null) thành transform thì Enemi đc spawn sẽ là con của enemi luôn. 
                // EnamiController enemy = Instantiate(m_EnamiPrefab,transform);
                EnamiController enemy = m_EnemiPool.Spawn(path.WayPoints[0].position,transform);
                enemy.Init(path.GetTransforms() );
            }
        }

        // Tạo 1 Thread Coroutine Function 
        // private IEnumerable IETestCoroutine(){
        //     //Truyền vào 1 delegate (kien thức nâng cao )
        //     yield return new WaitUntil(() => m_Active);
        //     for(int i = 0 ; i < 5; i++){ 
        //         Debug.Log(i);
        //         // delay 1s
        //         yield return new WaitForSeconds(1f); 
        //     }
        // }
        //Hàm trung gian với Destroy trong EnemiController
        public void ReleaseEnemy(EnamiController obj){
            m_EnemiPool.Release(obj);
        }
        public ProjectiltController SpawnEnemyProjectitle(Vector3 position){
            ProjectiltController  obj = m_ProjectiltControllerPool_Enemy.Spawn(position,transform);
            obj.SetFromPlayer(false);
            return obj;
        }
        public void ReleaseProjectiltEnemyController (ProjectiltController projectiltController){
            m_ProjectiltControllerPool_Enemy.Release(projectiltController);
        }
        public ProjectiltController SpawnPlayerProjectitle(Vector3 position){
            ProjectiltController obj = m_ProjectiltControllerPool_Player.Spawn(position,transform);
            obj.SetFromPlayer(true);
            return obj;
        }
        public void ReleaseProjectiltPlayerController(ProjectiltController projectiltController){
            m_ProjectiltControllerPool_Player.Release(projectiltController);
        }
        public ParticleFX SpawnHitFX(Vector3 position){
            ParticleFX fx = m_HitFXPool.Spawn(position,transform);
            fx.SetPool(m_HitFXPool);
            return fx;
        }
        public void ReleaseHitFx(ParticleFX obj){
            m_HitFXPool.Release(obj);
        }
        public ParticleFX SpawnShootingFX(Vector3 position){
            ParticleFX fx = m_ShootingFXPool.Spawn(position,transform);
            fx.SetPool(m_ShootingFXPool);
            return fx ;
        }
        public void ReleaseShootingFx(ParticleFX obj){
            m_ShootingFXPool.Release(obj);
        }
        public ParticleFX SpawnDestroyEnemy(Vector3 position){
            ParticleFX fx = m_EnemyDesTroyPool.Spawn(position,transform);
            fx.SetPool(m_EnemyDesTroyPool);
            return fx ;
        }
        public void ReleaseDestroyEnemy(ParticleFX obj){
            m_EnemyDesTroyPool.Release(obj);
        }
        public ParticleFX SpawnDestroyPlayer(Vector3 position){
            ParticleFX fx = m_PlayDesTroyPool.Spawn(position,transform);
            fx.SetPool(m_PlayDesTroyPool);
            return fx ;
        }
        public void ReleaseDestroyPlayer(ParticleFX obj){
            m_PlayDesTroyPool.Release(obj);
        }
        public bool IsClear(){
            if (m_SpawningEnemy || m_EnemiPool.acLiveObject.Count > 0)
            {
                return false;
            }
            return true;
        }
        public void Clear(){
            m_EnemiPool.clear();
            m_ProjectiltControllerPool_Enemy.clear();
            m_ProjectiltControllerPool_Player.clear();
            m_ShootingFXPool.clear();
            m_HitFXPool.clear();
            m_EnemyDesTroyPool.clear();
            StopAllCoroutines();
        }
    }
}
