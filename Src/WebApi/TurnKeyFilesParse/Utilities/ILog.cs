using System;

namespace Api.Utilities
{
    /// <summary>
    /// ILog 的摘要描述。
    /// </summary>
    public interface ILog
    {
        string Subject
        {
            get;
        }

        string ToString();

    }

    public interface ILog2 : ILog
    {
        string GetFileName(string currentLogPath, string qName, ulong key);
    }
}
