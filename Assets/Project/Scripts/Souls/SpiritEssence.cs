using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritEssence : Interactable
{
    [SerializeField] public Essence essence;
    [HideInInspector] public int soulCost;

    public GameObject essenceUI;
    public SuperTextMesh costText;
    public SuperTextMesh nameText;
    //public SuperTextMesh flavorText;

    private void Awake()
    {
        soulCost = essence.soulCost;

        //essenceUI.SetActive(false);
        nameText.text = essence.essenceName + " Essence";
        costText.text = "Costs: " + soulCost.ToString() + " souls";
    }

    private void Update()
    {

        costText.Text = (soulCost <= 0) ? "" : "Costs: " + soulCost.ToString() + " souls";
        nameText.text = essence.essenceName + " Essence";
        //flavorText.Text = essence.essenceDescription;
        //if (CharacterSelectManager.selectedCharacter == CharacterSelectManager.Characters.Dauntless && Vector2.Distance(FloorGenerator.instance.dauntless.transform.position, transform.position) <= 4)
        //{
        //    essenceUI.SetActive(true);
        //    //costText.color = new Color(costText.color.r, costText.color.g, costText.color.b, 1 + (10 / Vector2.Distance(transform.position, FloorGenerator.instance.dauntless.transform.position)));
        //}
        //else if (CharacterSelectManager.selectedCharacter == CharacterSelectManager.Characters.Corvid && Vector2.Distance(FloorGenerator.instance.corvid.transform.position, transform.position) < 4)
        //{
        //    essenceUI.SetActive(true);
        //    //costText.color = new Color(costText.color.r, costText.color.g, costText.color.b, 1 + (10 / Vector2.Distance(transform.position, FloorGenerator.instance.dauntless.transform.position)));
        //}
        //else if (CharacterSelectManager.selectedCharacter == CharacterSelectManager.Characters.Tethered && Vector2.Distance(FloorGenerator.instance.tethered.transform.position, transform.position) < 4)
        //{
        //    essenceUI.SetActive(true);
        //    //costText.color = new Color(costText.color.r, costText.color.g, costText.color.b, 1 + (10 / Vector2.Distance(transform.position, FloorGenerator.instance.dauntless.transform.position)));
        //}
        float alpha = 1 - (Vector2.Distance(BaseCharacter.playerCharacter.transform.position, transform.position) / 6) * .6f;
            essenceUI.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
            nameText.color = new Color(.01f, .01f, .01f, alpha);
            costText.color = new Color(.01f, .01f, .01f, alpha);
            //flavorText.color = new Color(.01f, .01f, .01f, alpha);
            //Debug.Log("Alpha value: " + nameText.color.a);
    }

    public override void OnInteract(BaseCharacter character)
    {
        SpiritCharacter spirit = character.possessingSpirit;
        if (spirit.Souls >= soulCost)
        {
            spirit.Souls -= soulCost;
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

            Destroy(gameObject);
        }

        
    }
}
