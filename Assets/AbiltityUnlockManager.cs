using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AbiltityUnlockManager : MonoBehaviour
{
    public List<AddedAbilityRules> Rules = new List<AddedAbilityRules>();

    List<AbilityClass> m_currentAbilities = new List<AbilityClass>();
    AbilityModuleManager abilityManager;
    private void Start()
    {
        abilityManager = GetComponent<AbilityModuleManager>();
    }
    public void AddColor(AbilityClass ability)
    {
        if (m_currentAbilities.Contains(ability))
            return;
        m_currentAbilities.Add(ability);
        Debug.Log("Added color " + ability.ToString());
        if (m_currentAbilities.Count > 1)
        {

            for (int i = 0; i < Rules.Count; i++)
            {

                if (Rules[i].abilities.All(m_currentAbilities.Contains) && Rules[i].abilities.Count == m_currentAbilities.Count)
                {
                    Debug.Log("Applied ruled");
                    abilityManager.ApplyAbilityUnlockList(Rules[i].abilityUnlock);
                    SetColor(Rules[i].newColor.color);
                    Rules[i].UnlockEvent?.Invoke();
                    return;
                }
            }
        }
    }
    public void SetColor(Color m_color)
    {
        SpriteRenderer spriteRenderer = transform.Find("SpriteTransformBase").transform.Find("SpriteAnimator").GetComponent<SpriteRenderer>();
        spriteRenderer.color = m_color;
        print(spriteRenderer.color.ToString());
    }
    [System.Serializable]
    public class AddedAbilityRules
    {
        public List<AbilityClass> abilities = new List<AbilityClass>();
        public AbilityUnlockList abilityUnlock;
        public Material newColor;
        public UnityEvent UnlockEvent;
    }
}
public enum AbilityClass
{
    Red, Blue, Green
}