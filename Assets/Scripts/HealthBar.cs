using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    private float _damage = 1;

    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public Slider HealthBarSlider;
    public Text HealthBarCurrentValue;

	// Use this for initialization
	void Start () {
        MaxHealth = 100f;
        CurrentHealth = MaxHealth;

        //this.HealthBarSlider.value = CalculateHealth();
       // HealthBarCurrentValue.text = CurrentHealth.ToString();
    }

    // Update is called once per frame
    void Update () {

	}

    public void DealDamage(float damageValue, Collision collision)
    {
        CurrentHealth -= damageValue;

        this.HealthBarCurrentValue.text = CurrentHealth.ToString();
        this.HealthBarSlider.value = CurrentHealth;
        if (CurrentHealth <= 0)
            Die(collision);
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    void Die(Collision collision)
    {
        CurrentHealth = _damage;
        //Destroy(GetComponent<HelicopterControl>());
        //Destroy(GetComponent<Animator>());
        //Physics.gravity = new Vector3(0, -2, 0);
        Debug.Log("Wasted");
    }
}
