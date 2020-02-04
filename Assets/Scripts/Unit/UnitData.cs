using UnityEngine;

public class UnitData : MonoBehaviour, IDamageable {
    [SerializeField]
    private int health, maxHealth;
    public delegate void HealthChangedAction(UnitData source, int health, int oldHealth);
    public event HealthChangedAction HealthChanged;
    public int Health {
        get => health;
        set {
            int oldHealth = health;
            health = value;
            if (health > MaxHealth)
                health = MaxHealth;
            HealthChanged?.Invoke(this, health, oldHealth);
        }
    }
    public int MaxHealth {
        get => maxHealth;
        set {
            maxHealth = value;
            if (Health > maxHealth)
                health = maxHealth;
        }
    }

    private void CheckHealth() {
        if (Health <= 0)
            Destroy();
    }

    public delegate void DamagedAction(UnitData source, int damage);
    public event DamagedAction Damaged;
    public void Damage(int damage) {
        Health -= damage;
        Damaged?.Invoke(this, damage);
        CheckHealth();
    }

    public delegate void HealedAction(UnitData source, int heal);
    public event HealedAction Healed;
    public void Heal(int heal) {
        Health += heal;
        Healed?.Invoke(this, heal);
        CheckHealth();
    }

    public delegate void DestroyedAction(UnitData source);
    public event DestroyedAction Destroyed;
    public void Destroy() {
        Destroyed?.Invoke(this);
    }
}
