using UnityEngine;
using System;

namespace SamDriver.Util
{
    [CreateAssetMenu(fileName = "Event", menuName = "Sam/Event")]
    public class EventSO : ScriptableObject
    {
        public event Action OnEvent;
        public void Raise() => OnEvent?.Invoke();
    }
}