using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTrigger : MonoBehaviour
{
    [SerializeField] AudioClip[] m_Sounds;
    AudioSource m_AudioSource = null;

    void Start() {
        m_AudioSource = GameManager.Instance.audioSource;
    }

    void OnTriggerEnter(Collider a_Collider) {
        if(a_Collider.gameObject.layer == 31)
        {
           if(m_AudioSource && m_Sounds.Length > 0) 
        {
            m_AudioSource.PlayOneShot(m_Sounds[Random.Range(0, m_Sounds.Length)], Random.Range(.6f, 1f));
        }
        }
    }
}
