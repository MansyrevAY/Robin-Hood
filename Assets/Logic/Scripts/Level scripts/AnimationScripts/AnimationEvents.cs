using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public AttackBehaviour attack;

    public void DealDamage()
    {
        attack.MakeAttack();
    }
}
