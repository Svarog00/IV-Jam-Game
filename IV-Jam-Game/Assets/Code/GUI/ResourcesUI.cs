using System.Collections;
using UnityEngine;
using TMPro;
using Assets.Code.Gameplay.Map;

namespace Assets.Code.GUI
{
    public class ResourcesUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeCostText;
        [SerializeField] private TMP_Text _energyCostText;

        private void Start()
        {
            AreaController.OnEnterAreaEventHandler += AreaController_OnEnterAreaEventHandler;
        }

        private void AreaController_OnEnterAreaEventHandler(object sender, OnEnterAreaEventArgs e)
        {
            SetValues(e.TimeCost, e.EnergyCost);
        }

        public void SetValues(int time, int energy)
        {
            _timeCostText.text = time.ToString();
            _energyCostText.text = energy.ToString();
        }
    }
}