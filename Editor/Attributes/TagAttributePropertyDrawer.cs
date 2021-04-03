using AlkimeeGames.TagLayerTypeGenerator.Attributes;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor.Attributes
{
    /// <summary>Converts a <see cref="string" /> property into a <see cref="EditorGUI.TagField(UnityEngine.Rect,string)" />.</summary>
    [CustomPropertyDrawer(typeof(TagAttribute))]
    internal sealed class TagAttributePropertyDrawer : PropertyDrawer
    {
        /// <inheritdoc />
        public override void OnGUI(Rect position, [NotNull] SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType != SerializedPropertyType.String)
                EditorGUI.PropertyField(position, property, label);
            else
                property.stringValue = EditorGUI.TagField(position, label, property.stringValue);

            EditorGUI.EndProperty();
        }
    }
}