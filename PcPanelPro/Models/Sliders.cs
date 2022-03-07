using PcPanelPro.Enums;
using System.Drawing;

namespace PcPanelPro.Models
{
    public class Sliders
    {
        private readonly PcPanelProClient _client;
        private byte[,] _colors { get; } = new byte[4, 7];

        public Sliders(PcPanelProClient client)
        {
            _client = client;
        }

        public void SetStaticColor(PcPanelProControl control, Color color)
        {
            SetGradientColor(control, color, color);
        }

        public void SetGradientColor(PcPanelProControl control, Color bottom, Color top)
        {
            var slider = (int)control - 5;
            if (slider < 0 || slider > 3)
                return;

            _colors[slider, 0] = 1; //Static

            _colors[slider, 1] = bottom.R;
            _colors[slider, 2] = bottom.G;
            _colors[slider, 3] = bottom.B;

            _colors[slider, 4] = top.R;
            _colors[slider, 5] = top.G;
            _colors[slider, 6] = top.B;
        }

        public void SetVolumeGradient(PcPanelProControl control, Color bottom, Color top)
        {
            var slider = (int)control - 5;
            if (slider < 0 || slider > 4)
                return;

            _colors[slider, 0] = 3; //Volume gradient (positional gradient, set through FW)

            _colors[slider, 1] = bottom.R;
            _colors[slider, 2] = bottom.G;
            _colors[slider, 3] = bottom.B;

            _colors[slider, 4] = top.R;
            _colors[slider, 5] = top.G;
            _colors[slider, 6] = top.B;
        }

        public void RequestColorChange()
        {
            var data = new byte[64];
            data[0] = 5; //Color change request
            data[1] = 0; //Sliders

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
