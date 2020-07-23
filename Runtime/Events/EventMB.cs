using UnityEngine;
using System;

namespace SamDriver.Util
{
    public class EventMB : MonoBehaviour
    {
        public string Name { get => _name; }
        [SerializeField] string _name = "Event";

        public event Action OnEvent;
        public void Raise() => OnEvent?.Invoke();

        public override string ToString() => Name;
    }
}