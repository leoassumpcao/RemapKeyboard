using RemapKeyboard.Enums;
using RemapKeyboard.Hotkeys;
using System.Xml;

namespace RemapKeyboard
{
    internal static class XmlReader
    {
        public static List<IHotkey> GetHotkeysFromFile(string filePath)
        {
            var result = new List<IHotkey>();

            XmlDocument doc = new();
            doc.Load(filePath);

            var elemSettings = doc["Hotkeys"];
            if (elemSettings is not null)
            {
                var hotkeysNode = elemSettings.ChildNodes;
                foreach (XmlNode node in hotkeysNode)
                {
                    var hotkey = GetHotkey(node);
                    if (hotkey is null)
                        continue;

                    result.Add(hotkey);
                }
            }

            return result;
        }

        private static IHotkey GetHotkey(XmlNode node)
        {
            var vkeys = GetHotkeyVKeys(node);
            var value = GetHotkeyValue(node);

            var keyTypeAttr = node.Attributes["type"];
            return keyTypeAttr is null || string.IsNullOrEmpty(keyTypeAttr.Value)
                ? null
                : (keyTypeAttr.Value.ToLower() switch
                {
                    "text" => new TextHotkey(vkeys, value),
                    "keypress" => new KeyHotkey(vkeys, GetHotkeyVKeys(value)),
                    _ => throw new Exception($"Invalid hotkey type: {keyTypeAttr.Value}"),
                });
        }

        private static List<VKeys> GetHotkeyVKeys(XmlNode node)
        {
            var vkeys = new List<VKeys>();

            var keysAttribute = node.Attributes["keys"];
            return keysAttribute is null
                ? throw new Exception($"Could not find keys attribute on node {node.Value}")
                : GetHotkeyVKeys(keysAttribute.Value);
        }

        private static List<VKeys> GetHotkeyVKeys(string keys)
        {
            var vkeys = new List<VKeys>();

            var keysSplitted = keys.Split('+', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var key in keysSplitted)
            {
                if (!Enum.TryParse<VKeys>(key, out var vkey))
                    throw new Exception($"Invalid vkey {key} on keys attribute: {keys}");

                vkeys.Add(vkey);
            }

            return vkeys;
        }

        private static string GetHotkeyValue(XmlNode node)
        {
            var attr = node.Attributes["value"];
            return attr is null
                ? throw new Exception($"Could not find value attribute on node {node.Value}")
                : string.IsNullOrEmpty(attr.Value) ? throw new Exception($"Value attribute cannot be empty. Node {node.Value}") : attr.Value;
        }
    }
}
