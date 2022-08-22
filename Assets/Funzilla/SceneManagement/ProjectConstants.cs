// This file is auto-generated. Modifications are not saved.

namespace Funzilla
{
    public enum SceneID
    {
        GameManager,
        Gameplay,
        Lose,
        END
    }

    public static class Layers
    {
        /// <summary>
        /// Index of layer 'Default'.
        /// </summary>
        public const int Default = 0;
        /// <summary>
        /// Index of layer 'TransparentFX'.
        /// </summary>
        public const int TransparentFX = 1;
        /// <summary>
        /// Index of layer 'Ignore Raycast'.
        /// </summary>
        public const int Ignore_Raycast = 2;
        /// <summary>
        /// Index of layer 'Water'.
        /// </summary>
        public const int Water = 4;
        /// <summary>
        /// Index of layer 'UI'.
        /// </summary>
        public const int UI = 5;
        /// <summary>
        /// Index of layer 'Ground'.
        /// </summary>
        public const int Ground = 12;

        /// <summary>
        /// Bitmask of layer 'Default'.
        /// </summary>
        public const int DefaultMask = 1 << 0;
        /// <summary>
        /// Bitmask of layer 'TransparentFX'.
        /// </summary>
        public const int TransparentFXMask = 1 << 1;
        /// <summary>
        /// Bitmask of layer 'Ignore Raycast'.
        /// </summary>
        public const int Ignore_RaycastMask = 1 << 2;
        /// <summary>
        /// Bitmask of layer 'Water'.
        /// </summary>
        public const int WaterMask = 1 << 4;
        /// <summary>
        /// Bitmask of layer 'UI'.
        /// </summary>
        public const int UIMask = 1 << 5;
        /// <summary>
        /// Bitmask of layer 'Ground'.
        /// </summary>
        public const int GroundMask = 1 << 12;
    }

    public static class SceneNames
    {
        public const string INVALID_SCENE = "InvalidScene";
        public static readonly string[] ScenesNameArray = {
            "GameManager",
            "Gameplay",
            "Lose"
        };
        /// <summary>
        /// Convert from enum to string
        /// </summary>
        public static string GetSceneName(SceneID scene) {
              int index = (int)scene;
              if(index > 0 && index < ScenesNameArray.Length) {
                  return ScenesNameArray[index];
              } else {
                  return INVALID_SCENE;
              }
        }
    }

    public static class ExtentionHelpers {
        /// <summary>
        /// Shortcut to change enum to string
        /// </summary>
        public static string GetName(this SceneID scene) {
              return SceneNames.GetSceneName(scene);
        }
    }
}

