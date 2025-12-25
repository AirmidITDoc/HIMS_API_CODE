using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class AppSetting
    {
        public string SettingKey { get; set; } = null!;
        public string SettingValue { get; set; } = null!;
    }
}
