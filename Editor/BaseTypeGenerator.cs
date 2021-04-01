using JetBrains.Annotations;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor
{
    /// <summary>Share share settings between different close constructed types.</summary>
    public abstract class BaseTypeGenerator
    {
        /// <summary>Backing field for <see cref="Settings" />.</summary>
        private static TypeGeneratorSettings _settings;

        /// <summary>The <see cref="TypeGeneratorSettings" /> to use when generating files.</summary>
        [NotNull] protected static TypeGeneratorSettings Settings => _settings ? _settings : _settings = TypeGeneratorSettings.GetOrCreateSettings();
    }
}