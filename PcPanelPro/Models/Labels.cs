using PcPanelPro.Enums;
using System.Drawing;

namespace PcPanelPro.Models
{
    public class Labels
    {
        private readonly PcPanelProClient _client;
        private byte[,] _colors { get; } = new byte[4, 7];

        public Labels(PcPanelProClient client)
        {
            _client = client;
        }

        public void SetStaticColor(PcPanelProControl control, Color color)
        {
            var slider = (int)control - 5;
            if (slider < 0 || slider > 3)
                return;

            _colors[slider, 0] = 1; //Static

            _colors[slider, 1] = color.R;
            _colors[slider, 2] = color.G;
            _colors[slider, 3] = color.B;
        }

        public void RequestColorChange()
        {
            var data = new byte[64];
            data[0] = 5; //Color change request
            data[1] = 1; //Labels

            for (int slider = 0; slider < 4; slider++)
            {
                for (int i = 0; i < 7; i++)
                {
                    var index = slider * 7 + i + 2;
                    data[index] = _colors[slider, i];
                }
            }

            _client.Write(data);
        }
    }
}
