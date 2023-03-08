using UnityEngine;
using UnityEngine.Events;

namespace CGT.Unity.TimerSys
{
	[System.Serializable]
	public class CountdownUnityEvents : TimerUnityEvents
	{
		[SerializeField]
		[Tooltip("Triggers when this countdown reaches zero.")]
		protected UnityEvent onEnd = new UnityEvent();
		public virtual UnityEvent OnEnd { get { return onEnd; } }
	}
}