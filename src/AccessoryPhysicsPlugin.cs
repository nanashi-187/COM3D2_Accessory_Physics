using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace COM3D2.AccessoryPhysics
{
    [BepInPlugin(Guid, Name, Version)]
    public sealed class AccessoryPhysicsPlugin : BaseUnityPlugin
    {
        public const string Guid = "local.com3d2.accessoryphysics";
        public const string Name = "COM3D2 Accessory Physics";
        public const string Version = "0.1.0";

        internal static AccessoryPhysicsPlugin Instance { get; private set; }

        private readonly Dictionary<TBody.SlotID, SlotGroup> _groups = new Dictionary<TBody.SlotID, SlotGroup>();

        private void Awake()
        {
            Instance = this;
            Log.Init(Logger);

            AddGroup("Nipple", "nipple accessories", TBody.SlotID.accNipR, TBody.SlotID.accNipL);
            AddGroup("Belly Button", "belly button (navel) accessories", TBody.SlotID.accHeso);
            AddGroup("Earrings", "earrings", TBody.SlotID.accMiMiR, TBody.SlotID.accMiMiL);
            AddGroup("Front Hole", "front-hole accessories (both slots)", TBody.SlotID.accVag, TBody.SlotID.accXXX);

            Harmony.CreateAndPatchAll(typeof(Patches), Guid);
            Log.Info("Loaded " + Version + ".");
        }

        private void Update()
        {
            if (Time.frameCount % 300 == 0)
            {
                SwingAmplifier.PruneStale();
            }
        }

        // Settings for the slot a chain belongs to, or null when that slot is not handled.
        internal SlotGroup GroupFor(TBody.SlotID slot)
        {
            SlotGroup group;
            return _groups.TryGetValue(slot, out group) && group.Enabled.Value ? group : null;
        }

        private void AddGroup(string section, string what, params TBody.SlotID[] slots)
        {
            var group = new SlotGroup
            {
                Enabled = Config.Bind(section, "Enabled", true, "Apply swing to " + what + "."),
                Strength = Config.Bind(section, "Swing Strength", 1.5f, new ConfigDescription(
                    "Multiplies how far the chains of " + what + " swing away from their settled pose. "
                    + "1 = the game's own motion, below 1 dampens it. Only legacy (TBoneHair) rigs, i.e. bones "
                    + "named _yure_soft_ / _yure_hard_ / _yure_, are affected.",
                    new AcceptableValueRange<float>(0.5f, 4f))),
                Taper = Config.Bind(section, "Swing Taper", 0.5f, new ConfigDescription(
                    "How the extra swing is spread along a chain. The total stays the same; higher values move it "
                    + "from the top of the chain (under the most tension from a pendant) to the lowest joints. "
                    + "0 = equal at every joint, 0.5 = linear ramp, 1 = quadratic, 3 = almost all at the last joint.",
                    new AcceptableValueRange<float>(0f, SwingAmplifier.MaxTaper)))
            };

            foreach (var slot in slots)
            {
                _groups[slot] = group;
            }
        }
    }

    internal sealed class SlotGroup
    {
        public ConfigEntry<bool> Enabled;
        public ConfigEntry<float> Strength;
        public ConfigEntry<float> Taper;
    }
}
