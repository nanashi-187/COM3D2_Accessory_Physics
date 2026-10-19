using HarmonyLib;

namespace COM3D2.AccessoryPhysics
{
    internal static class Patches
    {
        // TBoneHair_.Update simulates a slot's legacy chains and poses their bones (THair1.Calc + Proc).
        // It runs from TBodySkin.Update for every visible slot, and from the Double Nipple plugin for its
        // second accessory, whose controller still belongs to the nipple slot; the second front-hole
        // accessory of Double Front Hole is a native slot (accVag). Only configured slots are amplified.
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TBoneHair_), nameof(TBoneHair_.Update))]
        private static void BoneHairUpdatePostfix(TBoneHair_ __instance)
        {
            var plugin = AccessoryPhysicsPlugin.Instance;
            var slot = __instance.bodyskin;
            if (plugin == null || slot == null)
            {
                return;
            }

            var group = plugin.GroupFor(slot.SlotId);
            if (group != null)
            {
                SwingAmplifier.Apply(__instance, group.Strength.Value, group.Taper.Value);
            }
        }
    }
}
