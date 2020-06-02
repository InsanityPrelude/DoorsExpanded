﻿using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace DoorsExpanded
{
    public enum DoorType
    {
        Standard = 0,
        Stretch,
        DoubleSwing,
        FreePassage,
        StretchVertical,
    }

    [Obsolete("Use CompProperties_DoorExpanded instead")]
    public class DoorExpandedDef : ThingDef
    {
        public const float DefaultStretchPercent = 0.2f;

        public bool fixedPerspective = false;
        public bool singleDoor = false;
        public DoorType doorType = DoorType.Standard;
        public bool rotatesSouth = true;
        public int tempLeakRate = TemperatureTuning.Door_TempEqualizeIntervalClosed;
        public float doorOpenMultiplier = Building_DoorExpanded.VisualDoorOffsetEnd;
        [Obsolete("Use the DoorOpenSpeed stat instead (note: DoorOpenSpeed stat is the inverse of confusingly named doorOpenSpeedRate)")]
        public float doorOpenSpeedRate = 1.0f;
        public GraphicData doorFrame;
        public Vector3 doorFrameOffset;
        public GraphicData doorFrameSplit;
        public Vector3 doorFrameSplitOffset;
        public GraphicData doorAsync;
        public Vector2 stretchCloseSize;
        public Vector2 stretchOpenSize;
        public Vector2? stretchOffset;

        public DoorExpandedDef()
        {
            // Following are already defined in the AbstractHeronDoorBase ThingDef,
            // but just in case anyone doesn't inherit from that def, so that they're at least minimally functional.
            thingClass = typeof(Building_DoorExpanded);
            category = ThingCategory.Building;
            tickerType = TickerType.Normal;
            drawerType = DrawerType.RealtimeOnly;
        }

        public override void ResolveReferences()
        {
            var compProps = GetCompProperties<CompProperties_DoorExpanded>();
            if (compProps == null)
            {
                compProps = new CompProperties_DoorExpanded
                {
                    remoteDoor = typeof(Building_DoorRemote).IsAssignableFrom(thingClass),
                    doorType = doorType,
                    fixedPerspective = fixedPerspective,
                    singleDoor = singleDoor,
                    rotatesSouth = rotatesSouth,
                    tempLeakRate = tempLeakRate,
                    doorOpenMultiplier = doorOpenMultiplier,
                    doorFrame = doorFrame,
                    doorFrameOffset = doorFrameOffset,
                    doorFrameSplit = doorFrameSplit,
                    doorFrameSplitOffset = doorFrameSplitOffset,
                    doorAsync = doorAsync,
                    stretchCloseSize = stretchCloseSize,
                    stretchOpenSize = stretchOpenSize,
                    stretchOffset = stretchOffset,
                };
                comps.Add(compProps);
            }

            base.ResolveReferences();

            // Default missing DoorOpenSpeed to inverse of obsolete doorOpenSpeedRate value.
            if (doorOpenSpeedRate != 1.0f && !this.StatBaseDefined(StatDefOf.DoorOpenSpeed))
            {
                this.SetStatBaseValue(StatDefOf.DoorOpenSpeed, 1f / doorOpenSpeedRate);
            }
        }
    }
}
