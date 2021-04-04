using System;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor.Sync
{
    /// <summary>Syncs values in a project with values in a type.</summary>
    internal interface ISync
    {
        /// <summary>Are the values in project different to the values in the type?</summary>
        /// <param name="generatingType">The type to sync with.</param>
        /// <returns>True if there are changes to the values in the project.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="generatingType" /> is null.</exception>
        /// <returns>True if there are changes to the values in the project.</returns>
        bool HasUpdates(Type generatingType);
    }
}