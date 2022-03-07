using PcPanelPro;
using PcPanelPro.Enums;
using System.Drawing;

//Not sure if this is only needed on my machine.
var compensateOffByOne = true;

var client = new PcPanelProClient(compensateOffByOne);

//Gets updates on adjustments and clicks:
client.ValueUpdated += (control, value) => Console.WriteLine($"[{control}]: {value}");
client.ClickUpdated += (control, value) => Console.WriteLine($"[{control}]: {value}");

//Request the current state:
client.RequestValueStates();

//client.FullBody.SetStaticColor(Color.Red);
//client.FullBody.SetRainbow(0xFF, 0xFF, 0xFF, false);
//client.FullBody.SetWave(0xFF, 0xFF, 0xFF, false, false);
//client.FullBody.SetBreath(0xFF, 0xFF, 0xFF);
//client.FullBody.RequestColorChange();

//Request changes to the colors of the sliders:
client.Sliders.SetGradientColor(PcPanelProControl.Slider1, Color.Blue, Color.Green);
client.Sliders.SetStaticColor(PcPanelProControl.Slider2, Color.Red);
client.Sliders.SetStaticColor(PcPanelProControl.Slider3, Color.Black);
client.Sliders.SetVolumeGradient(PcPanelProControl.Slider4, Color.Yellow, Color.Turquoise);
client.Sliders.RequestColorChange();

//Request changes to the color of the slider labels:
client.Labels.SetStaticColor(PcPanelProControl.Slider1, Color.Red);
client.Labels.SetStaticColor(PcPanelProControl.Slider2, Color.Black);
client.Labels.SetStaticColor(PcPanelProControl.Slider3, Color.Green);
client.Labels.SetStaticColor(PcPanelProControl.Slider4, Color.Blue);
client.Labels.RequestColorChange();

//Request color changes to the knobs:
client.Knobs.SetStaticColor(PcPanelProControl.Knob1, Color.Red);
client.Knobs.SetVolumeGradient(PcPanelProControl.Knob5, Color.Blue, Color.Green);
client.Knobs.RequestColorChange();

//Request color changed to the logo:
client.Logo.SetSolidColor(Color.Red);
//client.Logo.SetRainbow(0xFF, 0xFF);
//client.Logo.SetBreath(0xFF, 0xFF, 0xFF);
client.Logo.RequestColorChange();

//Keeps the console alive untill you press enter:
Console.ReadLine();
