using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEffects : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private ParticleSystem smoke; 

    IEnumerator RestoreSpeed(float timer, PlayerMovement jogador)
    {
        jogador.moveSpeed = jogador.auxSpeed;
        yield return new WaitForSeconds(timer);
    }
    
    IEnumerator RestoreVisibility(float timer, SpriteRenderer jogador)
    {
        jogador.enabled = false;
        yield return new WaitForSeconds(timer);
    }

    IEnumerator DisableSmoke(float timer)
    {
        smoke.Stop();
        yield return new WaitForSeconds(timer);
    }

    public void SpeedBoost(float newSpeed, float duration)
    {
        PlayerMovement movimento = player.GetComponent<PlayerMovement>();
        movimento.moveSpeed += newSpeed;
        StartCoroutine(RestoreSpeed(duration, movimento));    
    }

    public void SpeedNerf(float newSpeed, float duration)
    {
        PlayerMovement movimento = player.GetComponent<PlayerMovement>();
        movimento.moveSpeed -= newSpeed;
        StartCoroutine(RestoreSpeed(duration, movimento));    
    }

    public void Invisibility(float duration)
    {
        SpriteRenderer jogador = player.GetComponent<SpriteRenderer>();
        jogador.enabled = false;
        StartCoroutine(RestoreVisibility(duration, jogador));
    }

    public void SmokeScreen(float duration)
    {
        smoke.Play();
        StartCoroutine(DisableSmoke(duration)); 
    }

}
