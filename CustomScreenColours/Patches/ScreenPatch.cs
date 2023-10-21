using GorillaNetworking;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace CustomScreenColours.Patches
{
    [HarmonyPatch(typeof(GorillaLevelScreen))]
    [HarmonyPatch("Awake", MethodType.Normal)]
    internal class ScreenPatch
    {
        private static void Prefix(GorillaLevelScreen __instance)
        {
            Material mat = new Material(__instance.goodMaterial);
            Material mat2 = new Material(__instance.badMaterial);
            __instance.goodMaterial = mat;
            __instance.badMaterial = mat2;
            __instance.goodMaterial.color = Plugin.Instance.goodColour.Value;
            __instance.badMaterial.color = Plugin.Instance.badColour.Value;
        }
    }
    [HarmonyPatch(typeof(GorillaText))]
    [HarmonyPatch("Initialize", MethodType.Normal)]
    internal class TextPatch
    {
        private static void Postfix(GorillaText __instance)
        {
            Material mat = new Material(__instance.meshRenderer.material);
            Material mat2 = new Material(__instance.failureMaterial);
            mat.color = Plugin.Instance.goodColour.Value;
            mat2.color = Plugin.Instance.badColour.Value;
            __instance.meshRenderer.material = mat;
            __instance.failureMaterial = mat2;
        }
    }
}
