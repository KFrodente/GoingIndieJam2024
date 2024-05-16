using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

namespace Stats
{
    public class AreaStatManipulator : MonoBehaviour
    {
        [SerializeField] private bool removeOnExit;
        [SerializeField] private List<StatEffect> effects = new List<StatEffect>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Damagable d) && d.IsPlayer)
            {
                foreach (var e in effects)
                {
                    d.baseCharacter.characterStats.AddStatModifier(e.GetModifier());
                }
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if(!removeOnExit) return;
            if (other.TryGetComponent(out Damagable d) && d.IsPlayer)
            {
                foreach (var e in effects)
                {
                    d.baseCharacter.characterStats.RemoveStatModifier(e.GetModifier());
                }
            }
        }
    }

    
}