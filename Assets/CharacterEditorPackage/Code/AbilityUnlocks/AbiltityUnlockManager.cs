using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AbiltityUnlockManager : MonoBehaviour
{
    public List<AddedAbilityRule> m_Rules = new List<AddedAbilityRule>();

    List<AbilityClass> m_currentAbilities = new List<AbilityClass>();
    AbilityModuleManager abilityManager;
    private void Start()
    {
        abilityManager = GetComponent<AbilityModuleManager>();
    }
    public void AddAbilityClass(AbilityClass ability)
    {
        if (m_currentAbilities.Contains(ability))
            return;
        m_currentAbilities.Add(ability);
        Debug.Log("Added color " + ability.ToString());

        updateAbilities();
    }

    public void RemoveAbilityClass(AbilityClass ability)
    {
        if (!m_currentAbilities.Remove(ability))
            return;
        Debug.Log("Removed color " + ability.ToString());
        
        updateAbilities();
    }

    public bool RemoveRandomAbilityClass()
    {
        if(m_currentAbilities.Count == 0)
            return false; // No more abilities could be removed
        System.Random rng = new System.Random();
        int idx = rng.Next(m_currentAbilities.Count);
        RemoveAbilityClass(m_currentAbilities[idx]);
        return true;
    }

    public void SetColor(Color m_color)
    {
        SpriteRenderer spriteRenderer = transform.Find("SpriteTransformBase").transform.Find("SpriteAnimator").GetComponent<SpriteRenderer>();
        spriteRenderer.color = m_color;
        print(spriteRenderer.color.ToString());
    }

    void updateAbilities() {
        for (int i = 0; i < m_Rules.Count; i++)
        {
            AddedAbilityRule rule = m_Rules[i];
            if (rule.abilities.All(m_currentAbilities.Contains))
            {
                Debug.Log("Applied rule " + rule.m_RuleName);
                abilityManager.ApplyAbilityUnlockList(rule.abilityUnlock);
                SetColor(rule.newColor.color);
                rule.UnlockEvent?.Invoke();
            }
            else
            {
                abilityManager.ApplyAbilityUnlockList(rule.abilityUnlock, true);
            }
        }
        if(m_currentAbilities.Count == 0) {
            SetColor(Color.white);
        }
    }


    [System.Serializable]
    public class AddedAbilityRule
    {
        public string m_RuleName;
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