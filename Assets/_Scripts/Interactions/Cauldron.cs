using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class Cauldron : MonoBehaviour
{
    private GameObject[] slots;
    private int currentIndex = 0;
    private ItemSO[] cauldronItems;

    private PlayerInputActions playerInputActions;
    private bool playerIsNear = false;

    private PlayerInteraction player;
    private Spoon spoon;
    [SerializeField] private ItemSO flaskSO;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float interactionRange = 2f;

    private PotionSO[] allPotionsSO;
    private PotionSO currentPotion;
    [SerializeField] private PotionSO wrongPotion;
    private bool existingRecipe = false;

    private Animator anim;
    [SerializeField] private AnimatorController animDerramando;
    [SerializeField] private AnimatorController animMarrom;
    [SerializeField] private AnimatorController animRosa;
    [SerializeField] private AnimatorController animRoxo;
    [SerializeField] private AnimatorController animVermelho;
    [SerializeField] private AnimatorController animAgua;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        player = FindObjectOfType<PlayerInteraction>();
        spoon = FindObjectOfType<Spoon>();
        allPotionsSO = Resources.LoadAll<PotionSO>("PotionsSO");
        anim = GetComponent<Animator>();

        slots = new GameObject[transform.childCount];
        cauldronItems = new ItemSO[transform.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = transform.GetChild(i).gameObject;
        }
        currentPotion = wrongPotion;
    }

    private void OnEnable()
    {
        playerInputActions.Player.VirarCaldeirao.performed += EmptyCauldron;
        playerInputActions.Player.MexerComColher.performed += MixCauldron;
    }

    private void OnDisable()
    {
        playerInputActions.Player.VirarCaldeirao.performed -= EmptyCauldron;
        playerInputActions.Player.MexerComColher.performed -= MixCauldron;
    }

    private void FillFlask()
    {
        if (!playerIsNear || !player.IsHoldingFlask)
            return;

        var flask = player.GetCurrentFlask();
        Debug.Log(flask);
        flask.FillFlask(currentPotion.id);
    }

    private void MixCauldron(InputAction.CallbackContext context)
    {
        if (!spoon.IsOnHand || !playerIsNear)
            return;

        existingRecipe = CheckIfRecipeMatch();
        if (existingRecipe)
        {
            switch(currentPotion.id)
            {
                case 6:
                    anim.runtimeAnimatorController = animVermelho;
                    break;
                case 7:
                    anim.runtimeAnimatorController = animRosa;
                    break;
                case 8:
                    anim.runtimeAnimatorController = animRoxo;
                    break;
                case 9:
                    anim.runtimeAnimatorController = animAgua;
                    break;
                case 10:
                    anim.runtimeAnimatorController = animRosa;
                    break;
            }

        } 

    }
    public bool CheckIfRecipeMatch()
    {
        bool matched = false;
        foreach (var potion in allPotionsSO)
        {
            for (int i = 0; i < cauldronItems.Length; i++)
            {          
                if (!cauldronItems[i].Equals(potion.ingredients[i]))
                    break;

                if(i == cauldronItems.Length - 1)
                    matched = true;
            }

            if (matched)
            {
                currentPotion = potion;
                
                break;
            } 
            else
            {
                currentPotion = wrongPotion;
            }
        }
        return matched;
    }

    private float lenght;
    private void EmptyCauldron(InputAction.CallbackContext context)
    {
        if (player.IsHoldingItem)
            return;

        if (!playerIsNear)
            return;

        anim.runtimeAnimatorController = animDerramando;
        lenght = anim.GetCurrentAnimatorStateInfo(0).length;

        StartCoroutine(ResetCauldron());

        Debug.Log(currentPotion.id + " Effect");
        PotionEffects.OnEffect?.Invoke(currentPotion.id);
        currentIndex = 0;
        existingRecipe = false;

        AudioManager.instance.StopSound("cauldron");

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<SpriteRenderer>().sprite = null;
            cauldronItems[i] = null;
        }
    }

    IEnumerator ResetCauldron()
    {
        yield return new WaitForSeconds(lenght);
        anim.runtimeAnimatorController = null;
    }

    public bool PlaceItemOnCauldron(ItemSO item)
    {
        if (spoon.IsOnHand || !player.IsHoldingItem)
            return false;

        if(!AudioManager.instance.IsPlaying("cauldron"))
            AudioManager.instance.PlaySound("cauldron");

        if (item == flaskSO)
        {
            FillFlask();
            return false;
        }

        if (currentIndex >= cauldronItems.Length)
            return false;

        cauldronItems[currentIndex] = item;

        if(item == null)
            return false;

        slots[currentIndex].GetComponent<SpriteRenderer>().sprite = cauldronItems[currentIndex].sprite;
        currentIndex++;
        return true;
    }

    private void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, interactionRange, playerMask);

        if(player == null)
            playerIsNear = false;
        else
            playerIsNear = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

}
