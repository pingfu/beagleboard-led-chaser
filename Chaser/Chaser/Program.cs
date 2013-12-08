using System;
using System.IO;
using System.Threading;

namespace Chaser
{
    /// <summary>
    /// This is a simple program to demonstrate using C# to control the LEDs on the BeagleBone Black
    /// </summary>
    class Program
    {
        /// <summary>
        /// Millisecond interval between chase jumps
        /// </summary>
        private const int BlinkDelay = 50;

        /// <summary>
        /// An array of StreamWriters referencing the special files on disk we write to in order to control the hardware
        /// </summary>
        private static readonly StreamWriter[] Lights = new StreamWriter[4];

        /// <summary>
        /// This program needs root to run
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            OpenFiles();

            if (args.Length == 0)
            {
                Console.WriteLine("starting chaser...");
                StartChaser();
            }
            else
            {
                Console.WriteLine("restoring defaults...");
                RestoreDefaults();
            }
        }

        /// <summary>
        /// Open the 'trigger' file for each system LED on the BeagleBoard Black.
        /// 
        /// We flashed our BeagleBoard with a specially prepared image of Ubuntu made by 
        /// Robert C Nelson (https://github.com/RobertCNelson)
        /// 
        /// The version we used in December 2013 was BBB-eMMC-flasher-ubuntu-13.04-2013-10-08.img.xz 
        /// from http://rcn-ee.net/deb/flasher/raring/. The same paths ought to work for Angstrom too, 
        /// we just find mono easier on Ubuntu
        /// </summary>
        static void OpenFiles()
        {
            Lights[0] = new StreamWriter("/sys/class/leds/beaglebone:green:usr0/trigger");
            Lights[1] = new StreamWriter("/sys/class/leds/beaglebone:green:usr1/trigger");
            Lights[2] = new StreamWriter("/sys/class/leds/beaglebone:green:usr2/trigger");
            Lights[3] = new StreamWriter("/sys/class/leds/beaglebone:green:usr3/trigger");

            foreach (var light in Lights)
            {
                light.AutoFlush = true;
            }
        }

        /// <summary>
        /// Simple LED chaser
        /// </summary>
        private static void StartChaser()
        {
            while (true)
            {
                for (var n = 0; n <= 3; n++)
                {
                    // switch current led on
                    Lights[n].Write("default-on");

                    // wait
                    Thread.Sleep(BlinkDelay);

                    // switch current led off
                    Lights[n].Write("none");
                }
            }
        }

        /// <summary>
        /// Restores the default LED configuration
        /// 
        /// LED USER0 is the heartbeat indicator from the Linux kernel.
        /// LED USER1 turns on when the SD card is being accessed
        /// LED USER2 is an activity indicator. It turns on when the kernel is not in the idle loop.
        /// LED USER3 turns on when the onboard eMMC is being accessed.
        /// </summary>
        private static void RestoreDefaults()
        {
            Lights[0].Write("heartbeat");
            Lights[1].Write("mmc0");
            Lights[2].Write("cpu0");
            Lights[3].Write("mmc1");

            /* this is the same as;
             *
             *    $ echo heartbeat > /sys/class/leds/beaglebone:green:usr0/trigger
             *    $ echo mmc0 > /sys/class/leds/beaglebone:green:usr1/trigger
             *    $ echo cpu0 > /sys/class/leds/beaglebone:green:usr2/trigger
             *    $ echo mmc1 > /sys/class/leds/beaglebone:green:usr3/trigger
             */
        }
    }
}