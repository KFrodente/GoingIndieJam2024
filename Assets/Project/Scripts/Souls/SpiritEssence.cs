using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritEssence : Interactable
{
    [SerializeField] public Essence essence;
    public int soulCost;

    private void Awake()
    {
        soulCost = essence.soulCost;
    }

    public override void OnInteract(BaseCharacter character)
    {
        base.OnInteract(character);
        StatEffect essenceEffect1;
        StatEffect essenceEffect2;
        StatEffect essenceEffect3;
        if (essence.statType1 != StatType.None)
        {
            essenceEffect1 = new StatEffect(essence.statType1, essence.operatorType1, essence.Value1);
            character.characterStats.AddStatModifier(essenceEffect1.GetModifier());
        }
        if (essence.statType2 != StatType.None)
        {
            essenceEffect2 = new StatEffect(essence.statType2, essence.operatorType2, essence.Value2);
            character.characterStats.AddStatModifier(essenceEffect2.GetModifier());
        }
        if (essence.statType3 != StatType.None)
        {
            essenceEffect3 = new StatEffect(essence.statType3, essence.operatorType3, essence.Value3);
            character.characterStats.AddStatModifier(essenceEffect3.GetModifier());
        }
        
    }
}
