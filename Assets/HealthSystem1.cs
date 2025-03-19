using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHealthChanged))] 
    public float healthAmount = 100f;

    [Header("Health Bar")]
    [SerializeField] private Image healthBar;

    
    void Start()
    {
        if (isLocalPlayer)
        {
            if (healthBar == null)
            {
                healthBar = GetComponentInChildren<Image>();
                if (healthBar == null)
                {
                    Debug.LogError("HealthBar not found in player's children!");
                }
                else
                {
                    Debug.Log("HealthBar found and assigned!");
                }
            }


            healthBar.gameObject.SetActive(true);
            UpdateHealthBar();
        }
        else
        {
            if (healthBar != null)
            {
                healthBar.gameObject.SetActive(false);
            }
        }
    }

    private void OnHealthChanged(float oldHealth, float newHealth)
    {
        Debug.Log($"Health changed from {oldHealth} to {newHealth}");
        healthAmount = newHealth;
        UpdateHealthBar();

        if (healthAmount <= 0)
        {
            Die();
        }
    }

    [Command]
    public void CmdTakeDamage(float damage)
    {
        Debug.Log($"Player took {damage} damage!");
        healthAmount = Mathf.Clamp(healthAmount - damage, 0, 100);
        Debug.Log("New health: " + healthAmount);

        if (healthAmount <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        if (isServer)
        {
            CmdTakeDamage(damage);
        }
    }

    [Command]
    public void CmdHeal(float healAmount)
    {
        healthAmount = Mathf.Clamp(healthAmount + healAmount, 0, 100);
        Debug.Log("Player healed! New health: " + healthAmount);
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = healthAmount / 100f;
            Debug.Log("Health Bar Updated: " + healthBar.fillAmount);
        }
        else
        {
            Debug.LogWarning("Health Bar UI is NOT assigned!");
        }
    }

    void Die()
    {
        Debug.Log("Game Over!");
        if (isServer)
        {
            RpcDie();
        }
    }

    [ClientRpc]
    void RpcDie()
    {
        Debug.Log("Player died!");
        if (isLocalPlayer)
        {
            
            gameObject.SetActive(false);
            SceneManager.LoadScene("MenuMort");
        }
    }

}