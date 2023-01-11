using UnityEngine;

[CreateAssetMenu(fileName ="PlayerInputReference", menuName = "Scriptable/Player/PlayerInputReference")]
public class PlayerInputReference: ScriptableObject
{
    public Vector2 Direction { get; set; }
    public bool OptionMenu { get; set; }
}
