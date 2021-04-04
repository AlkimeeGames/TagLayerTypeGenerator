using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditorInternal;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor
{
    /// <summary>Checks for updates to the Tags in the Project.</summary>
    internal sealed class TagUpdatesChecker
    {
        /// <summary>Used to check what tag strings are in the Tag type.</summary>
        private readonly HashSet<string> _inType = new HashSet<string>();

        /// <summary>Used to check what tag strings are in the project.</summary>
        private readonly HashSet<string> _inUnity = new HashSet<string>();

        /// <summary>Are the Tags different to the Tags in the type?</summary>
        /// <param name="generatingType"></param>
        /// <returns>True if there are changes to the Tags in the project.</returns>
        public bool HasUpdates([NotNull] Type generatingType)
        {
            if (generatingType == null) throw new ArgumentNullException(nameof(generatingType));

            _inUnity.Clear();

            foreach (string tag in InternalEditorUtility.tags)
                _inUnity.Add(tag.Replace(" ", String.Empty));

            _inType.Clear();

            FieldInfo[] fields = generatingType.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo fieldInfo in fields)
                if (fieldInfo.IsLiteral)
                    _inType.Add(fieldInfo.Name);

            return !_inType.SetEquals(_inUnity);
        }
    }
}