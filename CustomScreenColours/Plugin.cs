using BepInEx;
using BepInEx.Configuration;
using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections;
using System.EnterpriseServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CustomScreenColours
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static volatile Plugin Instance;
        public ConfigEntry<Color> goodColour;
        public ConfigEntry<Color> badColour;
        public Material mat;
        Color good;
        Color bad;
        public Material fixjkmat;
        void Awake()
        {
            HarmonyPatches.ApplyHarmonyPatches();
            Instance = this;
            goodColour = Config.Bind("Change Board Colours", "Good Colour", Color.black, "Colour for whenever you are in a private/Deafult Colour (Replaces Green)");
            badColour = Config.Bind("Change Board Colours", "Bad Colour", Color.blue, "Colour for other Boards whenever you are in a public/Banned Colour (Replaces Red)");
            good = goodColour.Value;
            bad = badColour.Value;
        }

        void Start()
        {
            Invoke("RealStart", 2);
        }
        void RealStart()
        {
            fixjkmat = FindObjectOfType<WardrobeFunctionButton>().unpressedMaterial;
            mat = new Material(fixjkmat);
            mat.color = Plugin.Instance.goodColour.Value;
            GorillaComputer.instance.computerScreenRenderer.material = mat;
            GameObject motdboard = GameObject.Find("motdscreen");
            motdboard.GetComponent<Renderer>().material = mat;
            Renderer renderer = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/StaticUnlit/screen").GetComponent<Renderer>();
            renderer.enabled = true;
            renderer.material = mat;
            GameObject.Find("LocalObjects_Prefab").AddComponent<SJ>();
            GameObject.Find("LocalObjects_Prefab").AddComponent<CA>();
            GameObject.Find("LocalObjects_Prefab").AddComponent<CI>();
            GameObject.Find("LocalObjects_Prefab").AddComponent<MA>();
            GameObject.Find("LocalObjects_Prefab").AddComponent<BE>();
            GameObject.Find("LocalObjects_Prefab").AddComponent<CAV>();
            Material mat1 = new Material(fixjkmat);
            Material mat2 = new Material(fixjkmat);
            foreach (GorillaLevelScreen scrren in Resources.FindObjectsOfTypeAll<GorillaLevelScreen>())
            {
                scrren.goodMaterial = mat;
                scrren.badMaterial = mat2;
                scrren.goodMaterial.color = goodColour.Value;
                scrren.badMaterial.color = badColour.Value;
            }
        }
    }
    class SJ : MonoBehaviour
    {
        void Update()
        {
            if (transform.GetChild(7).gameObject.activeSelf)
            {
                transform.GetChild(7).GetChild(12).GetChild(3).GetChild(2).GetComponent<Renderer>().material = Plugin.Instance.mat;
                GameObject newmon = Instantiate(transform.GetChild(7).GetChild(12).GetChild(3).transform.GetChild(0).gameObject, transform.GetChild(7).GetChild(12).GetChild(3));
                transform.GetChild(7).GetChild(12).GetChild(3).transform.GetChild(0).gameObject.SetActive(false);
                newmon.GetComponent<Renderer>().material = Plugin.Instance.fixjkmat;
                transform.GetChild(7).GetChild(12).GetChild(0).GetChild(0).GetComponent<Renderer>().material = Plugin.Instance.mat;
                Destroy(this);
            }
        }
    }
    class CA : MonoBehaviour
    {
        void Update()
        {
            if (transform.GetChild(5).gameObject.activeSelf)
            {
                GameObject canyanm = GameObject.Find("Canyon/canyonmonitor");
                Material[] mats2 = canyanm.GetComponent<Renderer>().materials;
                mats2[1] = Plugin.Instance.mat;
                canyanm.GetComponent<Renderer>().materials = mats2;
                Destroy(this);
            }
        }
    }
    class CI : MonoBehaviour
    {
        void Update()
        {
            if (transform.GetChild(3).gameObject.activeSelf)
            {
                GameObject cityboard = GameObject.Find("CosmeticsRoomAnchor/monitor (1)");
                cityboard.GetComponent<Renderer>().enabled = true;
                cityboard.GetComponent<Renderer>().material = Plugin.Instance.mat;
                cityboard.transform.localPosition = new Vector3(cityboard.transform.localPosition.x + 0.001f, cityboard.transform.localPosition.y, cityboard.transform.localPosition.z);
                GameObject citboard2 = GameObject.Find("info page 1/stand (1)");
                citboard2.GetComponent<Renderer>().enabled = true;
                citboard2.GetComponent<Renderer>().material = Plugin.Instance.mat;
                GameObject citboard3 = GameObject.Find("InfoAnchor/info page 2 (1)");
                citboard3.GetComponent<Renderer>().enabled = true;
                citboard3.GetComponent<Renderer>().material = Plugin.Instance.mat;
                citboard3.transform.localPosition = new Vector3(citboard3.transform.localPosition.x, citboard3.transform.localPosition.y, citboard3.transform.localPosition.z + 0.001f);
                Destroy(this);
            }
        }
    }
    class MA : MonoBehaviour
    {
        void Update()
        {
            if (transform.GetChild(9).gameObject.activeSelf)
            {
                GameObject mtnsboard = transform.GetChild(9).GetChild(2).GetChild(27).GetChild(0).GetChild(1).gameObject;
                Material[] mats = mtnsboard.GetComponent<Renderer>().materials;
                mtnsboard.GetComponent<Renderer>().material = Plugin.Instance.mat;
                Destroy(this);
            }
        }
    }
    class BE : MonoBehaviour
    {
        void Update()
        {
            if (transform.GetChild(6).gameObject.activeSelf)
            {
                GameObject beboard = transform.GetChild(6).GetChild(7).GetChild(0).GetChild(1).gameObject;
                beboard.GetComponent<Renderer>().material = Plugin.Instance.mat;
                Destroy(this);
            }
        }
    }
    class CAV : MonoBehaviour
    {
        void Update()
        {
            if (transform.GetChild(10).gameObject.activeSelf)
            {
                GameObject cavb = transform.GetChild(10).GetChild(10).GetChild(7).GetChild(0).GetChild(1).gameObject;
                cavb.GetComponent<Renderer>().material = Plugin.Instance.mat;
                Destroy(this);
            }
        }
    }
}