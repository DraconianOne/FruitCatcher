using Unity;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "Throwable", menuName = "Throwables")]
    public class ThrownObject : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite itemSprite;
        [SerializeField] private int itemPoints = 1;
        [SerializeField] private bool isCatchable;

        public int ItemPoints { get { return itemPoints; }}
        public bool IsCatchable { get { return isCatchable; }}

    }
}