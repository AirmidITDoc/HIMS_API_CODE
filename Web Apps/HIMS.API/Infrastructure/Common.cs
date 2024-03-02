using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HIMS.Core.Infrastructure;

namespace HIMS.API.Infrastructure
{
    public static class Common
    {


        public static List<SelectListItem> DropDownBindWithEnumDesc(Type enumType, bool IsDescription = false)
        {
            return enumType.ToSelectListItemsDesc(IsDescription);
        }

        /// <summary>
        /// From enum type convert to SelectListItems
        /// </summary>
        /// <param name="enumType">Type of enum</param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectListItemsDesc(this Type enumType, bool IsDescription = false)
        {
            List<SelectListItem> items = new();
            foreach (Enum cur in Enum.GetValues(enumType))
            {
                items.Add(new SelectListItem()
                {
                    Text = IsDescription ? cur.ToDescription() : cur.ToString(),
                    Value = GetEnumValue(cur)
                });
            }
            return items;
        }
        public static List<SelectListItem> DropDownBindWithEnum(Type enumType)
        {
            return enumType.ToSelectListItems();
        }
        public static List<SelectListItem> DropDownBindWithEnumDisplay(Type enumType)
        {
            return enumType.ToSelectListItemsDisplay();
        }

        public static List<SelectListItem> DropDownBindWithEnumText(Type enumType)
        {
            return enumType.ToSelectListItemsText();
        }
        public static List<SelectListItem> ToSelectListItemsText(this Type enumType)
        {
            List<SelectListItem> items = new();
            foreach (Enum cur in Enum.GetValues(enumType))
            {
                items.Add(new SelectListItem()
                {
                    Text = cur.ToString().Replace('_', ' '),
                    Value = cur.ToString().Replace('_', ' ')
                });
            }
            return items;
        }
        public static List<SelectListItem> ToSelectListItems(this Type enumType)
        {
            List<SelectListItem> items = new();
            foreach (Enum cur in Enum.GetValues(enumType))
            {
                items.Add(new SelectListItem()
                {
                    Text = cur.ToString().Replace('_', ' '),
                    Value = GetEnumValue(cur)
                });
            }
            return items;
        }
        public static List<SelectListItem> ToSelectListItemsDisplay(this Type enumType)
        {
            List<SelectListItem> items = new();
            foreach (Enum cur in Enum.GetValues(enumType))
            {
                items.Add(new SelectListItem()
                {
                    Text = cur.ToDisplayName().ToString().Replace('_', ' '),
                    Value = GetEnumValue(cur)
                });
            }
            return items;
        }
        public static List<SelectListItem> DropDownBindWithValue(int Value)
        {
            List<SelectListItem> items = new();
            for (int i = 0; i <= Value; i++)
            {
                items.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }
            return items;
        }

        #region :: Get Enum value ::
        public static string GetEnumValue(this Enum EnumType)
        {
            return Convert.ToString((int)(object)EnumType);
        }
        #endregion

        #region  :: To Int & Decimal ::
        public static int ToInt(this object a)
        {
            try
            {
                return Convert.ToInt32(a);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static decimal ToDecimal(this object a)
        {
            try
            {
                return Convert.ToDecimal(a);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion
        #region  :: Cache Api for Get list of All message ::

        public static bool GetCache(IMemoryCache cache, string cacheKey, out object resObject)
        {
            return cache.TryGetValue(cacheKey, out resObject);
        }
        public static void SetCache(IMemoryCache cache, string cacheKey, object resObject)
        {
            MemoryCacheEntryOptions cacheExpirationOptions = new()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(8),
                Priority = CacheItemPriority.Normal
            };
            cache.Set(cacheKey, resObject, cacheExpirationOptions);
        }
        public static void RemoveCache(IMemoryCache cache, string cacheKey)
        {
            cache.Remove(cacheKey);
        }

        public static async Task<string> GetTextFromFileAsync(string FilePath)
        {
            if (System.IO.File.Exists(FilePath))
            {
                using System.IO.StreamReader fileStream = new(FilePath);
                return await fileStream.ReadToEndAsync();
            }
            else
                return "";
        }
        #endregion
    }
}