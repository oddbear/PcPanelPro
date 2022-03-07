using System.Drawing;

namespace PcPanelPro.Models
{
    public class Logo
    {
        private readonly PcPanelProClient _client;
        private byte[] _colors = new byte[7];

        public Logo(PcPanelProClient client)
        {
            _client = client;
        }

        public void SetSolidColor(Color color)
        {
            var data = _colors;
            data[0] = 1; //Static

            data[1] = color.R;
            data[2] = color.G;
            data[3] = color.B;
        }

        public void SetRainbow(byte brightness, byte speed)
        {
            var data = _colors;
            data[0] = 2; //Rainbow

            data[1] = 0xFF;
            data[2] = brightness;
            data[3] = speed;
        }

        public void SetBreath(byte hue, byte brightness, byte speed)
        {
            var data = _colors;
            data[0] = 3; //Breath

            //You can use something like this, to get the spectrum (should just need a 0-360 to 0-255 conversion):
            //https://stackoverflow.com/questions/359612/how-to-convert-rgb-color-to-hsv/1626175#1626175
            //Or use this that seems to be a little more straight forward:
            //http://james-ramsden.com/convert-from-hsl-to-rgb-colour-codes-in-c/
            data[1] = hue;
            data[2] = 0xFF;
            data[3] = brightness;
            data[4] = speed;
        }

        public void RequestColorChange()
        {
            var data = new byte[64];
            data[0] = 5; //Color change request
            data[1] = 3; //Logo

            Array.Copy(_colors, 0, data, 2, 7);

            _client.Write(data);
        }
    }
}
