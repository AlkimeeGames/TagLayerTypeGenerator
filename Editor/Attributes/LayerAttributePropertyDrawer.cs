using AlkimeeGames.TagLayerTypeGenerator.Attributes;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor.Attributes
{
    /// <summary>Converts an <see cref="int" /> property into a <see cref="EditorGUI.LayerField(UnityEngine.Rect,int)" />.</summary>
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    internal sealed class LayerAttributePropertyDrawer : PropertyDrawer
    {
        /// <inheritdoc />
        public override void OnGUI(Rect position, [NotNull] SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType != SerializedPropertyType.Integer)
                EditorGUI.PropertyField(position, property, label);
            else
                property.intValue = EditorGUI.LayerField(position, label, property.intValue);

            EditorGUI.EndProperty();
        }
    }
}