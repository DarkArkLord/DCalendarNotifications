using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DCalendarNotifications.Core.Config
{
    public static class ConfigParser
    {
        /// <summary>
        /// Считывание XML-документа
        /// </summary>
        /// <param name="path">Путь к документу</param>
        /// <returns>XML-документ</returns>
        public static XDocument ReadXml(string path)
        {
            // Xml не умеет обрабатываеть "&", поэтому заменяем их на "&amp;"
            var file = File.ReadAllText(path).Replace("&", "&amp;");
            var xDocument = XDocument.Parse(file);
            return xDocument;
        }

        /// <summary>
        /// Парсинг XML-документа с конфигурацией
        /// </summary>
        /// <param name="xml">XML-документ с конфигурацией</param>
        /// <returns>Конфигурация DCalendarNotifications</returns>
        public static ConfigData Parse(XDocument xml)
        {
            var configNode = GetConfigNode(xml);
            var config = new ConfigData();

            SetSourceValue(configNode, config);
            SetRequestsValue(configNode, config);
            SetIntervalsValues(configNode, config);
            SetMaxLogsCountValue(configNode, config);
            SetNotificationOffsetsValues(configNode, config);

            return config;
        }

        // Считывание основной секции
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

        // Установка поля источника данных 
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

        // Установка параметров запроса 
        private static void SetRequestsValue(XElement configNode, ConfigData config)
        {
            var requestsElement = configNode.Element("Requests");
            if (requestsElement is null)
            {
                throw new Exception($"Секция \"Requests\" не обнаружена.");
            }

            SetRequestsTriesCountValue(requestsElement, config);
            SetRequestDelayValue(requestsElement, config);
        }

        // Установка количества запросов
        private static void SetRequestsTriesCountValue(XElement requestsElement, ConfigData config)
        {
            var requestsTriesCountString = requestsElement.Attribute("RequestsTriesCount")?.Value?.Trim();
            if (string.IsNullOrEmpty(requestsTriesCountString))
            {
                throw new Exception($"Атрибут \"RequestsTriesCount\" секции \"Requests\" не заполнен.");
            }

            if (int.TryParse(requestsTriesCountString, out int requestsTriesCount))
            {
                if (requestsTriesCount > 0)
                {
                    config.RequestsTriesCount = requestsTriesCount;
                }
                else
                {
                    throw new Exception($"Атрибут \"RequestsTriesCount\" секции \"Requests\" должен быть натуральным числом ({requestsTriesCountString}).");
                }
            }
            else
            {
                throw new Exception($"Атрибут \"RequestsTriesCount\" секции \"Requests\" не может быть преобразован в число ({requestsTriesCountString}).");
            }
        }

        // Установка задержки запросов
        private static void SetRequestDelayValue(XElement requestsElement, ConfigData config)
        {
            var requestDelayString = requestsElement.Attribute("RequestDelay")?.Value?.Trim();
            if (string.IsNullOrEmpty(requestDelayString))
            {
                throw new Exception($"Атрибут \"RequestDelay\" секции \"Requests\" не заполнен.");
            }

            if (int.TryParse(requestDelayString, out int requestDelay))
            {
                if (requestDelay >= 0)
                {
                    config.RequestDelay = requestDelay;
                }
                else
                {
                    throw new Exception($"Атрибут \"RequestDelay\" секции \"Requests\" должен быть неотрицательным числом ({requestDelayString}).");
                }
            }
            else
            {
                throw new Exception($"Атрибут \"RequestDelay\" секции \"Requests\" не может быть преобразован в число ({requestDelayString}).");
            }
        }

        // Установка полей интервалов
        private static void SetIntervalsValues(XElement configNode, ConfigData config)
        {
            var timersNode = configNode.Element("Timers");
            if (timersNode is null)
            {
                throw new Exception($"Секция \"Timers\" не обнаружена.");
            }

            SetUpdateIntervalValue(timersNode, config);
            SetReminderIntervalValue(timersNode, config);
        }

        // Установка интервала обновления данных
        private static void SetUpdateIntervalValue(XElement timersNode, ConfigData config)
        {
            var updateIntervalString = timersNode.Attribute("UpdateInterval")?.Value?.Trim();
            if (string.IsNullOrEmpty(updateIntervalString))
            {
                throw new Exception($"Атрибут \"UpdateInterval\" секции \"Timers\" не заполнен.");
            }

            if (int.TryParse(updateIntervalString, out int updateInterval))
            {
                if (updateInterval > 0)
                {
                    config.UpdateInterval = updateInterval;
                }
                else
                {
                    throw new Exception($"Атрибут \"UpdateInterval\" секции \"Timers\" должен быть натуральным числом ({updateIntervalString}).");
                }
            }
            else
            {
                throw new Exception($"Атрибут \"UpdateInterval\" секции \"Timers\" не может быть преобразован в число ({updateIntervalString}).");
            }
        }

        // Установка интервала проверки выдачи уведомлений
        private static void SetReminderIntervalValue(XElement timersNode, ConfigData config)
        {
            var reminderIntervalString = timersNode.Attribute("ReminderInterval")?.Value?.Trim();
            if (string.IsNullOrEmpty(reminderIntervalString))
            {
                throw new Exception($"Атрибут \"ReminderInterval\" секции \"Timers\" не заполнена.");
            }

            if (int.TryParse(reminderIntervalString, out int reminderInterval))
            {
                if (reminderInterval > 0)
                {
                    config.ReminderInterval = reminderInterval;
                }
                else
                {
                    throw new Exception($"Атрибут \"ReminderInterval\" секции \"Timers\" должен быть натуральным числом ({reminderIntervalString}).");
                }
            }
            else
            {
                throw new Exception($"Атрибут \"ReminderInterval\" секции \"Timers\" не может быть преобразован в число ({reminderIntervalString}).");
            }
        }

        // Установка максимального количества записей для журнала логов
        private static void SetMaxLogsCountValue(XElement configNode, ConfigData config)
        {

            var maxLogsCountElement = configNode.Element("MaxLogsCount");
            if (maxLogsCountElement is null)
            {
                throw new Exception($"Секция \"MaxLogsCount\" не обнаружена.");
            }

            var maxLogsCountString = maxLogsCountElement.Value?.Trim();
            if (string.IsNullOrEmpty(maxLogsCountString))
            {
                throw new Exception($"Секция \"MaxLogsCount\" не заполнена.");
            }

            if (int.TryParse(maxLogsCountString, out int maxLogsCount))
            {
                if (maxLogsCount > 0)
                {
                    config.MaxLogsCount = maxLogsCount;
                }
                else
                {
                    throw new Exception($"Значение секции \"MaxLogsCount\" должно быть натуральным числом ({maxLogsCount}).");
                }
            }
            else
            {
                throw new Exception($"Значение секции \"MaxLogsCount\" не может быть преобразовано в число ({maxLogsCountString}).");
            }
        }

        // Установка временных сдвигов для выдачи уведомлений
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
        }

        // Расчет и добавление временного сдвига
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

        // Считывание атрибута временного сдвига
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
