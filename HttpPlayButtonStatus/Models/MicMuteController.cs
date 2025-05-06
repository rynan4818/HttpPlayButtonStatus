using UnityEngine;
using UnityEngine.UI;
using HMUI;
using HttpSiraStatus.Enums;
using HttpSiraStatus.Interfaces;
using HttpSiraStatus.Util;
using HttpPlayButtonStatus.Configuration;

namespace HttpPlayButtonStatus.Models
{
    public class MicMuteController
    {
        public static readonly Vector2 CanvasSize = new Vector2(30, 30);
        public static readonly Vector3 CanvasScale = new Vector3(0.005f, 0.005f, 0.005f);
        public static readonly Vector3 CanvasPosition = new Vector3(-0.2f, -0.3f, 0.5f);
        public static readonly Vector3 CanvasRotation = new Vector3(0, 0, 0);
        private IStatusManager _statusManager;
        public bool MenuIsMute { get; set; } = false;
        public bool GameIsMute { get; set; } = true;

        public MicMuteController(IStatusManager statusManager)
        {
            this._statusManager = statusManager;
        }
        public GameObject NewCanvasCreate(string canvasName)
        {
            var rootObject = new GameObject(canvasName, typeof(Canvas), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
            var mainCamera = GameObject.FindGameObjectsWithTag("MainCamera");
            if (mainCamera.Length == 0)
            {
                Plugin.Log.Error("MainCamera not found");
                return null;
            }
            rootObject.transform.parent = mainCamera[0].transform;
            var sizeFitter = rootObject.GetComponent<ContentSizeFitter>();
            sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            var canvas = rootObject.GetComponent<Canvas>();
            canvas.sortingOrder = 3;
            canvas.renderMode = RenderMode.WorldSpace;
            var rectTransform = canvas.transform as RectTransform;
            rectTransform.sizeDelta = CanvasSize;
            rootObject.transform.localPosition = CanvasPosition + new Vector3(PluginConfig.Instance.MicMuteXposOffset, PluginConfig.Instance.MicMuteYposOffset, PluginConfig.Instance.MicMuteZposOffset);
            rootObject.transform.localEulerAngles = CanvasRotation + new Vector3(PluginConfig.Instance.MicMuteXrotOffset, PluginConfig.Instance.MicMuteYrotOffset, PluginConfig.Instance.MicMuteZrotOffset); ;
            rootObject.transform.localScale = CanvasScale;
            var micMuteTextMesh = this.CreateText(canvas.transform as RectTransform, string.Empty, new Vector2(10, 31));
            rectTransform = micMuteTextMesh.transform as RectTransform;
            rectTransform.SetParent(canvas.transform, false);
            rectTransform.anchoredPosition = Vector2.zero;
            micMuteTextMesh.fontSize = PluginConfig.Instance.MicMuteFontSize;
            micMuteTextMesh.color = Color.white;
            micMuteTextMesh.text = "MUTE";
            return rootObject;
        }
        private CurvedTextMeshPro CreateText(RectTransform parent, string text, Vector2 anchoredPosition)
        {
            return this.CreateText(parent, text, anchoredPosition, new Vector2(0, 0));
        }
        private CurvedTextMeshPro CreateText(RectTransform parent, string text, Vector2 anchoredPosition, Vector2 sizeDelta)
        {
            var gameObj = new GameObject("CustomUIText");
            gameObj.SetActive(false);

            var textMesh = gameObj.AddComponent<CurvedTextMeshPro>();
            textMesh.rectTransform.SetParent(parent, false);
            textMesh.text = text;
            textMesh.fontSize = 4;
            textMesh.overrideColorTags = true;
            textMesh.color = Color.white;
            textMesh.rectTransform.anchorMin = new Vector2(0f, 0f);
            textMesh.rectTransform.anchorMax = new Vector2(0f, 0f);
            textMesh.rectTransform.sizeDelta = sizeDelta;
            textMesh.rectTransform.anchoredPosition = anchoredPosition;

            gameObj.SetActive(true);
            return textMesh;
        }
        public void SendHttpStatus(bool isMute)
        {
            if (!PluginConfig.Instance.MicMuteChangeEnable)
                return;
            var rootObj = new JSONObject();
            rootObj["MicMute"] = isMute;
            this._statusManager.OtherJSON["HttpPlayButtonStatus"] = rootObj;
            this._statusManager.EmitStatusUpdate(ChangedProperty.Other, BeatSaberEvent.Other);
        }
    }
}
