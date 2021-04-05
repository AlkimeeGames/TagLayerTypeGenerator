using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditorInternal;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor.Sync
{
    /// <summary>Checks for updates to the Tags in the Project.</summary>
    internal sealed class TagSync : ISync
    {
        /// <summary>Used to check what tag strings are in the Tag type.</summary>
        private readonly HashSet<string> _inType = new HashSet<string>();

        /// <summary>Used to check what tag strings are in the project.</summary>
        private readonly HashSet<string> _inUnity = new HashSet<string>();

        /// <inheritdoc />
        public bool IsInSync([NotNull] Type generatingType)
        {
            if (generatingType == null) throw new ArgumentNullException(nameof(generatingType));

            _inUnity.Clear();

            foreach (string tag in InternalEditorUtility.tags)
                _inUnity.Add(tag.Replace(" ", string.Empty));

            _inType.Clear();

            FieldInfo[] fields = generatingType.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo fieldInfo in fields)
                if (fieldInfo.IsLiteral)
                    _inType.Add(fieldInfo.Name);

            return _inType.SetEquals(_inUnity);
        }
    }
}