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
 * address and port. It is hacky, I'm working on a cleaned up version. I know what clean code is, trust me.
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
        public static Point currentPoint = new Point(0,0);

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
            UDPSender sender = instantiateNewUDPStream(commandLineArgs[0], Int32.Parse(commandLineArgs[1]));

            Console.WriteLine("Waiting for Eye Bar Input...");
            startStreamData(gazePointDataStream);

            while (true)
            {
                System.Threading.Thread.Sleep(500);
                OscMessage msg = buildMessageFromPoint(currentPoint);
                sendEyeCoordsOverOSC(sender, msg);
            }
        }

        static UDPSender instantiateNewUDPStream(string destinationIP, int destinationPort)
        {
            UDPSender sender = new SharpOSC.UDPSender(destinationIP, destinationPort);
            return sender;
        }

        static OscMessage buildMessageFromPoint(Point p)
        {
            OscMessage message = new SharpOSC.OscMessage("/wek/inputs", (float)p.x, (float)p.y);
            return message;
        }

        static void startStreamData(GazePointDataStream currentTobiiStream)
        {
            GazePointDataStream gazeStream = currentTobiiStream.GazePoint((x, y, ts) => { currentPoint.x = x; currentPoint.y = y; });
            return;
        }

        static void sendEyeCoordsOverOSC(UDPSender sender, OscMessage message)
        {
            Console.WriteLine("Sent point (" + message.Arguments[0] + ", " + message.Arguments[1] + ")");
            sender.Send(message);
            return;
        }
    }
}
