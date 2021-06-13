using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource = null;
    [SerializeField] private AudioClip[] m_CollectSounds;
    [SerializeField] public UnityEvent m_CollectEvent;

    void OnTriggerEnter(Collider a_Collider) {
        if(a_Collider.gameObject.layer == 31)
        {
            // TODO: Add score
            Collect();
        }
    }

    void Collect() {
        if(m_AudioSource && m_CollectSounds.Length > 0) 
        {
            m_AudioSource.PlayOneShot(m_CollectSounds[Random.Range(0, m_CollectSounds.Length)], Random.Range(.6f, 1f));
        }
        m_CollectEvent?.Invoke();

        gameObject.SetActive(false);
    }
}
