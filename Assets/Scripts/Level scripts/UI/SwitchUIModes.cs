using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchUIModes : MonoBehaviour
{
    public GameObject attackUI;
    public GameObject preparationUI;

    public void ChangeToAttackUI()
    {
        preparationUI.SetActive(false);
        attackUI.SetActive(true);
    }
}
