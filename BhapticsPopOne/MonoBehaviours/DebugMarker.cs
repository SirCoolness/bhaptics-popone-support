using UnityEngine;

namespace BhapticsPopOne.MonoBehaviours
{
    public class DebugMarker : MonoBehaviour
    {
        public DebugMarker(System.IntPtr ptr) : base(ptr) {}

        public LineDrawer DebugLine;

        public float _size;

        // public Collider Collider;
        
        private void OnEnable()
        {
            // Collider = GetComponent<Collider>();
            DebugLine = new LineDrawer();
        }

        private void FixedUpdate()
        {
            var position = transform.position;
            DebugLine.DrawLineInGameView(position + new Vector3(0, -(_size / 2f), 0), position + new Vector3(0, _size / 2f, 0), Color.blue);
        }

        private void OnDestroy()
        {
            DebugLine.Destroy();
        }

        public void SetSize()
        {
            DebugLine.Destroy();
            DebugLine = new LineDrawer(_size);
        }
    }
}