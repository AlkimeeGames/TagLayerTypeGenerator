﻿using System;
using System.CodeDom;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using static System.String;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor
{
    /// <summary>Generates a file containing a type; which contains constant int definitions for each Layer in the project.</summary>
    internal sealed class LayerTypeGenerator : TypeGenerator<LayerTypeGenerator>
    {
        /// <summary>Checks for updates to the Layers in the Project.</summary>
        private readonly LayerUpdateChecker _layerUpdateChecker;

        /// <inheritdoc />
        private LayerTypeGenerator([NotNull] TypeGeneratorSettings.Settings settings) : base(settings)
        {
            _layerUpdateChecker = new LayerUpdateChecker();
        }

        /// <summary>Runs when the Editor starts or on a domain reload.</summary>
        [InitializeOnLoadMethod]
        public static void InitializeOnLoad() => new LayerTypeGenerator(TypeGeneratorSettings.GetOrCreateSettings.Layer);

        /// <summary>Are the Layers different to the Layers in the type?</summary>
        /// <returns>True if there are changes to the Layers in the project.</returns>
        protected override bool HasUpdates() => _layerUpdateChecker.HasUpdates(GeneratingType);

        /// <summary>Creates members for each layer in the project and adds them to the <paramref name="layerType" /> along with a nested type called "Mask".</summary>
        /// <param name="layerType">The <see cref="CodeTypeDeclaration" /> to add the layer ID's to.</param>
        protected override void CreateMembers(CodeTypeDeclaration layerType)
        {
            // Make a nested type for the LayerMasks
            var maskType = new CodeTypeDeclaration("Mask") {IsClass = true, TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed};
            layerType.Members.Add(maskType);

            foreach (string layer in InternalEditorUtility.layers)
            {
                if (LayerMask.NameToLayer(layer) == 31)
                    throw new InvalidOperationException("Layer 31 is used internally by the Editor’s Preview window mechanics. To prevent clashes, do not use this layer.");

                string safeName = layer.Replace(" ", Empty);
                const MemberAttributes attributes = MemberAttributes.Public | MemberAttributes.Const;

                AddLayerField(layerType, safeName, attributes, layer);
                AddLayerMaskField(maskType, safeName, attributes, layer);
            }

            AddCommentsToLayerType(layerType);
            AddCommentsToLayerMaskType(maskType);
        }

        private static void AddLayerField([NotNull] CodeTypeDeclaration layerType, [NotNull] string safeName, MemberAttributes attributes, [NotNull] string layer)
        {
            var layerField = new CodeMemberField(typeof(int), safeName) {Attributes = attributes, InitExpression = new CodePrimitiveExpression(LayerMask.NameToLayer(layer))};
            ValidateIdentifier(layerField, layer);

            layerType.Members.Add(layerField);
        }

        private static void AddLayerMaskField([NotNull] CodeTypeDeclaration maskType, [NotNull] string safeName, MemberAttributes attributes, string layer)
        {
            var maskField = new CodeMemberField(typeof(int), safeName) {Attributes = attributes, InitExpression = new CodePrimitiveExpression(LayerMask.GetMask(layer))};
            ValidateIdentifier(maskField, layer);

            maskType.Members.Add(maskField);
        }

        /// <summary>Adds a verbose comment on how to use the Layer enum.</summary>
        /// <param name="typeDeclaration">The <see cref="CodeTypeDeclaration" /> to add the comment to.</param>
        private void AddCommentsToLayerType([NotNull] CodeTypeMember typeDeclaration)
        {
            var commentStatement = new CodeCommentStatement(
                "<summary>\r\n Use this type in place of layer names in code / scripts.\r\n </summary>" +
                $"\r\n <example>\r\n <code>\r\n if (other.gameObject.layer == {Settings.TypeName}.Characters) {{\r\n     Destroy(other.gameObject);" +
                "\r\n }\r\n </code>\r\n </example>",
                true);

            typeDeclaration.Comments.Add(commentStatement);
        }

        /// <summary>Adds a verbose comment on how to use the Layer.Mask type.</summary>
        /// <param name="typeDeclaration">The <see cref="CodeTypeDeclaration" /> to add the comment to.</param>
        private void AddCommentsToLayerMaskType([NotNull] CodeTypeMember typeDeclaration)
        {
            var commentStatement = new CodeCommentStatement(
                "<summary>\r\n Use this type in place of layer or layer mask values in code / scripts.\r\n </summary>\r\n <example>\r\n <code>\r\n if " +
                "(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, " +
                $"{Settings.TypeName}.Mask.Characters | {Settings.TypeName}.Mask.Water) {{\r\n     Debug.Log(\"Did Hit\");\r\n }}\r\n </code>\r\n </example>",
                true);

            typeDeclaration.Comments.Add(commentStatement);
        }
    }
}