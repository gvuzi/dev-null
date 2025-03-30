using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour {
    public Image healthBarSprite;
    public float animationReduceSpeed = 2;
    private float target = 0; // start of health bar
    
    public void UpdateHealth(float maxHealth, float currentHealth) {
        target = currentHealth / maxHealth;
    }

    void Update() {
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, target, animationReduceSpeed * Time.deltaTime);
    }

}

    

