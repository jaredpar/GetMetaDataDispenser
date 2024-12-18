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
using static HRESULT;

namespace Host
{
    public unsafe class Program
    {
        public static void Main(string[] args)
        {
            var lib = @"C:\Users\jaredpar\code\GetMetaDataDispenser\artifacts\bin\getmddisp.dll";

            if (!TryLoad(lib, out nint handle))
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

            int hr = ((delegate* unmanaged[Stdcall]<out void*, int>)export)(out void* dispenserPtr);
            if (hr < 0)
            {
                Console.Error.WriteLine($"Failed to get dispenser instance: 0x{hr:x}.");
                return;
            }

            Console.WriteLine($"Successfully acquired dispenser instance: 0x{(nint)dispenserPtr:x}.");

            var dispenser = (IMetaDataDispenser)Marshal.GetObjectForIUnknown((nint)dispenserPtr);
            Guid iidMetaDataImport2 = new("FCE5EFA0-8BBA-4f8e-A036-8F2022B08466");

            var assemblyPath = @"C:\Program Files\dotnet\packs\Microsoft.WindowsDesktop.App.Ref\8.0.11\ref\net8.0\System.Windows.Forms.dll";
            dispenser.OpenScope(assemblyPath, 0 /* read */, ref iidMetaDataImport2, out nint importPtr);

            var import = (IMetaDataImport2)Marshal.GetObjectForIUnknown(importPtr);
            Test(import);

            Marshal.Release((nint)dispenserPtr);
        }

        static void Test(IMetaDataImport2 import)
        {
            uint[] typeDefs = new uint[1];
            nint e = 0;
            do 
            {
                var hr = import.EnumTypeDefs(ref e, typeDefs, 1, out uint found);
                if (hr != HRESULT.S_OK)
                {
                    break;
                }
                ForTypes(import, typeDefs[0]);
            } while (true);
        }

        static void ForTypes(IMetaDataImport2 import, uint typeDef)
        {
            var name = GetString(import, typeDef);
            Console.WriteLine(name);

            uint[] methodDefs = new uint[1];
            nint e = 0;
            do
            {
                var hr = import.EnumMethods(ref e, typeDef, methodDefs, 1, out uint found);
                if (hr != HRESULT.S_OK)
                {
                    break;
                }
                ForMethods(import, typeDef, methodDefs[0]);
            } while(true);

            import.CloseEnum(e);
        }

        static void ForMethods(IMetaDataImport2 import, uint typeDef, uint methodDef)
        {
            var name = GetString(import, methodDef);
            Console.Write($"\t{name}(");
            if (name.Contains("GetOwnNeighboringToolsRectangles"))
            {

            }
            uint[] paramDefs = new uint[5];
            nint e = 0;
            var first = true;
            do
            {
                var hr = import.EnumParams(ref e, methodDef, paramDefs, paramDefs.Length, out uint found);
                if (hr != HRESULT.S_OK)
                {
                    break;
                }

                for (int i = 0; i < found; i++)
                {
                    if (!first)
                    {
                        Console.Write(", ");
                    }

                    Console.Write(GetString(import, paramDefs[i]));
                    first = false;
                }

            } while(true);
            Console.WriteLine(")");

            import.CloseEnum(e);
        }

        static string GetString(IMetaDataImport2 import, uint token)
        {
            ThrowIfFailed(import.GetNameFromToken(token, out nint namePtr));
            return Marshal.PtrToStringAnsi(namePtr);
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