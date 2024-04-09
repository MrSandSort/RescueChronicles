using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab;

    [SerializeField]
    private float attackCooldown = 1f; 
    private bool canAttack = true; 
    public void FireProjectile() 
    {
        if (canAttack) 
        {
            GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);

            Vector3 origScale = projectile.transform.localScale;

            projectile.transform.localScale = new Vector2(origScale.x * transform.localScale.x > 0 ? 1 : -1, origScale.y);

            Destroy(projectile, 2f);

            StartCoroutine(AttackCooldown());
        }
      
    
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false; 
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
