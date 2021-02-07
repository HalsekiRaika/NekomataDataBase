using System;
using System.Runtime.InteropServices;
using Log5RLibs.Services;
using RavenLauncher.Builders;
using static RavenLauncher.Schemes.LauncherScheme;

namespace RavenLauncher {
    public static class RavenSetupProcess {
        public static void BuildController(string clonePath) {
            AlConsole.WriteLine(BuildInfoScheme, "ビルドを開始します。");
            int statusCode = RavenAppBuilder.Build(clonePath, true);
            if (statusCode != 0) {
                AlConsole.WriteLine(BuildFailureScheme, "ビルドに失敗しました。");
                AlConsole.WriteLine(BuildFailureScheme, $"ステータスコード : [{statusCode}]");
                Environment.Exit(-1);
            }
            AlConsole.WriteLine(BuildInfoScheme, "ビルドに成功。");
        }
    }
}