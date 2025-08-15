using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDealer : MonoBehaviour
{
    [SerializeField] int bonusScore = 55;

    public int GetBonusScore()
    {
        return bonusScore;
    }

    public void Touch()
    {
        Destroy(gameObject);
    }
}
