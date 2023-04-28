using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public HealthBar healthBar;
    public Text healthText;
    public GameManager gameover;

    public void Hit(float damage)
    {
        currentHealth -= damage;
        healthText.text = currentHealth.ToString() + " Health";
        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            //SceneManager.LoadScene(0);
            gameover.EndGame();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
