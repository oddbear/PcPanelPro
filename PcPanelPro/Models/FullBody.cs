using System.Drawing;

namespace PcPanelPro.Models
{
    public class FullBody
    {
        private readonly PcPanelProClient _client;
        private byte[] _colors = new byte[7];

        public FullBody(PcPanelProClient client)
        {
            _client = client;
        }

        public void SetStaticColor(Color color)
        {
            _colors[0] = 2; //Static

            _colors[1] = color.R;
            _colors[2] = color.G;
            _colors[3] = color.B;
        }

        public void SetRainbow(byte phaseShift, byte brightness, byte speed, bool reverseDirection)
        {
            _colors[0] = 1; //Rainbow

            _colors[1] = phaseShift;
            _colors[2] = 0xFF;
            _colors[3] = brightness;
            _colors[4] = speed;

            _colors[5] = Convert.ToByte(reverseDirection);
        }

        public void SetWave(byte hue, byte brightness, byte speed, bool reverseDirection, bool bounce)
        {
            _colors[0] = 3; //Wave

            _colors[1] = hue;
            _colors[2] = 0xFF;
            _colors[3] = brightness;
            _colors[4] = speed;
            _colors[6] = Convert.ToByte(reverseDirection);
            _colors[7] = Convert.ToByte(bounce);
        }

        public void SetBreath(byte hue, byte brightness, byte speed)
        {
            _colors[0] = 4; //Breath

            _colors[1] = hue;
            _colors[2] = 0xFF;
            _colors[3] = brightness;
            _colors[4] = speed;
        }

        public void RequestColorChange()
        {
            var data = new byte[64];
            data[0] = 5; //Color change request
            data[1] = 4; //Full body

            Array.Copy(_colors, 0, data, 2, 7);

            _client.Write(data);
        }
    }
}
