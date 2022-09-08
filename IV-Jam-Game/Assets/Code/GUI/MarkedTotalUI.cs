using System.Collections;
using UnityEngine;
using TMPro;
using Assets.Code.Gameplay.Map;

namespace Assets.Code.GUI
{
    public class MarkedTotalUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeCostText;
        [SerializeField] private TMP_Text _energyCostText;

        private int _totalEnergy = 0;
        private int _totalTime = 0;

        private void Start()
        {
            FindObjectOfType<PathBuildingManager>().OnChangePathEventHandler += MarkedTotalUI_OnChangePathEventHandler;
            FindObjectOfType<PathBuildingManager>().OnPathClearActionHandler += MarkedTotalUI_OnPathClearActionHandler;
        }

        private void MarkedTotalUI_OnPathClearActionHandler(object sender, System.EventArgs e)
        {
            _totalTime = 0;
            _totalEnergy = 0;
            SetValues(_totalTime, _totalEnergy);
        }

        private void MarkedTotalUI_OnChangePathEventHandler(object sender, OnChangePathEventArgs e)
        {
            _totalTime += e.TimeCost;
            _totalEnergy += e.EnergyCost;
            SetValues(_totalTime, _totalEnergy);
        }

        public void SetValues(int time, int energy)
        {
            _timeCostText.text = time.ToString();
            _energyCostText.text = energy.ToString();
        }
    }
}
