using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BlendShapeAnimationFinder : EditorWindow
{
    private SkinnedMeshRenderer targetRenderer;

    private string[] blendShapeNames = Array.Empty<string>();
    private int selectedBlendShape = -1;

    private float batchValue = 0f;

    private Vector2 scrollPosition;
    private Vector2 blendShapeScrollPosition;

    private readonly List<Result> results = new List<Result>();

    private class Result
    {
        public AnimationClip clip;
        public string path;
        public string propertyName;
        public string assetPath;
        public float minValue;
        public float maxValue;
        public float firstValue;
        public float lastValue;
        public bool selected = true;
    }

    // ============================================================
    // 多语言支持 (Localization / 多言語対応)
    // ============================================================

    private enum Lang
    {
        EN = 0,
        ZH = 1,
        JA = 2
    }

    private static readonly string[] LangDisplayNames =
    {
        "English",
        "中文",
        "日本語"
    };

    private const string LangPrefKey = "BlendShapeAnimationFinder.Lang";

    private static Lang currentLang =
        (Lang)EditorPrefs.GetInt(LangPrefKey, (int)Lang.ZH);

    // key -> [EN, ZH, JA]
    private static readonly Dictionary<string, string[]> LocTable =
        new Dictionary<string, string[]>
        {
            ["window_tab_title"] = new[]
            {
                "BlendShape Finder", "BlendShape Finder", "BlendShape Finder"
            },
            ["title"] = new[]
            {
                "BlendShape Animation Finder",
                "BlendShape Animation Finder",
                "BlendShape Animation Finder"
            },
            ["help_intro"] = new[]
            {
                "Drag in a Skinned Mesh Renderer that has BlendShapes, " +
                "then pick a BlendShape from the dropdown.",

                "拖入包含 BlendShape 的 Skinned Mesh Renderer，" +
                "然后从下拉列表选择 BlendShape。",

                "BlendShape を持つ Skinned Mesh Renderer をドラッグしてから、" +
                "ドロップダウンから BlendShape を選択してください。"
            },
            ["warn_no_renderer"] = new[]
            {
                "Please drag in a SkinnedMeshRenderer first.",
                "请先拖入 SkinnedMeshRenderer。",
                "先に SkinnedMeshRenderer をドラッグしてください。"
            },
            ["warn_no_mesh"] = new[]
            {
                "This SkinnedMeshRenderer has no Mesh.",
                "这个 SkinnedMeshRenderer 没有 Mesh。",
                "この SkinnedMeshRenderer には Mesh がありません。"
            },
            ["warn_no_blendshape"] = new[]
            {
                "This Mesh has no BlendShapes.",
                "这个 Mesh 没有 BlendShape。",
                "この Mesh には BlendShape がありません。"
            },
            ["label_blendshape"] = new[]
            {
                "BlendShape", "BlendShape", "BlendShape"
            },
            ["not_selected"] = new[]
            {
                "Not selected", "未选择", "未選択"
            },
            ["btn_search"] = new[]
            {
                "Search animations using this BlendShape",
                "搜索修改此 BlendShape 的动画",
                "この BlendShape を使用するアニメーションを検索"
            },
            ["batch_title"] = new[]
            {
                "Batch Modify", "批量修改", "一括変更"
            },
            ["label_set_to"] = new[]
            {
                "Set to", "修改为", "変更後の値"
            },
            ["batch_help"] = new[]
            {
                "Check the animations below that you want to modify.\n" +
                "You can select them manually, or modify all of them.\n\n" +
                "Ctrl+Z undo is supported.",

                "勾选下面搜索结果中需要修改的动画。\n" +
                "可以手动选择，也可以直接修改全部。\n\n" +
                "支持 Ctrl+Z 撤销。",

                "下の検索結果から変更したいアニメーションにチェックを入れてください。\n" +
                "手動で選択することも、すべてを一括で変更することもできます。\n\n" +
                "Ctrl+Z による取り消しに対応しています。"
            },
            ["btn_select_all"] = new[]
            {
                "Select All", "全选", "すべて選択"
            },
            ["btn_select_none"] = new[]
            {
                "Select None", "全不选", "すべて解除"
            },
            ["btn_invert_selection"] = new[]
            {
                "Invert Selection", "反选", "選択を反転"
            },
            ["selected_count"] = new[]
            {
                "{0} / {1} results selected",
                "已选择 {0} / {1} 个结果",
                "{0} / {1} 件を選択中"
            },
            ["btn_apply_selected"] = new[]
            {
                "Modify Selected Keyframes",
                "修改选中的关键帧",
                "選択したキーフレームを変更"
            },
            ["btn_apply_all"] = new[]
            {
                "Modify All Keyframes",
                "修改全部关键帧",
                "すべてのキーフレームを変更"
            },
            ["results_count"] = new[]
            {
                "Results: {0}",
                "搜索结果：{0}",
                "検索結果：{0}"
            },
            ["result_value"] = new[]
            {
                "BlendShape Value", "BlendShape 值", "BlendShape の値"
            },
            ["result_path"] = new[]
            {
                "GameObject Path", "GameObject Path", "GameObject パス"
            },
            ["result_property"] = new[]
            {
                "Property", "Property", "Property"
            },
            ["result_file"] = new[]
            {
                "File", "文件", "ファイル"
            },
            ["btn_select_anim"] = new[]
            {
                "Select Animation", "选择动画", "アニメーションを選択"
            },
            ["btn_open_anim"] = new[]
            {
                "Open Animation Window", "打开 Animation", "Animation ウィンドウを開く"
            },
            ["selector_title"] = new[]
            {
                "Select BlendShape", "选择 BlendShape", "BlendShape を選択"
            },
            ["selector_header"] = new[]
            {
                "Select a BlendShape ({0} total)",
                "选择 BlendShape（共 {0} 个）",
                "BlendShape を選択（全 {0} 件）"
            },
            ["label_search"] = new[]
            {
                "Search", "搜索", "検索"
            },
            ["frame_info"] = new[]
            {
                "first {0}, last {1}",
                "首帧 {0}，末帧 {1}",
                "先頭 {0}、末尾 {1}"
            },
            ["log_search_result"] = new[]
            {
                "BlendShape Animation Finder: found {1} animation(s) for \"{0}\".",
                "BlendShape Animation Finder: 「{0}」找到 {1} 个动画。",
                "BlendShape Animation Finder: 「{0}」に一致するアニメーションが {1} 件見つかりました。"
            },
            ["warn_no_results"] = new[]
            {
                "No animations to modify were found.",
                "没有找到需要修改的动画。",
                "変更対象のアニメーションが見つかりませんでした。"
            },
            ["dialog_no_selection_title"] = new[]
            {
                "No Animation Selected", "没有选择动画", "アニメーションが選択されていません"
            },
            ["dialog_no_selection_msg"] = new[]
            {
                "Please check at least one search result, or use \"Modify All Keyframes\".",
                "请至少勾选一个搜索结果，或者使用“修改全部关键帧”。",
                "検索結果を1つ以上チェックするか、「すべてのキーフレームを変更」を使用してください。"
            },
            ["dialog_confirm_title"] = new[]
            {
                "Confirm Batch Modify", "确认批量修改", "一括変更の確認"
            },
            ["dialog_confirm_msg"] = new[]
            {
                "Set every keyframe of\n\n\"{1}\"\n\n" +
                "to {2} across {0} animation(s)?\n\n" +
                "This can be undone with Ctrl+Z.",

                "确定要将 {0} 个动画中的\n\n「{1}」\n\n" +
                "所有关键帧修改为 {2} 吗？\n\n" +
                "此操作支持 Ctrl+Z 撤销。",

                "{0} 件のアニメーションに含まれる\n\n「{1}」\n\n" +
                "のすべてのキーフレームを {2} に変更しますか？\n\n" +
                "この操作は Ctrl+Z で取り消せます。"
            },
            ["btn_modify"] = new[]
            {
                "Modify", "修改", "変更"
            },
            ["btn_cancel"] = new[]
            {
                "Cancel", "取消", "キャンセル"
            },
            ["ok"] = new[]
            {
                "OK", "确定", "OK"
            },
            ["log_complete"] = new[]
            {
                "BlendShape batch modification complete.\n" +
                "BlendShape: {0}\n" +
                "Animations: {1}\n" +
                "Curves: {2}\n" +
                "Keyframes: {3}\n" +
                "Value: {4}\n\n" +
                "You can undo this with Ctrl+Z.",

                "BlendShape 批量修改完成。\n" +
                "BlendShape: {0}\n" +
                "动画数量: {1}\n" +
                "曲线数量: {2}\n" +
                "关键帧数量: {3}\n" +
                "修改值: {4}\n\n" +
                "可以使用 Ctrl+Z 撤销。",

                "BlendShape の一括変更が完了しました。\n" +
                "BlendShape: {0}\n" +
                "アニメーション数: {1}\n" +
                "カーブ数: {2}\n" +
                "キーフレーム数: {3}\n" +
                "変更後の値: {4}\n\n" +
                "Ctrl+Z で取り消せます。"
            },
            ["dialog_done_title"] = new[]
            {
                "Modification Complete", "修改完成", "変更が完了しました"
            },
            ["dialog_done_msg"] = new[]
            {
                "BlendShape: {0}\n\n" +
                "Animations: {1}\n" +
                "Curves: {2}\n" +
                "Keyframes: {3}\n\n" +
                "Set to: {4}\n\n" +
                "You can undo this with Ctrl+Z.",

                "BlendShape：{0}\n\n" +
                "动画：{1}\n" +
                "曲线：{2}\n" +
                "关键帧：{3}\n\n" +
                "已修改为：{4}\n\n" +
                "可以使用 Ctrl+Z 撤销。",

                "BlendShape：{0}\n\n" +
                "アニメーション：{1}\n" +
                "カーブ：{2}\n" +
                "キーフレーム：{3}\n\n" +
                "変更後の値：{4}\n\n" +
                "Ctrl+Z で取り消せます。"
            },
        };

    private static string Tr(string key)
    {
        if (LocTable.TryGetValue(key, out string[] arr))
            return arr[(int)currentLang];

        return key;
    }

    private static string Tr(string key, params object[] args)
    {
        return string.Format(Tr(key), args);
    }

    private static void SetLanguage(Lang lang)
    {
        if (currentLang == lang)
            return;

        currentLang = lang;
        EditorPrefs.SetInt(LangPrefKey, (int)lang);
    }

    // ============================================================
    // 打开窗口
    // ============================================================

    [MenuItem("Tools/BlendShape Animation Finder")]
    public static void ShowWindow()
    {
        BlendShapeAnimationFinder window =
            GetWindow<BlendShapeAnimationFinder>(
                Tr("window_tab_title")
            );

        window.minSize = new Vector2(650, 500);
    }

    // ============================================================
    // GUI
    // ============================================================

    private void OnGUI()
    {
        DrawLanguageBar();

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField(
            Tr("title"),
            EditorStyles.boldLabel
        );

        EditorGUILayout.Space(5);

        EditorGUILayout.HelpBox(
            Tr("help_intro"),
            MessageType.Info
        );

        EditorGUILayout.Space(10);

        // ========================================================
        // SkinnedMeshRenderer
        // ========================================================

        EditorGUI.BeginChangeCheck();

        targetRenderer = (SkinnedMeshRenderer)
            EditorGUILayout.ObjectField(
                "Skinned Mesh Renderer",
                targetRenderer,
                typeof(SkinnedMeshRenderer),
                true
            );

        if (EditorGUI.EndChangeCheck())
        {
            LoadBlendShapes();
        }

        // ========================================================
        // 没有 Renderer
        // ========================================================

        if (targetRenderer == null)
        {
            EditorGUILayout.Space(10);

            EditorGUILayout.HelpBox(
                Tr("warn_no_renderer"),
                MessageType.Warning
            );

            return;
        }

        // ========================================================
        // 没有 Mesh
        // ========================================================

        if (targetRenderer.sharedMesh == null)
        {
            EditorGUILayout.Space(10);

            EditorGUILayout.HelpBox(
                Tr("warn_no_mesh"),
                MessageType.Warning
            );

            return;
        }

        // ========================================================
        // 没有 BlendShape
        // ========================================================

        if (blendShapeNames.Length == 0)
        {
            EditorGUILayout.Space(10);

            EditorGUILayout.HelpBox(
                Tr("warn_no_blendshape"),
                MessageType.Warning
            );

            return;
        }

        EditorGUILayout.Space(5);

        // ========================================================
        // BlendShape 选择按钮
        // ========================================================

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField(
            Tr("label_blendshape"),
            GUILayout.Width(100)
        );

        string selectedName =
            selectedBlendShape >= 0 &&
            selectedBlendShape < blendShapeNames.Length
                ? blendShapeNames[selectedBlendShape]
                : Tr("not_selected");

        if (GUILayout.Button(
                selectedName,
                EditorStyles.popup,
                GUILayout.Height(22)))
        {
            ShowBlendShapeSelector();
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        // ========================================================
        // 搜索按钮
        // ========================================================

        if (GUILayout.Button(
                Tr("btn_search"),
                GUILayout.Height(32)))
        {
            Search();
        }

        EditorGUILayout.Space(15);

        // ========================================================
        // 批量修改
        // ========================================================

        DrawBatchModification();

        EditorGUILayout.Space(15);

        // ========================================================
        // 搜索结果
        // ========================================================

        EditorGUILayout.LabelField(
            Tr("results_count", results.Count),
            EditorStyles.boldLabel
        );

        EditorGUILayout.Space(5);

        scrollPosition =
            EditorGUILayout.BeginScrollView(
                scrollPosition
            );

        foreach (Result result in results)
        {
            DrawResult(result);
        }

        EditorGUILayout.EndScrollView();
    }

    // ============================================================
    // 语言切换栏
    // ============================================================

    private void DrawLanguageBar()
    {
        EditorGUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

        EditorGUI.BeginChangeCheck();

        int selected = GUILayout.Toolbar(
            (int)currentLang,
            LangDisplayNames,
            GUILayout.Width(220),
            GUILayout.Height(18)
        );

        if (EditorGUI.EndChangeCheck())
        {
            SetLanguage((Lang)selected);
        }

        EditorGUILayout.EndHorizontal();
    }

    // ============================================================
    // 批量修改 GUI
    // ============================================================

    private void DrawBatchModification()
    {
        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.LabelField(
            Tr("batch_title"),
            EditorStyles.boldLabel
        );

        EditorGUILayout.Space(5);

        batchValue = EditorGUILayout.FloatField(
            Tr("label_set_to"),
            batchValue
        );

        EditorGUILayout.Space(5);

        EditorGUILayout.HelpBox(
            Tr("batch_help"),
            MessageType.None
        );

        EditorGUILayout.Space(5);

        GUI.enabled = results.Count > 0;

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button(Tr("btn_select_all")))
            SetAllResultsSelected(true);

        if (GUILayout.Button(Tr("btn_select_none")))
            SetAllResultsSelected(false);

        if (GUILayout.Button(Tr("btn_invert_selection")))
        {
            foreach (Result result in results)
            {
                if (result != null)
                    result.selected = !result.selected;
            }
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(3);

        int selectedCount = GetSelectedResultCount();

        EditorGUILayout.LabelField(
            Tr("selected_count", selectedCount, results.Count)
        );

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button(
                Tr("btn_apply_selected"),
                GUILayout.Height(30)))
        {
            BatchSetValue(batchValue, false);
        }

        if (GUILayout.Button(
                Tr("btn_apply_all"),
                GUILayout.Height(30)))
        {
            BatchSetValue(batchValue, true);
        }

        EditorGUILayout.EndHorizontal();

        GUI.enabled = true;

        EditorGUILayout.EndVertical();
    }

    // ============================================================
    // 显示搜索结果
    // ============================================================

    private void DrawResult(Result result)
    {
        if (result == null || result.clip == null)
            return;

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.BeginHorizontal();

        result.selected = EditorGUILayout.Toggle(
            result.selected,
            GUILayout.Width(18)
        );

        EditorGUILayout.LabelField(
            result.clip.name,
            EditorStyles.boldLabel
        );

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(2);

        EditorGUILayout.LabelField(
            Tr("result_value"),
            FormatBlendShapeValue(result)
        );

        EditorGUILayout.LabelField(
            Tr("result_path"),
            result.path
        );

        EditorGUILayout.LabelField(
            Tr("result_property"),
            result.propertyName
        );

        EditorGUILayout.LabelField(
            Tr("result_file"),
            result.assetPath
        );

        EditorGUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button(Tr("btn_select_anim")))
        {
            SelectAnimation(result.clip);
        }

        if (GUILayout.Button(Tr("btn_open_anim")))
        {
            SelectAnimation(result.clip);

            EditorApplication.ExecuteMenuItem(
                "Window/Animation/Animation"
            );
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(4);
    }

    // ============================================================
    // BlendShape 选择器
    // ============================================================

    private void ShowBlendShapeSelector()
    {
        if (blendShapeNames == null || blendShapeNames.Length == 0)
            return;

        BlendShapeSelectorWindow.Show(
            this,
            blendShapeNames,
            selectedBlendShape,
            index =>
            {
                if (selectedBlendShape != index)
                {
                    selectedBlendShape = index;
                    results.Clear();
                    Repaint();
                }
            }
        );
    }

    private class BlendShapeSelectorWindow : EditorWindow
    {
        private BlendShapeAnimationFinder owner;
        private string[] names;
        private int selectedIndex;
        private Action<int> onSelected;
        private Vector2 scrollPosition;
        private string searchText = "";

        public static void Show(
            BlendShapeAnimationFinder owner,
            string[] names,
            int selectedIndex,
            Action<int> onSelected)
        {
            BlendShapeSelectorWindow window =
                CreateInstance<BlendShapeSelectorWindow>();

            window.owner = owner;
            window.names = names;
            window.selectedIndex = selectedIndex;
            window.onSelected = onSelected;

            window.titleContent =
                new GUIContent(Tr("selector_title"));

            window.minSize = new Vector2(420, 350);
            window.maxSize = new Vector2(800, 700);

            Vector2 mousePosition = GUIUtility.GUIToScreenPoint(
                Event.current.mousePosition
            );

            Rect position = new Rect(
                mousePosition.x - 210,
                mousePosition.y + 10,
                420,
                450
            );

            window.ShowAsDropDown(
                position,
                new Vector2(420, 450)
            );
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(8);

            EditorGUILayout.LabelField(
                Tr("selector_header", names.Length),
                EditorStyles.boldLabel
            );

            EditorGUILayout.Space(5);

            searchText = EditorGUILayout.TextField(
                Tr("label_search"),
                searchText
            );

            EditorGUILayout.Space(5);

            scrollPosition = EditorGUILayout.BeginScrollView(
                scrollPosition
            );

            for (int i = 0; i < names.Length; i++)
            {
                if (!string.IsNullOrEmpty(searchText) &&
                    names[i].IndexOf(
                        searchText,
                        StringComparison.OrdinalIgnoreCase
                    ) < 0)
                {
                    continue;
                }

                bool selected = i == selectedIndex;

                GUIStyle style = selected
                    ? EditorStyles.toolbarButton
                    : EditorStyles.miniButton;

                if (GUILayout.Button(
                        names[i],
                        style,
                        GUILayout.Height(24)))
                {
                    selectedIndex = i;

                    if (onSelected != null)
                        onSelected(i);

                    Close();
                    GUIUtility.ExitGUI();
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void OnDestroy()
        {
            if (owner != null)
                owner.Repaint();
        }
    }

    // ============================================================
    // 读取 BlendShape
    // ============================================================

    private void LoadBlendShapes()
    {
        results.Clear();

        blendShapeNames = Array.Empty<string>();
        selectedBlendShape = -1;

        if (targetRenderer == null)
            return;

        if (targetRenderer.sharedMesh == null)
            return;

        Mesh mesh = targetRenderer.sharedMesh;

        int count = mesh.blendShapeCount;

        if (count == 0)
            return;

        blendShapeNames = new string[count];

        for (int i = 0; i < count; i++)
        {
            blendShapeNames[i] =
                mesh.GetBlendShapeName(i);
        }

        selectedBlendShape = 0;
    }

    // ============================================================
    // 搜索 AnimationClip
    // ============================================================

    private void Search()
    {
        results.Clear();

        if (targetRenderer == null)
            return;

        if (targetRenderer.sharedMesh == null)
            return;

        if (selectedBlendShape < 0 ||
            selectedBlendShape >= blendShapeNames.Length)
        {
            return;
        }

        string targetBlendShape =
            blendShapeNames[selectedBlendShape];

        // 注意：targetObjectPath 目前只用于统计/未来扩展，
        // 并不会影响是否收录结果 —— 见下方 AddResult 调用，
        // 无论路径是否相同都会加入（VRChat/Avatar 场景下
        // 同一个 BlendShape 名称可能出现在不同 Prefab 路径中）。
        string targetObjectPath =
            AnimationUtility.CalculateTransformPath(
                targetRenderer.transform,
                null
            );

        string[] guids =
            AssetDatabase.FindAssets(
                "t:AnimationClip"
            );

        HashSet<string> found =
            new HashSet<string>();

        foreach (string guid in guids)
        {
            string assetPath =
                AssetDatabase.GUIDToAssetPath(guid);

            AnimationClip clip =
                AssetDatabase.LoadAssetAtPath<AnimationClip>(
                    assetPath
                );

            if (clip == null)
                continue;

            EditorCurveBinding[] bindings =
                AnimationUtility.GetCurveBindings(
                    clip
                );

            foreach (EditorCurveBinding binding in bindings)
            {
                if (!IsBlendShape(binding.propertyName))
                    continue;

                if (!IsTargetBlendShape(
                        binding.propertyName,
                        targetBlendShape))
                {
                    continue;
                }

                AddResult(
                    clip,
                    binding,
                    assetPath,
                    found
                );
            }
        }

        Debug.Log(
            Tr("log_search_result", targetBlendShape, results.Count)
        );
    }

    // ============================================================
    // 添加搜索结果
    // ============================================================

    private void AddResult(
        AnimationClip clip,
        EditorCurveBinding binding,
        string assetPath,
        HashSet<string> found)
    {
        string key =
            assetPath +
            "|" +
            binding.path +
            "|" +
            binding.propertyName;

        if (!found.Add(key))
            return;

        AnimationCurve curve =
            AnimationUtility.GetEditorCurve(
                clip,
                binding
            );

        float minValue = 0f;
        float maxValue = 0f;
        float firstValue = 0f;
        float lastValue = 0f;

        if (curve != null && curve.length > 0)
        {
            Keyframe[] keys = curve.keys;

            minValue = keys[0].value;
            maxValue = keys[0].value;
            firstValue = keys[0].value;
            lastValue = keys[keys.Length - 1].value;

            for (int i = 1; i < keys.Length; i++)
            {
                minValue = Mathf.Min(minValue, keys[i].value);
                maxValue = Mathf.Max(maxValue, keys[i].value);
            }
        }

        results.Add(
            new Result
            {
                clip = clip,
                path = binding.path,
                propertyName =
                    binding.propertyName,
                assetPath = assetPath,
                minValue = minValue,
                maxValue = maxValue,
                firstValue = firstValue,
                lastValue = lastValue
            }
        );
    }

    // ============================================================
    // 搜索结果选择
    // ============================================================

    private int GetSelectedResultCount()
    {
        int count = 0;

        foreach (Result result in results)
        {
            if (result != null && result.selected)
                count++;
        }

        return count;
    }

    private void SetAllResultsSelected(bool selected)
    {
        foreach (Result result in results)
        {
            if (result != null)
                result.selected = selected;
        }
    }

    // ============================================================
    // 批量修改 BlendShape
    // ============================================================

    private void BatchSetValue(float value, bool modifyAll)
    {
        if (results.Count == 0)
        {
            Debug.LogWarning(
                Tr("warn_no_results")
            );

            return;
        }

        if (selectedBlendShape < 0 ||
            selectedBlendShape >= blendShapeNames.Length)
        {
            return;
        }

        string blendShapeName =
            blendShapeNames[selectedBlendShape];

        int selectedCount = GetSelectedResultCount();

        if (!modifyAll && selectedCount == 0)
        {
            EditorUtility.DisplayDialog(
                Tr("dialog_no_selection_title"),
                Tr("dialog_no_selection_msg"),
                Tr("ok")
            );

            return;
        }

        int targetCount = modifyAll ? results.Count : selectedCount;

        /*
         * Unity BlendShape 的正常范围一般是 0~100。
         *
         * 这里不强制限制，因为某些动画/工作流
         * 可能需要其他数值。
         */

        bool confirmed =
            EditorUtility.DisplayDialog(
                Tr("dialog_confirm_title"),
                Tr("dialog_confirm_msg", targetCount, blendShapeName, value),
                Tr("btn_modify"),
                Tr("btn_cancel")
            );

        if (!confirmed)
            return;

        // ========================================================
        // 创建一个 Undo Group
        // ========================================================

        Undo.SetCurrentGroupName(
            $"Batch Set BlendShape: {blendShapeName}"
        );

        int undoGroup =
            Undo.GetCurrentGroup();

        int modifiedCount = 0;
        int modifiedCurveCount = 0;
        int modifiedKeyCount = 0;

        // ========================================================
        // 修改目标动画
        // ========================================================

        foreach (Result result in results)
        {
            if (result == null || (!modifyAll && !result.selected))
                continue;

            AnimationClip clip =
                result.clip;

            if (clip == null)
                continue;

            EditorCurveBinding binding =
                new EditorCurveBinding
                {
                    path =
                        result.path,

                    type =
                        typeof(SkinnedMeshRenderer),

                    propertyName =
                        result.propertyName
                };

            AnimationCurve curve =
                AnimationUtility.GetEditorCurve(
                    clip,
                    binding
                );

            if (curve == null)
                continue;

            if (curve.length == 0)
                continue;

            // ====================================================
            // 记录 Undo
            // ====================================================

            Undo.RecordObject(
                clip,
                $"Set {blendShapeName} to {value}"
            );

            // ====================================================
            // 修改所有 Keyframe
            // ====================================================

            Keyframe[] keys =
                curve.keys;

            for (int i = 0; i < keys.Length; i++)
            {
                Keyframe key =
                    keys[i];

                /*
                 * 只修改 value。
                 *
                 * time
                 * inTangent
                 * outTangent
                 * weightedMode
                 *
                 * 等信息全部保留。
                 */

                key.value = value;

                keys[i] = key;

                modifiedKeyCount++;
            }

            curve.keys = keys;

            // ====================================================
            // 写回 AnimationClip
            // ====================================================

            AnimationUtility.SetEditorCurve(
                clip,
                binding,
                curve
            );

            EditorUtility.SetDirty(clip);

            modifiedCurveCount++;
            modifiedCount++;
        }

        // ========================================================
        // 合并 Undo
        // ========================================================

        Undo.CollapseUndoOperations(
            undoGroup
        );

        // ========================================================
        // 保存
        // ========================================================

        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();

        // ========================================================
        // 完成提示
        // ========================================================

        Debug.Log(
            Tr(
                "log_complete",
                blendShapeName,
                modifiedCount,
                modifiedCurveCount,
                modifiedKeyCount,
                value
            )
        );

        EditorUtility.DisplayDialog(
            Tr("dialog_done_title"),
            Tr(
                "dialog_done_msg",
                blendShapeName,
                modifiedCount,
                modifiedCurveCount,
                modifiedKeyCount,
                value
            ),
            Tr("ok")
        );
    }

    // ============================================================
    // BlendShape 值显示
    // ============================================================

    private string FormatBlendShapeValue(Result result)
    {
        if (result == null)
            return "-";

        if (Mathf.Approximately(
                result.minValue,
                result.maxValue))
        {
            return result.minValue.ToString("0.###");
        }

        return
            $"{result.minValue:0.###} ~ {result.maxValue:0.###}" +
            $"（{Tr("frame_info", result.firstValue.ToString("0.###"), result.lastValue.ToString("0.###"))}）";
    }

    // ============================================================
    // 选择 AnimationClip
    // ============================================================

    private void SelectAnimation(
        AnimationClip clip)
    {
        if (clip == null)
            return;

        Selection.activeObject = clip;

        EditorGUIUtility.PingObject(clip);
    }

    // ============================================================
    // 判断是否为 BlendShape 曲线
    // ============================================================

    private bool IsBlendShape(
        string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return false;

        return propertyName.IndexOf(
            "blendShape",
            StringComparison.OrdinalIgnoreCase
        ) >= 0;
    }

    // ============================================================
    // 判断是否为目标 BlendShape
    // ============================================================

    private bool IsTargetBlendShape(
        string propertyName,
        string targetName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return false;

        if (string.IsNullOrEmpty(targetName))
            return false;

        /*
         * 例如：
         *
         * blendShape.まばたき
         * blendShape.笑い
         * blendShape.口_あ
         *
         * 使用 Ordinal 可以保证日文、
         * 中文以及特殊字符按照原始字符串比较。
         */

        // Unity 的 BlendShape 曲线属性格式通常是：
        // blendShape.<BlendShape名称>
        // 必须完整匹配，不能使用 Contains / IndexOf，
        // 否则例如 "Smile" 会错误匹配 "SmileBig"。
        string expectedProperty =
            "blendShape." + targetName;

        return string.Equals(
            propertyName,
            expectedProperty,
            StringComparison.Ordinal
        );
    }
}
