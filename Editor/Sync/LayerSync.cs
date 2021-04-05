using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditorInternal;
using UnityEngine;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor.Sync
{
    /// <summary>Checks for updates to the Layers in the Project.</summary>
    internal sealed class LayerSync : ISync
    {
        /// <summary>Used to check if what layer strings and IDs are in the Layer type.</summary>
        private readonly HashSet<ValueTuple<string, int>> _inType = new HashSet<ValueTuple<string, int>>();

        /// <summary>Used to check if what layer strings and IDs are in the project.</summary>
        private readonly HashSet<ValueTuple<string, int>> _inUnity = new HashSet<ValueTuple<string, int>>();

        /// <inheritdoc />
        public bool IsInSync([NotNull] Type generatingType)
        {
            if (generatingType == null) throw new ArgumentNullException(nameof(generatingType));

            _inUnity.Clear();

            foreach (string layer in InternalEditorUtility.layers)
            {
                string layerName = layer.Replace(" ", string.Empty);
                _inUnity.Add(new ValueTuple<string, int>(layerName, LayerMask.NameToLayer(layer)));
            }

            _inType.Clear();

            FieldInfo[] fields = generatingType.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo fieldInfo in fields)
                if (fieldInfo.IsLiteral)
                    _inType.Add(new ValueTuple<string, int>(fieldInfo.Name, (int) fieldInfo.GetValue(null)));

            return _inType.SetEquals(_inUnity);
        }
    }
}