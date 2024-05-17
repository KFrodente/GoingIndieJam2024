using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TombBodyInteractable : Interactable
{
    [SerializeField] public int cost;
    [SerializeField] private BaseCharacter body;
    private bool bought;
    [Header("UI")]
    [SerializeField] private SpriteRenderer textBox;
    public SuperTextMesh costText;
    public SuperTextMesh nameText;
    public SuperTextMesh loreText;
    public override void OnInteract(BaseCharacter character)
    {
        if (bought) return;
        SpiritCharacter spirit = character.possessingSpirit;
        if (spirit.Souls >= cost)
        {
            spirit.Souls -= cost;
            body.transform.gameObject.SetActive(true);
            bought = true;
            body.transform.SetParent(null);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        costText.Text = (cost <= 0) ? "" : "Costs: " + cost.ToString() + " souls";

        float alpha = 1 - (Vector2.Distance(BaseCharacter.playerCharacter.transform.position, transform.position) / 6) * .6f;
        textBox.color = new Color(1, 1, 1, alpha);
        nameText.color = new Color(.01f, .01f, .01f, alpha);
        costText.color = new Color(.01f, .01f, .01f, alpha);
        loreText.color = new Color(.01f, .01f, .01f, alpha);
    }
}
