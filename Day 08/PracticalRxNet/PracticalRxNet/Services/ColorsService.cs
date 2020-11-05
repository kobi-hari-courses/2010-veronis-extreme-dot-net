using Newtonsoft.Json.Linq;
using PracticalRxNet.Models;
using PracticalRxNet.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace PracticalRxNet.Services
{
    public class ColorsService
    {
        public static ColorsService Instance { get; } = new ColorsService();

        private static List<ColorRecord> _allColors;

        static ColorsService()
        {
            var json = Resources.ColorsJson;
            var tokens = JObject.Parse(json) as IEnumerable<KeyValuePair<string, JToken>>;

            _allColors = tokens
                .Select(pair => new ColorRecord(
                    displayName: pair.Value.ToString(),
                    color: _fromHex(pair.Key)
                    ))
                .ToList();
        }

        private ColorsService()
        {

        }

        private static Color _fromHex(string hex)
        {
            return (Color)ColorConverter.ConvertFromString(hex);
        }

        public async Task<List<ColorRecord>> Search(string keyword)
        {
            Debug.WriteLine("Started search for " + keyword);
            await Task.Delay(3000);

            if (string.IsNullOrWhiteSpace(keyword)) return new List<ColorRecord>();

            var res = _allColors
                .Where(clr => clr.DisplayName.ToLower().Contains(keyword.ToLower()))
                .ToList();

            Debug.WriteLine("Completed search for " + keyword);
            return res;
        }
    }
}
