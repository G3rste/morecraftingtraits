using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace MoreCraftingTraits
{
    public class ClayRecipeSystemPatch
    {
        public static Dictionary<string, string> exclusiveRecipes = new Dictionary<string, string>
        {
            { "crucible-*", "bowyer" },
            { "storagevessel-*", "precise" }
        };
        public static void Patch(Harmony harmony)
        {
            harmony.Patch(methodInfo(),
                postfix: new HarmonyMethod(typeof(ClayRecipeSystemPatch)
                        .GetMethod("Postfix",
                        BindingFlags.Static | BindingFlags.Public)));
        }

        public static void Unpatch(Harmony harmony)
        {
            harmony.Unpatch(methodInfo(),
                HarmonyPatchType.Postfix,
                "MoreCraftingTraits");
        }

        public static MethodInfo methodInfo()
        {
            return typeof(ApiAdditions).GetMethod("GetClayformingRecipes",
                BindingFlags.Static | BindingFlags.Public);
        }

        public static void Postfix(ref List<ClayFormingRecipe> __result, ICoreAPI api)
        {
            if (api is ICoreClientAPI capi)
            {
                __result = __result
                    .Where(recipe => !exclusiveRecipes
                        .Where(keyValuePair => 
                        {
                            var path = recipe.Output.Code.Path;
                            if(keyValuePair.Key.EndsWith("*")){
                                return path.StartsWith(keyValuePair.Key.Substring(0, keyValuePair.Key.Length - 1));
                            } else{
                                return path == keyValuePair.Key;
                            }
                        })
                        .Any(keyValuePair => !capi.ModLoader.GetModSystem<CharacterSystem>().HasTrait(capi.World.Player, keyValuePair.Value)))
                    .ToList();
            }
        }
    }
    public class SmithingRecipeSystemPatch
    {
        public static Dictionary<string, string> exclusiveRecipes = new Dictionary<string, string>
        {
            { "anvilpart-*", "bowyer" },
            { "bladehead-*", "precise" }
        };
        public static void Patch(Harmony harmony)
        {
            harmony.Patch(methodInfo(),
                postfix: new HarmonyMethod(typeof(SmithingRecipeSystemPatch)
                        .GetMethod("Postfix",
                        BindingFlags.Static | BindingFlags.Public)));
        }

        public static void Unpatch(Harmony harmony)
        {
            harmony.Unpatch(methodInfo(),
                HarmonyPatchType.Postfix,
                "MoreCraftingTraits");
        }

        public static MethodInfo methodInfo()
        {
            return typeof(ApiAdditions).GetMethod("GetSmithingRecipes",
                BindingFlags.Static | BindingFlags.Public);
        }

        public static void Postfix(ref List<SmithingRecipe> __result, ICoreAPI api)
        {
            if (api is ICoreClientAPI capi)
            {
                __result = __result
                    .Where(recipe => !exclusiveRecipes
                        .Where(keyValuePair => 
                        {
                            var path = recipe.Output.Code.Path;
                            if(keyValuePair.Key.EndsWith("*")){
                                return path.StartsWith(keyValuePair.Key.Substring(0, keyValuePair.Key.Length - 1));
                            } else{
                                return path == keyValuePair.Key;
                            }
                        })
                        .Any(keyValuePair => !capi.ModLoader.GetModSystem<CharacterSystem>().HasTrait(capi.World.Player, keyValuePair.Value)))
                    .ToList();
            }
        }
    }
}