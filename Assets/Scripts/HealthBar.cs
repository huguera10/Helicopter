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

    public void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
        CurrentHealth = CalculateHealth();

        showSmoke();

        this.HealthBarCurrentValue.text = CurrentHealth.ToString();
        this.HealthBarSlider.value = CurrentHealth;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    int CalculateHealth()
    {
        float x = (CurrentHealth / MaxHealth);
        return (int)(x*MaxHealth);
    }

    void showSmoke()
    {
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        var emission = ps.emission;
        emission.rateOverTime = (MaxHealth/20 -(CurrentHealth / 20));
    }

    void Die()
    {
        CurrentHealth = 0;
        GetComponent<HelicopterControl>().IsTurnedOn = false;
        //Destroy(GetComponent<HelicopterControl>());
        //Destroy(GameObject.Find("MainCamera").GetComponent<FollowingCamera>());
        //Destroy(GetComponent<Animator>());
        //Physics.gravity = new Vector3(0, -2, 0);
        GameObject.Find("EventSystem").GetComponent<SceneController>().isDead = true;
    }
}
