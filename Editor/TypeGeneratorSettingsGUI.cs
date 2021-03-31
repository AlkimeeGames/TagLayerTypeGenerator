using UnityEditor;
using UnityEngine;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor
{
    [CustomEditor(typeof(TypeGeneratorSettings))]
    internal sealed class TypeGeneratorSettingsGUI : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel);
            EditorGUI.BeginDisabledGroup(TagTypeGenerator.Generator.CanGenerate() == false);
            if (GUILayout.Button("Regenerate Tag Type File")) TagTypeGenerator.Generator.GenerateFile();
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(LayerTypeGenerator.Generator.CanGenerate() == false);
            if (GUILayout.Button("Regenerate Layer Type File")) LayerTypeGenerator.Generator.GenerateFile();
            EditorGUI.EndDisabledGroup();

            if (GUILayout.Button("Open Project Settings")) SettingsService.OpenProjectSettings(TypeGeneratorSettingsProvider.ProjectSettingPath);
        }
    }
}