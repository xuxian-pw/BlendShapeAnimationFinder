# BlendShape Animation Finder

A Unity **Editor** tool for VRChat avatar creators: find every `AnimationClip` in your
project that animates a specific BlendShape, then batch-set all of its keyframes to a
single value — with full **Ctrl+Z** undo support.

The UI is localized in **English / 中文 / 日本語**.

## Features

- Drag in any `SkinnedMeshRenderer` and pick a BlendShape from a **searchable** dropdown.
- Scans every `AnimationClip` in the project for curves targeting that BlendShape.
  Matching is an exact `blendShape.<name>` comparison, so `Smile` never matches
  `SmileBig`.
- **Batch edit** — set every keyframe to one value, for the selected clips or all clips
  at once, while preserving timing and tangents.
- **Undo-friendly** — the whole batch is a single Ctrl+Z-able Undo group.
- Trilingual UI (EN / ZH / JA), remembered across sessions via `EditorPrefs`.

## Installation

Choose **one** of the methods below.

### 1. VCC (VRChat Creator Companion) — recommended for VRChat

Click to add this repository to VCC, then install the package from
**Projects → Manage Project → Packages**.

[➕ Add to VCC](vcc://vpm/addRepo?url=https%3A%2F%2Fraw.githubusercontent.com%2Fxuxian-pw%2FBlendShapeAnimationFinder%2Fmain%2Fvpm.json)

If the button does not open VCC, copy this URL into
**Settings → Packages → Add Repository**:

```
https://raw.githubusercontent.com/xuxian-pw/BlendShapeAnimationFinder/main/vpm.json
```

> VCC installs this package by cloning the git tag `v1.0.0`, so the tag must exist in
> the repository before the VCC install link works.

### 2. Unity Package Manager (UPM) — Git URL

1. In Unity, open **Window → Package Manager**.
2. Click **+ → Add package from git URL…**.
3. Paste:

```
https://github.com/xuxian-pw/BlendShapeAnimationFinder.git
```

To pin a specific version, append the tag:

```
https://github.com/xuxian-pw/BlendShapeAnimationFinder.git#v1.0.0
```

### 3. Unity Package (`.unitypackage`)

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

1. Bump `version` in `package.json`, `vpm.json` and `CHANGELOG.md`.
2. Commit, then tag and push:

```bash
git tag v1.0.0
git push origin main --tags
```

3. Rebuild the `.unitypackage` (see below) and attach it to a new GitHub Release.

## Building the `.unitypackage`

The `.unitypackage` is distributed via GitHub Releases. To (re)build it:

1. Create a folder `Assets/Editor/` in any Unity project.
2. Copy `Editor/BlendShapeAnimationFinder.cs` and
   `Editor/BlendShapeAnimationFinder.Editor.asmdef` from this repository into it.
3. In the Project window, select both files (their `.meta` files are exported
   automatically).
4. Right-click → **Export Package…**, untick **Include dependencies**, and save as
   `BlendShapeAnimationFinder.unitypackage`.

## License

[AGPL-3.0-or-later](LICENSE) © XuXian
