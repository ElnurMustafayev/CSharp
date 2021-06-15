using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Multithreading {
    public class Department {
        // all processes' actions
        public List<Action> actions;

        // BEHAVIOR for DoStuff() method
        public enum BEHAVIOR { AWAIT, SYNC, PARALLEL};
        public BEHAVIOR behavior;

        public Department(BEHAVIOR behavior, params Action[] actions) {
            this.actions = new List<Action>(actions);
            this.behavior = behavior;
        }

        // Execute all actions depending on the behavior
        public void DoStuff() {
            switch(this.behavior) {
                case BEHAVIOR.AWAIT:
                    this.DoStuffWithAwait();
                    break;
                case BEHAVIOR.SYNC:
                    this.DoStuffSync();
                    break;
                case BEHAVIOR.PARALLEL:
                    this.DoStuffParallel();
                    break;
                default:
                    Console.WriteLine($"No realization for '{this.behavior}' behaviour!");
                    break;
            }
        }

        // multithread,     async, step by step
        private async void DoStuffWithAwait() {
            foreach(var action in actions)
                await this.DoTaskAsync(action);
        }
        // singlethread,    sync, step by step
        private void DoStuffSync() => this.actions.ForEach(a => a?.Invoke());
        // multithread,     parallel, many threads in one time
        private void DoStuffParallel() => this.actions.ForEach(a => this.DoTaskAsync(a));

        // method for "awaiting" actions from the outside
        protected Task DoTaskAsync(Action action) => Task.Run(action);
    }
}