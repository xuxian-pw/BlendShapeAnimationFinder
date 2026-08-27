# BlendShape Animation Finder

一个面向 VRChat 的 Unity **编辑器**工具：查找项目中所有修改某个 BlendShape 的
`AnimationClip`，然后把它们的所有关键帧批量改成同一个值——支持完整的 **Ctrl+Z** 撤销。

支持 **[English](README.md) / [中文](README.zh.md) / [日本語](README.ja.md)**。

## 功能

- 拖入任意 `SkinnedMeshRenderer`，从**可搜索**的下拉框中选择一个 BlendShape。
- 扫描项目中的所有 `AnimationClip`，找出修改该 BlendShape 的曲线。
  采用精确的 `blendShape.<name>` 匹配，`Smile` 绝不会误匹配 `SmileBig`。
- **批量修改**——把选中（或全部）结果的所有关键帧改成同一个值，同时保留时间和切线。
- **支持撤销**——整批操作是一个 Ctrl+Z 即可撤销的 Undo 组。
- 三语界面（EN / ZH / JA），通过 `EditorPrefs` 记住选择。

## 安装

选择以下**一种**方式。

### 1. VCC（VRChat Creator Companion）——VRChat 推荐

点击下方按钮，打开 VCC 并添加此仓库，然后在包列表中安装。

[➕ Add to VCC](https://xuxian-pw.github.io/BlendShapeAnimationFinder/)

> GitHub 不会把 `vcc://` 链接渲染成可点击的，所以按钮会先打开一个跳转页再唤起 VCC。
> 如果没反应，把下面这行复制到浏览器地址栏回车：

```
vcc://vpm/addRepo?url=https%3A%2F%2Fraw.githubusercontent.com%2Fxuxian-pw%2FBlendShapeAnimationFinder%2Fmain%2Fvpm.json
```

或者手动添加仓库：**Settings → Packages → Add Repository**，粘贴：

```
https://raw.githubusercontent.com/xuxian-pw/BlendShapeAnimationFinder/main/vpm.json
```

> 包以 `vpm.json` 中列出的 zip 形式分发（带 `zipSHA256` 校验）。zip 放在 `dist/`，
> 因此必须提交到仓库。

### 2. Unity 包（`.unitypackage`）

1. 打开 [Releases](https://github.com/xuxian-pw/BlendShapeAnimationFinder/releases) 页面。
2. 下载 `BlendShapeAnimationFinder.unitypackage`。
3. 双击导入（或用 **Assets → Import Package → Custom Package…**）。

工具会出现在菜单 **Tools → BlendShape Animation Finder**。

## 使用

打开 **Tools → BlendShape Animation Finder**：

1. 拖入一个带 BlendShape 的 `SkinnedMeshRenderer`。
2. 从下拉框选择一个 BlendShape（可搜索）。
3. 点击 **Search animations using this BlendShape**。
4. 勾选要修改的动画（可用 Select All / Invert），输入数值，点击
   **Modify Selected Keyframes**——或 **Modify All Keyframes** 修改全部结果。
5. 随时用 **Ctrl+Z** 撤销。

## 发布新版本

1. 修改 `Packages/com.xuxian-pw.blendshape-animation-finder/package.json`、`vpm.json` 和
   `CHANGELOG.md` 中的 `version`。
2. 重新打包 `.zip`（见下），并更新 `vpm.json` 中的 `url` + `zipSHA256`。
3. 提交，然后打 tag 并推送：

```bash
git tag v1.0.0
git push origin main --tags
```

4. 重新构建 `.unitypackage`（见下）并上传到新的 GitHub Release。

## 构建 `.zip`

`dist/` 里的 `.zip` 是 VCC/ALCOM 安装用的。重新构建时，把
`Packages/com.xuxian-pw.blendshape-animation-finder/` 的**内容**（让 `package.json`
位于 zip 根目录）打包成 `com.xuxian-pw.blendshape-animation-finder-<version>.zip`，
然后把 `vpm.json` 里的 `zipSHA256` 设为该文件的 SHA-256（小写十六进制）。

## 构建 `.unitypackage`

`.unitypackage` 通过 GitHub Releases 分发。构建方法：

1. 在任意 Unity 项目中新建 `Assets/Editor/` 文件夹。
2. 把仓库里的
   `Packages/com.xuxian-pw.blendshape-animation-finder/Editor/BlendShapeAnimationFinder.cs`
   和
   `Packages/com.xuxian-pw.blendshape-animation-finder/Editor/BlendShapeAnimationFinder.Editor.asmdef`
   复制进去。
3. 在 Project 窗口选中这两个文件（`.meta` 会自动导出）。
4. 右键 → **Export Package…**，取消勾选 **Include dependencies**，保存为
   `BlendShapeAnimationFinder.unitypackage`。

## 许可证

[AGPL-3.0-or-later](LICENSE) © XuXian
