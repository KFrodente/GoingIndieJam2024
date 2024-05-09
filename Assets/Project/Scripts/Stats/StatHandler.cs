using Stats;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [SerializeField] private BaseStats statVariables;
    [SerializeField] private BaseStats spiritStatModifiers;
    public Stats.Stats baseStats { get; private set; }
    public Stats.Stats modifierStats { get; private set; }

    private void Awake()
    {
        baseStats = new Stats.Stats(new StatMediator(), statVariables);
        modifierStats = new Stats.Stats(new StatMediator(), spiritStatModifiers);
    }

    private void Update()
    {
        baseStats.mediator.Update(Time.deltaTime);
        modifierStats.mediator.Update(Time.deltaTime);
    }

    public void AddStatModifier(StatModifier mod)
    {
        mod.MarkedForRemoval = false;
        baseStats.mediator.AddModifier(mod);
        modifierStats.mediator.AddModifier(mod);
    }
    public void RemoveStatModifier(StatModifier mod)
    {
        mod.MarkedForRemoval = true;
    }
    
}