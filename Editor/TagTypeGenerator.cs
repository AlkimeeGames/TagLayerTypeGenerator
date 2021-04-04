using System.CodeDom;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditorInternal;
using static System.String;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor
{
    internal sealed class TagTypeGenerator : TypeGenerator<TagTypeGenerator>
    {
        /// <summary>Checks for updates to the Tags in the Project.</summary>
        private readonly TagUpdatesChecker _tagUpdatesChecker;

        /// <inheritdoc />
        private TagTypeGenerator([NotNull] TypeGeneratorSettings.Settings settings) : base(settings)
        {
            _tagUpdatesChecker = new TagUpdatesChecker();
        }

        /// <summary>Runs when the Editor starts or on a domain reload.</summary>
        [InitializeOnLoadMethod]
        public static void InitializeOnLoad() => new TagTypeGenerator(TypeGeneratorSettings.GetOrCreateSettings.Tag);

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
                    $"<summary>\r\n Use these string constants when comparing tags in code / scripts.\r\n </summary>\r\n <example>\r\n <code>\r\n if (other.gameObject.CompareTag({Settings.TypeName}.Player)) {{\r\n     Destroy(other.gameObject);\r\n }}\r\n </code>\r\n </example>",
                    true);

            typeDeclaration.Comments.Add(commentStatement);
        }

        /// <summary>Are the Tags different to the Tags in the type?</summary>
        /// <returns>True if there are changes to the Tags in the project.</returns>
        protected override bool HasUpdates() => _tagUpdatesChecker.HasUpdates(GeneratingType);
    }
}