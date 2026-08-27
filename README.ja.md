# BlendShape Animation Finder

VRChat 向けの Unity **エディタ**ツールです。特定の BlendShape を操作するすべての
`AnimationClip` をプロジェクトから探し出し、そのすべてのキーフレームを一括で同じ値に
変更できます。**Ctrl+Z** による取り消しにも対応しています。

対応言語: **[English](README.md) / [中文](README.zh.md) / [日本語](README.ja.md)**。

## 機能

- `SkinnedMeshRenderer` をドラッグし、**検索可能**なドロップダウンから BlendShape を選択。
- プロジェクト内のすべての `AnimationClip` から、その BlendShape を操作するカーブを検索。
  完全一致の `blendShape.<name>` で判定するため、`Smile` が `SmileBig` に誤って一致しません。
- **一括変更**——選択した（またはすべての）結果のキーフレームを同じ値に変更。時間とタンジェントは保持されます。
- **取り消し対応**——一括操作は 1 つの Ctrl+Z で取り消せる Undo グループです。
- 3 言語 UI（EN / ZH / JA）。選択は `EditorPrefs` に記憶されます。

## インストール

以下のいずれか**1 つ**の方法で。

### 1. VCC（VRChat Creator Companion）—— VRChat では推奨

下のボタンをクリックして VCC を開き、このリポジトリを追加してからパッケージ一覧で
インストールします。

[➕ Add to VCC](https://xuxian-pw.github.io/BlendShapeAnimationFinder/)

> GitHub は `vcc://` リンクをクリック可能にしないため、ボタンは一度リダイレクトページを
> 開いてから VCC を起動します。反応しない場合は、以下をブラウザのアドレスバーに貼り付けて
> Enter を押してください：

```
vcc://vpm/addRepo?url=https%3A%2F%2Fraw.githubusercontent.com%2Fxuxian-pw%2FBlendShapeAnimationFinder%2Fmain%2Fvpm.json
```

または手動で **Settings → Packages → Add Repository** に以下を貼り付け：

```
https://raw.githubusercontent.com/xuxian-pw/BlendShapeAnimationFinder/main/vpm.json
```

> パッケージは `vpm.json` に記載された zip（`zipSHA256` 付き）として配布されます。
> zip は `dist/` にあるため、リポジトリにコミットする必要があります。

### 2. Unity パッケージ（`.unitypackage`）

1. [Releases](https://github.com/xuxian-pw/BlendShapeAnimationFinder/releases) ページを開く。
2. `BlendShapeAnimationFinder.unitypackage` をダウンロード。
3. ダブルクリックしてインポート（または **Assets → Import Package → Custom Package…**）。

ツールは **Tools → BlendShape Animation Finder** に追加されます。

## 使い方

**Tools → BlendShape Animation Finder** を開く：

1. BlendShape を持つ `SkinnedMeshRenderer` をドラッグ。
2. ドロップダウンから BlendShape を選択（検索可能）。
3. **Search animations using this BlendShape** をクリック。
4. 変更したいアニメーションにチェックを入れ（Select All / Invert も利用可）、値を入力し、
   **Modify Selected Keyframes**（または全件なら **Modify All Keyframes**）をクリック。
5. いつでも **Ctrl+Z** で取り消せます。

## 新バージョンのリリース

1. `Packages/com.xuxian-pw.blendshape-animation-finder/package.json`、`vpm.json`、
   `CHANGELOG.md` の `version` を更新。
2. `.zip` を再ビルド（下記）し、`vpm.json` の `url` と `zipSHA256` を更新。
3. コミットして、タグを打ってプッシュ：

```bash
git tag v1.0.0
git push origin main --tags
```

4. `.unitypackage` を再ビルド（下記）して新しい GitHub Release に添付。

## `.zip` のビルド

`dist/` の `.zip` が VCC/ALCOM のインストールに使われます。再ビルドするには、
`Packages/com.xuxian-pw.blendshape-animation-finder/` の**中身**（`package.json` が
zip のルートに来るように）を `com.xuxian-pw.blendshape-animation-finder-<version>.zip`
として圧縮し、`vpm.json` の `zipSHA256` をそのファイルの SHA-256（小文字 hex）に設定します。

## `.unitypackage` のビルド

`.unitypackage` は GitHub Releases で配布します。ビルド手順：

1. 任意の Unity プロジェクトに `Assets/Editor/` フォルダを作成。
2. リポジトリの
   `Packages/com.xuxian-pw.blendshape-animation-finder/Editor/BlendShapeAnimationFinder.cs`
   と
   `Packages/com.xuxian-pw.blendshape-animation-finder/Editor/BlendShapeAnimationFinder.Editor.asmdef`
   をコピー。
3. Project ウィンドウでこの 2 ファイルを選択（`.meta` は自動的にエクスポートされます）。
4. 右クリック → **Export Package…**、**Include dependencies** のチェックを外し、
   `BlendShapeAnimationFinder.unitypackage` として保存。

## ライセンス

[AGPL-3.0-or-later](LICENSE) © XuXian
