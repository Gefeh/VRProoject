using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [Tooltip("The current health of the player.")]
    public int health = 100;

    [Tooltip("How often the player takes damage (in seconds) when in contact with an enemy.")]
    public float damageInterval = 1.0f;
    private float nextDamageTime = 0f;
    private bool isDead = false;

    /// <summary>
    /// This is called repeatedly for every physics frame an object is inside the trigger.
    /// </summary>
    /*private void OnTriggerStay(Collider other)
    {
        if (isDead)
        {
            return;
        }

        if (other.CompareTag("Enemy") && other.GetComponentInParent<CrocodileStateMachine>().currentStateName == "Attacking")
        {
            if (Time.time >= nextDamageTime)
            {
                health -= 10;
                Debug.Log("Player hit by crocodile! Health: " + health);

                nextDamageTime = Time.time + damageInterval;

                if (health <= 0)
                {
                    Die();
                }
            }
        }
    }*/

    public void TakeDamage()
    {
        if (isDead)
        {
            return;
        }

            if (Time.time >= nextDamageTime)
            {
                health -= 10;
                Debug.Log("Player hit by crocodile! Health: " + health);

                nextDamageTime = Time.time + damageInterval;

                if (health <= 0)
                {
                    Die();
                }
            }
        
    }

    /// <summary>
    /// Handles the player's death.
    /// </summary>
    private void Die()
    {
        isDead = true;
        Debug.Log("Player has died.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
