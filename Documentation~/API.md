# API and Extensibility

The public API for the Tag and Layer Generators is very straightforward.

## OnFileGeneration Event

You can subscribe to the FileGeneration event from either TagTypeGenerator.Generator.FileGeneration or LayerTypeGenerator.Generator.FileGeneration from an Editor script. This
event is invoked immediately after the new type file has been stored to the Asset Database. One use of this might be to add further code to the script after the initial type
generation.

```c#
public sealed class MyEditorScript
{
    [InitializeOnLoadMethod]
    private static void RegisterOnFileGenerated()
    {
        TagTypeGenerator.Generator.FileGenerated += OnFileGenerated;
        LayerTypeGenerator.Generator.FileGenerated += OnFileGenerated;
    }

    private static void OnTagFileGenerated() {
        Debug.Log("File Generated");
    }
}
```

## Manually Trigger Generation

You can also trigger a generation of the file at any time from an Editor script using the CanGenerate() and GenerateFile() method.

```c#
public sealed class MyEditorScript
{
    private static void DuringBuild()
    {
        if (TagTypeGenerator.Generator.CanGenerate())
        {
            TagTypeGenerator.Generator.GenerateFile();
        }
    }
}
```

CanGenerate() returns a bool if the settings are correct and will allow for a safe file generation. However, during GenerateFile(), exceptions will be thrown if there are any
errors during the compilation of the code itself.