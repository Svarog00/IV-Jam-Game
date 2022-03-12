using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Code.GUI
{
    public class ResourcesUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeCostText;
        [SerializeField] private TMP_Text _energyCostText;

        public void SetValues(int time, int energy)
        {
            _timeCostText.text = time.ToString();
            _energyCostText.text = energy.ToString();
        }
    }
}