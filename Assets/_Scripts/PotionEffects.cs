using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PotionEffects : MonoBehaviour
{
    private GameObject player;
    private ParticleSystem smoke;
    private SpriteRenderer sr;
    
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
        sr = player.GetComponent<SpriteRenderer>();
        smoke = GameObject.Find("Smoke").GetComponent<ParticleSystem>();
        smoke.gameObject.SetActive(false);
        
    } 

    void Effect (int id)
    {
        Debug.Log("Effect" + id);
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
        yield return new WaitForSeconds(timer);
        jogador.moveSpeed = jogador.auxSpeed;
    }
    
    IEnumerator RestoreVisibility(float timer, SpriteRenderer jogador)
    {
        jogador.enabled = false;
        yield return new WaitForSeconds(timer);
        jogador.enabled = true;
    }

    IEnumerator DisableSmoke(float timer)
    {
        yield return new WaitForSeconds(timer);
        smoke.Stop();
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
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
        StartCoroutine(RestoreVisibility(duration, sr));
    }

    public void SmokeScreen(float duration)
    {
        smoke.gameObject.SetActive(true);
        smoke.Play();
        StartCoroutine(DisableSmoke(duration)); 
    }

}
