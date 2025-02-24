﻿using System;
using System.ComponentModel.Composition;
using System.Text.Json;
using System.Threading.Tasks;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;
using OngekiFumenEditor.Utils;

namespace OngekiFumenEditor.Kernel.MiscMenu.Commands.CallFullGC
{
    [CommandHandler]
    public class CallFullGCCommandHandler : CommandHandlerBase<CallFullGCCommandDefinition>
    {
        public override Task Run(Command command)
        {
            var before = GC.GetTotalMemory(false);
            var beforePriv = GC.GetTotalAllocatedBytes(false);
            var info = GC.GetGCMemoryInfo(GCKind.Any);
            GC.Collect(0, GCCollectionMode.Forced);
            var after = GC.GetTotalMemory(true);
            var afterPriv = GC.GetTotalAllocatedBytes(false);
            Log.LogInfo($"GC called, {FileSizeDisplayerHelper.Format(before)}({FileSizeDisplayerHelper.Format(beforePriv)}) -> {FileSizeDisplayerHelper.Format(after)}({FileSizeDisplayerHelper.Format(afterPriv)})");
            return TaskUtility.Completed;
        }
    }
}