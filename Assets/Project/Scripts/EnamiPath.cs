using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project
{
    public class EnamiPath : MonoBehaviour
    { 
        
        // Start is called before the first frame update
        [SerializeField] private Transform[] m_WayPoints;
        [SerializeField] private Color m_Color;
        [SerializeField]private bool m_Show; 

        public Transform[] WayPoints => m_WayPoints;
        public Transform[] GetTransforms(){
            return m_WayPoints;
        }
       private void OnDrawGizmos(){
           if (!m_Show)
           {
               return;
           }
           // Có ít nhất 1 điểm WayPoint trong mảng
           if (m_WayPoints != null && m_WayPoints.Length >1)
           {
               // quy định màu cho Gizmos
               Gizmos.color = m_Color;
               for(int i = 0 ; i < m_WayPoints.Length -1 ; i++){
                   //Điểm i
                   Transform from = m_WayPoints[i];
                   //Điểm i+1
                   Transform to = m_WayPoints[i+1];
                   // Nối điểm bằng Gizmos.DrwaLine có 2 tham số : Điểm đầu , cuối
                   Gizmos.DrawLine(from.position , to.position);
               }
               // nối nốt điểm đầu và cuối
               Gizmos.DrawLine(m_WayPoints[0].position, m_WayPoints[m_WayPoints.Length-1].position);
           }
       }
    }
}
