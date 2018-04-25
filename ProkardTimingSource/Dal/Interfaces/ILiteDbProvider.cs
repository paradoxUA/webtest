using Dal.Entities;
using System;
using System.Collections.Generic;

namespace Dal.Interfaces
{
    public interface ILiteDbProvider
    {
        void SavePageSettings(PageSettings settings);
        PageSettings GetPageSettings();
        List<PageSettings> GetPageSettingsCollection();
    }
}
