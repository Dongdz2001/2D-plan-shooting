using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class ParticleFX : MonoBehaviour
    {
        [SerializeField] private float m_LifeTime;
        private ParticleFXPool m_Pool;
        private float m_CurrentLifeTime;
        // private  m_Pool;
        private void OnEnable(){
            m_CurrentLifeTime = m_LifeTime;
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (m_CurrentLifeTime <= 0)
            {
                m_Pool.Release(this);
            }
            m_CurrentLifeTime -= Time.deltaTime ;
        }
        public void SetPool(ParticleFXPool pool){
            m_Pool = pool ;
        }
    }
}
