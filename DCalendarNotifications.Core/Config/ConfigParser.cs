using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DCalendarNotifications.Core.Config
{
    public static class ConfigParser
    {
        public static XDocument ReadXml(string path)
        {
            var file = File.ReadAllText(path);
            var xDocument = XDocument.Parse(file);
            return xDocument;
        }

        public static ConfigData Parse(XDocument xml)
        {
            var configNode = GetConfigNode(xml);
            var config = new ConfigData();

            SetSourceValue(configNode, config);
            SetTimersValues(configNode, config);
            SetNotificationOffsetsValues(configNode, config);

            return config;
        }

        private static XElement GetConfigNode(XDocument xml)
        {
            if (xml is null)
            {
                throw new ArgumentNullException(nameof(xml));
            }

            var configNode = xml.Element("Config");
            if (configNode is null)
            {
                throw new Exception($"Секция \"Config\" не обнаружена.");
            }

            return configNode;
        }

        private static void SetSourceValue(XElement configNode, ConfigData config)
        {
            var sourceElement = configNode.Element("Source");
            if (sourceElement is null)
            {
                throw new Exception($"Секция \"Source\" не обнаружена.");
            }

            var elementValue = sourceElement.Value?.Trim();
            if (string.IsNullOrEmpty(elementValue))
            {
                throw new Exception($"Секция \"Source\" не заполнена.");
            }

            if (Uri.TryCreate(elementValue, UriKind.Absolute, out var createdUri))
            {
                config.Source = createdUri!;
            }
            else
            {
                throw new Exception($"Значение \"Source\" не может быть преобразовано в ссылку ({elementValue}).");
            }
        }

        private static void SetTimersValues(XElement configNode, ConfigData config)
        {
            var timersNode = configNode.Element("Timers");
            if (timersNode is null)
            {
                throw new Exception($"Секция \"Timers\" не обнаружена.");
            }

            SetUpdateIntervalValue(timersNode, config);
            SetReminderIntervalValue(timersNode, config);
        }

        private static void SetUpdateIntervalValue(XElement timersNode, ConfigData config)
        {
            var updateIntervalString = timersNode.Attribute("UpdateInterval")?.Value?.Trim();
            if (string.IsNullOrEmpty(updateIntervalString))
            {
                throw new Exception($"Атрибут \"UpdateInterval\" секции \"Timers\" не заполнена.");
            }

            if (int.TryParse(updateIntervalString, out int updateInterval))
            {
                config.UpdateInterval = updateInterval;
            }
            else
            {
                throw new Exception($"Атрибут \"UpdateInterval\" секции \"Timers\" не может быть преобразован в число ({updateIntervalString}).");
            }
        }

        private static void SetReminderIntervalValue(XElement timersNode, ConfigData config)
        {
            var reminderIntervalString = timersNode.Attribute("ReminderInterval")?.Value?.Trim();
            if (string.IsNullOrEmpty(reminderIntervalString))
            {
                throw new Exception($"Атрибут \"ReminderInterval\" секции \"Timers\" не заполнена.");
            }

            if (int.TryParse(reminderIntervalString, out int reminderInterval))
            {
                config.ReminderInterval = reminderInterval;
            }
            else
            {
                throw new Exception($"Атрибут \"ReminderInterval\" секции \"Timers\" не может быть преобразован в число ({reminderIntervalString}).");
            }
        }

        private static void SetNotificationOffsetsValues(XElement configNode, ConfigData config)
        {
            var offsetsListNode = configNode.Element("NotificationTimeOffsetsList");
            if (offsetsListNode is null)
            {
                throw new Exception($"Секция \"NotificationTimeOffsetsList\" не обнаружена.");
            }

            var offsetsListNodeChildNodes = offsetsListNode.Elements();
            if (offsetsListNodeChildNodes is null)
            {
                throw new Exception($"В секции \"NotificationTimeOffsetsList\" нет вложеных элементов.");
            }

            var notOffsetsNodes = offsetsListNodeChildNodes
                .Where(node => node.Name != "TimeOffset")
                .ToArray();
            if (notOffsetsNodes is not null && notOffsetsNodes.Length > 0)
            {
                var nodesNames = notOffsetsNodes.Select(node => $"\"${node.Name}\"");
                var nodesNamesText = string.Join(", ", nodesNames);
                throw new Exception($"В секции \"NotificationTimeOffsetsList\" есть вложеные элементы, которые не являются \"TimeOffset\": {nodesNamesText}.");
            }

            var offsetsNodes = offsetsListNodeChildNodes
                .Where(node => node.Name == "TimeOffset")
                .ToArray();
            if (offsetsNodes is null || offsetsNodes.Length < 1)
            {
                throw new Exception($"В секции \"NotificationTimeOffsetsList\" нет элементов \"TimeOffset\".");
            }

            var offsetsList = (config.NotificationOffsets as List<int>)!;

            foreach (var node in offsetsNodes)
            {
                AddNotificationOffsetValue(node, offsetsList);
            }

            offsetsList.Sort();
        }

        private static void AddNotificationOffsetValue(XElement offsetNode, List<int> offsetsList)
        {
            if (offsetNode is null)
            {
                throw new ArgumentNullException(nameof(offsetNode));
            }

            var hours = GetNotificationOffsetAttribute(offsetNode, "Hours");
            var minutes = GetNotificationOffsetAttribute(offsetNode, "Minutes");
            var seconds = GetNotificationOffsetAttribute(offsetNode, "Seconds");

            var totalOffset = hours * 60 * 60 + minutes * 60 + seconds;

            offsetsList.Add(totalOffset);
        }

        private static int GetNotificationOffsetAttribute(XElement offsetNode, string attrName)
        {
            var attrValueString = offsetNode.Attribute(attrName)?.Value?.Trim();
            if (string.IsNullOrEmpty(attrValueString))
            {
                throw new Exception($"Атрибут \"{attrName}\" объекта \"TimeOffset\" секции \"NotificationTimeOffsetsList\" не заполнен.");
            }

            if (!int.TryParse(attrValueString, out int attrValue))
            {
                throw new Exception($"Атрибут \"{attrName}\" объекта \"TimeOffset\" секции \"NotificationTimeOffsetsList\" не может быть преобразован в число ({attrValueString}).");
            }

            return attrValue;
        }
    }
}
