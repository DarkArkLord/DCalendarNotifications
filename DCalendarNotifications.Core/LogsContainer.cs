using System;
using System.Collections.Generic;

namespace DCalendarNotifications.Core
{
    public class LogsContainer
    {
        private int maxRecordsCount = 100;
        private LinkedList<string> logs = new LinkedList<string>();

        private void RemoveLogsAfterMax()
        {
            while (logs.Count > maxRecordsCount)
            {
                logs.RemoveLast();
            }
        }

        public void SetMaxRecordsCount(int maxRecordsCount)
        {
            this.maxRecordsCount = maxRecordsCount;
            RemoveLogsAfterMax();
        }

        public void AddLog(string message)
        {
            logs.AddFirst(message);
            RemoveLogsAfterMax();
        }

        public string GetLogs()
        {
            return string.Join(Environment.NewLine, logs);
        }
    }
}
