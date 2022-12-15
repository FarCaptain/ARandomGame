using System;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace CRTMonitor
{
    [Serializable]
    [PostProcess(typeof(PPS_CRTRenderer), PostProcessEvent.AfterStack, "Custom/PPS_CRT_Monitor")]
    public sealed class PPS_CRTMonitorSetting : PostProcessEffectSettings
    {
        [Range(0.0f, 2.0f)] public FloatParameter ScreenScale = new FloatParameter();
        public BoolParameter IsLight = new BoolParameter();
        [Range(0.0f, 1.0f)] public FloatParameter OverallLight = new FloatParameter();
        [Range(0.0f, 1.0f)] public FloatParameter CenterLightScale = new FloatParameter();
        public TextureParameter CRT_Tex = new TextureParameter();
        public TextureParameter Moire_Tex = new TextureParameter();
    }

    public sealed class PPS_CRTRenderer : PostProcessEffectRenderer<PPS_CRTMonitorSetting>
    {
        public static PPS_CRTRenderer _instance;

        public static bool IsEnabled
        {
            get
            {
                if (_instance != null)
                {
                    return _instance.settings.IsEnabledAndSupported(null);
                }
                
                return false;
            }
        }

        public override void Init()
        {
            _instance = this;
            base.Init();
        }

        public override void Render(PostProcessRenderContext context)
        {
            var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/PPS_CRT_Monitor"));
            sheet.properties.SetFloat("_Scale", settings.ScreenScale);
            sheet.properties.SetFloat("_IsLight", (settings.IsLight) ? 1.0f : 0.0f);
            sheet.properties.SetFloat("_OverallLight", settings.OverallLight);
            sheet.properties.SetFloat("_CenterLightScale", settings.CenterLightScale);
            sheet.properties.SetTexture("_CRT_Tex", settings.CRT_Tex);
            sheet.properties.SetTexture("_Moire_Tex", settings.Moire_Tex);
            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
    }
}
