using KeySim.Common;
using KeySim.Common.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KeyboardSim.Model
{
    public class Action : ModelBase
    {
        private const string Separator = "|";

        public string Name { get; set; }

        [JsonProperty("ShortKey")]
        public string JSShortKey { get; set; }
        [JsonProperty("Actions")]
        public string[] JSActions { get; set; }

        [JsonIgnore]
        public string ShortKeydisplay => $"({JSShortKey.Replace(Separator, " + ")})".ToUpper();

        [JsonIgnore]
        public Tuple<uint, uint> ShortKey
        {
            get
            {
                if (string.IsNullOrEmpty(JSShortKey))
                {
                    return null;
                }
                string[] a = JSShortKey.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
                if (a.Length == 2)
                {
                    uint modKey = Utils.ConvertModKey(a[0]);
                    uint key = Utils.ConvertKey(a[1]);
                    return new Tuple<uint, uint>(modKey, key);
                }
                return null;
            }
        }

        [JsonIgnore]
        public Tuple<ActionType, string>[] Actions
        {
            get
            {
                List<Tuple<ActionType, string>> actions = new List<Tuple<ActionType, string>>();
                foreach (var action in JSActions)
                {
                    if (string.IsNullOrEmpty(action) || action.Length < 3)
                    {
                        return null;
                    }

                    Enum.TryParse(action.Substring(0, 1), true, out ActionType actType);
                    actions.Add(new Tuple<ActionType, string>(actType, action.Substring(2)));
                }
                return actions.ToArray();
            }
        }

        #region Action Command
        [JsonIgnore]
        public RelayCommand ActionCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Utils.ExecuteKeyboardCommand(Actions);
                });
            }
        }

        public void Execute()
        {
            ActionCommand.Execute(null);
        }
        #endregion

        internal void RegisterHotKey()
        {
            HotKeyManager.RegisterHotKey(ShortKey.Item1, ShortKey.Item2, Execute);
        }

        internal void UnregisterHotKey()
        {
            HotKeyManager.UnregisterHotKey(ShortKey.Item1, ShortKey.Item2);
        }

    }
}
