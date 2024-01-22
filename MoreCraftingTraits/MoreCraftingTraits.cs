using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace MoreCraftingTraits
{
    public class MoreCraftingTraits : ModSystem
    {
        private Harmony harmony = new Harmony("MoreCraftingTraits");
        public override void StartClientSide(ICoreClientAPI api)
        {
            ClayRecipeSystemPatch.Patch(harmony);
            SmithingRecipeSystemPatch.Patch(harmony);
        }
    }
}
