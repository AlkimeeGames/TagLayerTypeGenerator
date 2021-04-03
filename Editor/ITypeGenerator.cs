using JetBrains.Annotations;
using UnityEngine.Events;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor
{
    /// <summary>Public interface for a Type generator.</summary>
    [PublicAPI]
    public interface ITypeGenerator
    {
        /// <summary>Invoked when the a file is generated.</summary>
        [PublicAPI] event UnityAction FileGenerated;

        /// <summary>Generates a new type file.</summary>
        [PublicAPI] void GenerateFile();

        /// <summary>Are the settings correct to generate a new type file.</summary>
        /// <returns>True if all settings are correct.</returns>
        [PublicAPI] bool CanGenerate();
    }
}