using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_Player : MonoBehaviour
{
    public GameObject player;

    public GameObject respawnPoint;

    public float transitionDuration = 2f;

    Damageable damageable;
    
    
    void Start()
    {
        damageable = player.GetComponent<Damageable>();
    }

    private void Update()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        Vector3 startPosition = player.transform.position;
        Vector3 targetPosition = respawnPoint.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / transitionDuration);
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        player.transform.position = targetPosition;
    }
}

