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

#include <cassert>
#include <cstdint>
#include <cstdlib>
#include <cstdio>

#ifdef _MSC_VER
#define NOMINMAX
#define WIN32_LEAN_AND_MEAN
#include <Windows.h>

#define LIBEXPORT __declspec(dllexport)

#define WINDOWS_HOST
#else

#define LIBEXPORT __attribute__((visibility("default")))

#include <string>
#include <dlfcn.h>
#include <dncp.h>

#endif // !_MSC_VER

#include <cor.h>

using MetaDataGetDispenser_fptr = HRESULT(STDMETHODCALLTYPE*)(REFCLSID, REFIID, void**);
MetaDataGetDispenser_fptr g_GetDispenser;

#ifdef WINDOWS_HOST
void* GetRuntimeModuleHandle()
{
    HMODULE hnd;
    // There is no need to change the ref count of the module.
    // Check for coreclr then for clr. Order here is because coreclr is assumed to be
    // the more common scenario.
    if (TRUE != ::GetModuleHandleExA(GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT, "coreclr", &hnd))
        (void)::GetModuleHandleExA(GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT, "clr", &hnd);
    return hnd;
}
#else

// Adapted from the dotnet/runtime host PAL
// https://github.com/dotnet/runtime/blob/e57438026c25707bf6dd52cd332db657e919bbd4/src/native/corehost/hostmisc/pal.unix.cpp#L142-L186
void* GetRuntimeModuleHandle()
{
#ifdef __APPLE__
    char const* clrName = "libcoreclr.dylib";
#else
    char const* clrName = "libcoreclr.so";
#endif

    std::FILE* file = std::fopen("/proc/self/maps", "r");
    if (file == nullptr)
        return nullptr;

    char* line = nullptr;
    size_t lineLen = 0;
    ssize_t read;

    char buf[1024];

    bool found = false;
    std::string path_local;
    while ((read = ::getline(&line, &lineLen, file)) != -1)
    {
        if (sscanf(line, "%*p-%*p %*[-rwxsp] %*p %*[:0-9a-f] %*d %s\n", buf) == 1)
        {
            path_local = buf;
            size_t pos = path_local.rfind('/');
            if (pos == std::string::npos)
                continue;

            pos = path_local.find(clrName, pos);
            if (pos != std::string::npos)
            {
                found = true;
                break;
            }
        }
    }

    std::fclose(file);
    std::free(line);
    if (!found)
        return nullptr;

    return ::dlopen(path_local.c_str(), RTLD_LAZY | RTLD_NOLOAD);
}
#endif // !WINDOWS_HOST

MetaDataGetDispenser_fptr GetDispenserEntry()
{
    if (g_GetDispenser == nullptr)
    {
        void* hnd = GetRuntimeModuleHandle();
        if (hnd == nullptr)
            return nullptr;

        char const* exportName = "MetaDataGetDispenser";
#ifdef WINDOWS_HOST
        g_GetDispenser = (MetaDataGetDispenser_fptr)::GetProcAddress((HMODULE)hnd, exportName);
#else
        g_GetDispenser = (MetaDataGetDispenser_fptr)::dlsym(hnd, exportName);
#endif // !WINDOWS_HOST
    }

    assert(g_GetDispenser != nullptr);
    return g_GetDispenser;
}

extern "C" LIBEXPORT HRESULT GetMetaDataDispenser(IMetaDataDispenserEx** dispenser)
{
    assert(dispenser != nullptr);
    MetaDataGetDispenser_fptr fptr = GetDispenserEntry();
    if (fptr == nullptr)
        return E_NOT_VALID_STATE;

    return fptr(CLSID_CorMetaDataDispenser, IID_IMetaDataDispenserEx, reinterpret_cast<void**>(dispenser));
}
