# BlendShape Animation Finder

VRChat向けのツール。プロジェクト内にある指定のBlendShapeを変更している `AnimationClip` を全検索し、その全キーフレームの値を一括で指定した数値に変更できます

対応言語: **[English](README.md)** **/** **[中文](README.zh.md)** **/** **[日本語](README.ja.md)**。

## 使い方

`Tools` → `BlendShape Animation Finder` からウィンドウを開きます。

1. BlendShapeを持つ `SkinnedMeshRenderer` をドラッグ＆ドロップします。
2. ドロップダウン（検索可能）から変更したいBlendShapeを選びます。
3. **「このBlendShapeを変更しているアニメーションを検索」** をクリックします。
4. 変更したいアニメーションにチェックを入れ（全選択・反選も可能）、設定したい数値を入力したら **「選択したキーフレームを変更」** をクリックします。（すべての結果を一括変更したい場合は **「すべてのキーフレームを変更」** を押します）
5. 間違えても `Ctrl+Z` でいつでも元に戻せます。

## **インストール方法**

### 1. VCC（VRChat Creator Companion）

下のボタンをクリックして VCC を開き、このリポジトリを追加してからパッケージ一覧で
インストールします。

[➕ Add to VCC](https://xuxian-pw.github.io/BlendShapeAnimationFinder/)

または手動で **Settings → Packages → Add Repository** に以下を貼り付け：

```
https://raw.githubusercontent.com/xuxian-pw/BlendShapeAnimationFinder/main/vpm.json
```

### 2. Unity パッケージ（`.unitypackage`）

1. [Releases](https://github.com/xuxian-pw/BlendShapeAnimationFinder/releases) ページを開く。
2. `BlendShapeAnimationFinder.unitypackage` をダウンロードします。
3. ダブルクリックしてインポートします（または Unity 上の `Assets` → `Import Package` → `Custom Package…` から読み込み）。
4. 上部メニューの `Tools` → `BlendShape Animation Finder` からツールを起動できます。

## **主な機能**

* **BlendShapeのスマート検索**：`SkinnedMeshRenderer` をドラッグ＆ドロップするだけで、検索付きドロップダウンからサクッと目的のBlendShapeを選択できます。
* **正確な判定**：プロジェクト内の全AnimationClipをスキャンし、指定のBlendShapeを変更しているカーブを検出します。`blendShape.<name>` による完全一致判定のため、例えば「Smile」を検索した際に「SmileBig」が誤検知される心配はありません。
* **一括数値変更**：選択した（またはすべてのアニメーションの）全キーフレームを一括で指定した値に書き換えます。時間軸やタンジェント（補間情報）はそのまま保持されます。
* **Undo（元に戻す）対応**：一連の一括操作はまとめて1つのUndoグループとして記録されるため、`Ctrl+Z` ワンアクションですぐにやり直せます。
  
## **Translation**
  * **\[10]** — Japanese `README.md` translation
 
 許可証
---

[AGPL-3.0-or-later](LICENSE) © XuXian

