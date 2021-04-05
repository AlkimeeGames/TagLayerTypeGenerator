using System.CodeDom;
using AlkimeeGames.TagLayerTypeGenerator.Editor.Settings;
using AlkimeeGames.TagLayerTypeGenerator.Editor.Sync;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditorInternal;
using static System.String;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor
{
    /// <summary>Generates a file containing a type; which contains constant string definitions for each Tag in the project.</summary>
    public sealed class TagTypeGenerator : TypeGenerator<TagTypeGenerator>
    {
        /// <inheritdoc />
        private TagTypeGenerator([NotNull] TypeGeneratorSettings.Settings settings, ISync sync) : base(settings, sync)
        {
        }

        /// <summary>Runs when the Editor starts or on a domain reload.</summary>
        [InitializeOnLoadMethod]
        public static void InitializeOnLoad() => new TagTypeGenerator(TypeGeneratorSettings.GetOrCreateSettings.Tag, new TagSync());

        /// <summary>Creates members for each tag in the project and adds them to the <paramref name="typeDeclaration" />.</summary>
        /// <param name="typeDeclaration">The <see cref="CodeTypeDeclaration" /> to add the tag members to.</param>
        protected override void CreateMembers(CodeTypeDeclaration typeDeclaration)
        {
            foreach (string tag in InternalEditorUtility.tags)
            {
                const MemberAttributes attributes = MemberAttributes.Public | MemberAttributes.Const;
                var field = new CodeMemberField(typeof(string), tag.Replace(" ", Empty)) {Attributes = attributes, InitExpression = new CodePrimitiveExpression(tag)};

                ValidateIdentifier(field, tag);

                typeDeclaration.Members.Add(field);
            }

            AddComments(typeDeclaration);
        }

        /// <summary>Adds a verbose comment (like this one) to the type.</summary>
        /// <param name="typeDeclaration">The <see cref="CodeTypeDeclaration" /> to add the comment to.</param>
        private void AddComments([NotNull] CodeTypeMember typeDeclaration)
        {
            var commentStatement =
                new CodeCommentStatement(
                    "<summary>\r\n Use these string constants when comparing tags in code / scripts.\r\n </summary>\r\n <example>\r\n <code>\r\n if " +
                    $"(other.gameObject.CompareTag({Settings.TypeName}.Player)) {{\r\n     Destroy(other.gameObject);\r\n }}\r\n </code>\r\n </example>",
                    true);

            typeDeclaration.Comments.Add(commentStatement);
        }
    }
}