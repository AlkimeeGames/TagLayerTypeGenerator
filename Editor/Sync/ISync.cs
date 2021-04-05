using System;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor.Sync
{
    /// <summary>Syncs values in a project with values in a type.</summary>
    public interface ISync
    {
        /// <summary>Are the values in the Type the same as the values in the Project.</summary>
        /// <param name="generatingType">The type to sync with.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="generatingType" /> is null.</exception>
        /// <returns>True if the Type values and Project values are in sync.</returns>
        bool IsInSync(Type generatingType);
    }
}