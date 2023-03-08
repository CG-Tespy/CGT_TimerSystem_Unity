using TMPro;
using UnityEngine;

namespace CGT.Unity.TimerSys
{
    [AddComponentMenu("CGT TimerSys/TMPro UI Timer Display")]
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
            textField.text = TextToDisplay;
        }

        public override void StopDisplayingTime()
        {
            IsDisplayingTime = false;
        }
    }
}