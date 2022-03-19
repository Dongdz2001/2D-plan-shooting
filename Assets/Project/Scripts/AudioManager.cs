using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project
{

    public class AudioManager : MonoBehaviour
    {
        // Am nen ung su dung AudioSource
        [SerializeField] private AudioSource m_Music;
        [SerializeField] private AudioSource m_SFX;

        // Am hieu ung su dung AudioClip
        [SerializeField] private AudioClip m_HomeMusicClip;
        [SerializeField] private AudioClip m_BattleMusicCip;
        [SerializeField] private AudioClip m_LazerSFXClip;
        [SerializeField] private AudioClip m_PlasmaSFXClip;
        [SerializeField] private AudioClip m_HitSFXClip;
        [SerializeField] private AudioClip m_EnemyDestroyClip;
        [SerializeField] private AudioClip m_PlayerDestroyClip;
        [SerializeField] private AudioClip m_PlasmaPlayerSFXClip;

        public void PlayHomeMusic()
        {
            m_Music.loop = true;
            m_Music.clip = m_HomeMusicClip;
            m_Music.Play();
        }
        public void PlayBattleMusic()
        {
            m_Music.loop = true;
            m_Music.clip = m_BattleMusicCip;
            m_Music.Play();
        }
        public void PlayLazerSFX(){
            m_SFX.PlayOneShot(m_LazerSFXClip);
        }
        public void PlayPlasmaSFXClip(){
            m_SFX.PlayOneShot(m_PlasmaSFXClip);
        }
        public void PlayHitSFXClip(){
            m_SFX.PlayOneShot(m_HitSFXClip);
        }
        public void PlayEnemyDestroyClip(){
            m_SFX.PlayOneShot(m_EnemyDestroyClip);
        }
        public void PlayPlayerDestroyClip(){
            m_SFX.PlayOneShot(m_PlayerDestroyClip);
        }
        public void PlayPlasmaPlayerSFXClip(){
            m_SFX.PlayOneShot(m_PlasmaPlayerSFXClip);
        }
    }
}