using TMPro;
using UnityEngine;

namespace CGT.Unity.TimerSys
{
    public class TMProUITimerDisplay : TimerDisplay
    {
        [SerializeField]
        protected TextMeshProUGUI textField;

        public override void DisplayTime()
        {
            UpdateTextField();
        }

        protected virtual void UpdateTextField()
        {
            string timeToDisplayString = TimeToDisplay.ToString(FormatToDisplayIn);
            textField.text = timeToDisplayString;
        }

        public override void StopDisplayingTime()
        {
            IsDisplayingTime = false;
        }
    }
}