using HidLibrary;
using PcPanelPro.Enums;
using PcPanelPro.Models;

namespace PcPanelPro
{
    public class PcPanelProClient
    {
        public delegate void AdjustmentEvent(PcPanelProControl control, byte value);
        public delegate void ClickEvent(PcPanelProControl control, PcPanelProClickValue value);

        private readonly HidDevice _hidDevice;
        private readonly Thread _thread;
        private readonly bool _compensateOffByOne;

        public event AdjustmentEvent? ValueUpdated;
        public event ClickEvent? ClickUpdated;

        public Sliders Sliders { get; }
        public Logo Logo { get; }
        public Labels Labels { get; }
        public Knobs Knobs { get; }
        public FullBody FullBody { get; }

        public PcPanelProClient(bool compensateOffByOne)
        {
            _compensateOffByOne = compensateOffByOne;
            _hidDevice = HidDevices.Enumerate(0x483, 0xa3c5).First();

            Sliders = new Sliders(this);
            Logo = new Logo(this);
            Labels = new Labels(this);
            Knobs = new Knobs(this);
            FullBody = new FullBody(this);

            _thread = new Thread(ThreadListener) { IsBackground = true };
            _thread.Start();
        }

        private void ThreadListener()
        {
            while (true)
            {
                var data = Read();

                //01: adjustment
                //02: click
                if (!TrySetEnum<PcPanelProAction>(data[0], out var action))
                    continue;

                //00-04: k1-k5
                //05-08: s1-s4
                if (!TrySetEnum<PcPanelProControl>(data[1], out var control))
                    continue;

                if (action == PcPanelProAction.Click)
                {
                    //00: Up
                    //01: Down
                    if (!TrySetEnum<PcPanelProClickValue>(data[2], out var value))
                        continue;

                    ClickUpdated?.Invoke(control, value);
                }
                else
                {
                    var value = data[2];
                    ValueUpdated?.Invoke(control, value);
                }
            }
        }

        /// <summary>
        /// Gets the current value/position of all sliders and knobs.
        /// </summary>
        public void RequestValueStates()
        {
            var data = new byte[64];
            data[0] = 1;

            Write(data);
        }

        public byte[] Read()
        {
            if (!_compensateOffByOne)
                return _hidDevice.Read().Data;

            //For some reason theere is a off-by-one issue at my computer... need to skip the first value on read:
            var data = _hidDevice.Read().Data;
            var buffer = new byte[64];
            Array.Copy(data, 1, buffer, 0, 64);
            return buffer;
        }

        public void Write(byte[] data)
        {
            if (!_compensateOffByOne)
            {
                _hidDevice.Write(data, 1000);
                return;
            }

            //For some reason theere is a off-by-one issue at my computer... need to write with '0' byte before buffer:
            var buffer = new byte[65];
            Array.Copy(data, 0, buffer, 1, 64);
            _hidDevice.Write(buffer, 1000);
        }

        private bool TrySetEnum<TEnum>(object value, out TEnum enumValue)
            where TEnum : struct, Enum
        {
            if (Enum.IsDefined(typeof(TEnum), value))
            {
                enumValue = (TEnum)Enum.ToObject(typeof(TEnum), value);
                return true;
            }
            else
            {
                enumValue = default;
                Console.WriteLine($"Enum '{typeof(TEnum).Name}' unknown value '{value}'");
                return false;
            }
        }
    }
}
