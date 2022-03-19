using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private Material m_Nebula;
        [SerializeField] private Material m_BigStar;
        [SerializeField] private Material m_MediumStar;
        [SerializeField] private float m_BigStarBgScrollSpeed;
        [SerializeField] private float m_MediumStarBgScrollSpeed;
        [SerializeField] private float m_NebulaSpeed;
        private int m_MainTexID;
        // Start is called before the first frame update
        void Start()
        {
            m_MainTexID = Shader.PropertyToID("_MainTex");
        }

        // Update is called once per frame
        void Update()
        {
            
            Vector2 offset = m_BigStar.GetTextureOffset(m_MainTexID);
            if (offset[1] < -0.24)
            {
                offset = new Vector2(0,0.3f);
            }
            offset += new Vector2(0, m_BigStarBgScrollSpeed * Time.deltaTime);
            m_BigStar.SetTextureOffset(m_MainTexID, offset);

            offset = m_MediumStar.GetTextureOffset(m_MainTexID);
            offset += new Vector2(0, m_MediumStarBgScrollSpeed * Time.deltaTime);
            m_MediumStar.SetTextureOffset(m_MainTexID, offset);
           
            Vector2 offsetBg = m_Nebula.GetTextureOffset(m_MainTexID);
            if (offsetBg[1] > 0.45 || offsetBg[1] < -0.5 )
            {
                offsetBg = new Vector2(0,0.45f);
                Debug.Log("offset= "+offsetBg);
            }
            offsetBg += new Vector2(0, m_NebulaSpeed * Time.deltaTime);
            m_Nebula.SetTextureOffset(m_MainTexID, offsetBg);
        }
    }
}