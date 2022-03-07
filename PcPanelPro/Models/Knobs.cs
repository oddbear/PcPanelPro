using PcPanelPro.Enums;
using System.Drawing;

namespace PcPanelPro.Models
{
    public class Knobs
    {
        private readonly PcPanelProClient _client;
        private byte[,] _colors { get; } = new byte[5, 7];

        public Knobs(PcPanelProClient client)
        {
            _client = client;
        }

        public void SetStaticColor(PcPanelProControl control, Color color)
        {
            var slider = (int)control;
            if (slider < 0 || slider > 5)
                return;

            _colors[slider, 0] = 1; //Static

            _colors[slider, 1] = color.R;
            _colors[slider, 2] = color.G;
            _colors[slider, 3] = color.B;
        }

        public void SetVolumeGradient(PcPanelProControl control, Color zero, Color full)
        {
            var slider = (int)control;
            if (slider < 0 || slider > 4)
                return;

            _colors[slider, 0] = 2; //Volume gradient (positional gradient, set through FW)

            _colors[slider, 1] = zero.R;
            _colors[slider, 2] = zero.G;
            _colors[slider, 3] = zero.B;

            _colors[slider, 4] = full.R;
            _colors[slider, 5] = full.G;
            _colors[slider, 6] = full.B;
        }

        public void RequestColorChange()
        {
            var data = new byte[64];
            data[0] = 5; //Color change request
            data[1] = 2; //Knobs

            for (int slider = 0; slider < 5; slider++)
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
