# Tag / Layer Type Generator

## What is the Tag / Layer Type Generator?

The Tag / Layer Type Generator automatically generates types for the [Tags and Layers](https://docs.unity3d.com/Manual/class-TagManager.html) defined in your Unity project. A
developer can then use these types to ensure that at compile-time, any tags or layers used during runtime will be correct. Using tags and layers in this way ensures that should a
tag be changed or a layer ID updated, the .NET compiler will flag this as a compilation error long before bugs appear at runtime, which is often a symptom of using
so-called '[magic strings](https://en.wikipedia.org/wiki/Magic_string)'.

## Why use strongly typed Tags and Layers?

Using statically typed tags and layers in your projects code provides compile-time checking of the tag and layer values. However, that's not the only advantage. Another use for the
generated LayerMasks enum is as a property of any MonoBehaviour or ScriptableObject, allowing you to specify multiple Layers as a property directly on gameObject. This can be
useful if you want to define in the editor what layers to check for a collision or raycast against (or anywhere that Unity accepts
a [LayerMask](https://docs.unity3d.com/ScriptReference/LayerMask.html)). See examples below for more details.

## Whats wrong with [UnityEngine.LayerMask](https://docs.unity3d.com/ScriptReference/LayerMask.html)?

Unity's LayerMask struct provides methods for converting a layer name from a *string* into its corresponding ID or LayerMask bitmask (and conversely a layer ID back into it's
string name). However this requires you specify the string in your code which is prone to causing errors at runtime should a layer name be changed (the same applies to tags, too).
You could mitigate this by defining a class with the LayerNames as strings and refer to that everywhere in your code, but then you lose the benefit of being able to use the
generated LayerMasks Enum as a property in your MonoBehaviours or ScriptableObjects. It's also not a great developer experience having to call LayerMask.GetMask("LayerName"), when
a constantly defined type and value would be sufficient less prone to bugs.

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

### Layer Masks

#### As property of GameObjects

```c#
public sealed class Bullet : MonoBehaviour
{
    /// Set which layers you want to collide with. The inspector will show a multi-select dropdown of the layers.
    [SerializedField] private LayerMasks _collideAgainst;

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, 20.0f, (int) _collideAgainst))
        {
            OnCollision();
        }
    }
 }
```

#### Used for assignment

```c#
public class ExampleClass : MonoBehaviour
{
    [SerializedField] private Camera _mainCamera;

    void Start()
    {
        // Only render objects on the default layer.
        _mainCamera.cullingMask = (int) Layer.Default;
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

## LayerMasks finer details

The Layer Type Generator creates two [Enums](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum) inside a single generated file. One is the layer
identifier. The other is a LayerMasks Enum which can be used to combine multiple layers together to create a LayerMask. Due to the usage of
the [Flags](https://docs.microsoft.com/en-us/dotnet/api/system.flagsattribute?view=netstandard-2.0) attribute, it becomes very straight forward to check for multiple layers
in [Physics.Raycast](https://docs.unity3d.com/ScriptReference/Physics.Raycast.html) calls (for example) and can be used as a property on a gameObject, adjustable right in the
inspector!

The only downside to using Enum's for layers is the required explicit cast of the Enum to an int. This has little-to-no performance impact, whilst I gaining all of benefits
mentioned above, such as the compile-time error checking and access as a property in the inspector.

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