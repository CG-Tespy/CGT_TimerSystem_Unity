using UnityEngine;
using UnityEngine.UI;

namespace CGT.Unity.TimerSys
{
    /// <summary>
    /// For displaying timers in Unity UI Text fields.
    /// </summary>
    [AddComponentMenu("CGT TimerSys/UI Timer Display")]
    public class UITimerDisplay : TimerDisplay
    {
        [SerializeField]
        protected Text textField;

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