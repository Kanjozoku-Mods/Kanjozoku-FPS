using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace Kanjozoku {
    [BepInPlugin("KanjozokuFPS", "KanjozokuFPS", "1.0.0")]
	[BepInProcess("Kanjozoku Game.exe")]
    public class KanjoFPS : BaseUnityPlugin {
		
		public Harmony Harmony { get; } = new Harmony("KanjozokuFPS");
		
		public static KanjoFPS Instance = null;
		
        private void Awake() {
            // Plugin startup logic
            Logger.LogInfo("KanjozokuFPS is loaded!");
			
			KanjoFPS.Instance = this;
			
			Harmony.PatchAll();
			
        }
		
		private void _LogInfo(string msg) {
			Logger.LogInfo(msg);
		}

		public static void LogInfo(string msg) {
			if (KanjoFPS.Instance == null)
				return;
			Instance._LogInfo(msg);
		}

    }
	
    [HarmonyPatch(typeof(GlobalManager), "Start")]
    public static class FPSPatch {
        private static void Prefix(GlobalManager __instance) {
			int fpscap = 60;
			KanjoFPS.LogInfo($"Capping FPS at {fpscap}");
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = fpscap;
        }
    }
}
