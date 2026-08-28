# BlendShape Animation Finder

面向 VRChat 的工具：查找项目中所有修改某个 BlendShape 的 `AnimationClip`，然后把它们的所有关键帧批量改成同一个值。

支持 **[English](README.md) / [中文](README.zh.md) / [日本語](README.ja.md)**。

## 使用

打开 **Tools → BlendShape Animation Finder**：

1. 拖入一个带 BlendShape 的 `SkinnedMeshRenderer`。
2. 从下拉框选择一个 BlendShape（可搜索）。
3. 点击 **搜索修改此 Blend Shape 的动画**。
4. 勾选要修改的动画（可用`全选`/`反选`），输入数值，点击
   **修改选中的关键帧**——或 **修改全部关键帧** 修改全部结果。
5. 随时用 **Ctrl+Z** 撤销。

## 安装

### 1. VCC（VRChat Creator Companion）——VRChat 推荐

点击下方按钮，打开 VCC 并添加此仓库，然后在包列表中安装。

[➕ Add to VCC](https://xuxian-pw.github.io/BlendShapeAnimationFinder/)

或者手动添加仓库：**Settings → Packages → Add Repository**，粘贴：

```
https://raw.githubusercontent.com/xuxian-pw/BlendShapeAnimationFinder/main/vpm.json
```

### 2. Unity 包（`.unitypackage`）

1. 打开 [Releases](https://github.com/xuxian-pw/BlendShapeAnimationFinder/releases) 页面。
2. 下载 `BlendShapeAnimationFinder.unitypackage`。
3. 双击导入（或用 **Assets → Import Package → Custom Package…**）。

工具会出现在菜单 **Tools → BlendShape Animation Finder**。

## 功能

- 拖入任意 `SkinnedMeshRenderer`，从**可搜索**的下拉框中选择一个 BlendShape。
- 扫描项目中的所有 `AnimationClip`，找出修改该 BlendShape 的曲线。
  采用精确的 `blendShape.<name>` 匹配，`Smile` 绝不会误匹配 `SmileBig`。
- **批量修改**——把选中（或全部）结果的所有关键帧改成同一个值，同时保留时间和切线。
- **支持撤销**——整批操作是一个 Ctrl+Z 即可撤销的 Undo 组。

## **Translation**
  * **\[10]** — Japanese `README.md` translation

## 许可证

[AGPL-3.0-or-later](LICENSE) © XuXian
