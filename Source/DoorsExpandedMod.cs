﻿using Verse;

namespace DoorsExpanded
{
    public class DoorsExpandedMod : Mod
    {
        public DoorsExpandedMod(ModContentPack content) : base(content)
        {
            HarmonyPatches.EarlyPatches();
        }

        // TODO: Mod options
    }
}
