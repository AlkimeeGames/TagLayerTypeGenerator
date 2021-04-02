﻿using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.CSharp;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using static System.String;

namespace AlkimeeGames.TagLayerTypeGenerator.Editor
{
    /// <summary>Generates a file containing a class; which contains constant string definitions for each tag in the project.</summary>
    public sealed class TagTypeGenerator : TypeGenerator<TagTypeGenerator>
    {
        /// <summary>Used to check what tag strings are in the Tag class.</summary>
        private readonly HashSet<string> _inClass = new HashSet<string>();

        /// <summary>Used to check what tag strings are in the project.</summary>
        private readonly HashSet<string> _inUnity = new HashSet<string>();

        /// <summary>The absolute path to the file containing the tags.</summary>
        [NotNull] private static string TagFilePath => $"{Application.dataPath}/{Settings.Tag.FilePath}";

        /// <summary>Used to read the values from the Class. If we don't use reflection to find the Class, we tie ourselves to a specific configuration which isn't ideal.</summary>
        [CanBeNull] private static Type TagType => Type.GetType($"{Settings.Tag.Namespace}.{Settings.Tag.TypeName}, {Settings.Tag.Assembly}");

        /// <summary>Configures the callback for when the editor sends a message the project has changed.</summary>
        [InitializeOnLoadMethod]
        private static void ConfigureCallback()
        {
            Instance = new TagTypeGenerator();
            EditorApplication.projectChanged += Instance.OnProjectChanged;
        }

        /// <summary>If the project has changed, check if I can generate the file and if any tags have been updated.</summary>
        private void OnProjectChanged()
        {
            if (!Settings.Layer.AutoGenerate || !CanGenerate()) return;
            if (File.Exists(TagFilePath) && TypeExists() && !HasChangedTags()) return;

            GenerateFile();
        }

        /// <summary>Checks if the type exists. This will let us know if we can use reflection on it to check for changes in tags.</summary>
        /// <returns>True if the type exists.</returns>
        private bool TypeExists()
        {
            if (TagType != null) return true;

            if (File.Exists(TagFilePath))
                Debug.LogWarning($"{Settings.Tag.Namespace}.{Settings.Tag.TypeName} is missing from {Settings.Tag.Assembly}. " +
                                 $"Check correct {nameof(Settings.Tag.AssemblyDefinition)} is set then regenerate via the Project Settings' menu.", Settings);

            return false;
        }

        /// <summary>Checks if the values defined in the class are the same as in Unity itself.</summary>
        /// <returns>True if the tags in the project don't match the tags in the class.</returns>
        private bool HasChangedTags()
        {
            _inUnity.Clear();

            foreach (string tag in InternalEditorUtility.tags)
                _inUnity.Add(tag.Replace(" ", Empty));

            _inClass.Clear();

            FieldInfo[] fields = TagType.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo fieldInfo in fields)
                if (fieldInfo.IsLiteral)
                    _inClass.Add(fieldInfo.Name);

            return !_inClass.SetEquals(_inUnity);
        }

        /// <summary>Validates if we can generate a new tags file.</summary>
        /// <returns><see langword="true" /> if all conditions are met.</returns>
        public override bool CanGenerate()
        {
            if (!Settings.Tag.IsValidTypeName()) return false;
            if (!Settings.Tag.IsValidNamespace()) return false;
            if (!Settings.Tag.IsValidFilePath()) return false;

            return true;
        }

        /// <summary>Generates a new Tags class file.</summary>
        public override void GenerateFile()
        {
            // Start with a compileUnit to create our code and give it an optional namespace.
            var compileUnit = new CodeCompileUnit();
            var codeNamespace = new CodeNamespace(Settings.Tag.Namespace);
            compileUnit.Namespaces.Add(codeNamespace);

            // Validate the namespace.
            ValidateIdentifier(codeNamespace, Settings.Tag.Namespace);

            // Declare a type that is public and sealed.
            var typeDeclaration = new CodeTypeDeclaration(Settings.Tag.TypeName)
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed
            };

            // Validate the type name.
            ValidateIdentifier(typeDeclaration, Settings.Tag.TypeName);

            // Add some comments so the class describes it's intended usage.
            AddComments(typeDeclaration);

            // Create members in the type for each tag in the project.
            CreateTagMembers(typeDeclaration);

            // Add the type declaration to the namespace.
            codeNamespace.Types.Add(typeDeclaration);

            // With a StringWriter and a CSharpCodeProvider; generate the code.

            using (var stringWriter = new StringWriter())
            {
                using (var codeProvider = new CSharpCodeProvider())
                    codeProvider.GenerateCodeFromCompileUnit(compileUnit, stringWriter, new CodeGeneratorOptions
                    {
                        BracingStyle = "C",
                        BlankLinesBetweenMembers = false
                    });

                // Create the asset path if it doesn't already exist.
                CreateAssetPathIfNotExists(TagFilePath);

                // Write the code to the file system and refresh the AssetDatabase.
                File.WriteAllText(TagFilePath, stringWriter.ToString());
            }

            AssetDatabase.Refresh();

            InvokeOnFileGeneration();
        }

        /// <summary>Adds a verbose comment (like this one) to the class.</summary>
        /// <param name="typeDeclaration">The <see cref="CodeTypeDeclaration" /> to add the comment to.</param>
        private void AddComments([NotNull] CodeTypeMember typeDeclaration)
        {
            var commentStatement = new CodeCommentStatement(
                "<summary>\r\n Use these string constants when comparing tags in code / scripts.\r\n </summary>" +
                "\r\n <example>\r\n <code>\r\n if (other.gameObject.CompareTag(Tags.Player)) {\r\n     Destroy(other.gameObject);" +
                "\r\n }\r\n </code>\r\n </example>",
                true);

            typeDeclaration.Comments.Add(commentStatement);
        }

        /// <summary>Creates members for each tag in the project and adds them to the <paramref name="typeDeclaration" />.</summary>
        /// <param name="typeDeclaration">The <see cref="CodeTypeDeclaration" /> to add the tag members to.</param>
        private void CreateTagMembers(CodeTypeDeclaration typeDeclaration)
        {
            foreach (string tag in InternalEditorUtility.tags)
            {
                var field = new CodeMemberField
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Const,
                    Name = tag.Replace(" ", Empty),
                    Type = new CodeTypeReference(typeof(string)),
                    InitExpression = new CodePrimitiveExpression(tag)
                };

                ValidateIdentifier(field, tag);

                typeDeclaration.Members.Add(field);
            }
        }
    }
}