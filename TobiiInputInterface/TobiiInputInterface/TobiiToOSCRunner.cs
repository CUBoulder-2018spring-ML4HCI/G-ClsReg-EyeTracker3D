using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SharpOSC;
using Tobii.Interaction;

/*
 * This code was authored to connect to the Tobii EyeTracking Bar and send a constant stream of OSC
 * messages containing the user's current eye gaze position to the specified 
 * address and port. It IS hacky, although I am currently working on a cleaned-up version.
 * 
 * Questions? Contact me. 
 * Jon Meade
 * jonathan.meade@colorado.edu
*/

namespace TobiiInputInterface
{
    class Point
    {
        public double x;
        public double y;

        public Point()
        {

        }

        public Point(double _x, double _y)
        {
            this.x = _x;
            this.y = _y;
        }
    }

    class TobiiToOSCRunner
    {
        public static String[] commandLineArgs;

        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: TobiiInputInterface [dest-ip] [dest-port]");
                System.Environment.Exit(1);
            }
            commandLineArgs = args;
            var tobiiHost = new Host();
            var gazePointDataStream = tobiiHost.Streams.CreateGazePointDataStream();
            Console.WriteLine("Waiting for Eye Bar Input...");
            while (true)
            {
                sendCurrentPointOverOSCAndWait(gazePointDataStream);
            }
        }

        static UDPSender instantiateNewUDPStream(string destinationIP, int destinationPort)
        {
            UDPSender sender = new SharpOSC.UDPSender(destinationIP, destinationPort);
            return sender;
        }

        static OscMessage buildMessageFromPoint(Point p)
        {
            OscMessage message = new SharpOSC.OscMessage("/wek/inputs", p.x, p.y);
            return message;
        }

        static void sendCurrentPointOverOSCAndWait(GazePointDataStream currentTobiiStream)
        {
            UDPSender sender = instantiateNewUDPStream(commandLineArgs[0], Int32.Parse(commandLineArgs[1]));
            GazePointDataStream gazeStream = currentTobiiStream.GazePoint((x,y,ts) => sendEyeCoordsOverOSC(sender, buildMessageFromPoint(new Point(x, y))));
        }

        static void sendEyeCoordsOverOSC(UDPSender sender, OscMessage message)
        {
            Console.WriteLine("Sent point (" + message.Arguments[0] + ", " + message.Arguments[1] + ")");
            sender.Send(message);
        }
    }
}
