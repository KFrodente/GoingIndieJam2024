using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    
    protected float targetAngle;
    protected bool movementFrozen;
    protected bool rotationFrozen;
    protected List<Timer> timers = new List<Timer>();

    public virtual void Freeze(float movementDuration, float rotationDuration)
    {
        if (movementDuration > 0)
        {
            movementFrozen = true;
            Timer timer = new CountdownTimer(movementDuration);
            timer.OnTimerStop += () => EndTimer(true, false, ref timer);
            timer.Start();
            timers.Add(timer);
        }

        if (rotationDuration > 0)
        {
            rotationFrozen = true;
            Timer timer = new CountdownTimer(rotationDuration);
            timer.OnTimerStop += () => EndTimer(false, true, ref timer);
            timer.Start();
            timers.Add(timer);
        }
        
    }

    protected void EndTimer(bool m, bool r, ref Timer t)
    {
        timers.Remove(t);
        UnFreeze(m, r);
    }

    protected virtual void Update()
    {
        UpdateTimers();
    }
    protected virtual void UpdateTimers()
    {
        foreach (Timer t in timers)
        {
            t.Tick(Time.deltaTime);
        }
    }

    public virtual void ExplodeAway(Vector2 center, float power)
    {
        Move(((Vector2)transform.position - center).normalized, power, ForceMode2D.Impulse, savedCharacter, true); // Explosion Always Forced
    }

    public virtual void UnFreeze(bool movement = true, bool rotation = true)
    {
        if (movement) movementFrozen = false;
        if (rotation) rotationFrozen = false;
    }
    public virtual void Move(Vector2 direction, float speed, ForceMode2D forceMode, BaseCharacter c, bool forcedAction = false)
    {
        if (movementFrozen && !forcedAction) return;
        c.rb.AddForce(direction * speed, forceMode);
    }
    public virtual void SetTargetAngle(Vector2 direction, bool forcedAction = false)
    {
        if (rotationFrozen && !forcedAction) return;
        targetAngle = InputUtils.GetAngle(direction);
    }
    public virtual void AngleTowardTargetAngle(float speed, BaseCharacter c)
    {
        transform.rotation = Quaternion.Euler(0, 0,Mathf.LerpAngle(transform.rotation.z, targetAngle, Time.deltaTime * speed));
    }

    public virtual void ChangeAngle(Vector2 direction)
    {
        
    }
    protected BaseCharacter savedCharacter;

    public void SetCharacter(BaseCharacter c)
    {
        savedCharacter = c;
    }
}
