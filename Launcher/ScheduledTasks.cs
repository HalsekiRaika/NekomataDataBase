using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Timers;
using Log5RLibs.Services;
using Log5RLibs.utils;
using Timer = System.Timers.Timer;
using static Launcher.Schemes.LauncherScheme;

namespace Launcher {
    public static class ScheduledTasks {
        private static Timer _timer;
        public static void Fire() {
            AlConsole.WriteLine(LauncherInfoScheme, "Service Fired (*´ω｀*)");
            //_timer = new Timer(60 * 60 * 1000);
#if DEBUG
            _timer = new Timer(60 * 1000);
#else 
            _timer = new Timer(60 * 60 * 1000);     
#endif
            _timer.Elapsed += new ElapsedEventHandler(OnTimeEvent);
            _timer.Start();
            CollectStart();
        }
        
        private static void OnTimeEvent(object obj, ElapsedEventArgs eventArgs) {
            _timer.Start();
            AlConsole.WriteLine(RunCollectorScheme, "Collect Start");
            int statusCode = CollectStart();
            if (statusCode != 0) {
                AlConsole.WriteLine(FailureCollectScheme, "コントローラーの起動に失敗しました。");
                Interval();
                if (!StatusChecker.IsRecoveryMode()) { Settings.RecvFile.Create(); }
                using (StreamWriter writer = new StreamWriter(Settings.StatusDir + Settings.RecoveryStat, true)) {
                    writer.Write($"Recovery Boot Time:{DateTime.Now:g}\n");
                    AlConsole.WriteLine(RecoveryBootScheme, "コントローラーを再起動します。");                    
                    for (int i = 0; i < 5; i++) {
                        statusCode = CollectStart();
                        if (statusCode != 0) {
                            AlConsole.WriteLine(FailureCollectScheme, "コントローラーの再起動に失敗しました。");
                            AlConsole.WriteLine(FailureCollectScheme,
                                i < 4 ? $"リトライします。[{i + 1}]" : $"起動に{i + 1}回失敗しました。強制終了します。");
                            Interval();
                            writer.Write($"Recovery Boot Time:{DateTime.Now:g}\n");
                        } else {
                            AlConsole.WriteLine(FailureCollectScheme, $"リカバリーに成功しました。");
                            writer.Write($"Successfully Recovery Boot Time:{DateTime.Now:g}\n");
                            break;
                        }
                    }
                }
            }
            AlConsole.WriteLine(RunCollectorScheme, "Collect Finished Successfully!");
            AlConsole.WriteLine(LauncherInfoScheme, "Set Next Scheduled!");
        }

        private static int CollectStart() {
            Process process = new Process {
                StartInfo = new ProcessStartInfo() {
                    FileName = Settings.Controller,
                    Arguments = $"--user {Settings.DataBaseUserName} --pass {Settings.DataBasePassWord}" + " " +
                                $"{(Settings.DataBaseIsLocal ? "--local true" : string.Empty)}" + " " +
                                $"--ignore {Settings.IgnoreDataArray}",
                    UseShellExecute = true
                }
            };
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }

        private static void Interval() {
            AlConsole.Write(AlStatusEnum.Caution, $"{"Recovery Boot", -15}", $"{"Launcher", -15}", "Interval.");
            for (int i = 0; i < 10; i++) {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.Write("\n");
        }

    }
}