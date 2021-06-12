using UnityEngine;

public class AbilityClassUnlockTrigger : MonoBehaviour
{
    [SerializeField] AbilityClass m_AbilityClass;
    [SerializeField] bool m_DisableAfterTriggering = false;
    void OnTriggerEnter(Collider a_Collider)
    {
        AbiltityUnlockManager unlockManager = a_Collider.GetComponent<AbiltityUnlockManager>();
        if (m_DisableAfterTriggering)
        {
            gameObject.SetActive(false);
        }

        unlockManager.AddAbilityClass(m_AbilityClass);
    }
}