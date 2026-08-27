# BlendShape Animation Finder

VRChat 向けのツールです。特定の BlendShape を操作するすべての `AnimationClip` を
プロジェクトから探し出し、そのすべてのキーフレームを一括で同じ値に変更できます。

対応言語: **[English](README.md) / [中文](README.zh.md) / [日本語](README.ja.md)**。

## 使い方

**Tools → BlendShape Animation Finder** を開く：

1. BlendShape を持つ `SkinnedMeshRenderer` をドラッグ。
2. ドロップダウンから BlendShape を選択（検索可能）。
3. **Search animations using this BlendShape** をクリック。
4. 変更したいアニメーションにチェックを入れ（Select All / Invert も利用可）、値を入力し、
   **Modify Selected Keyframes**（または全件なら **Modify All Keyframes**）をクリック。
5. いつでも **Ctrl+Z** で取り消せます。

## インストール

### 1. VCC（VRChat Creator Companion）—— VRChat では推奨

下のボタンをクリックして VCC を開き、このリポジトリを追加してからパッケージ一覧で
インストールします。

[➕ Add to VCC](https://xuxian-pw.github.io/BlendShapeAnimationFinder/)

または手動で **Settings → Packages → Add Repository** に以下を貼り付け：

```
https://raw.githubusercontent.com/xuxian-pw/BlendShapeAnimationFinder/main/vpm.json
```

### 2. Unity パッケージ（`.unitypackage`）

1. [Releases](https://github.com/xuxian-pw/BlendShapeAnimationFinder/releases) ページを開く。
2. `BlendShapeAnimationFinder.unitypackage` をダウンロード。
3. ダブルクリックしてインポート（または **Assets → Import Package → Custom Package…**）。

ツールは **Tools → BlendShape Animation Finder** に追加されます。

## 機能

- `SkinnedMeshRenderer` をドラッグし、**検索可能**なドロップダウンから BlendShape を選択。
- プロジェクト内のすべての `AnimationClip` から、その BlendShape を操作するカーブを検索。
  完全一致の `blendShape.<name>` で判定するため、`Smile` が `SmileBig` に誤って一致しません。
- **一括変更**——選択した（またはすべての）結果のキーフレームを同じ値に変更。時間とタンジェントは保持されます。
- **取り消し対応**——一括操作は 1 つの Ctrl+Z で取り消せる Undo グループです。

## ライセンス

[AGPL-3.0-or-later](LICENSE) © XuXian
