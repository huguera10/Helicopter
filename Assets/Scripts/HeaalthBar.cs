using UnityEngine;
using UnityEngine.UI;

public class HeaalthBar : MonoBehaviour {
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public Slider HealthBar;
    public Text HealthBarCurrentValue;

	// Use this for initialization
	void Start () {
        MaxHealth = 100f;
        CurrentHealth = MaxHealth;

        HealthBar.value = CalculateHealth();
        HealthBarCurrentValue.text = CurrentHealth.ToString();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.X))
            DealDamage(2);
	}

    void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
        HealthBar.value = CalculateHealth();
        HealthBarCurrentValue.text = CurrentHealth.ToString();

        if (CurrentHealth <= 0)
            Die();
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    void Die()
    {
        CurrentHealth = 0;
        Debug.Log("Wasted");
    }
}
