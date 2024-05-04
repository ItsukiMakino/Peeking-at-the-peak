using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
namespace MyGame
{
    public class PlayerInput : IDisposable
    {

        // リバインド対象のScheme

        InputAction _jumpAction;
        public InputAction JumpAction => _jumpAction;
        InputAction _leftAction;
        public InputAction LeftAction => _leftAction;
        InputAction _rightAction;
        public InputAction RightAction => _rightAction;
        InputAction _upperAction;
        public InputAction UpperAction => _upperAction;
        InputAction _lowerAction;
        public InputAction LowerAction => _lowerAction;

        PlayerAct inputs;
        public PlayerAct MyAction { get => inputs; set => inputs = value; }

        private string scheme = "Keyboard";
        private InputActionRebindingExtensions.RebindingOperation _rebindOperation;


        public PlayerInput()
        {

            this.inputs = new PlayerAct();
            _jumpAction = inputs.Player.Jump;
            _leftAction = inputs.Player.Left;
            _rightAction = inputs.Player.Right;
            _upperAction = inputs.Player.Upper;
            _lowerAction = inputs.Player.Lower;
            Load();

        }
        void IDisposable.Dispose()
        {
            CleanUpOperation();
        }
        public void StartRebinding(InputAction action, TMP_Text _pathText, GameObject mask)
        {
            if (action == null) return;

            // リバインド対象のBindingIndexを取得
            var bindingIndex = action.GetBindingIndex(
                InputBinding.MaskByGroup(scheme)
            );

            // リバインドを開始する
            OnStartRebinding(action, bindingIndex, _pathText, mask);
        }

        // 上書きされた情報をリセットする
        public void ResetOverrides(InputAction action, TMP_Text _pathText)
        {
            // Bindingの上書きを全て解除する
            action?.RemoveAllBindingOverrides();
            RefreshDisplay(action, _pathText);
        }

        // 現在のキーバインド表示を更新
        public void RefreshDisplay(InputAction action, TMP_Text _pathText)
        {
            if (action == null || _pathText == null) return;

            action.GetBindingDisplayString(action.GetBindingIndex(), out var deviceLayoutName, out var controlPath);
            _pathText.SetText(controlPath.ToUpper());
        }

        // 指定されたインデックスのBindingのリバインドを開始する
        private void OnStartRebinding(InputAction action, int bindingIndex, TMP_Text _pathText, GameObject mask)
        {
            // もしリバインド中なら、強制的にキャンセル
            // Cancelメソッドを実行すると、OnCancelイベントが発火する
            _rebindOperation?.Cancel();

            // リバインド前にActionを無効化する必要がある
            action.Disable();

            // ブロッキング用マスクを表示
            if (mask != null)
                mask.SetActive(true);

            // リバインドが終了した時の処理を行うローカル関数
            void OnFinished(InputAction action, GameObject mask, bool hideMask = true)
            {
                // オペレーションの後処理
                CleanUpOperation();

                // 一時的に無効化したActionを有効化する
                action.Enable();
                Save();
                // ブロッキング用マスクを非表示
                if (mask != null && hideMask)
                    mask.SetActive(false);
            }

            // リバインドのオペレーションを作成し、
            // 各種コールバックの設定を実施し、
            // 開始する
            _rebindOperation = action
                .PerformInteractiveRebinding(bindingIndex)
                .OnComplete(_ =>
                {
                    // リバインドが完了した時の処理
                    RefreshDisplay(action, _pathText);

                    var bindings = action.bindings;
                    var nextBindingIndex = bindingIndex + 1;

                    if (nextBindingIndex <= bindings.Count - 1 && bindings[nextBindingIndex].isPartOfComposite)
                    {
                        // Composite Bindingの一部なら、次のBindingのリバインドを開始する
                        OnFinished(action, mask, false);
                        OnStartRebinding(action, nextBindingIndex, _pathText, mask);
                    }
                    else
                    {
                        OnFinished(action, mask);
                    }
                })
                .OnCancel(_ =>
                {
                    // リバインドがキャンセルされた時の処理
                    OnFinished(action, mask);
                })
                .OnMatchWaitForAnother(0.2f) // 次のリバインドまでの待機時間を設ける
                .Start(); // ここでリバインドを開始する
        }

        // リバインドオペレーションを破棄する
        private void CleanUpOperation()
        {
            // オペレーションを作成したら、Disposeしないとメモリリークする
            _rebindOperation?.Dispose();
            _rebindOperation = null;
        }

        // 上書き情報の保存先
        private string _savePath = "InputActionOverrides.json";

        // 上書き情報の保存
        public void Save()
        {
            if (inputs == null) return;

            // inputsActionAssetの上書き情報の保存
            var json = inputs.SaveBindingOverridesAsJson();

            // ファイルに保存
            var path = Path.Combine(Application.persistentDataPath, _savePath);
            File.WriteAllText(path, json);
        }

        // 上書き情報の読み込み
        public void Load()
        {
            if (inputs == null) return;

            // ファイルから読み込み
            var path = Path.Combine(Application.persistentDataPath, _savePath);
            if (!File.Exists(path)) return;

            var json = File.ReadAllText(path);

            // inputsActionAssetの上書き情報を設定
            inputs.LoadBindingOverridesFromJson(json);
        }

    }
}