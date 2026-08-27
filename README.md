# BlendShape Animation Finder

A Unity **Editor** tool for VRChat avatars: find every `AnimationClip` in your project
that animates a specific BlendShape, then batch-set all of its keyframes to a single
value — with full **Ctrl+Z** undo support.

Support **[English](README.md) / [中文](README.zh.md) / [日本語](README.ja.md)**.

## Features

- Drag in any `SkinnedMeshRenderer` and pick a BlendShape from a **searchable** dropdown.
- Scans every `AnimationClip` in the project for curves targeting that BlendShape.
  Matching is an exact `blendShape.<name>` comparison, so `Smile` never matches `SmileBig`.
- **Batch edit** — set every keyframe to one value, for the selected clips or all clips
  at once, while preserving timing and tangents.
- **Undo-friendly** — the whole batch is a single Ctrl+Z-able Undo group.
- Trilingual UI (EN / ZH / JA), remembered across sessions via `EditorPrefs`.

## Installation

Choose **one** of the methods below.

### 1. VCC (VRChat Creator Companion) — recommended for VRChat

Click the button below to open VCC and add this repository, then install the package
from the package list.

[➕ Add to VCC](https://xuxian-pw.github.io/BlendShapeAnimationFinder/)

> GitHub does not render `vcc://` links as clickable, so the button opens a tiny
> redirect page that launches VCC. If it doesn't work, copy this into your browser
> address bar and press Enter:

```
vcc://vpm/addRepo?url=https%3A%2F%2Fraw.githubusercontent.com%2Fxuxian-pw%2FBlendShapeAnimationFinder%2Fmain%2Fvpm.json
```

Or add the repository manually via **Settings → Packages → Add Repository**:

```
https://raw.githubusercontent.com/xuxian-pw/BlendShapeAnimationFinder/main/vpm.json
```

> The package is distributed as a zip listed in `vpm.json` (with a `zipSHA256`
> checksum). The zip lives in `dist/`, so it must be committed to the repository.

### 2. Unity Package (`.unitypackage`)

1. Open the [Releases](https://github.com/xuxian-pw/BlendShapeAnimationFinder/releases) page.
2. Download `BlendShapeAnimationFinder.unitypackage`.
3. Double-click it (or use **Assets → Import Package → Custom Package…**) and import.

The tool is added under **Tools → BlendShape Animation Finder**.

## Usage

Open **Tools → BlendShape Animation Finder**:

1. Drag a `SkinnedMeshRenderer` that has BlendShapes into the field.
2. Pick a BlendShape from the dropdown (searchable).
3. Click **Search animations using this BlendShape**.
4. Tick the clips to modify (use Select All / Invert if needed), enter a value, then
   click **Modify Selected Keyframes** — or **Modify All Keyframes** to change every
   result.
5. Undo anytime with **Ctrl+Z**.

## Releasing a new version

1. Bump `version` in `Packages/com.xuxian-pw.blendshape-animation-finder/package.json`,
   `vpm.json` and `CHANGELOG.md`.
2. Rebuild the `.zip` (see below) and update `url` + `zipSHA256` in `vpm.json`.
3. Commit, then tag and push:

```bash
git tag v1.0.0
git push origin main --tags
```

4. Rebuild the `.unitypackage` (see below) and attach it to a new GitHub Release.

## Building the `.zip`

The `.zip` in `dist/` is what VCC/ALCOM installs. To rebuild it, zip the **contents** of
`Packages/com.xuxian-pw.blendshape-animation-finder/` (so `package.json` is at the zip
root) as `com.xuxian-pw.blendshape-animation-finder-<version>.zip`, then set `zipSHA256`
in `vpm.json` to the file's SHA-256 (lowercase hex).

## Building the `.unitypackage`

The `.unitypackage` is distributed via GitHub Releases. To (re)build it:

1. Create a folder `Assets/Editor/` in any Unity project.
2. Copy `Packages/com.xuxian-pw.blendshape-animation-finder/Editor/BlendShapeAnimationFinder.cs`
   and `Packages/com.xuxian-pw.blendshape-animation-finder/Editor/BlendShapeAnimationFinder.Editor.asmdef`
   from this repository into it.
3. In the Project window, select both files (their `.meta` files are exported
   automatically).
4. Right-click → **Export Package…**, untick **Include dependencies**, and save as
   `BlendShapeAnimationFinder.unitypackage`.

## License

[AGPL-3.0-or-later](LICENSE) © XuXian
