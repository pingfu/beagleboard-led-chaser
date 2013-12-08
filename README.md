beagleboard-led-chaser
======================

This is a simple "Hello World" test program which demonstrates hardware control over BeagleBoard Black using C# and Mono to blink the four USER LEDs.

## Ingredients

 - BeagleBoard Black
 - Angstorm, or Ubuntu ([Installing Ubuntu On BeagleBone Black][0])
 - Mono

## Instructions

I baked this in Visual Studio under Windows, and used WinSCP to copy the code over to the BeagleBoard using SSH, whilst USB tethered.

Installing Mono is straight forward with apt. I tend to install `mono-gmcs` `mono-mcs` `mono-xsp4` `mono-xsp4-base` `mono-devel` `mono-xsp4` `mono-csharp-shell`. XSP makes a handy ASP.NET webserver.

```
$ sudo apt-get install mono-gmcs mono-mcs mono-xsp4 mono-xsp4-base mono-devel mono-xsp4 mono-csharp-shell
```

The installation can be much slimer, of course.

The only code you really need to run the demonstration is `Program.cs`, so the easiest option might be wget:

```
$ cd /tmp
$ wget https://github.com/pingfu/beagleboard-led-chaser/blob/master/Chaser/Chaser/Program.cs
$ dmcs Program.cs
$ sudo ./Program.cs
```

That's it.

## About

This program is a simple wrapper to the following bash commands. Manipulating the BeagleBoard hardware is as simple as writing data to a file.

```
echo default-on > /sys/class/leds/beaglebone:green:usr0/trigger
echo default-on > /sys/class/leds/beaglebone:green:usr1/trigger
echo default-on > /sys/class/leds/beaglebone:green:usr2/trigger
echo default-on > /sys/class/leds/beaglebone:green:usr3/trigger
```

Set the LEDs back to their default configuration

```
echo heartbeat > /sys/class/leds/beaglebone:green:usr0/trigger
echo mmc0 > /sys/class/leds/beaglebone:green:usr1/trigger
echo cpu0 > /sys/class/leds/beaglebone:green:usr2/trigger
echo mmc1 > /sys/class/leds/beaglebone:green:usr3/trigger
```

We flashed our BeagleBoard with a specially prepared image of Ubuntu made by [Robert C Nelson][1]. The version I used in December 2013 was `BBB-eMMC-flasher-ubuntu-13.04-2013-10-08.img.xz` from `http://rcn-ee.net/deb/flasher/raring/`. The same paths ought to work for Angstrom too, we just find mono easier on Ubuntu thanks to apt.

[More information about directly working with files to control the hardware here][2]

If you're planning to do any serious work with C# and the BeagleBoard, it's worth knowing about Ahead-of-time compliation. There is more information about that on the [Mono project's website][3].

## Lastly

Cheers http://markable.in/editor/. Also check out [The making of BeagleBone Black][4] and the [BeagleBone GamingCape][5]


 [0]: http://elinux.org/Beagleboard:Ubuntu_On_BeagleBone_Black
 [1]: https://github.com/RobertCNelson
 [2]: http://www.circuidipity.com/bbb-led.html
 [3]: http://www.mono-project.com/AOT
 [4]: http://www.youtube.com/watch?v=FcqQvH41OR4
 [5]: http://www.youtube.com/watch?v=wj1T84orbeY
 
 
 