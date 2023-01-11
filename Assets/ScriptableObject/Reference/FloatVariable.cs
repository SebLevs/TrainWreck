using UnityEngine;

namespace Reference.Variable
{
    [CreateAssetMenu(fileName = "FloatReference", menuName = "Scriptable/Reference/Float")]
    public class FloatVariable : ScriptableObject
    {
        public float Value;
    }
}