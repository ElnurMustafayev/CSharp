using System;
using System.Collections.Generic;
using System.Threading;

namespace Multithreading {
    class Program {
        // process item
        class TaskList {
            // will be filled during the process
            public List<int> List = new List<int>();
            // number of taсеs
            public int Final;

            public TaskList(int final) => this.Final = final;
        }

        // each task -> each process and progress bar in console
        static TaskList[] tasks = {
                new TaskList(50),
                new TaskList(38),
                new TaskList(44),
                new TaskList(23),
                new TaskList(13),
            };

        static void Main() {
            // set behaviour (parallel, async, ...)
            var behavior = Department.BEHAVIOR.PARALLEL;

            // for optimization:
            // if behavior is parallel -> only the first action will print all progress bars
            Action toprint = null;
            if(behavior != Department.BEHAVIOR.PARALLEL)
                toprint = () => PrintLists();

            // actions which will be called depending on the behavior
            Action[] actions = new Action[] {
                // init actions
                () => AddNumbers(tasks[0], PrintLists),
                () => AddNumbers(tasks[1], toprint),
                () => AddNumbers(tasks[2], toprint, 150),
                () => AddNumbers(tasks[3], toprint),
                () => AddNumbers(tasks[4], toprint, 400),
            };

            #region DOESN'T WORK !!! TOFIX
            //actions.Add(() => AddNumbers(tasks[0], PrintLists));
            //for(int i = 1; i < tasks.Length; i++)
            //    actions.Add(() => AddNumbers(tasks[i], toprint));
            #endregion

            // create department and throw actions there
            Department department = new Department(behavior, actions);

            // execute all actions
            department.DoStuff();

            // bcz parallel execution
            Console.ReadKey();
        }

        // the main method of each process
        static void AddNumbers(TaskList task, Action action = null, int milliseconds = 100) {
            for(int i = 0; i < task.Final; i++) {
                // some stuff... (nothing serious)
                task.List.Add(i);

                // calling an additional method during the main process if it exists
                action?.Invoke();

                // slowing down the process
                Thread.Sleep(milliseconds);
            }
        }

        // prints all processes
        static void PrintLists() {
            Console.Clear();
            for(int i = 0; i < tasks.Length; i++)
                PrintList(tasks[i], $"{i + 1} Thread: \t");
        }

        // prints each processes
        static void PrintList(TaskList task, string message = "") {
            Console.Write(message);
            int percent = (100 * task.List.Count) / task.Final;
            PrintProgressBar(percent);
            Console.WriteLine();
        }

        // prints progress bar for given process
        static void PrintProgressBar(int percent, int barSize = 4) {
            Console.ForegroundColor = percent == 100 ? ConsoleColor.Green : ConsoleColor.Yellow;
            Console.Write($"{percent} % \t");

            // progress bar
            percent /= barSize;
            Console.Write('[');
            for(int i = 0; i < (100 / barSize); i++)
                Console.Write(i < percent ? '|' : '.');
            Console.Write(']');

            Console.ResetColor();
        }
    }
}