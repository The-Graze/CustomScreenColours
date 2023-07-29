using GorillaNetworking;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace CustomScreenColours.Patches
{
    /// <summary>
    /// This is an example patch, made to demonstrate how to use Harmony. You should remove it if it is not used.
    /// </summary>
    [HarmonyPatch(typeof(GorillaLevelScreen))]
    [HarmonyPatch("Awake", MethodType.Normal)]
    internal class ScreenPatch
    {
        private static void Prefix(GorillaLevelScreen __instance)
        {
            Material mat = new Material(Shader.Find("Gorilla/GorillaUnlit"));
            Material mat2 = new Material(Shader.Find("Gorilla/GorillaUnlit"));
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
            Material mat = new Material(Shader.Find("Gorilla/GorillaUnlit"));
            Material mat2 = new Material(Shader.Find("Gorilla/GorillaUnlit"));
            mat.color = Plugin.Instance.goodColour.Value;
            mat2.color = Plugin.Instance.badColour.Value;
            __instance.meshRenderer.material = mat;
            __instance.failureMaterial = mat2;
        }
    }
}
