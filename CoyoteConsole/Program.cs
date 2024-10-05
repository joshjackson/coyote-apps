using CoyoteLinux.Terminal;

namespace CoyoteConsole {

    public enum ConsoleRunState {
        rsDefault,
        rsAdmin,
        rsNetConfig,
        rsMonitor,
        rsAdminMonitor,
        rsReboot,
        rsShutdown
    }

    public static class RunState {

        public static Stack<ConsoleRunState> RunStack = new Stack<ConsoleRunState>();

        public static ConsoleRunState Current {
            get { return RunStack.Peek(); }
        }
        public static ConsoleRunState Previous;

        public static void PushState(ConsoleRunState newState) {
            Previous = Current;
            if (newState != Current) {
                RunStack.Push(newState);
            }
        }

        public static void PopState() {
            if (RunStack.Count > 1) {
                Previous = RunStack.Pop();
            } else {
                ResetState();
            }
        }

        public static void SetState(ConsoleRunState newState) {
            Previous = Current;
            RunStack.Push(newState);
        }

        public static void ResetState() {
            RunStack.Clear();
            RunStack.Push(ConsoleRunState.rsDefault);
        }
    }


    internal class Program {
        static int Main(string[] args) {
            Application.Init(false);
            Application.Timeout = 1000;
            Application.Iteration += HandleApplicationIteration;

            DefaultConsoleFrame fCoyote = new DefaultConsoleFrame();
            AdminConsoleFrame adminFrame = new AdminConsoleFrame();
            NetworkConfigFrame netConfigFrame = new NetworkConfigFrame();
            StatusConsoleFrame statusFrame = new StatusConsoleFrame(false);
            StatusConsoleFrame adminStatusFrame = new StatusConsoleFrame(true);

            RunState.ResetState();

            // Primary console runstate loop
            try {
                while (true) {
                    switch (RunState.Current) {
                        case ConsoleRunState.rsAdmin:
                            Application.Run(adminFrame);
                            break;
                        case ConsoleRunState.rsNetConfig:
                            Application.Run(netConfigFrame);
                            break;
                        case ConsoleRunState.rsMonitor:
                            Application.Run(statusFrame);
                            RunState.PopState();
                            break;
                        case ConsoleRunState.rsAdminMonitor:
                            Application.Run(adminStatusFrame);
                            RunState.PopState();
                            break;
                        case ConsoleRunState.rsDefault:
                            Application.Run(fCoyote);
                            break;
                        case ConsoleRunState.rsReboot:
                            return 1;
                        case ConsoleRunState.rsShutdown:
                            return 2;
                    }
                }
            } catch {
                return 255;
            }
        }

        static void HandleApplicationIteration(object sender, EventArgs e) {

        }
    }
}
