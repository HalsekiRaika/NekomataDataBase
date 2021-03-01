using System.Collections.Generic;
using System.Runtime.InteropServices;
using SetupLibs.Logger;
using SetupLibs.Model;
using static SetupLibs.Model.ConfigModel;

namespace SetupLibs {
    public static class ConfigUpdater {
        public static void onUpdate([Optional] string apiKey, [Optional] string dbUserName, 
                [Optional] string dbPassWord, [Optional] List<IgnoreData> ignoreData) {
            ConfigModel updateTarget = ConfigImporter.onDeserialize(false);
            ConfigModel updatedModel = new ConfigModel() {
                API_KEY = updateApiKey(updateTarget, apiKey),
                DB_ACCESS_USERNAME = updateDbUserName(updateTarget, dbUserName),
                DB_ACCESS_PASSWORD = updateDbPassWord(updateTarget, dbPassWord),
                IS_LOCAL_MODE = false,
                ignoreData = updateIgnoreData(updateTarget, ignoreData)
            };

            ConfigExporter.onExport(updatedModel, true);
        }

        private static string updateApiKey(ConfigModel targetModel, [Optional] string apiKey) {
            if (apiKey == null) { AlLite.WriteLine(WriteMode.INFO, "No Update ApiKey."); return targetModel.API_KEY; }
            AlLite.WriteLine(WriteMode.INFO, $"Update ApiKey: {apiKey.Substring(0, 3)}");
            WarningText("apiKey");
            return apiKey;
        }
        
        private static string updateDbUserName(ConfigModel targetModel, [Optional] string dbUserName) {
            if (dbUserName == null) { AlLite.WriteLine(WriteMode.INFO, "No Update DataBase UserName."); return targetModel.DB_ACCESS_USERNAME; }
            AlLite.WriteLine(WriteMode.INFO, $"Update DataBase UserName: {dbUserName}");
            return dbUserName;
        }
        
        private static string updateDbPassWord(ConfigModel targetModel, [Optional] string dbPassWord) {
            if (dbPassWord == null) { AlLite.WriteLine(WriteMode.INFO, "No Update DataBase PassWord."); return targetModel.DB_ACCESS_PASSWORD; }
            AlLite.WriteLine(WriteMode.INFO, $"Update DataBase PassWord: {Encrypter.onEncrypt(dbPassWord)}");
            WarningText("Password");
            return dbPassWord;
        }

        private static List<IgnoreData> updateIgnoreData(ConfigModel targetModel, List<IgnoreData> ignoreData) {
            if (ignoreData.Count <= 0) { AlLite.WriteLine(WriteMode.INFO, "No Update Ignore Data."); return targetModel.ignoreData; }
            ignoreData.ForEach(data => targetModel.ignoreData.Add(data));
            List<IgnoreData> update = targetModel.ignoreData;
            AlLite.WriteLine(WriteMode.INFO, $"Update Ignore Data: {ignoreData.Count} updates");
            return update;
        }

        private static void WarningText(string updateTarget) {
            AlLite.WriteLine(WriteMode.WARN, $" :: If you have entered a {updateTarget.ToLower()} as a command line argument, ");
            AlLite.WriteLine(WriteMode.WARN, $" :: it is recommended that you clear your command history in any case.");
            AlLite.WriteLine(WriteMode.WARN, $" :: => history -d <history_num> OR history -c");
            AlLite.WriteLine(WriteMode.WARN, $" ::   <history_num> - If you run history with no arguments, you will see a numbered command history.");
        }
    }
}