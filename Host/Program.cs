// Copyright 2022 Aaron R Robinson
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is furnished
// to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
// PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Runtime.InteropServices;

namespace Host
{
    public unsafe class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Must supply native library to load.");
                return;
            }

            if (!TryLoad(args[0], out nint handle))
            {
                Console.Error.WriteLine($"Failed to load native binary: {args[0]}.");
                return;
            }

            const string exportName = "GetMetaDataDispenser";
            if (!TryGetExport(handle, exportName, out nint export))
            {
                Console.Error.WriteLine($"Failed to find '{exportName}' export.");
                return;
            }

            int hr = ((delegate* unmanaged[Stdcall]<out void*, int>)export)(out void* dispenser);
            if (hr < 0)
            {
                Console.Error.WriteLine($"Failed to get dispenser instance: 0x{hr:x}.");
                return;
            }

            Console.WriteLine($"Successfully acquired dispenser instance: 0x{(nint)dispenser:x}.");
            Marshal.Release((nint)dispenser);
        }

        private static bool TryLoad(string name, out nint handle)
        {
#if NET6_0_OR_GREATER
            return NativeLibrary.TryLoad(name, out handle);
#else
            handle = LoadLibraryA(name);
            return handle != 0;

            [DllImport("kernel32", CharSet = CharSet.Ansi)]
            extern static nint LoadLibraryA(string name);
#endif
        }

        private static bool TryGetExport(nint handle, string exportName, out nint export)
        {
#if NET6_0_OR_GREATER
            return NativeLibrary.TryGetExport(handle, exportName, out export);
#else
            export = GetProcAddress(handle, exportName);
            return export != 0;

            [DllImport("kernel32", CharSet = CharSet.Ansi)]
            extern static nint GetProcAddress(nint handle, string name);
#endif
        }

    }
}