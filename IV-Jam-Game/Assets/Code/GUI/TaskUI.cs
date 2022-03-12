using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeCostText;
    [SerializeField] private TMP_Text _energyCostText;

    public void SetValues(int time, int energy)
    {
        _timeCostText.text = time.ToString();
        _energyCostText.text = energy.ToString();
    }
}
