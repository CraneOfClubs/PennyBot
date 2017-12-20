using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TelegaEventsBotDotNet
{
    class Button
    {
        private String _text;
        public String Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        private String _callback;
        public String Callback
        {
            get
            {
                return _callback;
            }
            set
            {
                _callback = value;
            }
        }

    }
    class MessageWithButtons
    {
        private String _text;
        private List<Button> _buttons;
        public String Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public List<Button> Buttons
        {
            get
            {
                return _buttons;
            }
            set
            {
                _buttons = value;
            }
        }

    }
    class SettingsWrapper
    {
        private XmlDocument _settingsDocument;
        private String _xmlFileName;
        public SettingsWrapper(String FileName)
        {
            XmlDocument _settingsDocument = new XmlDocument();
            _xmlFileName = FileName;
            _settingsDocument = new XmlDocument();
            _settingsDocument.Load(_xmlFileName);
        }

        public void ReloadSettings()
        {
            _settingsDocument.Load(_xmlFileName);
        }

        public void ReloadSettings(String FileName)
        {
            _xmlFileName = FileName;
            _settingsDocument.Load(_xmlFileName);
        }

        private MessageWithButtons ParseMessageWithButtons(String MessageName)
        {
            var message = new MessageWithButtons();
            _settingsDocument = new XmlDocument();
            _settingsDocument.Load(_xmlFileName);
            XmlElement xRoot = _settingsDocument.DocumentElement;

            foreach (XmlNode xnode in xRoot)
            {
                // получаем атрибут name
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("name");
                    XmlNode attrType = xnode.Attributes.GetNamedItem("type");
                    if (attr != null && attrType != null)
                        if (attr.Value == MessageName)
                        {
                            List<Button> MessageButtons = new List<Button>();
                            foreach (XmlNode messageParams in xnode.ChildNodes)
                            {

                                if (messageParams.Name == "Text")
                                {
                                    message.Text = messageParams.InnerText;
                                }

                                if (messageParams.Name == "Buttons")
                                {
                                    foreach (XmlNode button in messageParams.ChildNodes)
                                    {
                                        Button CurrentButton = new Button();

                                        foreach (XmlNode buttonParams in button.ChildNodes)
                                        {
                                            if (buttonParams.Name == "Text")
                                                CurrentButton.Text = buttonParams.InnerText;
                                            if (buttonParams.Name == "Callback")
                                                CurrentButton.Callback = buttonParams.InnerText;
                                        }
                                        MessageButtons.Add(CurrentButton);
                                    }
                                }

                            }
                            message.Buttons = MessageButtons;
                        }
                }

            }
            return message;
        }


        private String FormatMarkUp(XmlNode eventNode, RLEvent rLEvent)
        {
            String Ready = "";
            Dictionary<String, String> replaceMacro = new Dictionary<String, String>();
            foreach (XmlNode eventParameter in eventNode)
            {
                if (eventParameter.Name == "Label")
                {
                    replaceMacro.Add(eventParameter.InnerText, rLEvent.Label);
                }
                if (eventParameter.Name == "Text")
                {
                    replaceMacro.Add(eventParameter.InnerText, rLEvent.Description);
                }
                if (eventParameter.Name == "Location")
                {
                    replaceMacro.Add(eventParameter.InnerText, rLEvent.Location);
                }
                if (eventParameter.Name == "DateTime")
                {
                    replaceMacro.Add(eventParameter.InnerText, rLEvent.dateTime.ToString("yyyy-MMMM-dd HH:mm"));
                }
                if (eventParameter.Name == "OptionalParameter1")
                {
                    replaceMacro.Add(eventParameter.InnerText, rLEvent.Optional1);
                }
                if (eventParameter.Name == "OptionalParameter2")
                {
                    replaceMacro.Add(eventParameter.InnerText, rLEvent.Optional2);
                }
                if (eventParameter.Name == "OptionalParameter3")
                {
                    replaceMacro.Add(eventParameter.InnerText, rLEvent.Optional3);
                }
                if (eventParameter.Name == "OptionalParameter4")
                {
                    replaceMacro.Add(eventParameter.InnerText, rLEvent.Optional4);
                }
                if (eventParameter.Name == "OptionalParameter5")
                {
                    replaceMacro.Add(eventParameter.InnerText, rLEvent.Optional5);
                }
                if (eventParameter.Name == "OptionalParameter6")
                {
                    replaceMacro.Add(eventParameter.InnerText, rLEvent.Optional6);
                }
                if (eventParameter.Name == "MarkUp")
                {
                    Ready = eventParameter.InnerText;
                }
            }
            foreach(KeyValuePair<String, String> entry in replaceMacro)
            {
                Ready = Ready.Replace(entry.Key, entry.Value);
            }
            return Ready;
        }

        public String ParseEvent(RLEvent rLEvent)
        {
            var message = new MessageWithButtons();
            _settingsDocument = new XmlDocument();
            _settingsDocument.Load(_xmlFileName);
            String MarkedUpText = "";
            XmlElement xRoot = _settingsDocument.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                // получаем атрибут name
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("name");
                    XmlNode attrType = xnode.Attributes.GetNamedItem("type");
                    if (attr != null && attrType != null)
                        if (attr.Value == "ConcreteEvent")
                        {
                            List<Button> MessageButtons = new List<Button>();
                            foreach (XmlNode events in xnode.ChildNodes[0].ChildNodes)
                            {
                                XmlNode attrTypeEvent = events.Attributes.GetNamedItem("markup_type");
                                if (attr != null)
                                {
                                    if (events.Name == "Event" && attrTypeEvent.Value == rLEvent.MarkupType)
                                    {
                                        MarkedUpText = FormatMarkUp(events, rLEvent);
                                    }
                                }
                            }
                            message.Buttons = MessageButtons;
                        }
                }

            }
            return MarkedUpText;
        }

        public MessageWithButtons StartSearchMessage()
        {
            return ParseMessageWithButtons("StartSearchMessage");
        }

        public MessageWithButtons SearchNearbyDate()
        {
            return ParseMessageWithButtons("SearchNearbyDate");
        }
    }
}
