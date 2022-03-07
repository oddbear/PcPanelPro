# PCPanel Pro SDK (Unofficial)

This project shows how to interact with the PCPanel Pro device.

This way we can make plugins / applicaitons to be used with this device.

On time of developing the official PCPanel Software was in version 2.1.1

### Warning 1:

Don't expect it work together with the **official PCPanel Software**, or other applications using the SDK.<br />
The colors will **not sync**. You can listen on sliders and knobs values, but if you set colors, it will get out of sync.

### Warning 2:
I get a off by one error in the buffer when reading and writing.<br />
Not sure is this is only on my machine, but if you get the same, adjust the compensateOffByOne constructor value.

### Supports:

- Requisting Fader/Knobs positions/values 
- Receiving Fader/Knobs position/value
- Receiving Knobs push up/down event
- Setting Full Body colors
- Setting Slider colors
- Setting Knobs colors
- Setting Label colors
- Setting Logo colors

## How it works

The PCPanel Pro device is recognized as a HID device. <br />
Therefor it's quite straightforward to read and write events.
