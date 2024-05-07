using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

namespace Stats
{
    public class AreaStatManipulator : MonoBehaviour
    {
        [SerializeField] private List<StatEffect> effects = new List<StatEffect>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out StatHandler handler))
            {
                foreach (var e in effects)
                {
                    handler.AddStatModifier(e.GetModifier());
                }
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out StatHandler handler))
            {
                foreach (var e in effects)
                {
                    handler.RemoveStatModifier(e.GetModifier());
                }
            }
        }
    }

    
}