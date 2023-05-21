using BepInEx;
using BepInEx.Configuration;
using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
namespace CustomScreenColours
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static volatile Plugin Instance;
        public ConfigEntry<Color> goodColour;
        public ConfigEntry<Color> badColour;
        Material mat;
        Color good;
        Color bad;
        Material fixjkmat;
        void Awake()
        {
            //HarmonyPatches.ApplyHarmonyPatches();
            Instance = this;
            goodColour = Config.Bind("Change Board Colours", "Good Colour", Color.black, "Colour for whenever you are in a private/Deafult Colour (Replaces Green)");
            badColour = Config.Bind("Change Board Colours", "Bad Colour", Color.blue, "Colour for other Boards whenever you are in a public/Banned Colour (Replaces Red)");
            good = goodColour.Value;
            bad = badColour.Value;
        }

        void OnEnable()
        {HarmonyPatches.ApplyHarmonyPatches();}

        void OnDisable()
        {HarmonyPatches.RemoveHarmonyPatches();}

        public IEnumerator SetStuff(float t)
        {
            yield return new WaitForSeconds(t);
            if (this.enabled)
            {
                fixjkmat = FindObjectOfType<WardrobeFunctionButton>().unpressedMaterial;
                mat = new Material(Shader.Find("Gorilla/GorillaUnlit"));
                mat.color = Plugin.Instance.goodColour.Value;
                GorillaComputer.instance.computerScreenRenderer.material = mat;
                GameObject.Find("Level/lower level/StaticUnlit/screen").GetComponent<Renderer>().material = mat;
                GameObject.Find("motdscreen").GetComponent<Renderer>().material = mat;
                GameObject cityboard = GameObject.Find("Level/city/CosmeticsRoomAnchor/monitor (1)");
                cityboard.GetComponent<Renderer>().enabled = true;
                cityboard.GetComponent<Renderer>().material = mat;
                cityboard.transform.localPosition = new Vector3(-8.0778f, -19.3584f, -5.565f);
                GameObject mtnsboard = GameObject.Find("Level/mountain/Geometry/scoreboard (1)/board");
                mtnsboard.GetComponent<Renderer>().enabled = true;
                mtnsboard.transform.localPosition = new Vector3(0, 0, 0.0045f);
                Material[] mats = mtnsboard.GetComponent<Renderer>().materials;
                mats[1] = mat;
                mtnsboard.GetComponent<Renderer>().materials = mats ;
                GameObject.Find("Level/skyjungle/UI/CloudsScoreboard/REMOVE board").GetComponent<Renderer>().material = mat;
                GameObject canyanm = GameObject.Find("Level/canyon/Canyon/canyonmonitor");
                Material[] mats2 = canyanm.GetComponent<Renderer>().materials;
                mats2[1] = mat;
                canyanm.GetComponent<Renderer>().materials = mats2;
                //SJ PC's screen colour setting is broken so i had to fix like this or it would have been ALL the user colour
                if (GameObject.Find("Level/skyjungle/UI/PhysicalComputer (3)").transform.GetChild(0).gameObject.activeSelf)
                {
                    GameObject.Find("Level/skyjungle/UI/PhysicalComputer (3)").transform.GetChild(2).GetComponent<Renderer>().material = mat;
                    GameObject newmon = Instantiate(GameObject.Find("Level/skyjungle/UI/PhysicalComputer (3)").transform.GetChild(0).gameObject, GameObject.Find("Level/skyjungle/UI/PhysicalComputer (3)").transform);
                    GameObject.Find("Level/skyjungle/UI/PhysicalComputer (3)").transform.GetChild(0).gameObject.SetActive(false);
                    newmon.GetComponent<Renderer>().material = fixjkmat;

                }
            }
            else
            {
                yield break;
            }
        }
    }
}
