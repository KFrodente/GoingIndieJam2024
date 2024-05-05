using Stats;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [SerializeField] private BaseStats statVariables;
    public Stats.Stats stats { get; private set; }

    private void Awake()
    {
        stats = new Stats.Stats(new StatMediator(), statVariables);
    }

    private void Update()
    {
        stats.mediator.Update(Time.deltaTime);
    }

    public void AddStatModifier(StatModifier mod)
    {
        mod.MarkedForRemoval = false;
        stats.mediator.AddModifier(mod);
    }
    public void RemoveStatModifier(StatModifier mod)
    {
        mod.MarkedForRemoval = true;
    }
}