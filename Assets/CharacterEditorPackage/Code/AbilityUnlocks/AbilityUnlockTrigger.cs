using UnityEngine;

public class AbilityUnlockTrigger : MonoBehaviour
{
    [SerializeField] AbilityUnlockList m_List = null;
    [SerializeField] bool m_DisableAfterTriggering = false;
    [SerializeField] AbilityClass m_abilityClass;
    void OnTriggerEnter(Collider a_Collider)
    {
        AbilityModuleManager abilityManager = a_Collider.GetComponent<AbilityModuleManager>();
        AbiltityUnlockManager unlockManager = a_Collider.GetComponent<AbiltityUnlockManager>();
        if (abilityManager)
        {
            abilityManager.ApplyAbilityUnlockList(m_List);
            if (m_DisableAfterTriggering)
            {
                gameObject.SetActive(false);
            }
        }

        Renderer renderer = GetComponent<Renderer>();
        if (!renderer) return;

        Color color = renderer.material.color;
        print(color.ToString());
        SpriteRenderer spriteRenderer = a_Collider.transform.Find("SpriteTransformBase").transform.Find("SpriteAnimator").GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
        print(spriteRenderer.color.ToString());
        unlockManager.AddColor(m_abilityClass);
    }
}