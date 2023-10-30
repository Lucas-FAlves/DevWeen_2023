using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PotionEffects : MonoBehaviour
{
    private GameObject player;
    private ParticleSystem smoke;
    
    [SerializeField] private float smokeDuration;
    [SerializeField] private float invisTimer;
    [SerializeField] private float nerfDuration;
    [SerializeField] private float boostDuration;
    [SerializeField] private float nerf;
    [SerializeField] private float boost;
    
    public static Action<int> OnEffect;
    private void OnEnable() 
    {
        OnEffect += Effect;   
    }

    private void OnDisable() 
    {
        OnEffect -= Effect;   
    }
    
    private void Start() 
    {
        player = GameObject.Find("Player");
        smoke = GameObject.Find("Smoke").GetComponent<ParticleSystem>();
        
    } 

    void Effect (int id)
    {
        switch(id)
        {
            case 6:
            SmokeScreen(smokeDuration);
            break;

            case 7:
            Debug.Log("Instabilidade");
            break;

            case 8:
            Invisibility(invisTimer);
            break;

            case 9:
            SpeedNerf(nerf, nerfDuration);
            break;

            case 10:
            SpeedBoost(boost, boostDuration);
            break;
        }

    }

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
