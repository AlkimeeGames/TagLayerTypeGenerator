using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor
{
    /// <summary>Abstract Type Generator.</summary>
    public abstract class TypeGenerator<T> : BaseTypeGenerator, ITypeGenerator where T : TypeGenerator<T>
    {
        /// <summary>Backing field for <see cref="Generator" />.</summary>
        protected static T Instance;

        /// <summary>Instance of <see cref="ITypeGenerator" />.</summary>
        [PublicAPI] public static ITypeGenerator Generator => Instance;

        /// <summary>Invoked when the file is generated.</summary>
        public event UnityAction OnFileGeneration;

        /// <inheritdoc />
        public abstract void GenerateFile();

        /// <inheritdoc />
        public abstract bool CanGenerate();

        /// <summary>Invokes <see cref="OnFileGeneration" />.</summary>
        protected void InvokeOnFileGeneration()
        {
            OnFileGeneration?.Invoke();
        }

        /// <summary>Validates a given field.</summary>
        /// <param name="field">The member field to validate.</param>
        /// <param name="identifier">The identifier we're creating a member for.</param>
        protected static void ValidateIdentifier([NotNull] CodeObject field, string identifier)
        {
            try
            {
                Assert.IsNotNull(field);
                CodeGenerator.ValidateIdentifiers(field);
            }
            catch (ArgumentException)
            {
                Debug.LogError($"'{identifier}' is not a valid identifier. See <a href=\"https://bit.ly/IdentifierNames\">https://bit.ly/IdentifierNames</a> for details.");
                throw;
            }
        }

        /// <summary>Creates the path for the file asset.</summary>
        /// <param name="path">The path to use to create the file asset.</param>
        protected static void CreateAssetPathIfNotExists(string path)
        {
            Assert.IsNotNull(path);
            path = path.Remove(path.LastIndexOf('/'));

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
    }
}