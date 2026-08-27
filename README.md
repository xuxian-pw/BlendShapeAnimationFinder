# BlendShape Animation Finder

A tool for VRChat avatar : find every `AnimationClip` in your
project that animates a specific BlendShape, then batch-set all of its keyframes to a
single value.

Support **[English](README.md) / [中文](README.zh.md) / [日本語](README.ja.md)**.

## Usage

Open **Tools → BlendShape Animation Finder**:

1. Drag a `SkinnedMeshRenderer` that has BlendShapes into the field.
2. Pick a BlendShape from the dropdown (searchable).
3. Click **Search animations using this BlendShape**.
4. Tick the clips to modify (use Select All / Invert if needed), enter a value, then
   click **Modify Selected Keyframes** — or **Modify All Keyframes** to change every
   result.
5. Undo anytime with **Ctrl+Z**.

## Installation

### 1. VCC (VRChat Creator Companion) — recommended for VRChat

Click the button below to open VCC and add this repository, then install the
package from the package list.

[➕ Add to VCC](https://xuxian-pw.github.io/BlendShapeAnimationFinder/)

Or add the repository manually via **Settings → Packages → Add Repository**:

```
https://raw.githubusercontent.com/xuxian-pw/BlendShapeAnimationFinder/main/vpm.json
```

### 2. Unity Package (`.unitypackage`)

1. Open the [Releases](https://github.com/xuxian-pw/BlendShapeAnimationFinder/releases) page.
2. Download `BlendShapeAnimationFinder.unitypackage`.
3. Double-click it (or use **Assets → Import Package → Custom Package…**) and import.

The tool is added under **Tools → BlendShape Animation Finder**.

## Features

- Drag in any `SkinnedMeshRenderer` and pick a BlendShape from a **searchable** dropdown.
- Scans every `AnimationClip` in the project for curves targeting that BlendShape.
  Matching is an exact `blendShape.<name>` comparison, so `Smile` never matches
  `SmileBig`.
- **Batch edit** — set every keyframe to one value, for the selected clips or all clips
  at once, while preserving timing and tangents.
- **Undo-friendly** — the whole batch is a single Ctrl+Z-able Undo group.

## License

[AGPL-3.0-or-later](LICENSE) © XuXian
