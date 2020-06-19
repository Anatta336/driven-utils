using UnityEngine;
using System;

namespace SamDriver.Util
{
    public class EventMB : MonoBehaviour
    {
        public string Name = "Event";
        public event Action OnEvent;
        public void Raise() => OnEvent?.Invoke();

        // attempts to make this event more identifiable
        public new string name { get => Name; }
        public override string ToString() => Name;
    }
}