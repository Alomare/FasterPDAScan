using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection.Emit;
using System;

namespace Alomare.FasterPDAScan
{
    [HarmonyPatch(typeof(PDAScanner))]
    public static class PDAScannerPatches
    {
        public static float MultiplyProgress(float progress)
        {
            return progress * (1.0f / FasterPDAScanPlugin.ModOptions.ScanSpeedMultiplier);
        }

        [HarmonyPatch(nameof(PDAScanner.Scan))]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> ScanTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            return new CodeMatcher(instructions)
                .MatchForward(false, new CodeMatch(OpCodes.Callvirt, AccessTools.Method(typeof(NoCostConsoleCommand), "get_fastScanCheat")))
                .Advance(10)
                .Insert(Transpilers.EmitDelegate<Func<float, float>>(MultiplyProgress))
                .InstructionEnumeration();
        }
    }
}