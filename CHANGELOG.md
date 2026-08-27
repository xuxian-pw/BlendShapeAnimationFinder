# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2026-08-28

### Added

- Initial release.
- Find every `AnimationClip` that animates a selected BlendShape
  (`blendShape.<name>` exact match).
- Batch-set all keyframes of selected (or all) results to a single value.
- Single Ctrl+Z undo group for the whole batch operation.
- Trilingual UI: English, 中文, 日本語 (remembered via `EditorPrefs`).
- Menu entry: `Tools/BlendShape Animation Finder`.
- Packaging: UPM/VPM `package.json`, VCC listing `vpm.json`, and a
  `.unitypackage` distribution.
