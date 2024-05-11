using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritEssence : Interactable
{
    [SerializeField] public Essence essence;
    [HideInInspector] public int soulCost;

    private void Awake()
    {
        soulCost = essence.soulCost;
    }

    public override void OnInteract(BaseCharacter character)
    {
        if(SpiritCharacter.souls >= soulCost)
        {
            Debug.Log("Enough souls, starting purchase, Current souls: " + SpiritCharacter.souls);
            SpiritCharacter.souls -= soulCost;
            Debug.Log("Souls deducted, Current souls: " + SpiritCharacter.souls);
            base.OnInteract(character);
            StatEffect essenceEffect1;
            StatEffect essenceEffect2;
            StatEffect essenceEffect3;
            if (essence.statType1 != StatType.None)
            {
                Debug.Log("Adding effect 1");
                Debug.Log("Damage Before: " + character.characterStats.baseStats.Damage);
                essenceEffect1 = new StatEffect(essence.statType1, essence.operatorType1, essence.Value1);
                character.characterStats.AddStatModifier(essenceEffect1.GetModifier());
                Debug.Log("Damage After: " + character.characterStats.baseStats.Damage);
            }
            if (essence.statType2 != StatType.None)
            {
                Debug.Log("Adding effect 2");
                essenceEffect2 = new StatEffect(essence.statType2, essence.operatorType2, essence.Value2);
                character.characterStats.AddStatModifier(essenceEffect2.GetModifier());
            }
            if (essence.statType3 != StatType.None)
            {
                Debug.Log("Adding effect 3");
                essenceEffect3 = new StatEffect(essence.statType3, essence.operatorType3, essence.Value3);
                character.characterStats.AddStatModifier(essenceEffect3.GetModifier());
            }

            Destroy(gameObject);
        }

        
    }
}
