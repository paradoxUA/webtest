using Dal.Interfaces;
using System;
using LiteDB;
using System.Linq;
using System.IO;
using Dal.Entities;
using System.Collections.Generic;

namespace Dal.Providers
{
    public class LiteDbProvider : ILiteDbProvider
    {
        private static ILiteDbProvider _liteDbProvider;
        private static readonly object Locker = new object();
        private readonly LiteDatabase _database;

        private LiteDbProvider()
        {
            var dirBase = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Data"));
            var fileBase = new FileInfo(Path.Combine(dirBase.FullName, "MyData.db"));
            if (!dirBase.Exists) dirBase.Create();

            _database = new LiteDatabase(fileBase.FullName);
        }

        public static ILiteDbProvider Create()
        {
            lock (Locker)
            {
                return _liteDbProvider ?? (_liteDbProvider = new LiteDbProvider());
            }
        }

        public void SavePageSettings(PageSettings settings)
        {
            var col = _database.GetCollection<PageSettings>(nameof(PageSettings));

            var toDisable = col.Find(x=>x.IsActive ==true);

            foreach (var item in toDisable)
                    item.IsActive = false;

            col.Upsert(toDisable);

            settings.IsActive = true;
            col.Upsert(settings);
        }

        public PageSettings GetPageSettings()
        {
            var col = _database.GetCollection<PageSettings>(nameof(PageSettings));
            return col.Find(x => x.IsActive == true)?.FirstOrDefault() ?? PageSettings.Default();
        }

        public List<PageSettings> GetPageSettingsCollection()
        {
            var col = _database.GetCollection<PageSettings>(nameof(PageSettings));
            var temp = col.FindAll()?.ToList();

            if (temp == null || temp.Count < 1)
            {
                temp = new List<PageSettings>();
                temp.Add(PageSettings.Default());
            }

            return temp;
        }
    }
}
