# Tag / Layer Type Generator

## What is Tag / Layer Type Generator?

The Tag / Layer Type Generator automatically generates types / classes of the
[tags and layers](https://docs.unity3d.com/Manual/class-TagManager.html) defined in your Unity project. A developer can then use these types to ensure that at compile-time, any
tags or layers used during runtime will be statically typed. Using tags and layers in this way ensures that should a tag be changed or a layer ID updated, the .NET compiler will
flag this as a compilation error long before bugs appear at runtime when using "[magic strings](https://en.wikipedia.org/wiki/Magic_string)".

## Setup

The Tag / Layer Type generator works out of the box with no dependencies other than Unity itself. Just install it and you're ready to go. It's pure CSharp, fully open-source, and
the public API has complete XML documentation built right in, including the generated CSharp code.

## Generating Types

The Tag / Layer Type generator subscribes to the Unity Editor's
[ProjectChanged](https://docs.unity3d.com/ScriptReference/EditorApplication-projectChanged.html) event. When this event is raised, the generators check to see if any new tags or
layers have been added or previous tags or layers have been updated. A new CSharp file is generated in either case. It's also possible to manually generate the files either via a
button in the Project Settings windows or inspecting the TagLayerSettings asset.

## Configuration

A single configuration file is automatically created and placed into the Asset folder when the package is installed. This configuration file allows you to change the following
parameters of both the Tag and Layer Types:

- Auto Generate (e.g.: Should the Tag or Layer file be automatically generated on changes to the project)
- Type Name (e.g.: Tag, Layer)
- File Path (e.g.: Scripts/Tag.cs, Scripts/Layer.cs)
- Optional: Namespace (e.g.: MyCustomNamespace)
- Optional: AssemblyDefinition (e.g.: MyCustomNamespace.MyCustomAssembly)

## Project Settings Window

![ProjectSettings](https://media.githubusercontent.com/media/AlkimeeGames/TagLayerTypeGenerator/main/Documentation~/ProjectSettings.png)

*Note:* If using [AssemblyDefinitions](https://docs.unity3d.com/Manual/ScriptCompilationAssemblyDefinitionFiles.html) in your project, it's important to ensure that the File Path
and AssemblyDefinition settings are set correctly, as the Generators use reflection on the generated types to determine if new Tags or Layers have been added. It can only perform
this reflection if it knows which assembly the generated types are. You can leave this set to none if AssemblyDefinitions aren't in use in the project.

## Layer Masks

The Layer Type Generator creates two [Enums](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum)
when generating a file. One is the layer ID which is used in Unity to identify a single layer. The other is a LayerMasks Enum which can be used to combine multiple layers together
during checks. Due to the usage of the
[Flags](https://docs.microsoft.com/en-us/dotnet/api/system.flagsattribute?view=netstandard-2.0) attribute, it becomes very straight forward to check for multiple layers
in [Physics.Raycast](https://docs.unity3d.com/ScriptReference/Physics.Raycast.html)
calls, for example.

## API and Extensibility

Please see the [API and Extensibility](https://github.com/AlkimeeGames/TagLayerTypeGenerator/blob/develop/Documentation~/API.md) documentation.

## Installation

### Install via OpenUPM

The package is available on the [openupm registry](https://openupm.com/packages/com.alkimeegames.taglayertypegenerator/). It's recommended to install it
via [openupm-cli](https://github.com/openupm/openupm-cli).

```
openupm add com.alkimeegames.taglayertypegenerator
```

### Install Via Package Manager

Via the Package Manager, you can add the package as a Git dependency. Follow the instructions for
[Installing froma Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html) and further information around
[Git dependencies](https://docs.unity3d.com/Manual/upm-git.html) in Unity. We advise specifically locking to the
[main](https://github.com/AlkimeeGames/TagLayerTypeGenerator/tree/main) branch.

### Install Via Manifest.json

Open *Packages/manifest.json* with your favorite text editor. Add the following line to the dependencies block.

    {
        "dependencies": {
            "com.alkimeegames.taglayertypegenerator": "https://github.com/AlkimeeGames/TagLayerTypeGenerator.git#main"
        }
    }

#### Git Updates

The Unity Package Manager records the current commit to a lock entry of the *manifest.json*. To update to the latest version, change the hash value manually or remove the lock
entry to re-resolve the package.

    "lock": {
      "com.alkimeegames.taglayertypegenerator": {
        "revision": "main",
        "hash": "..."
      }
    }

### Examples Of Generated Files

#### Tags

```c#
namespace Alkimee
{
    /// <summary>
    /// Use these string constants when comparing tags in code / scripts.
    /// </summary>
    /// <example>
    /// <code>
    /// if (other.gameObject.CompareTag(Tag.Player)) {
    ///     Destroy(other.gameObject);
    /// }
    /// </code>
    /// </example>
    public sealed class Tag
    {
        public const string Untagged = "Untagged";
        public const string Respawn = "Respawn";
        public const string Finish = "Finish";
        public const string EditorOnly = "EditorOnly";
        public const string MainCamera = "MainCamera";
        public const string Player = "Player";
        public const string GameController = "GameController";
        public const string Collectable = "Collectable";
        public const string Projectile = "Projectile";
    }
}
```

#### Layers

```c#
namespace Alkimee
{
    using System;

    /// <summary>
    /// Use this enum in place of layer names in code / scripts.
    /// </summary>
    /// <example>
    /// <code>
    /// if (other.gameObject.layer == Layer.Collectable) {
    ///     Destroy(other.gameObject);
    /// }
    /// </code>
    /// </example>
    public enum Layer
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,
        Collectable = 6,
    }
    /// <summary>
    /// Use this enum in place of layer mask values in code / scripts.
    /// </summary>
    /// <example>
    /// <code>
    /// if (Physics.Raycast(t.position, t.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, (int) (LayerMasks.Collectable | LayerMasks.Water)) {
    ///     Debug.Log("Did Hit");
    /// }
    /// </code>
    /// </example>
    [FlagsAttribute()]
    public enum LayerMasks
    {
        Default = 1,
        TransparentFX = 2,
        IgnoreRaycast = 4,
        Water = 16,
        UI = 32,
        Collectable = 64,
    }
}

```