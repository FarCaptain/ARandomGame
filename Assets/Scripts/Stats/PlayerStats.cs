using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO. Stats and Inventories for now, might need a better structure
public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public int maxFace = 100;
    public int currentFace { get; private set; }

    public Stat perception;
    public Stat intelligence;
    public Stat coordination;
    public Stat strength;

    public delegate void OnPlayerStatsChanged();
    public event OnPlayerStatsChanged onPlayerStatsChanged;

    public delegate void OnCharacterBasicsChanged();
    public event OnCharacterBasicsChanged onCharacterBasicsChanged;

    #region Singleton
    public static PlayerStats instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    void Start()
    {

        currentHealth = maxHealth;
        currentFace = maxFace;

        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem)
        {
            perception.AddModifier(newItem.perceptionModifier);
            intelligence.AddModifier(newItem.intelligenceModifier);
            coordination.AddModifier(newItem.coordinationModifier);
            strength.AddModifier(newItem.strengthModifier);
        }

        if(oldItem)
        {
            perception.RemoveModifier(oldItem.perceptionModifier);
            intelligence.RemoveModifier(oldItem.intelligenceModifier);
            coordination.RemoveModifier(oldItem.coordinationModifier);
            strength.RemoveModifier(oldItem.strengthModifier);
        }

        onPlayerStatsChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + "takes" + damage + "damage.");

        if(currentHealth <= 0)
        {
            Die();
        }
        onCharacterBasicsChanged?.Invoke();
    }
    
    public void LoseFace(int damage)
    {
        currentFace -= damage;
        Debug.Log(transform.name + "loses" + damage + "face.");

        if(currentFace <= 0)
        {
            Die();
        }
        onCharacterBasicsChanged?.Invoke();
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + "Died!");
    }

    public int GetPerception()
    {
        return perception.GetValue();
    }

    public int GetIntelligence()
    {
        return intelligence.GetValue();
    }

    public int GetCoordination()
    {
        return coordination.GetValue();
    }
    
    public int GetStrength()
    {
        return strength.GetValue();
    }

    // TODO. put those in another script
    public int Roll2D6()
    {
        int[] d6Value = new int[2];
    // TODO.rolling animation in event
        d6Value[0] = Random.Range(1, 6);
        d6Value[1] = Random.Range(1, 6);

        Debug.Log("2D6 = " + d6Value[0] + "," + d6Value[1]);
        return d6Value[0] + d6Value[1];
    }

    public int RollD20()
    {
        int d20Value = Random.Range(1, 20);
        Debug.Log("D20 = " + d20Value);
        return d20Value;
    }
}
