﻿using System;
using System.Text;
using System.Threading;

namespace WIXWriter.au.com.fullcirclesolutions.wixwriter {
    public static class GuidGenerator {

        // guid version types
        private enum GuidVersion : byte {
            TimeBased = 0x01,
            //Reserved = 0x02,
            //NameBased = 0x03,
            //Random = 0x04
        }

        // number of bytes in guid
        public const int ByteArraySize = 16;

        // multiplex variant info
        public const int VariantByte = 8;
        public const int VariantByteMask = 0x3f;
        public const int VariantByteShift = 0x80;

        // multiplex version info
        public const int VersionByte = 7;
        public const int VersionByteMask = 0x0f;
        public const int VersionByteShift = 4;

        // indexes within the uuid array for certain boundaries
        private const byte TimestampByte = 0;
        private const byte GuidClockSequenceByte = 8;
        private const byte NodeByte = 10;

        // offset to move from 1/1/0001, which is 0-time for .NET, to gregorian 0-time of 10/15/1582
        private static readonly DateTime GregorianCalendarStart = new DateTime(1582, 10, 15, 0, 0, 0, DateTimeKind.Utc);

        // random node that is 16 bytes
        private static readonly byte[] RandomNode;

        private static readonly Random Random = new Random();

        static GuidGenerator() {
            RandomNode = new byte[6];
            Random.NextBytes(RandomNode);
        }

        public static string GenerateTimeBasedGuidStringWithNoDashes() {
            var sb = new StringBuilder(GenerateTimeBasedGuid().ToString());
            sb.Replace("-", "");
            return sb.ToString().ToUpper();
        }

        public static string GenerateTimeBasedGuidString() {
            return GenerateTimeBasedGuid().ToString().ToUpper();
        }

        private static Guid GenerateTimeBasedGuid() {
            Thread.Sleep(100);
            return GenerateTimeBasedGuid(DateTime.UtcNow, RandomNode);
        }

        //private static Guid GenerateTimeBasedGuid(DateTime dateTime) {
        //    return GenerateTimeBasedGuid(dateTime, RandomNode);
        //}

        
        private static Guid GenerateTimeBasedGuid(DateTime dateTime, byte[] node) {
            var ticks = dateTime.Ticks - GregorianCalendarStart.Ticks;

            var guid = new byte[ByteArraySize];
            var clockSequenceBytes = BitConverter.GetBytes(Convert.ToInt16(Environment.TickCount % Int16.MaxValue));
            var timestamp = BitConverter.GetBytes(ticks);

            // copy node
            Array.Copy(node, 0, guid, NodeByte, node.Length);

            // copy clock sequence
            Array.Copy(clockSequenceBytes, 0, guid, GuidClockSequenceByte, clockSequenceBytes.Length);

            // copy timestamp
            Array.Copy(timestamp, 0, guid, TimestampByte, timestamp.Length);

            // set the variant
            guid[VariantByte] &= VariantByteMask;
            guid[VariantByte] |= VariantByteShift;

            // set the version
            guid[VersionByte] &= VersionByteMask;
            guid[VersionByte] |= (byte)GuidVersion.TimeBased << VersionByteShift;

            return new Guid(guid);
        }
    }
}