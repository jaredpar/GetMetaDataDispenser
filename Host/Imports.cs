using System;
using System.Runtime.InteropServices;

public static class HRESULT
{
    public const int S_OK = 0x00000000;
    public const int S_FALSE = 0x00000001;

    public static void ThrowIfFailed(int hr)
    {
        if (hr < 0)
        {
            Marshal.ThrowExceptionForHR(hr);
        }
    }
}


[ComImport, Guid("809C652E-7396-11D2-9771-00A0C9B4D50C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMetaDataDispenser
{
    void DefineScope(); // Not used here
    void OpenScope(
        [MarshalAs(UnmanagedType.LPWStr)] string szScope,
        int dwOpenFlags,
        ref Guid riid,
        out IntPtr ppIUnk);
}

[ComImport]
[Guid("7DAC8207-D3AE-4c75-9B67-92801A497D44")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMetaDataImport
{
    [PreserveSig]
    void CloseEnum(IntPtr hEnum);

    [PreserveSig]
    int CountEnum(IntPtr hEnum, out int count);

    [PreserveSig]
    int ResetEnum(IntPtr hEnum, uint ulPos);

    [PreserveSig]
    int EnumTypeDefs(ref IntPtr phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rTypeDefs, int cMax, out uint pcTypeDefs);

    [PreserveSig]
    int EnumInterfaceImpls(ref IntPtr phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rImpls, int cMax, out uint pcImpls);

    [PreserveSig]
    int EnumTypeRefs(ref IntPtr phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rTypeDefs, int cMax, out uint pcTypeRefs);

    [PreserveSig]
    int FindTypeDefByName([MarshalAs(UnmanagedType.LPWStr)]string szTypeDef, uint tkEnclosingClass, out uint ptd);

    [PreserveSig]
    int GetScopeProps([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]char[] szName, int cchName, out int pchName, out Guid pmvid);

    [PreserveSig]
    int GetModuleFromScope(out uint pmd);

    [PreserveSig]
    int GetTypeDefProps(uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szTypeDef, int cchTypeDef, out int pchTypeDef, out uint pdwTypeDefFlags, out uint ptkExtends);

    [PreserveSig]
    int GetInterfaceImplProps(uint iiImpl, out uint pClass, out uint ptkIface);

    [PreserveSig]
    int GetTypeRefProps(uint tr, out uint ptkResolutionScope, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]char[] szName, int cchName, out int pchName);

    [PreserveSig]
    int ResolveTypeRef(uint tr, ref Guid riid, [MarshalAs(UnmanagedType.Interface)]out object ppIScope, out uint ptd);

    [PreserveSig]
    int EnumMembers(ref IntPtr phEnum, uint cl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rMembers, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumMembersWithName(ref IntPtr phEnum, uint cl, [MarshalAs(UnmanagedType.LPWStr)]string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]uint[] rMembers, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumMethods(ref IntPtr phEnum, uint cl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rMethods, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumMethodsWithName(ref IntPtr phEnum, uint cl, [MarshalAs(UnmanagedType.LPWStr)]string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]uint[] rMethods, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumFields(ref IntPtr phEnum, uint cl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rFields, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumFieldsWithName(ref IntPtr phEnum, uint cl, [MarshalAs(UnmanagedType.LPWStr)]string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]uint[] rFields, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumParams(ref IntPtr phEnum, uint mb, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rParams, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumMemberRefs(ref IntPtr phEnum, uint tkParent, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rMemberRefs, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumMethodImpls(ref IntPtr phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rMethodBody, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rMethodDecl, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumPermissionSets(ref IntPtr phEnum, uint tk, uint dwActions, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rPermission, int cMax, out uint pcTokens);

    [PreserveSig]
    int FindMember(uint td, [MarshalAs(UnmanagedType.LPWStr)]string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]byte[] pvSigBlob, uint cbSigBlob, out uint pmb);

    [PreserveSig]
    int FindMethod(uint td, [MarshalAs(UnmanagedType.LPWStr)]string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]byte[] pvSigBlob, uint cbSigBlob, out uint pmb);

    [PreserveSig]
    int FindField(uint td, [MarshalAs(UnmanagedType.LPWStr)]string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]byte[] pvSigBlob, uint cbSigBlob, out uint pmb);

    [PreserveSig]
    int FindMemberRef(uint td, [MarshalAs(UnmanagedType.LPWStr)]string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]byte[] pvSigBlob, int cbSigBlob, out uint pmr);

    [PreserveSig]
    int GetMethodProps(uint mb, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szMethod, int cchMethod, out int pchMethod, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pulCodeRVA, out uint pdwImplFlags);

    [PreserveSig]
    int GetMemberRefProps(uint mr, out uint ptk, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szMember, int cchMember, out int pchMember, out IntPtr ppvSigBlob, out uint pbSigBlob);

    [PreserveSig]
    int EnumProperties(ref IntPtr phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rProperties, int cMax, out uint pcProperties);

    [PreserveSig]
    int EnumEvents(ref IntPtr phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rEvents, int cMax, out uint pcEvents);

    [PreserveSig]
    int GetEventProps(uint ev, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szEvent, int cchEvent, out int pchEvent, out uint pdwEventFlags, out uint ptkEventType, out uint pmdAddOn, out uint pmdRemoveOn, out uint pmdFire, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 10)]uint[] rmdOtherMethod, int cMax, out int pcOtherMethod);

    [PreserveSig]
    int EnumMethodSemantics(ref IntPtr phEnum, uint mb, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rEventProp, int cMax, out uint pcEventProp);

    [PreserveSig]
    int GetMethodSemantics(uint mb, uint tkEventProp, out uint pdwSemanticsFlags);

    [PreserveSig]
    int GetClassLayout(uint td, out uint pdwPackSize, [Out][MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] COR_FIELD_OFFSET[] rFieldOffset, int cMax, out int pcFieldOffset, out uint pulClassSize);

    [PreserveSig]
    int GetFieldMarshal(uint tk, out IntPtr ppvNativeType, out uint pcbNativeType);

    [PreserveSig]
    int GetRVA(uint tk, out uint pulCodeRVA, out uint pdwImplFlags);

    [PreserveSig]
    int GetPermissionSetProps(uint pm, out uint pdwAction, out IntPtr ppvPermission, out uint pcbPermission);

    [PreserveSig]
    int GetSigFromToken(uint mdSig, out IntPtr ppvSig, out uint pcbSig);

    [PreserveSig]
    int GetModuleRefProps(uint mur, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]char[] szName, int cchName, out int pchName);

    [PreserveSig]
    int EnumModuleRefs(ref IntPtr phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]uint[] rModuleRefs, int cMax, out uint pcModuleRefs);

    [PreserveSig]
    int GetTypeSpecFromToken(uint typespec, out IntPtr ppvSig, out uint pcbSig);

    [PreserveSig]
    int GetNameFromToken(uint tk, out IntPtr pszUtf8NamePtr);

    [PreserveSig]
    int EnumUnresolvedMethods(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]uint[] rMethods, int cMax, out uint pcTokens);

    [PreserveSig]
    int GetUserString(uint stk, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] char[] szString, int cchString, out int pchString);

    [PreserveSig]
    int GetPinvokeMap(uint tk, out uint pdwMappingFlags, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szImportName, int cchImportName, out int pchImportName, out uint pmrImportDLL);

    [PreserveSig]
    int EnumSignatures(ref IntPtr phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]uint[] rSignatures, int cMax, out uint pcSignatures);

    [PreserveSig]
    int EnumTypeSpecs(ref IntPtr phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]uint[] rTypeSpecs, int cMax, out uint pcTypeSpecs);

    [PreserveSig]
    int EnumUserStrings(ref IntPtr phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]uint[] rStrings, int cMax, out uint pcStrings);

    [PreserveSig]
    int GetParamForMethodIndex(uint md, uint ulParamSeq, out uint ppd);

    [PreserveSig]
    int EnumCustomAttributes(ref IntPtr phEnum, uint tk, uint tkType, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rCustomAttributes, int cMax, out uint pcCustomAttributes);

    [PreserveSig]
    int GetCustomAttributeProps(uint cv, out uint ptkObj, out uint ptkType, out IntPtr ppBlob, out uint pcbSize);

    [PreserveSig]
    int FindTypeRef(uint tkResolutionScope, [MarshalAs(UnmanagedType.LPWStr)]string szName, out uint ptr);

    [PreserveSig]
    int GetMemberProps(uint mb, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szMember, uint cchMember, out uint pchMember, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pulCodeRVA, out uint pdwImplFlags, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue);

    [PreserveSig]
    int GetFieldProps(uint mb, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szField, int cchField, out int pchField, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue);

    [PreserveSig]
    int GetPropertyProps(uint prop, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szProperty, int cchProperty, out int pchProperty, out uint pdwPropFlags, out IntPtr ppvSig, out uint pbSig, out uint pdwCPlusTypeFlag, out IntPtr ppDefaultValue, out uint pcchDefaultValue, out uint pmdSetter, out uint pmdGetter, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 13)]uint[] rmdOtherMethod, int cMax, out int pcOtherMethod);

    [PreserveSig]
    int GetParamProps(uint tk, out uint pmd, out uint pulSequence, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]char[] szName, int cchName, out int pchName, out uint pdwAttr, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue);

    [PreserveSig]
    int GetCustomAttributeByName(uint tkObj, [MarshalAs(UnmanagedType.LPWStr)]string szName, out IntPtr ppData, out uint pcbData);

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Bool)]
    bool IsValidToken(uint tk);

    [PreserveSig]
    int GetNestedClassProps(uint tdNestedClass, out uint ptdEnclosingClass);

    [PreserveSig]
    int GetNativeCallConvFromSig(IntPtr pvSig, uint cbSig, out uint pCallConv);

    [PreserveSig]
    int IsGlobal(uint pd, out uint pbGlobal);
}

[StructLayout(LayoutKind.Sequential)]
public struct COR_FIELD_OFFSET
{
    public uint ridOfField;
    public uint ulOffset;
}

[ComImport]
[Guid("FCE5EFA0-8BBA-4f8e-A036-8F2022B08466")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMetaDataImport2
{
    [PreserveSig]
    void CloseEnum(IntPtr hEnum);

    [PreserveSig]
    int CountEnum(IntPtr hEnum, out int count);

    [PreserveSig]
    int ResetEnum(IntPtr hEnum, uint ulPos);

    [PreserveSig]
    int EnumTypeDefs(ref IntPtr phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rTypeDefs, int cMax, out uint pcTypeDefs);

    [PreserveSig]
    int EnumInterfaceImpls(ref IntPtr phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rImpls, int cMax, out uint pcImpls);

    [PreserveSig]
    int EnumTypeRefs(ref IntPtr phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rTypeDefs, int cMax, out uint pcTypeRefs);

    [PreserveSig]
    int FindTypeDefByName([MarshalAs(UnmanagedType.LPWStr)]string szTypeDef, uint tkEnclosingClass, out uint ptd);

    [PreserveSig]
    int GetScopeProps([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]char[] szName, int cchName, out int pchName, out Guid pmvid);

    [PreserveSig]
    int GetModuleFromScope(out uint pmd);

    [PreserveSig]
    int GetTypeDefProps(uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szTypeDef, int cchTypeDef, out int pchTypeDef, out uint pdwTypeDefFlags, out uint ptkExtends);

    [PreserveSig]
    int GetInterfaceImplProps(uint iiImpl, out uint pClass, out uint ptkIface);

    [PreserveSig]
    int GetTypeRefProps(uint tr, out uint ptkResolutionScope, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]char[] szName, int cchName, out int pchName);

    [PreserveSig]
    int ResolveTypeRef(uint tr, ref Guid riid, [MarshalAs(UnmanagedType.Interface)]out object ppIScope, out uint ptd);

    [PreserveSig]
    int EnumMembers(ref IntPtr phEnum, uint cl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rMembers, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumMembersWithName(ref IntPtr phEnum, uint cl, [MarshalAs(UnmanagedType.LPWStr)]string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]uint[] rMembers, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumMethods(ref IntPtr phEnum, uint cl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rMethods, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumMethodsWithName(ref IntPtr phEnum, uint cl, [MarshalAs(UnmanagedType.LPWStr)]string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]uint[] rMethods, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumFields(ref IntPtr phEnum, uint cl, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rFields, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumFieldsWithName(ref IntPtr phEnum, uint cl, [MarshalAs(UnmanagedType.LPWStr)]string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]uint[] rFields, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumParams(ref IntPtr phEnum, uint mb, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rParams, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumMemberRefs(ref IntPtr phEnum, uint tkParent, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rMemberRefs, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumMethodImpls(ref IntPtr phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rMethodBody, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rMethodDecl, int cMax, out uint pcTokens);

    [PreserveSig]
    int EnumPermissionSets(ref IntPtr phEnum, uint tk, uint dwActions, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rPermission, int cMax, out uint pcTokens);

    [PreserveSig]
    int FindMember(uint td, [MarshalAs(UnmanagedType.LPWStr)]string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]byte[] pvSigBlob, uint cbSigBlob, out uint pmb);

    [PreserveSig]
    int FindMethod(uint td, [MarshalAs(UnmanagedType.LPWStr)]string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]byte[] pvSigBlob, uint cbSigBlob, out uint pmb);

    [PreserveSig]
    int FindField(uint td, [MarshalAs(UnmanagedType.LPWStr)]string szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]byte[] pvSigBlob, uint cbSigBlob, out uint pmb);

    [PreserveSig]
    int FindMemberRef(uint td, [MarshalAs(UnmanagedType.LPWStr)]string? szName, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]byte[]? pvSigBlob, int cbSigBlob, out uint pmr);

    [PreserveSig]
    int GetMethodProps(uint mb, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szMethod, int cchMethod, out int pchMethod, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pulCodeRVA, out uint pdwImplFlags);

    [PreserveSig]
    int GetMemberRefProps(uint mr, out uint ptk, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szMember, int cchMember, out int pchMember, out IntPtr ppvSigBlob, out uint pbSigBlob);

    [PreserveSig]
    int EnumProperties(ref IntPtr phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rProperties, int cMax, out uint pcProperties);

    [PreserveSig]
    int EnumEvents(ref IntPtr phEnum, uint td, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rEvents, int cMax, out uint pcEvents);

    [PreserveSig]
    int GetEventProps(uint ev, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szEvent, int cchEvent, out int pchEvent, out uint pdwEventFlags, out uint ptkEventType, out uint pmdAddOn, out uint pmdRemoveOn, out uint pmdFire, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 10)]uint[] rmdOtherMethod, int cMax, out int pcOtherMethod);

    [PreserveSig]
    int EnumMethodSemantics(ref IntPtr phEnum, uint mb, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rEventProp, int cMax, out uint pcEventProp);

    [PreserveSig]
    int GetMethodSemantics(uint mb, uint tkEventProp, out uint pdwSemanticsFlags);

    [PreserveSig]
    int GetClassLayout(uint td, out uint pdwPackSize, [Out][MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] COR_FIELD_OFFSET[] rFieldOffset, int cMax, out int pcFieldOffset, out uint pulClassSize);

    [PreserveSig]
    int GetFieldMarshal(uint tk, out IntPtr ppvNativeType, out uint pcbNativeType);

    [PreserveSig]
    int GetRVA(uint tk, out uint pulCodeRVA, out uint pdwImplFlags);

    [PreserveSig]
    int GetPermissionSetProps(uint pm, out uint pdwAction, out IntPtr ppvPermission, out uint pcbPermission);

    [PreserveSig]
    int GetSigFromToken(uint mdSig, out IntPtr ppvSig, out uint pcbSig);

    [PreserveSig]
    int GetModuleRefProps(uint mur, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]char[] szName, int cchName, out int pchName);

    [PreserveSig]
    int EnumModuleRefs(ref IntPtr phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]uint[] rModuleRefs, int cMax, out uint pcModuleRefs);

    [PreserveSig]
    int GetTypeSpecFromToken(uint typespec, out IntPtr ppvSig, out uint pcbSig);

    [PreserveSig]
    int GetNameFromToken(uint tk, out IntPtr pszUtf8NamePtr);

    [PreserveSig]
    int EnumUnresolvedMethods(ref uint phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]uint[] rMethods, int cMax, out uint pcTokens);

    [PreserveSig]
    int GetUserString(uint stk, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] char[] szString, int cchString, out int pchString);

    [PreserveSig]
    int GetPinvokeMap(uint tk, out uint pdwMappingFlags, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szImportName, int cchImportName, out int pchImportName, out uint pmrImportDLL);

    [PreserveSig]
    int EnumSignatures(ref IntPtr phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]uint[] rSignatures, int cMax, out uint pcSignatures);

    [PreserveSig]
    int EnumTypeSpecs(ref IntPtr phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]uint[] rTypeSpecs, int cMax, out uint pcTypeSpecs);

    [PreserveSig]
    int EnumUserStrings(ref IntPtr phEnum, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]uint[] rStrings, int cMax, out uint pcStrings);

    [PreserveSig]
    int GetParamForMethodIndex(uint md, uint ulParamSeq, out uint ppd);

    [PreserveSig]
    int EnumCustomAttributes(ref IntPtr phEnum, uint tk, uint tkType, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]uint[] rCustomAttributes, int cMax, out uint pcCustomAttributes);

    [PreserveSig]
    int GetCustomAttributeProps(uint cv, out uint ptkObj, out uint ptkType, out IntPtr ppBlob, out uint pcbSize);

    [PreserveSig]
    int FindTypeRef(uint tkResolutionScope, [MarshalAs(UnmanagedType.LPWStr)]string szName, out uint ptr);

    [PreserveSig]
    int GetMemberProps(uint mb, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szMember, uint cchMember, out uint pchMember, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pulCodeRVA, out uint pdwImplFlags, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue);

    [PreserveSig]
    int GetFieldProps(uint mb, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szField, int cchField, out int pchField, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue);

    [PreserveSig]
    int GetPropertyProps(uint prop, out uint pClass, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]char[] szProperty, int cchProperty, out int pchProperty, out uint pdwPropFlags, out IntPtr ppvSig, out uint pbSig, out uint pdwCPlusTypeFlag, out IntPtr ppDefaultValue, out uint pcchDefaultValue, out uint pmdSetter, out uint pmdGetter, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 13)]uint[] rmdOtherMethod, int cMax, out int pcOtherMethod);

    [PreserveSig]
    int GetParamProps(uint tk, out uint pmd, out uint pulSequence, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]char[] szName, int cchName, out int pchName, out uint pdwAttr, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue);

    [PreserveSig]
    int GetCustomAttributeByName(uint tkObj, [MarshalAs(UnmanagedType.LPWStr)]string szName, out IntPtr ppData, out uint pcbData);

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Bool)]
    bool IsValidToken(uint tk);

    [PreserveSig]
    int GetNestedClassProps(uint tdNestedClass, out uint ptdEnclosingClass);

    [PreserveSig]
    int GetNativeCallConvFromSig(IntPtr pvSig, uint cbSig, out uint pCallConv);

    [PreserveSig]
    int IsGlobal(uint pd, out uint pbGlobal);

    [PreserveSig]
    int EnumGenericParameters(ref IntPtr phEnum, uint tk, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rGenericParams, int cMax, out uint pcGenericParams);

    [PreserveSig]
    int GetGenericParamProps(uint gp, out uint pulParamSeq, out uint pdwParamFlags, out uint ptOwner, out uint reserved, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)]char[] szName, int cchName, out int pchName);

    [PreserveSig]
    int GetMethodSpecProps(uint mi, out uint tkParent, out IntPtr ppvSigBlob, out uint pcbSigBlob);

    [PreserveSig]
    int EnumGenericParamConstraints(ref IntPtr phEnum, uint tk, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rGenericParamConstraints, int cMax, out uint pcGenericParamConstraints);

    [PreserveSig]
    int GetGenericParamConstraintProps(uint gpc, out uint tkParam, out uint tkConstraintType);

    [PreserveSig]
    int GetPEKind(out uint pdwPEKind, out uint pdwMAchine);

    [PreserveSig]
    int GetVersionString([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]char[] pwzBuf, int cchBuf, out int pchBuf);

    [PreserveSig]
    int EnumMethodSpecs(ref IntPtr phEnum, uint tk, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]uint[] rMethodSpecs, int cMax, out uint pcMethodSpecs);
}