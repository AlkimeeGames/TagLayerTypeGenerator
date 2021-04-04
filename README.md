# Tag / Layer Type Generator

[![OpenUPM](https://img.shields.io/npm/v/com.alkimeegames.taglayertypegenerator?label=OpenUPM&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.alkimeegames.taglayertypegenerator/)
[![GitHub Release Date](https://img.shields.io/github/release-date/AlkimeeGames/TagLayerTypeGenerator)](https://github.com/AlkimeeGames/TagLayerTypeGenerator/releases)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-green.svg)](http://makeapullrequest.com)
[![Unity 2018.4 or Layer](https://img.shields.io/badge/unity-2018.4%20or%20later-green.svg)](https://unity3d.com/unity/qa/lts-releases)
[![MIT License](https://img.shields.io/github/license/AlkimeeGames/TagLayerTypeGenerator)](https://github.com/AlkimeeGames/TagLayerTypeGenerator/blob/main/LICENSE.md)
[![](https://img.shields.io/badge/Keep%20a%20Changelog-v1.0.0-green.svg?logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIGZpbGw9IiNmMTVkMzAiIHZpZXdCb3g9IjAgMCAxODcgMTg1Ij48cGF0aCBkPSJNNjIgN2MtMTUgMy0yOCAxMC0zNyAyMmExMjIgMTIyIDAgMDAtMTggOTEgNzQgNzQgMCAwMDE2IDM4YzYgOSAxNCAxNSAyNCAxOGE4OSA4OSAwIDAwMjQgNCA0NSA0NSAwIDAwNiAwbDMtMSAxMy0xYTE1OCAxNTggMCAwMDU1LTE3IDYzIDYzIDAgMDAzNS01MiAzNCAzNCAwIDAwLTEtNWMtMy0xOC05LTMzLTE5LTQ3LTEyLTE3LTI0LTI4LTM4LTM3QTg1IDg1IDAgMDA2MiA3em0zMCA4YzIwIDQgMzggMTQgNTMgMzEgMTcgMTggMjYgMzcgMjkgNTh2MTJjLTMgMTctMTMgMzAtMjggMzhhMTU1IDE1NSAwIDAxLTUzIDE2bC0xMyAyaC0xYTUxIDUxIDAgMDEtMTItMWwtMTctMmMtMTMtNC0yMy0xMi0yOS0yNy01LTEyLTgtMjQtOC0zOWExMzMgMTMzIDAgMDE4LTUwYzUtMTMgMTEtMjYgMjYtMzMgMTQtNyAyOS05IDQ1LTV6TTQwIDQ1YTk0IDk0IDAgMDAtMTcgNTQgNzUgNzUgMCAwMDYgMzJjOCAxOSAyMiAzMSA0MiAzMiAyMSAyIDQxLTIgNjAtMTRhNjAgNjAgMCAwMDIxLTE5IDUzIDUzIDAgMDA5LTI5YzAtMTYtOC0zMy0yMy01MWE0NyA0NyAwIDAwLTUtNWMtMjMtMjAtNDUtMjYtNjctMTgtMTIgNC0yMCA5LTI2IDE4em0xMDggNzZhNTAgNTAgMCAwMS0yMSAyMmMtMTcgOS0zMiAxMy00OCAxMy0xMSAwLTIxLTMtMzAtOS01LTMtOS05LTEzLTE2YTgxIDgxIDAgMDEtNi0zMiA5NCA5NCAwIDAxOC0zNSA5MCA5MCAwIDAxNi0xMmwxLTJjNS05IDEzLTEzIDIzLTE2IDE2LTUgMzItMyA1MCA5IDEzIDggMjMgMjAgMzAgMzYgNyAxNSA3IDI5IDAgNDJ6bS00My03M2MtMTctOC0zMy02LTQ2IDUtMTAgOC0xNiAyMC0xOSAzN2E1NCA1NCAwIDAwNSAzNGM3IDE1IDIwIDIzIDM3IDIyIDIyLTEgMzgtOSA0OC0yNGE0MSA0MSAwIDAwOC0yNCA0MyA0MyAwIDAwLTEtMTJjLTYtMTgtMTYtMzEtMzItMzh6bS0yMyA5MWgtMWMtNyAwLTE0LTItMjEtN2EyNyAyNyAwIDAxLTEwLTEzIDU3IDU3IDAgMDEtNC0yMCA2MyA2MyAwIDAxNi0yNWM1LTEyIDEyLTE5IDI0LTIxIDktMyAxOC0yIDI3IDIgMTQgNiAyMyAxOCAyNyAzM3MtMiAzMS0xNiA0MGMtMTEgOC0yMSAxMS0zMiAxMXptMS0zNHYxNGgtOFY2OGg4djI4bDEwLTEwaDExbC0xNCAxNSAxNyAxOEg5NnoiLz48L3N2Zz4K)](https://github.com/AlkimeeGames/TagLayerTypeGenerator/blob/develop/CHANGELOG.md)
[![GitHub Org's Stars](https://img.shields.io/github/stars/alkimeegames?style=social)](https://github.com/AlkimeeGames)

> Generates types for Tags and Layers in Unity projects - **automatically**, with no manual button pushes required. **Simply set and forget!**

## What is the Tag / Layer Type Generator?

The Tag / Layer Type Generator automatically generates types for the [Tags and Layers](https://docs.unity3d.com/Manual/class-TagManager.html) defined in your Unity project. A
developer can then use these types to ensure that at compile-time, any tags or layers used during runtime will be correct. Using tags and layers in this way ensures that should a
tag or layer change, the .NET compiler will flag this as a compilation error long before bugs appear at runtime, which is often a symptom of using
so-called '[magic strings](https://en.wikipedia.org/wiki/Magic_string)'.

## Why use typed Tags and Layers?

Using typed tags and layers in your project's code provides compile-time checking of the tag and layer values. Using the included Tag or Layer attributes on a string or int property
respectively also allows you to adjust these values in the inspector. See examples below.

## Whats wrong with [UnityEngine.LayerMask](https://docs.unity3d.com/ScriptReference/LayerMask.html)?

Nothing! The generated types are designed to augment the usage of Unity's LayerMask struct. The LayerMask struct provides methods for converting a layer from a string into its
corresponding ID (int) and vice versa. However, both approaches require you to use '[magic strings](https://en.wikipedia.org/wiki/Magic_string)' in your code. Using typed values for
both the layer id and the mask allows you to catch errors at compile time. The layer type also contains pre-computer masks for each layer, making it simple to create layer masks from
the types.

## Setup

The Tag / Layer Type Generator works out of the box, automatically and in the background with no dependencies other than Unity itself. It's pure CSharp and the public API has
complete XML documentation including the generated CSharp types!

## How does this work?

The Tag / Layer Type Generator subscribes to the Unity Editor's [ProjectChanged](https://docs.unity3d.com/ScriptReference/EditorApplication-projectChanged.html) event. When this
event is raised, the generators check to see if any new tags or layers have been added or previous tags or layers have been updated. A new CSharp file is generated in either case.
It's also possible to manually generate the files either via a button in the Project Settings windows or inspecting the TypeGeneratorSttings asset.

## Example usage of the generated types

### Tags

```c#
public sealed class Bullet : MonoBehaviour
{
    private void OnCollisionEnter([Collision other)
    {
        // Used in CampareTag.
        if (other.gameObject.CompareTag(Tag.Player)) {
            Destroy(other.gameObject);
        }
    }
 }
```

### Layers

```c#
public sealed class TransparentFX : MonoBehaviour
{
    private void Awake()
    {
        // Use the Layer ID for TransparentFX.
        gameObject.layer = Layer.TransparentFX;
    }
 }
```

### Layer Masks

```c#
public class ExampleClass : MonoBehaviour
{
    [SerializedField] private Camera _mainCamera;

    void Start()
    {
        // Only render objects on the default layer.
        _mainCamera.cullingMask = Layer.Mask.Default;
    }

    private void FixedUpdate()
    {
        // Collide against Water and Walls using their masks.
        if (Physics.Raycast(transform.position, transform.forward, 20.0f, Layer.Mask.Water | Layer.Mask.Walls))
        {
            OnCollision();
        }
    }
}
```

## Attributes

If you want to use Tags or Layers as properties in a MonoBehaviour or ScriptableObject you can use the attributes provided. This will display a Tag or Layer field in the inspector.

```c#
public class ExampleClass : MonoBehaviour
{
    [Tag] public string tag;
    [Layer] public int layer;

    // Unity already provides an inspecor for the UnityEngine.LayerMask struct.
    public LayerMask layerMask;

    private void OnCollisionEnter([Collision other)
    {
        // You can safely use the tag in CampareTag checks.
        if (other.gameObject.CompareTag(tag)) {
            Destroy(other.gameObject);
        }

        // ... or the layer ID
        other.layer = layer;
    }
}
```

## Configuration

A single configuration file is automatically created and placed into the Asset folder if one is not already available. This configuration file allows you to change the following
parameters of both the Tag and Layer Types:

- Auto Generate (e.g.: Should the Tag or Layer file be automatically generated on changes to the project)
- Type Name (e.g.: Tag, Layer)
- File Path (e.g.: Scripts/Tag.cs, Scripts/Layer.cs)
- Optional: Namespace (e.g.: MyCustomNamespace)
- Optional: AssemblyDefinition (e.g.: MyCustomNamespace.MyCustomAssembly)

## Project Settings Window

![ProjectSettings](https://media.githubusercontent.com/media/AlkimeeGames/TagLayerTypeGenerator/main/Documentation~/ProjectSettings.png)

*Note:* If [Assembly Definitions](https://docs.unity3d.com/Manual/ScriptCompilationAssemblyDefinitionFiles.html) are used in your project, it's important to ensure that the File
Path and AssemblyDefinition settings are set correctly, as the Generators use reflection against generated types to determine if new Tags or Layers have been added. It can only
perform this reflection if it knows which Assembly Definition the generated types are compiled in. You can leave this set to none if Assembly Definitions aren't in use in the
project.

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

## Examples Of Generated Files

#### Tags

```c#
namespace AlkimeeGames.Alkimee
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
namespace AlkimeeGames.Alkimee
{

    /// <summary>
    /// Use this type in place of layer names in code / scripts.
    /// </summary>
    /// <example>
    /// <code>
    /// if (other.gameObject.layer == Layer.Collectable) {
    ///     Destroy(other.gameObject);
    /// }
    /// </code>
    /// </example>
    public sealed class Layer
    {
        public const int Default = 0;
        public const int TransparentFX = 1;
        public const int IgnoreRaycast = 2;
        public const int Water = 4;
        public const int UI = 5;
        public const int Collectable = 6;
        public const int Layer30 = 30;

        /// <summary>
        /// Use this type in place of layer or layer mask values in code / scripts.
        /// </summary>
        /// <example>
        /// <code>
        /// if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, Layer.Mask.Collectable | Layer.Mask.Water) {
        ///     Debug.Log("Did Hit");
        /// }
        /// </code>
        /// </example>
        public sealed class Mask
        {
            public const int Default = 1;
            public const int TransparentFX = 2;
            public const int IgnoreRaycast = 4;
            public const int Water = 16;
            public const int UI = 32;
            public const int Collectable = 64;
            public const int Layer30 = 1073741824;
        }
    }
}

```