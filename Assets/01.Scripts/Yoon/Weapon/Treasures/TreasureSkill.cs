using UnityEngine;

public abstract class TreasureSkill : MonoBehaviour
{
    public abstract void UseSkill(Transform targetTransform, int damage, float duration);
}
