using UnityEngine;

namespace Feed
{
    public class CursorManager : MonoBehaviour
    {
        [System.Serializable]
        class CursorConfig
        {
            public Texture2D Texture;
            public Vector2 Hotspot;
        }

        [SerializeField] CursorMode _cursorMode = default;
        [SerializeField] CursorConfig _default = default;

        void Start()
        {
            SetCursor(_default);
        }

        void SetCursor(CursorConfig config)
        {
            Cursor.SetCursor(
              config.Texture,
              config.Hotspot * new Vector2(config.Texture.width, config.Texture.height),
              _cursorMode
            );
        }
    }
}