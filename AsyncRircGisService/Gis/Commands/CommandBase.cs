﻿using System;
using AsyncRircGisService.Gis.Configurations.Options;

namespace AsyncRircGisService.Gis.Commands
{
    public abstract class CommandBase<TOption> : ICommand
        where TOption : OptionBase
    {
        protected readonly TOption Option;

        protected abstract void OnExecute(TOption option);

        protected CommandBase(TOption option)
        {
            Option = option;
        }

        public void Execute()
        {
            try
            {
                OnExecute(Option);
            }
            catch (Exception ex)
            {
                Error($"Ошибка выполнения команды: {ex.Message}", ex);
            }
        }

        protected void Error(string message, Exception ex = null)
        {

            if (ex != null && Option.Verbose)
            {
                message += $"\n\rStackTrace:{ex.StackTrace}";
            } 
            PrintMessage(message, ConsoleColor.Red);
        } 
        protected void Info(string message) => PrintMessage(message, canWrite: Option.Verbose);
        protected void Warning(string message) => PrintMessage(message, ConsoleColor.Yellow);
        protected void Success(string message) => PrintMessage(message, ConsoleColor.Green);

        private static void PrintMessage(string message, ConsoleColor color = ConsoleColor.White, bool canWrite = true)
        {
            if (canWrite)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }
}
