using System;
using UnityEngine;

namespace Desdiene
{
    public class Logger
    {
        private readonly string _context;

        public Logger(Type context)
        {
            _context = context.ToString();
        }

        public Logger(string context)
        {
            _context = context;
        }

        public void Log(string tag, string message) => Log(GetFormattedTag(tag) + message);

        public void Log(string message) => Debug.Log(GetContextLog() + message);

        public void LogWarning(string tag, string message) => LogWarning(GetFormattedTag(tag) + message);

        public void LogWarning(string message) => Debug.LogWarning(GetContextLog() + message);

        public void LogError(string tag, string message) => LogError(GetFormattedTag(tag) + message);

        public void LogError(string message) => Debug.LogError(GetContextLog() + message);

        private string GetContextLog() => GetFormattedTag(_context);

        private string GetFormattedTag(string tag) => $"[{tag}] ";
    }
}
