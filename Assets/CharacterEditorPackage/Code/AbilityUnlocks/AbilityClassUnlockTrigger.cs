using UnityEngine;
using UnityEngine.Events;

public class AbilityClassUnlockTrigger : MonoBehaviour
{
    [SerializeField] AbilityClass m_AbilityClass;
    [SerializeField] bool m_DisableAfterTriggering = false;
    [SerializeField] UnityEvent m_TriggerEvent = null;
    void OnTriggerEnter(Collider a_Collider)
    {
        AbiltityUnlockManager unlockManager = a_Collider.GetComponent<AbiltityUnlockManager>();
        if (m_DisableAfterTriggering)
        {
            gameObject.SetActive(false);
        }

        unlockManager.AddAbilityClass(m_AbilityClass);
        m_TriggerEvent?.Invoke();
    }
}