# Changelog

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/) and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [3.0.1] - 2021-04-05

- Actually make public things public.

## [3.0.0] - 2021-04-05

- Stabilized Public API.

## [2.0.3] - 2021-04-04

- Fix stray import statement.

## [2.0.2] - 2021-04-04

- Further refactoring, along with some new public interfaces for further extension.

## [2.0.1] - 2021-04-04

- Massive refactoring. Code should be a little easier to navigate.

## [2.0.0] - 2021-04-03

- Generate types for both tags and layers (layers were previous enums).
- Introduced TagAttribute and LayerAttribute which can be applied to any 'int' or 'string' property.

## [1.0.7] - 2021-04-02

- Improved validation.
- Fixes regression in nested namespaces.

## [1.0.6] - 2021-04-02

- Fixed bug in layer names with spaces.
- Fixed settings getting "stuck".
- Show less warnings during normal operation.

## [1.0.5] - 2021-04-02

- Validate earlier that settings will not create invalid identifiers in the generated code.

## [1.0.4] - 2021-04-01

- Moved Project Settings path to 'Project/Tags and Layers/Automatic Type Generation'.

## [1.0.3] - 2021-04-01

- Moved Project Settings path to 'Alkimee Games'.
- Cleaned up documentation.

## [1.0.2] - 2021-04-01

- Added example use cases of both the generated Tag and Layer types.

## [1.0.1] - 2021-04-01

- Bumped minimum required Unity version to 2018.4.33f1.
- Added package to [OpenUPM](https://openupm.com/packages/com.alkimeegames.taglayertypegenerator/).
- Added [documentation](https://github.com/AlkimeeGames/TagLayerTypeGenerator/blob/develop/Documentation~/API.md) on how to extend the generator classes.
- Fixed C# 6 compatibility for older Unity versions.
- Fixed compatibility with experimental version of UnityEngine.UIElements.
- Fixed Assembly Definition referencing itself.

## [1.0.0] - 2021-03-31

- Initial release.