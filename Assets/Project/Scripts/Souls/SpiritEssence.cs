using Stats;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpiritEssence : Interactable
{
    [SerializeField] public Essence essence;
    [HideInInspector] public int soulCost;

    public GameObject essenceUI;
    public SuperTextMesh costText;
    public SuperTextMesh nameText;
    public SuperTextMesh flavorText;

    private SpriteRenderer essenceSpriteRenderer;

    private Vector2 characterPos;

    private void Awake()
    {
        soulCost = essence.soulCost;
        essenceSpriteRenderer = essenceUI.GetComponentInChildren<SpriteRenderer>();

        //essenceUI.SetActive(false);
        nameText.text = essence.essenceName + " Essence";
        costText.text = "Costs: " + soulCost.ToString() + " souls";
        essenceSpriteRenderer.color = new Color(1, 1, 1, 0);
        nameText.color = new Color(.01f, .01f, .01f, 0);
        costText.color = new Color(.01f, .01f, .01f, 0);
        flavorText.color = new Color(.01f, .01f, .01f, 0);
    }

    private void Update()
    {
        characterPos = BaseCharacter.playerCharacter.transform.position;
        float distance = Vector2.Distance(characterPos, transform.position);
        if (distance < 10)
        {

        costText.Text = (soulCost <= 0) ? "" : "Costs: " + soulCost.ToString() + " souls";
        nameText.text = essence.essenceName + " Essence";
        flavorText.Text = essence.essenceDescription;
        float alpha = 1 - (Vector2.Distance(characterPos, transform.position) / 6) * .6f;
            essenceSpriteRenderer.color = new Color(1, 1, 1, alpha);
            nameText.color = new Color(.01f, .01f, .01f, alpha);
            costText.color = new Color(.01f, .01f, .01f, alpha);
            flavorText.color = new Color(.01f, .01f, .01f, alpha);
        }
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
            AudioManager.instance.PlayPickUpSound();
            Destroy(gameObject);
        }

        
    }
}
