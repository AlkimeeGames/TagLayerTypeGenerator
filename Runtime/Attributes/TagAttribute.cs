using System;
using UnityEngine;

namespace AlkimeeGames.TagLayerTypeGenerator.Attributes
{
    /// <summary>Apply to any <see cref="string" /> property and it'll be converted into a tag field in the inspector.</summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TagAttribute : PropertyAttribute
    {
    }
}